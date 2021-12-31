using ISO;
using Collection;
using System.IO;
class Program
{
    static void Main(string[] args)
    {
        FileStream input = new FileInfo(@"D:\OS\Test\bin\Debug\netcoreapp2.0\cosmos\ISO.txt").OpenRead();
        input.Position += 32768;
        ISOStreamReader sr = new(input);
        VolumeDescriptor _PrimaryVolumeDescriptor = new(sr);
        PrimaryVolumeDescriptor PrimaryVolumeDescriptor = PrimaryVolumeDescriptor.Parse(_PrimaryVolumeDescriptor.Data);
        VolumeDescriptor _BootRecordDescriptor = new(sr);
        BootRecordDescriptor BootRecordDescriptor = BootRecordDescriptor.Parse(_BootRecordDescriptor.Data);
        VolumeDescriptor _SupplementaryVolumeDescriptor = new(sr);
        SupplementaryVolumeDescriptor SupplementaryVolumeDescriptor = SupplementaryVolumeDescriptor.Parse(_SupplementaryVolumeDescriptor.Data);
        VolumeDescriptor Terminator = new(sr);
        Console.WriteLine(SupplementaryVolumeDescriptor.VolumeRoot);
        long doffset = SupplementaryVolumeDescriptor.VolumeRoot.LogicalBlockSize.Value *
            SupplementaryVolumeDescriptor.VolumeRoot.Root.LocationOfExtent.Value;
        input.Position = doffset;
        DictionaryRecord record = new(sr);
        Console.WriteLine(record);
    }
}