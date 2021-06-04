using TestFramework;

namespace Hook.Test
{
    public class Hook_Test : ITest
    {
        public Hook_Test()
            :base("Hook_Test",3)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            Manager manager = new("ResourceForm.exe");
            manager.Start();
            update(1);
            long last = System.DateTime.Now.Ticks;
            long tick = (long)manager.Execute("GetTicks");
            UpdateInfo(tick - last);
            update(2);
            manager.Dispose();
            update(3);
        }
    }
}
