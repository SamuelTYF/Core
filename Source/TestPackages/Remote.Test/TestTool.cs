using System;
using Remote.Module;
using TestFramework;
namespace Remote.Test
{
    public class TestTool : ModuleTools
    {
        public UpdateInfo UpdateInfo;
        public TestTool(UpdateInfo update) => UpdateInfo = update;
        public override void Throw() => UpdateInfo(new Exception());
        public override void Write(object value, int color = -1) => UpdateInfo(value);
    }
}
