using DeskApp.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace DeskApp.Utils
{
    internal class SQLiteUtil
    {
        public static string DbName { get; set; } = "DeskApp.sqlite";
        /// <summary>
        /// 获得数据库链接
        /// </summary>
        public static string ConnectionString => new SQLiteConnectionStringBuilder()
        { DataSource = DbName ?? throw new Exception("dbname is null") }.ToString();
        /// <summary>
        /// 获得数据库实例
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient GetInstance()
        {
            SqlSugarClient Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true
            });
            return Db;
        }
        /// <summary>
        /// 根据table创建类文件
        /// </summary>
        public static void CreateClassFiles()
        {
            SqlSugarClient db = GetInstance();
            db.DbFirst.IsCreateAttribute().CreateClassFile("D:\\nn\\csharp\\CSharpTool\\DeskApp\\Models", "DeskApp.Models");
        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        public static void Initdb()
        {
            if (!File.Exists(DbName))
            {
                SqlSugarClient db = GetInstance();
                _ = db.Ado.ExecuteCommand(Properties.Resources.createtable);

                _ = db.Insertable(new Menu()
                {
                    Name = "复制路径",
                    Icon = Properties.Resources.copypath_icon.ToBitmap().Resize().ToBase64(),
                    Appname = "",
                    Auguments = "",
                    Grouptype = "explorer"
                }).ExecuteCommand();

                string[] systemFlags = { "C:\\Windows", PathUtil.CommonStartMenu, PathUtil.StartMenu };
                string[] otherFlags = { "iexplore.exe", "setlang.exe", "wmplayer.exe", "git", "update", "uninst", "url", "chm", "html", "setup" };
                List<string> flags = new List<string>();
                foreach (string path in PathUtil.GetStartMenuByCmd())
                {
                    // 真实路径
                    string appname = PathUtil.GetRealPath(path);
                    // 文件名称
                    string filename = Path.GetFileName(appname).ToLower();
                    if (!flags.Contains(Path.GetFileName(filename)) && !otherFlags.Where(filename.Contains).Any())
                    {
                        flags.Add(filename);
                        bool isSystem = systemFlags.Where(appname.Contains).Any();
                        _ = db.Insertable(new Menu()
                        {
                            Name = isSystem ? PathUtil.Stem(appname) : PathUtil.Stem(path),
                            Icon = PathUtil.GetIconByPath(path).Resize().ToBase64(),
                            Appname = appname,
                            Grouptype = isSystem ? "system" : "main"
                        }).ExecuteCommand();
                    }
                }
            }
        }
    }
}