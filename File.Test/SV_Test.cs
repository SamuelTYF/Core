using File.SV;
using System;
using System.IO;
using TestFramework;
namespace File.Test
{
    public class SV_Test : ITest
    {
        public SV_Test()
            :base("CSV_Test",4)
        {
        }
        public void Compare(SVFile a,SVFile b)
        {
            Ensure.Equal(a.Title.Values, b.Title.Values);
            Ensure.Equal(a.Length, b.Length);
            for (int i = 0; i < a.Values.Length; i++)
                Ensure.Equal(a[i].Values, b[i].Values);
        }
        public override void Run(UpdateTaskProgress update)
        {
            SVFile a = new("Number","Sqrt");
            for (int i = 1, j = 1; i <= 1000; i++)
            {
                a.Insert(i.ToString(), j.ToString());
                if (j * j == i) j++;
            }
            a.Write("1.csv");
            update(1);
            SVFile b=CSV_Reader.Read("1.csv");
            Compare(a, b);
            update(2);
            SV_Selector selector = new((values) =>
            {
                int a = int.Parse(values[0]);
                int b = int.Parse(values[1]);
                return b * b == a;
            });
            SVFile c = selector.Select(b, out SVFile d);
            c.Write("sqrt.tsv", '\t');
            d.Write("other.csv", ',');
            update(3);
            SVFile e = TSV_Reader.Read("sqrt.tsv");
            Compare(c, e);
            update(4);
            foreach (FileInfo fi in new DirectoryInfo(Environment.CurrentDirectory).EnumerateFiles("*sv"))
                fi.Delete();
        }
    }
}
