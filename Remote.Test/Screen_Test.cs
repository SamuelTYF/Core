using System;
using TestFramework;
using FileTransmission;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Remote.Test
{
    public class Screen_Test : ITest
    {
        public Screen_Test()
            : base("Screen_Test", 100)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            DirectoryInfo di = new($"{Environment.CurrentDirectory}/Temp");
            if (!di.Exists) di.Create();
            Server server = new(8888, 1, new TestTool(UpdateInfo));
            server.Register(new FileServerModule(Environment.CurrentDirectory));
            server.BeginAccept();
            Client client = new(IPHelper.GetIP().ToString(), 8888, 1);
            for (int i = 0; i < 100; i++)
            {
                Bitmap bm = new(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bm);
                g.CopyFromScreen(0, 0, 0, 0, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
                FileInfo fi = new($"{DateTime.Now.Ticks}.tiff");
                bm.Save(fi.Name, ImageFormat.Tiff);
                client.Insert(new FileClientModule((fi, fi.Name)));
                client.Start();
                client.WaitForEnd();
                fi.Delete();
                update(i + 1);
            }
            server.EndAccept();
            server.Close();
        }
    }
}
