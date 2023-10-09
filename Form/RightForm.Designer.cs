using System.Drawing;
using System.Windows.Forms;

namespace DeskApp
{
    partial class RightForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.namelabel = new System.Windows.Forms.Label();
            this.pathlabel = new System.Windows.Forms.Label();
            this.iconlabel = new System.Windows.Forms.Label();
            this.updatebutton = new System.Windows.Forms.Button();
            this.insertbutton = new System.Windows.Forms.Button();
            this.deletebutton = new System.Windows.Forms.Button();
            this.nametextBox = new System.Windows.Forms.TextBox();
            this.pathtextBox = new System.Windows.Forms.TextBox();
            this.icontextBox = new System.Windows.Forms.TextBox();
            this.arglabel = new System.Windows.Forms.Label();
            this.argtextBox = new System.Windows.Forms.TextBox();
            this.iconpictureBox = new System.Windows.Forms.PictureBox();
            this.grouplabel = new System.Windows.Forms.Label();
            this.grouptextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconpictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // namelabel
            // 
            this.namelabel.AutoSize = true;
            this.namelabel.Location = new System.Drawing.Point(11, 18);
            this.namelabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.namelabel.Name = "namelabel";
            this.namelabel.Size = new System.Drawing.Size(37, 15);
            this.namelabel.TabIndex = 0;
            this.namelabel.Text = "标题";
            // 
            // pathlabel
            // 
            this.pathlabel.AutoSize = true;
            this.pathlabel.Location = new System.Drawing.Point(11, 50);
            this.pathlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pathlabel.Name = "pathlabel";
            this.pathlabel.Size = new System.Drawing.Size(37, 15);
            this.pathlabel.TabIndex = 0;
            this.pathlabel.Text = "路径";
            // 
            // iconlabel
            // 
            this.iconlabel.AutoSize = true;
            this.iconlabel.Location = new System.Drawing.Point(11, 98);
            this.iconlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.iconlabel.Name = "iconlabel";
            this.iconlabel.Size = new System.Drawing.Size(37, 15);
            this.iconlabel.TabIndex = 0;
            this.iconlabel.Text = "图标";
            // 
            // updatebutton
            // 
            this.updatebutton.Location = new System.Drawing.Point(48, 197);
            this.updatebutton.Margin = new System.Windows.Forms.Padding(2);
            this.updatebutton.Name = "updatebutton";
            this.updatebutton.Size = new System.Drawing.Size(62, 26);
            this.updatebutton.TabIndex = 1;
            this.updatebutton.Text = "update";
            this.updatebutton.UseVisualStyleBackColor = true;
            this.updatebutton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UpdateButton_MouseClick);
            // 
            // insertbutton
            // 
            this.insertbutton.Location = new System.Drawing.Point(99, 197);
            this.insertbutton.Margin = new System.Windows.Forms.Padding(2);
            this.insertbutton.Name = "insertbutton";
            this.insertbutton.Size = new System.Drawing.Size(71, 26);
            this.insertbutton.TabIndex = 1;
            this.insertbutton.Text = "insert";
            this.insertbutton.UseVisualStyleBackColor = true;
            this.insertbutton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.InsertButton_MouseClick);
            // 
            // deletebutton
            // 
            this.deletebutton.Location = new System.Drawing.Point(154, 197);
            this.deletebutton.Margin = new System.Windows.Forms.Padding(2);
            this.deletebutton.Name = "deletebutton";
            this.deletebutton.Size = new System.Drawing.Size(65, 26);
            this.deletebutton.TabIndex = 1;
            this.deletebutton.Text = "delete";
            this.deletebutton.UseVisualStyleBackColor = true;
            this.deletebutton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DeleteButton_MouseClick);
            // 
            // nametextBox
            // 
            this.nametextBox.Location = new System.Drawing.Point(49, 18);
            this.nametextBox.Margin = new System.Windows.Forms.Padding(2);
            this.nametextBox.Name = "nametextBox";
            this.nametextBox.Size = new System.Drawing.Size(169, 25);
            this.nametextBox.TabIndex = 2;
            // 
            // pathtextBox
            // 
            this.pathtextBox.Location = new System.Drawing.Point(49, 50);
            this.pathtextBox.Margin = new System.Windows.Forms.Padding(2);
            this.pathtextBox.Name = "pathtextBox";
            this.pathtextBox.Size = new System.Drawing.Size(169, 25);
            this.pathtextBox.TabIndex = 2;
            this.pathtextBox.Leave += new System.EventHandler(this.PathtextBox_Leave);
            // 
            // icontextBox
            // 
            this.icontextBox.Location = new System.Drawing.Point(49, 92);
            this.icontextBox.Margin = new System.Windows.Forms.Padding(2);
            this.icontextBox.Name = "icontextBox";
            this.icontextBox.Size = new System.Drawing.Size(109, 25);
            this.icontextBox.TabIndex = 2;
            this.icontextBox.Leave += new System.EventHandler(this.IcontextBox_Leave);
            // 
            // arglabel
            // 
            this.arglabel.AutoSize = true;
            this.arglabel.Location = new System.Drawing.Point(11, 141);
            this.arglabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.arglabel.Name = "arglabel";
            this.arglabel.Size = new System.Drawing.Size(37, 15);
            this.arglabel.TabIndex = 0;
            this.arglabel.Text = "参数";
            // 
            // argtextBox
            // 
            this.argtextBox.Location = new System.Drawing.Point(49, 137);
            this.argtextBox.Margin = new System.Windows.Forms.Padding(2);
            this.argtextBox.Name = "argtextBox";
            this.argtextBox.Size = new System.Drawing.Size(169, 25);
            this.argtextBox.TabIndex = 2;
            // 
            // iconpictureBox
            // 
            this.iconpictureBox.Location = new System.Drawing.Point(162, 76);
            this.iconpictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.iconpictureBox.Name = "iconpictureBox";
            this.iconpictureBox.Size = new System.Drawing.Size(60, 60);
            this.iconpictureBox.TabIndex = 3;
            this.iconpictureBox.TabStop = false;
            // 
            // grouplabel
            // 
            this.grouplabel.AutoSize = true;
            this.grouplabel.Location = new System.Drawing.Point(11, 171);
            this.grouplabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.grouplabel.Name = "grouplabel";
            this.grouplabel.Size = new System.Drawing.Size(37, 15);
            this.grouplabel.TabIndex = 0;
            this.grouplabel.Text = "类别";
            // 
            // grouptextBox
            // 
            this.grouptextBox.Location = new System.Drawing.Point(49, 168);
            this.grouptextBox.Margin = new System.Windows.Forms.Padding(2);
            this.grouptextBox.Name = "grouptextBox";
            this.grouptextBox.Size = new System.Drawing.Size(169, 25);
            this.grouptextBox.TabIndex = 2;
            // 
            // RightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(239, 233);
            this.Controls.Add(this.iconpictureBox);
            this.Controls.Add(this.grouptextBox);
            this.Controls.Add(this.argtextBox);
            this.Controls.Add(this.icontextBox);
            this.Controls.Add(this.pathtextBox);
            this.Controls.Add(this.nametextBox);
            this.Controls.Add(this.deletebutton);
            this.Controls.Add(this.insertbutton);
            this.Controls.Add(this.updatebutton);
            this.Controls.Add(this.grouplabel);
            this.Controls.Add(this.arglabel);
            this.Controls.Add(this.iconlabel);
            this.Controls.Add(this.pathlabel);
            this.Controls.Add(this.namelabel);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RightForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.RightForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RightForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.iconpictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label namelabel;
        private Label pathlabel;
        private Label iconlabel;
        private Button updatebutton;
        private Button insertbutton;
        private Button deletebutton;
        private TextBox nametextBox;
        private TextBox pathtextBox;
        private TextBox icontextBox;
        private Label arglabel;
        private TextBox argtextBox;
        private PictureBox iconpictureBox;
        private Label grouplabel;
        private TextBox grouptextBox;
    }
}