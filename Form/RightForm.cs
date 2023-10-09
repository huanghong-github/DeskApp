using DeskApp.Models;
using DeskApp.UC;
using DeskApp.Utils;
using SqlSugar;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DeskApp
{
    public partial class RightForm : Form
    {

        private readonly Control control;
        private readonly SqlSugarClient db = SQLiteUtil.GetInstance();

        public new Models.Menu Menu { set; get; }
        public string Grouptype { set; get; } = default;
        public bool IsInsert { set; get; } = false;
        public RightForm(Control control)
        {
            this.control = control;
            TopMost = true;
            StartPosition = FormStartPosition.Manual;
            InitializeComponent();
        }
        /// <summary>
        /// 控件刷新
        /// </summary>
        /// <param name="control"></param>
        private void RefreshPancel(Control control)
        {
            if (control is MainForm mainForm)
            {
                mainForm.RefreshPancel();
            }
            else if (control is MenuForm menuForm)
            {
                menuForm.RefreshPancel();
            }
            else if (control is UCApplication uCApplication)
            {
                RefreshPancel(uCApplication.Parent?.Parent);
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (Menu != null)
            {
                _ = db.Updateable(new Models.Menu()
                {
                    Appname = pathtextBox.Text,
                    Name = nametextBox.Text,
                    Icon = ((Bitmap)iconpictureBox.Image).ToBase64(),
                    Auguments = argtextBox.Text,
                    Grouptype = grouptextBox.Text == string.Empty ? null : grouptextBox.Text
                }).Where(it => it.Id == Menu.Id).ExecuteCommand();
                RefreshPancel(control);
            }
            Close();

        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertButton_MouseClick(object sender, MouseEventArgs e)
        {
            _ = db.Insertable(new Models.Menu
            {
                Appname = pathtextBox.Text,
                Name = nametextBox.Text,
                Icon = ((Bitmap)iconpictureBox.Image).ToBase64(),
                Auguments = argtextBox.Text,
                Grouptype = grouptextBox.Text == string.Empty ? null : grouptextBox.Text
            }).ExecuteCommand();
            RefreshPancel(control);
            Close();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (Menu != null)
            {
                _ = db.Deleteable<Models.Menu>().Where(c => c.Id == Menu.Id).ExecuteCommand();
                RefreshPancel(control);
            }
            Close();
        }
        /// <summary>
        /// 将已有信息填充RightForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightForm_Load(object sender, EventArgs e)
        {
            if (Menu != null)
            {
                grouptextBox.Text = Menu.Grouptype;
                argtextBox.Text = Menu.Auguments;
                pathtextBox.Text = Menu.Appname;
                nametextBox.Text = Menu.Name;
                iconpictureBox.Image = Menu.Icon.ToImage();
            }
            if (!string.IsNullOrEmpty(Grouptype))
            {
                grouptextBox.Text = Grouptype;
            }

            if (IsInsert)
            {
                updatebutton.Visible = false;
                deletebutton.Visible = false;
            }
            else
            {
                insertbutton.Visible = false;
            }
        }
        /// <summary>
        /// Esc退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightForm_KeyDown(object sender, KeyEventArgs e)
        {
            //form的key事件要设置KeyPreview = true;
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        /// <summary>
        /// 填icon路径后，显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IcontextBox_Leave(object sender, EventArgs e)
        {
            if (File.Exists(icontextBox.Text))
            {
                iconpictureBox.Image = ((Bitmap)Image.FromFile(icontextBox.Text)).Resize();
            }
        }
        /// <summary>
        /// 填应用路径后，更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PathtextBox_Leave(object sender, EventArgs e)
        {
            if (pathtextBox.Text.Equals("cmd"))
            {
                pathtextBox.Text = PathUtil.CmdPath;
            }
            if (File.Exists(pathtextBox.Text))
            {
                nametextBox.Text = PathUtil.Stem(pathtextBox.Text);
                pathtextBox.Text = PathUtil.GetRealPath(pathtextBox.Text);
            }
            iconpictureBox.Image = PathUtil.GetIconByPath(pathtextBox.Text).Resize();
        }
    }
    /// <summary>
    /// 单例，如果存在则重建
    /// </summary>
    public static class SingleRightForm
    {
        private static RightForm _rightForm;
        public static RightForm RightForm
        {
            set
            {
                if (_rightForm != null && !_rightForm.IsDisposed)
                {
                    _rightForm.Close();
                }

                if (_rightForm is null || _rightForm.IsDisposed)
                {
                    _rightForm = value;
                }
            }
            get => _rightForm;
        }
    }
}
