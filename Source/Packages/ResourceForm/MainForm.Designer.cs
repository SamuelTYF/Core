
using System.Windows.Forms;

namespace ResourceForm
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			richTextBox1 = new System.Windows.Forms.RichTextBox();
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.SuspendLayout();
			menuStrip1.SuspendLayout();
			SuspendLayout();
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(0, 31);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(richTextBox1);
			splitContainer1.Size = new System.Drawing.Size(1581, 631);
			splitContainer1.SplitterDistance = 1015;
			splitContainer1.TabIndex = 0;
			richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			richTextBox1.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			richTextBox1.Location = new System.Drawing.Point(0, 0);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.Size = new System.Drawing.Size(1015, 631);
			richTextBox1.TabIndex = 0;
			richTextBox1.Text = "";
			menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { openToolStripMenuItem, toolStripTextBox1, findToolStripMenuItem });
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new System.Drawing.Size(1581, 31);
			menuStrip1.TabIndex = 1;
			menuStrip1.Text = "menuStrip1";
			openToolStripMenuItem.Name = "openToolStripMenuItem";
			openToolStripMenuItem.Size = new System.Drawing.Size(63, 27);
			openToolStripMenuItem.Text = "Open";
			openToolStripMenuItem.Click += new System.EventHandler(openToolStripMenuItem_Click);
			openFileDialog1.FileName = "openFileDialog1";
			findToolStripMenuItem.Name = "findToolStripMenuItem";
			findToolStripMenuItem.Size = new System.Drawing.Size(54, 27);
			findToolStripMenuItem.Text = "Find";
			findToolStripMenuItem.Click += new System.EventHandler(findToolStripMenuItem_Click);
			toolStripTextBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9f);
			toolStripTextBox1.Name = "toolStripTextBox1";
			toolStripTextBox1.Size = new System.Drawing.Size(500, 27);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 15f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1581, 662);
			base.Controls.Add(splitContainer1);
			base.Controls.Add(menuStrip1);
			base.Name = "MainForm";
			Text = "MainForm";
			splitContainer1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion



		private SplitContainer splitContainer1;

        private RichTextBox richTextBox1;

        private MenuStrip menuStrip1;

        private ToolStripMenuItem openToolStripMenuItem;

        private OpenFileDialog openFileDialog1;

        private ToolStripTextBox toolStripTextBox1;

        private ToolStripMenuItem findToolStripMenuItem;

    }
}

