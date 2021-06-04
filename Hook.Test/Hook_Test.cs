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
            Manager manager = new("Hook.TestForm.exe");
            manager.Start();
            int r=(int)manager.Execute("Get");
            Ensure.Equal(r,5);
            update(1);
            long last = System.DateTime.Now.Ticks;
            long tick = (long)manager.Execute("GetTick");
            UpdateInfo(tick - last);
            update(2);
            manager.Dispose();
            update(3);
        }
    }
}
