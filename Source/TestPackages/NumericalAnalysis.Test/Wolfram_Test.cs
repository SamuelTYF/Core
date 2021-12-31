using Wolfram.NETLink;
using TestFramework;

namespace NumericalAnalysis.Test
{
    public class Wolfram_Test:ITest
    {
        public Wolfram_Test()
            :base("Wolfram_Test",3)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            WrappedKernelLink link = MathLinkFactory.CreateKernelLink() as WrappedKernelLink;
            link.WaitAndDiscardAnswer();
            update(1);
            link.PutFunction("EvaluatePacket", 1);
            Command command = new("Table",
                new Command("Prime",new Symbol("x")),
                new Command("List",new Symbol("x"),1,10));
            command.ToLink(link);
            link.EndPacket();
            link.WaitForAnswer();
            update(2);
            Expr exp = link.GetExpr();
            Ensure.Equal(exp.Length, 10);
            update(3);
            link.NewPacket();
            command = new Command("Plot",
                new Symbol("x"),
                new Command("List", new Symbol("x"), 1, 10));
            command.ToLink(link);
            link.EndPacket();
            link.WaitForAnswer();
            exp = link.GetExpr();
            link.EvaluateToImage(exp, 1600, 900).Save("1.jpg");
            link.Close();
        }
    }
}
