using Hook;
using System;
using Collection;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ResourceForm
{
    public partial class MainForm : Form
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
		public MainForm()
		{
			InitializeComponent();
			for (int i = 'a'; i <= 'z'; i++)
				WhiteList[i] = true;
			for (int j = 'A'; j <= 'Z'; j++)
				WhiteList[j] = true;
			for (int k = '0'; k <= '9'; k++)
				WhiteList[k] = true;
			Hook = new HookManager();
			Hook.Register(new Action<byte[]>(Display));
			Hook.Register(new Action<int>(Highlight));
			Hook.Register(new Action<string>(OpenFile));
			Hook.Register(new Action<long>(RegisterOffset));
			Hook.Register(new Func<long>(GetTicks));
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
        public void RegisterOffset(long offset) => PositionOffset = offset;
        public void TackleStream(Stream stream)
		{
			byte[] array = new byte[16];
			List<byte> list = new();
			while (stream.Position < stream.Length)
			{
				list.AddRange(GetPosition(stream.Position + PositionOffset));
				int num = stream.Read(array, 0, 16) + 1;
				while (num < 16)
					array[num++] = 0;
				for (int i = 0; i < 8; i++)
					list.AddRange(Byte16[array[i] >> 4], Byte16[array[i] & 0xF], 32);
				list.Add(32);
				for (int j = 8; j < 16; j++)
					list.AddRange(Byte16[array[j] >> 4], Byte16[array[j] & 0xF], 32);
				list.AddRange(BlankArray);
				for (int k = 0; k < 16; k++)
					if (!WhiteList[array[k]])
						array[k] = 32;
				list.AddRange(array);
				list.Add(10);
			}
			richTextBox1.Text = Encoding.UTF8.GetString(list.ToArray());
			Last = 0;
		}
		public void Display(byte[] bytes)
		{
			using MemoryStream stream = new(bytes);
			TackleStream(stream);
		}
		protected override void WndProc(ref Message m)
		{
			try
			{
				if (m.Msg == 74)
				{
					m.Result = Hook.Execute(m.LParam);
					return;
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
		public static long GetTicks() => DateTime.Now.Ticks;
	}
}
