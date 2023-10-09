using DeskApp.UC;
using DeskApp.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DeskApp
{
    public partial class MainForm : Form
    {
        #region Native
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point p);
        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private const int WM_USER = 0x0400;
        #endregion

        private MenuForm menuForm;
        private readonly SqlSugarClient db = SQLiteUtil.GetInstance();
        private string currPanel;
        private RightForm rightForm;
        public MainForm()
        {
            InitializeComponent();
            menuForm = new MenuForm();
        }
        /// <summary>
        /// 鼠标当前所在应用
        /// </summary>
        /// <returns></returns>
        public string CurrentApp
        {
            get
            {
                IntPtr hwnd = WindowFromPoint(Cursor.Position);
                _ = GetWindowThreadProcessId(hwnd, out int pID);
                Process process = Process.GetProcessById(pID);
                return process.MainModule != null ? process.MainModule.FileName : string.Empty;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_USER)
            {
                int X = m.WParam.ToInt32();
                int Y = m.LParam.ToInt32();
                menuForm.CurrentFocuse = PathUtil.GetFocusePath();
                menuForm.CurrentApp = PathUtil.Stem(CurrentApp);
                menuForm.Text = menuForm.CurrentApp;
                if (menuForm.Text == "explorer")
                {
                    menuForm.Text += $" -> {Path.GetFileName(menuForm.CurrentFocuse)}";
                }
                menuForm.RefreshPancel();
                menuForm.Location = new Point(X - 150, Y - 50);
                _ = SetCursorPos(X - 100, Y);
                menuForm.Show();
            }
            base.WndProc(ref m);
        }
        /// <summary>
        /// 根据grouptype填充panel
        /// </summary>
        /// <param name="grouptype"></param>
        private void AddControlsToPanel(string grouptype)
        {
            currPanel = grouptype;
            List<Models.Menu> list = db.Queryable<Models.Menu>().Where(c => c.Grouptype == grouptype).ToList();
            panel1.Controls.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                Models.Menu menu = list[i];
                // 横向8个图标
                int x = (i & 7) * 70;
                int y = (i >> 3) * 80;
                panel1.Controls.Add(new UCApplication(menu) { Location = new Point(x, y) });
            }
        }
        /// <summary>
        /// 事件注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLoad(object sender, EventArgs e)
        {
            AddControlsToPanel("main");
            notifyIcon1.Visible = false;

            MouseHook mouseHook = new MouseHook();
            mouseHook.MouseDown += MiddleButtonClick;
            mouseHook.Start();
            KeyboardHook keyboardHook = new KeyboardHook();
            keyboardHook.KeyUp += AltSpaceUp;
            keyboardHook.Start();
        }
        /// <summary>
        /// 中键呼出子菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MiddleButtonClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if (menuForm.IsDisposed)
                {
                    menuForm = new MenuForm();
                }
                _ = PostMessage(Handle, WM_USER, (IntPtr)e.Location.X, (IntPtr)e.Location.Y);
            }
        }
        /// <summary>
        /// Alt+Space 呼出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AltSpaceUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Alt | Keys.Space))
            {
                Visible = !Visible;
                WindowState = FormWindowState.Normal;
            }
        }

        private void MainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddControlsToPanel("main");
        }

        private void SystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddControlsToPanel("system");
        }
        /// <summary>
        /// 右键创建RightForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rightForm = new RightForm(this) { Location = PointToScreen(e.Location), IsInsert = true, Grouptype = currPanel };
                SingleRightForm.RightForm = rightForm;
                SingleRightForm.RightForm.Show();
            }
        }
        /// <summary>
        /// 拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
        }
        /// <summary>
        /// 拖拽时获取应用路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (paths.Length == 1)
            {
                _ = db.Insertable(new Models.Menu
                {
                    Appname = PathUtil.GetRealPath(paths[0]),
                    Name = PathUtil.Stem(paths[0]),
                    Grouptype = currPanel,
                    Icon = PathUtil.GetIconByPath(paths[0]).Resize().ToBase64(),
                }).ExecuteCommand();
                RefreshPancel();
            }
        }
        /// <summary>
        /// 窗口最小时，隐藏到托盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Show();
                notifyIcon1.Visible = false;
            }
            else if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }
        /// <summary>
        /// 点击托盘图标，显示窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
        /// <summary>
        /// 刷新
        /// </summary>
        public void RefreshPancel()
        {
            AddControlsToPanel(currPanel);
        }
    }
}
