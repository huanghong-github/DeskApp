using Shell32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DeskApp.Utils
{
    internal class PathUtil
    {
        public static string SystemPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
        public static string CmdPath = Path.Combine(SystemPath, "cmd.exe");
        //public static string PowershellPath = ProcessUtil.RunApp("cmd", "/c where powershell", true).Trim();
        public static string StartMenu = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
        public static string CommonStartMenu = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
        /// <summary>
        /// 提取名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Stem(string path)
        {
            return path == null ? default : Path.GetFileNameWithoutExtension(path).ToLowerInvariant();
            ;
        }
        /// <summary>
        /// 获取选中的文件夹
        /// </summary>
        /// <returns></returns>
        public static List<string> GetPaths()
        {
            List<string> folderItems = new List<string>();
            dynamic shellWindows = new Shell().Windows();
            //桌面
            dynamic ieDesktop = shellWindows.FindWindowSW(0, null, 8, out int hwnd, 1);
            FolderItems selectDesktopItems = ((IShellFolderViewDual2)ieDesktop.Document).SelectedItems();
            folderItems.AddRange(from FolderItem folderItem in selectDesktopItems
                                 select folderItem.Path);
            //文件
            foreach (dynamic window in shellWindows)
            {
                if (Stem(window.FullName) == "explorer")
                {
                    FolderItems selectExplorerItems = ((IShellFolderViewDual2)window.Document).SelectedItems();
                    folderItems.AddRange(from FolderItem folderItem in selectExplorerItems
                                         select folderItem.Path);
                }
            }
            return folderItems;
        }
        /// <summary>
        /// 获取点击位置的文件
        /// </summary>
        /// <returns></returns>
        public static string GetFocusePath()
        {
            List<FolderItem> folderItems = new List<FolderItem>();
            dynamic shellWindows = new Shell().Windows();
            string currFocusedItem = string.Empty;
            //桌面
            dynamic ieDesktop = shellWindows.FindWindowSW(0, null, 8, out int hwnd, 1);
            currFocusedItem = ((IShellFolderViewDual2)ieDesktop.Document).FocusedItem.Path;
            //文件
            foreach (dynamic window in shellWindows)
            {
                if (Stem(window.FullName) == "explorer")
                {
                    currFocusedItem = ((IShellFolderViewDual2)window.Document).FocusedItem.Path;
                }
            }
            return currFocusedItem;
        }
        /// <summary>
        /// 解析路径快捷方式
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetRealPath(string path)
        {
            if (File.Exists(path) && Path.GetExtension(path).ToLower() == ".lnk")
            {
                string targetPath = ((IWshRuntimeLibrary.IWshShortcut)new IWshRuntimeLibrary.WshShell().CreateShortcut(path)).TargetPath;
                if (!string.IsNullOrEmpty(targetPath))
                {
                    return targetPath;
                }
            }
            return path;
        }
        /// <summary>
        /// 根据路径获取名称、图标
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Bitmap GetIconByPath(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return Icon.ExtractAssociatedIcon(GetRealPath(path)).ToBitmap();
                }
                else if (Directory.Exists(path))
                {
                    return Properties.Resources.explorer_icon.ToBitmap();
                }
                else if (CheckUrlIsValid(path))
                {
                    return Properties.Resources.edge_icon.ToBitmap();
                }
                return Properties.Resources.error_icon.ToBitmap();
            }
            catch
            {
                return Properties.Resources.error_icon.ToBitmap();
            }
        }
        /// <summary>
        /// 检测链接是否为合法的网址格式
        /// </summary>
        /// <param name="uri">待检测的链接</param>
        /// <returns></returns>
        public static bool CheckUrlIsValid(string uri)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                {
                    return false;
                }

                string regex = @"^(http(s)?:\/\/)?(www\.)?[\w-]+(\.\w{2,4})?\.\w{2,4}?(\/)?$";
                return new Regex(regex).IsMatch(uri.Trim());
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 获取系统菜单，需要管理员权限
        /// </summary>
        /// <returns></returns>
        public static FileInfo[] GetLnks(string path)
        {
            DirectoryInfo Dir = new DirectoryInfo(path);
            return Dir.GetFiles("*.lnk", SearchOption.AllDirectories);
        }
        public static List<string> GetStartMenu()
        {
            List<string> apps = new List<string>();
            apps.AddRange(GetLnks(StartMenu).Select(lnk => lnk.FullName));
            apps.AddRange(GetLnks(CommonStartMenu).Select(lnk => lnk.FullName));
            return apps;
        }
        /// <summary>
        /// 通过cmd获取系统菜单
        /// </summary>
        /// <returns></returns>
        public static string[] GetLnksByCmd(string path)
        {
            string command = $"for /r \"{path}\" %i in (*.lnk) do @echo %i";
            string output = ProcessUtil.RunCmd(command);
            return output.Split('\n').Select(lnk => lnk.Trim()).Where(lnk => lnk.EndsWith(".lnk") && File.Exists(lnk)).ToArray();
        }
        public static List<string> GetStartMenuByCmd()
        {
            List<string> apps = new List<string>();
            apps.AddRange(GetLnksByCmd(StartMenu));
            apps.AddRange(GetLnksByCmd(CommonStartMenu));
            return apps;
        }
    }
}
