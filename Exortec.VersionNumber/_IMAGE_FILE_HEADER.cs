//*******************************************************************************************
// Exortec.VersionNumber - _IMAGE_FILE_HEADER.cs
//*******************************************************************************************
// (c) 10-Dez-2014
//*******************************************************************************************

namespace Exortec.VersionNumber {
  struct _IMAGE_FILE_HEADER {
    public ushort Machine;
    public ushort NumberOfSections;
    public uint TimeDateStamp;
    public uint PointerToSymbolTable;
    public uint NumberOfSymbols;
    public ushort SizeOfOptionalHeader;
    public ushort Characteristics;
  }
}
