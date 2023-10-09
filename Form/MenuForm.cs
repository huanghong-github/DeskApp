using DeskApp.UC;
using DeskApp.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DeskApp
{
    public partial class MenuForm : Form
    {
        public string CurrentApp { set; get; } = default;
        public string CurrentFocuse { set; get; }
        private readonly SqlSugarClient db = SQLiteUtil.GetInstance();
        private RightForm rightForm;
        public MenuForm()
        {
            TopMost = true;// 置于顶层
            StartPosition = FormStartPosition.Manual; // 起始位置可调整
            Hide(); // 不可见
            InitializeComponent();
            AddControlsToPanel(null); //默认应用
        }
        /// <summary>
        /// 给子控件注册MouseLeave,将事件向上传递
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_Load(object sender, EventArgs e)
        {
            foreach (Control c in Controls)
            {
                c.MouseLeave += (object _, EventArgs e1) => base.OnMouseLeave(e1);
            }
        }

        /// <summary>
        /// 根据grouptype填充panel
        /// </summary>
        /// <param name="grouptype"></param>
        public void AddControlsToPanel(string grouptype)
        {
            List<Models.Menu> list = db.Queryable<Models.Menu>().Where(c => c.Grouptype == null || c.Grouptype == grouptype).ToList();
            panel1.Controls.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                Models.Menu menu = list[i];
                int x = (i & 3) * 70;
                int y = (i >> 2) * 80;
                panel1.Controls.Add(new UCApplication(menu) { Location = new Point(x, y) });
            }
        }
        /// <summary>
        /// 鼠标离开控件，隐藏
        /// 无法触发，可能是子控件拦截了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuFormHide(object sender, EventArgs e)
        {
            if (!RectangleToScreen(ClientRectangle).Contains(MousePosition))
            {
                Hide();
            }
        }
        /// <summary>
        /// 右键呼出RightForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rightForm = new RightForm(this) { Location = PointToScreen(e.Location), IsInsert = true, Grouptype = CurrentApp };
                SingleRightForm.RightForm = rightForm;
                SingleRightForm.RightForm.Show();
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        public void RefreshPancel()
        {
            AddControlsToPanel(CurrentApp);
        }
        public void CallMouseLeave(EventArgs e)
        {
            OnMouseLeave(e);
        }
    }
}