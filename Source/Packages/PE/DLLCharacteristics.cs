using System;
namespace PE
{
	[Flags]
	public enum DLLCharacteristics : ushort
	{
		IMAGE_DLLCHARACTERISTICS_HIGH_ENTROPY_VA = 0x20,
		IMAGE_DLLCHARACTERISTICS_DYNAMIC_BASE = 0x40,
		IMAGE_DLLCHARACTERISTICS_FORCE_INTEGRITY = 0x80,
		IMAGE_DLLCHARACTERISTICS_NX_COMPAT = 0x100,
		IMAGE_DLLCHARACTERISTICS_NO_ISOLATION = 0x200,
		IMAGE_DLLCHARACTERISTICS_NO_SEH = 0x400,
		IMAGE_DLLCHARACTERISTICS_NO_BIND = 0x800,
		IMAGE_DLLCHARACTERISTICS_APPCONTAINER = 0x1000,
		IMAGE_DLLCHARACTERISTICS_WDM_DRIVER = 0x2000,
		IMAGE_DLLCHARACTERISTICS_GUARD_CF = 0x4000,
		IMAGE_DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE = 0x8000
	}
}
