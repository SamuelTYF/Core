
using System.Windows.Forms;
namespace Automata.IDE
{
    partial class IDEForm
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
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createInstanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disposeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.continueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.breakPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HostBox = new System.Windows.Forms.ToolStripComboBox();
            this.StringBox = new System.Windows.Forms.ToolStripTextBox();
            this.CharBox = new System.Windows.Forms.ToolStripTextBox();
            this.flushToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stateOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AMSBox = new System.Windows.Forms.RichTextBox();
            this.contextmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AMSSBox = new System.Windows.Forms.RichTextBox();
            this.InfoBox = new System.Windows.Forms.RichTextBox();
            this.ConsoleBox = new System.Windows.Forms.RichTextBox();
            this.HostDialog = new System.Windows.Forms.OpenFileDialog();
            this.AMIDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.hostToolStripMenuItem,
            this.buildToolStripMenuItem,
            this.runToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.continueToolStripMenuItem,
            this.moveToNextToolStripMenuItem,
            this.breakPointToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.HostBox,
            this.StringBox,
            this.CharBox,
            this.flushToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menu.Size = new System.Drawing.Size(1540, 34);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.normalToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 28);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // hostToolStripMenuItem
            // 
            this.hostToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.hostToolStripMenuItem.Name = "hostToolStripMenuItem";
            this.hostToolStripMenuItem.Size = new System.Drawing.Size(58, 28);
            this.hostToolStripMenuItem.Text = "Host";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkToolStripMenuItem,
            this.createInstanceToolStripMenuItem,
            this.disposeToolStripMenuItem});
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(59, 28);
            this.buildToolStripMenuItem.Text = "Build";
            // 
            // checkToolStripMenuItem
            // 
            this.checkToolStripMenuItem.Name = "checkToolStripMenuItem";
            this.checkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.checkToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.checkToolStripMenuItem.Text = "Check";
            this.checkToolStripMenuItem.Click += new System.EventHandler(this.checkToolStripMenuItem_Click);
            // 
            // createInstanceToolStripMenuItem
            // 
            this.createInstanceToolStripMenuItem.Enabled = false;
            this.createInstanceToolStripMenuItem.Name = "createInstanceToolStripMenuItem";
            this.createInstanceToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.createInstanceToolStripMenuItem.Text = "Create Instance";
            this.createInstanceToolStripMenuItem.Click += new System.EventHandler(this.createInstanceToolStripMenuItem_Click);
            // 
            // disposeToolStripMenuItem
            // 
            this.disposeToolStripMenuItem.Enabled = false;
            this.disposeToolStripMenuItem.Name = "disposeToolStripMenuItem";
            this.disposeToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.disposeToolStripMenuItem.Text = "Dispose";
            this.disposeToolStripMenuItem.Click += new System.EventHandler(this.disposeToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(51, 28);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(72, 28);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // continueToolStripMenuItem
            // 
            this.continueToolStripMenuItem.Enabled = false;
            this.continueToolStripMenuItem.Name = "continueToolStripMenuItem";
            this.continueToolStripMenuItem.Size = new System.Drawing.Size(89, 28);
            this.continueToolStripMenuItem.Text = "Continue";
            this.continueToolStripMenuItem.Click += new System.EventHandler(this.continueToolStripMenuItem_Click);
            // 
            // moveToNextToolStripMenuItem
            // 
            this.moveToNextToolStripMenuItem.Enabled = false;
            this.moveToNextToolStripMenuItem.Name = "moveToNextToolStripMenuItem";
            this.moveToNextToolStripMenuItem.Size = new System.Drawing.Size(127, 28);
            this.moveToNextToolStripMenuItem.Text = "Move To Next";
            this.moveToNextToolStripMenuItem.Click += new System.EventHandler(this.moveToNextToolStripMenuItem_Click);
            // 
            // breakPointToolStripMenuItem
            // 
            this.breakPointToolStripMenuItem.Name = "breakPointToolStripMenuItem";
            this.breakPointToolStripMenuItem.Size = new System.Drawing.Size(101, 28);
            this.breakPointToolStripMenuItem.Text = "BreakPoint";
            this.breakPointToolStripMenuItem.Click += new System.EventHandler(this.breakPointToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(58, 28);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // HostBox
            // 
            this.HostBox.Name = "HostBox";
            this.HostBox.Size = new System.Drawing.Size(200, 28);
            this.HostBox.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // StringBox
            // 
            this.StringBox.Name = "StringBox";
            this.StringBox.Size = new System.Drawing.Size(200, 28);
            this.StringBox.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // CharBox
            // 
            this.CharBox.Name = "CharBox";
            this.CharBox.Size = new System.Drawing.Size(200, 28);
            // 
            // flushToolStripMenuItem
            // 
            this.flushToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceOnToolStripMenuItem,
            this.textOnToolStripMenuItem,
            this.stateOnToolStripMenuItem});
            this.flushToolStripMenuItem.Name = "flushToolStripMenuItem";
            this.flushToolStripMenuItem.Size = new System.Drawing.Size(60, 28);
            this.flushToolStripMenuItem.Text = "Flush";
            // 
            // sourceOnToolStripMenuItem
            // 
            this.sourceOnToolStripMenuItem.Name = "sourceOnToolStripMenuItem";
            this.sourceOnToolStripMenuItem.Size = new System.Drawing.Size(168, 26);
            this.sourceOnToolStripMenuItem.Text = "Source On";
            this.sourceOnToolStripMenuItem.Click += new System.EventHandler(this.sourceOnToolStripMenuItem_Click);
            // 
            // textOnToolStripMenuItem
            // 
            this.textOnToolStripMenuItem.Name = "textOnToolStripMenuItem";
            this.textOnToolStripMenuItem.Size = new System.Drawing.Size(168, 26);
            this.textOnToolStripMenuItem.Text = "Text On";
            this.textOnToolStripMenuItem.Click += new System.EventHandler(this.textOnToolStripMenuItem_Click);
            // 
            // stateOnToolStripMenuItem
            // 
            this.stateOnToolStripMenuItem.Name = "stateOnToolStripMenuItem";
            this.stateOnToolStripMenuItem.Size = new System.Drawing.Size(168, 26);
            this.stateOnToolStripMenuItem.Text = "State On";
            this.stateOnToolStripMenuItem.Click += new System.EventHandler(this.stateOnToolStripMenuItem_Click);
            // 
            // AMSBox
            // 
            this.AMSBox.ContextMenuStrip = this.contextmenu;
            this.AMSBox.Location = new System.Drawing.Point(14, 34);
            this.AMSBox.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.AMSBox.Name = "AMSBox";
            this.AMSBox.Size = new System.Drawing.Size(580, 764);
            this.AMSBox.TabIndex = 1;
            this.AMSBox.Text = "";
            this.AMSBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            // 
            // contextmenu
            // 
            this.contextmenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextmenu.Name = "contextMenuStrip1";
            this.contextmenu.Size = new System.Drawing.Size(61, 4);
            // 
            // AMSSBox
            // 
            this.AMSSBox.BackColor = System.Drawing.Color.White;
            this.AMSSBox.DetectUrls = false;
            this.AMSSBox.EnableAutoDragDrop = true;
            this.AMSSBox.Location = new System.Drawing.Point(604, 34);
            this.AMSSBox.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.AMSSBox.Name = "AMSSBox";
            this.AMSSBox.Size = new System.Drawing.Size(922, 511);
            this.AMSSBox.TabIndex = 2;
            this.AMSSBox.Text = "";
            // 
            // InfoBox
            // 
            this.InfoBox.Location = new System.Drawing.Point(604, 551);
            this.InfoBox.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.InfoBox.Name = "InfoBox";
            this.InfoBox.Size = new System.Drawing.Size(297, 247);
            this.InfoBox.TabIndex = 3;
            this.InfoBox.Text = "";
            // 
            // ConsoleBox
            // 
            this.ConsoleBox.Location = new System.Drawing.Point(911, 551);
            this.ConsoleBox.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.ConsoleBox.Name = "ConsoleBox";
            this.ConsoleBox.Size = new System.Drawing.Size(615, 247);
            this.ConsoleBox.TabIndex = 4;
            this.ConsoleBox.Text = "";
            // 
            // HostDialog
            // 
            this.HostDialog.FileName = ".dll";
            this.HostDialog.Filter = "DLL|*.dll|All Files|*";
            this.HostDialog.InitialDirectory = "D:\\Core";
            // 
            // AMIDialog
            // 
            this.AMIDialog.FileName = ".ams";
            this.AMIDialog.Filter = "AutomataScript|*ams|AutomataSamples|*amss|AutomataInfo|*ami";
            this.AMIDialog.FilterIndex = 3;
            this.AMIDialog.InitialDirectory = "..\\Automatas";
            this.AMIDialog.RestoreDirectory = true;
            // 
            // SaveDialog
            // 
            this.SaveDialog.FileName = ".ams";
            this.SaveDialog.Filter = "AutomataScript|*ams";
            this.SaveDialog.InitialDirectory = "D:\\Core\\Automata\\Scripts";
            // 
            // IDEForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 805);
            this.Controls.Add(this.ConsoleBox);
            this.Controls.Add(this.InfoBox);
            this.Controls.Add(this.AMSSBox);
            this.Controls.Add(this.AMSBox);
            this.Controls.Add(this.menu);
            this.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Name = "IDEForm";
            this.Text = "Automata.IDE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.IDEForm_SizeChanged);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private MenuStrip menu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem hostToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStripMenuItem buildToolStripMenuItem;
        private ToolStripMenuItem checkToolStripMenuItem;
        private ToolStripMenuItem createInstanceToolStripMenuItem;
        private ToolStripMenuItem runToolStripMenuItem;
        private ToolStripMenuItem debugToolStripMenuItem;
        private ToolStripMenuItem continueToolStripMenuItem;
        private ToolStripMenuItem moveToNextToolStripMenuItem;
        private RichTextBox AMSBox;
        private RichTextBox AMSSBox;
        private RichTextBox InfoBox;
        private RichTextBox ConsoleBox;
        private ToolStripMenuItem disposeToolStripMenuItem;
        private ToolStripComboBox HostBox;
        private OpenFileDialog HostDialog;
        private ToolStripMenuItem saveToolStripMenuItem;
        private OpenFileDialog AMIDialog;
        private SaveFileDialog SaveDialog;
        private ToolStripTextBox StringBox;
        private ToolStripTextBox CharBox;
        private ToolStripMenuItem breakPointToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripMenuItem flushToolStripMenuItem;
        private ToolStripMenuItem sourceOnToolStripMenuItem;
        private ToolStripMenuItem textOnToolStripMenuItem;
        private ToolStripMenuItem stateOnToolStripMenuItem;
        private ContextMenuStrip contextmenu;
        private ToolStripMenuItem normalToolStripMenuItem;
        #endregion
    }
}