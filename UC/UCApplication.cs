using DeskApp.Utils;
using System;
using System.Windows.Forms;

namespace DeskApp.UC
{
    public partial class UCApplication : UserControl
    {
        private readonly Models.Menu menu;
        private RightForm rightForm;

        public UCApplication(Models.Menu menu)
        {
            this.menu = menu;
            InitializeComponent();
        }
        private void UCApplication_Load(object sender, EventArgs e)
        {
            label1.Text = menu.Name[0] > 127 && menu.Name.Length > 5
                ? menu.Name.Substring(0, 5)
                : menu.Name.Length > 10 ? menu.Name.Substring(0, 10) : menu.Name;

            pictureBox1.Image = menu.Icon.ToImage();
            foreach (Control c in Controls)
            {
                c.MouseClick += (object _, MouseEventArgs e1) => base.OnMouseClick(e1);
                c.MouseLeave += (object _, EventArgs e2) => base.OnMouseLeave(e2);
            }
            if (Parent.Parent is MenuForm menuform)
            {
                MouseLeave += (object _, EventArgs e3) => menuform.CallMouseLeave(e3);
            }
        }
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCApplication_MouseClick(object sender, MouseEventArgs e)
        {
            // 左键
            if (e.Button == MouseButtons.Left)
            {
                // 复制路径
                if (menu.Name == "复制路径")
                {
                    if (Parent.Parent is MenuForm menuForm)
                    {
                        Clipboard.SetText(menuForm.CurrentFocuse);
                    }
                }
                // 运行
                else if (menu.Appname.ToLower().EndsWith("exe"))
                {
                    _ = ProcessUtil.RunApp(menu.Appname, menu.Auguments);
                }
                else
                {
                    ProcessUtil.RunShell(menu.Appname);
                }
                // 通过Parent控制父类,非主菜单则隐藏
                if (menu.Grouptype != "main" || menu.Grouptype != "system")
                {
                    Parent.Parent.Visible = false;
                }
            }
            // 右键RightForm
            else if (e.Button == MouseButtons.Right)
            {
                rightForm = new RightForm(this) { Location = PointToScreen(e.Location), IsInsert = false, Menu = menu };
                SingleRightForm.RightForm = rightForm;
                SingleRightForm.RightForm.Show();
            }
        }
    }
}
