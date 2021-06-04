using System;
using System.Windows.Forms;

namespace Hook.TestForm
{
    public partial class TestForm : Form
    {
		public HookManager HookManager;
		public TestForm()
		{
			InitializeComponent();
			HookManager = new HookManager();
			HookManager.Register((Func<int>)Get);
			HookManager.Register((Func<long>)GetTick);
		}
		protected override void WndProc(ref Message m)
		{
			try
			{
				if (m.Msg == 74)
				{
					m.Result=HookManager.Execute(m.LParam);
					return;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			base.WndProc(ref m);
		}
		public int Get() => 5;
		public long GetTick() => DateTime.Now.Ticks;
	}
}
