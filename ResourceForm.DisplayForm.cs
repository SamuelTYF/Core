// 警告：某些程序集引用无法自动解析。这可能会导致某些部分反编译错误，
// 例如属性 getter/setter 访问。要获得最佳反编译结果，请手动将缺少的引用添加到加载的程序集列表中。
// ResourceForm.DisplayForm
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Collection;
using Hook;

public class DisplayForm : Form
{
	public HookManager Hook;

	public static readonly byte[] Byte16 = new byte[16]
	{
		48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
		65, 66, 67, 68, 69, 70
	};

	public static readonly byte[] EmptyPostionByteArray = new byte[10] { 48, 48, 48, 48, 48, 48, 48, 48, 32, 32 };

	public static readonly byte[] BlankArray = new byte[4] { 32, 32, 32, 32 };

	public bool[] WhiteList = new bool[256];

	public long PositionOffset = 0L;

	public int Last;

	private IContainer components = null;

	private SplitContainer splitContainer1;

	private RichTextBox richTextBox1;

	private MenuStrip menuStrip1;

	private ToolStripMenuItem openToolStripMenuItem;

	private OpenFileDialog openFileDialog1;

	private ToolStripTextBox toolStripTextBox1;

	private ToolStripMenuItem findToolStripMenuItem;

	public DisplayForm()
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		InitializeComponent();
		for (int i = 48; i <= 57; i++)
		{
			WhiteList[i] = true;
		}
		for (int j = 97; j <= 122; j++)
		{
			WhiteList[j] = true;
		}
		for (int k = 65; k <= 90; k++)
		{
			WhiteList[k] = true;
		}
		WhiteList[46] = true;
		WhiteList[44] = true;
		WhiteList[61] = true;
		WhiteList[59] = true;
		Hook = new HookManager();
		Hook.Register((Delegate)new Action<byte[]>(Display));
		Hook.Register((Delegate)new Action<int>(Highlight));
		Hook.Register((Delegate)new Action<string>(OpenFile));
		Hook.Register((Delegate)new Action<long>(RegisterOffset));
	}

	private void openToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
			OpenFile(openFileDialog1.FileName);
	}

	public byte[] GetPosition(long Position)
	{
		byte[] array = new byte[10];
		Array.Copy(EmptyPostionByteArray, 0, array, 0, 10);
		int num = 8;
		while (Position > 0)
		{
			array[--num] = Byte16[Position & 0xF];
			Position >>= 4;
		}
		return array;
	}

	public void RegisterOffset(long offset)
	{
		PositionOffset = offset;
	}

	public void TackleStream(Stream stream)
	{
		byte[] array = new byte[16];
		List<byte> val = new List<byte>(16);
		while (stream.Position < stream.Length)
		{
			val.AddRange(GetPosition(stream.Position + PositionOffset));
			int num = stream.Read(array, 0, 16) + 1;
			while (num < 16)
			{
				array[num++] = 0;
			}
			for (int i = 0; i < 8; i++)
			{
				val.AddRange(new byte[3]
				{
					Byte16[array[i] >> 4],
					Byte16[array[i] & 0xF],
					32
				});
			}
			val.Add((byte)32);
			for (int j = 8; j < 16; j++)
			{
				val.AddRange(new byte[3]
				{
					Byte16[array[j] >> 4],
					Byte16[array[j] & 0xF],
					32
				});
			}
			val.AddRange(BlankArray);
			for (int k = 0; k < 16; k++)
			{
				if (!WhiteList[array[k]])
					array[k] = 32;
			}
			val.AddRange(array);
			val.Add((byte)10);
		}
		richTextBox1.Text = Encoding.UTF8.GetString(val.ToArray());
		Last = 0;
	}

	public void Display(byte[] bytes)
	{
		using MemoryStream stream = new MemoryStream(bytes);
		TackleStream(stream);
	}

	protected override void WndProc(ref Message m)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (m.Msg == 74)
			{
				COPYDATASTRUCT val = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));
				string lpData = val.lpData;
				Hook.Execute(val.lpData);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
		base.WndProc(ref m);
	}

	public void OpenFile(string fname)
	{
		using FileStream stream = new FileInfo(fname).OpenRead();
		TackleStream(stream);
	}

	public void Highlight(int end)
	{
		while (Last < end)
		{
			int num = Last >> 4;
			int num2 = Last++ - (num << 4);
			if (num2 < 8)
				richTextBox1.Select(num * 80 + 10 + num2 * 3, 2);
			else
				richTextBox1.Select(num * 80 + 11 + num2 * 3, 2);
			richTextBox1.SelectionColor = Color.Red;
			richTextBox1.Select(num * 80 + 63 + num2, 1);
			richTextBox1.SelectionColor = Color.Red;
		}
	}

	private void findToolStripMenuItem_Click(object sender, EventArgs e)
	{
		int num = richTextBox1.Text.IndexOf(toolStripTextBox1.Text, richTextBox1.SelectionStart);
		if (num != -1)
			richTextBox1.Select(num, toolStripTextBox1.Text.Length);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
			components.Dispose();
		base.Dispose(disposing);
	}

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
		base.Name = "DisplayForm";
		Text = "Form1";
		splitContainer1.Panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
		splitContainer1.ResumeLayout(false);
		menuStrip1.ResumeLayout(false);
		menuStrip1.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}
}
