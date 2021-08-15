namespace PE
{
	public struct Characteristics
	{
		public enum Characteristic : ushort
		{
			IMAGE_FILE_RELOCS_STRIPPED = 1,
			IMAGE_FILE_EXECUTABLE_IMAGE = 2,
			IMAGE_FILE_LINE_NUMS_STRIPPED = 4,
			IMAGE_FILE_LOCAL_SYMS_STRIPPED = 8,
			IMAGE_FILE_AGGRESSIVE_WS_TRIM = 0x10,
			IMAGE_FILE_LARGE_ADDRESS_AWARE = 0x20,
			IMAGE_FILE_BYTES_REVERSED_LO = 0x80,
			IMAGE_FILE_32BIT_MACHINE = 0x100,
			IMAGE_FILE_DEBUG_STRIPPED = 0x200,
			IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP = 0x400,
			IMAGE_FILE_NET_RUN_FROM_SWAP = 0x800,
			IMAGE_FILE_SYSTEM = 0x1000,
			IMAGE_FILE_DLL = 0x2000,
			IMAGE_FILE_UP_SYSTEM_ONLY = 0x4000,
			IMAGE_FILE_BYTES_REVERSED_HI = 0x8000
		}
		public ushort Value;
		public static readonly ushort[] _CharacteristicValues = new ushort[15]
		{
			1, 2, 4, 8, 16, 32, 128, 256, 512, 1024,
			2048, 4096, 8192, 16384, 32768
		};
		public static readonly Characteristic[] _Characteristics = new Characteristic[15]
		{
			Characteristic.IMAGE_FILE_RELOCS_STRIPPED,
			Characteristic.IMAGE_FILE_EXECUTABLE_IMAGE,
			Characteristic.IMAGE_FILE_LINE_NUMS_STRIPPED,
			Characteristic.IMAGE_FILE_LOCAL_SYMS_STRIPPED,
			Characteristic.IMAGE_FILE_AGGRESSIVE_WS_TRIM,
			Characteristic.IMAGE_FILE_LARGE_ADDRESS_AWARE,
			Characteristic.IMAGE_FILE_BYTES_REVERSED_LO,
			Characteristic.IMAGE_FILE_32BIT_MACHINE,
			Characteristic.IMAGE_FILE_DEBUG_STRIPPED,
			Characteristic.IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP,
			Characteristic.IMAGE_FILE_NET_RUN_FROM_SWAP,
			Characteristic.IMAGE_FILE_SYSTEM,
			Characteristic.IMAGE_FILE_DLL,
			Characteristic.IMAGE_FILE_UP_SYSTEM_ONLY,
			Characteristic.IMAGE_FILE_BYTES_REVERSED_HI
		};
        public Characteristics(ushort value) => Value = value;
        public override string ToString()
		{
			string text = null;
			for (int i = 0; i < _CharacteristicValues.Length; i++)
				if ((Value & _CharacteristicValues[i]) != 0)
					text = (text != null) ? (text + $"|{_Characteristics[i]}") : _Characteristics[i].ToString();
			return text ?? $"{Value:X}";
		}
	}
}
