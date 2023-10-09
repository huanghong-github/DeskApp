using SqlSugar;

namespace DeskApp.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Menu")]
    public partial class Menu
    {
        public Menu()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public int? Id { get; set; }

        /// <summary>
        /// Desc:标题
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:图标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Icon { get; set; }

        /// <summary>
        /// Desc: ProcessUtil.RunApp.filename
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Appname { get; set; }

        /// <summary>
        /// Desc: ProcessUtil.RunApp.auguments
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Auguments { get; set; }

        /// <summary>
        /// Desc: Menu中为当前应用，Main中为菜单
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Grouptype { get; set; }

        /// <summary>
        /// Desc: ProcessUtil.RunApp.UseShellExecute
        /// Default:
        /// Nullable:True
        /// </summary>           
        public object Shell { get; set; }

        /// <summary>
        /// Desc: ProcessUtil.RunApp.recordLog
        /// Default:
        /// Nullable:True
        /// </summary>           
        public object Record { get; set; }

    }
}
