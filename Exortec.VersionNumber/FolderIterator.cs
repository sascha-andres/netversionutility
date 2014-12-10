//*******************************************************************************************
// Exortec.VersionNumber - FolderIterator.cs
//*******************************************************************************************
// (c) 10-Dez-2014
//*******************************************************************************************

using System;

namespace Exortec.VersionNumber {
  internal class FolderIterator : IDisposable {
    private readonly CommandLineOptions _Options = null;
    private readonly string _Folder = string.Empty;

    public FolderIterator( CommandLineOptions options )
      : this( options, options.Folder ) {
    }

    public FolderIterator( CommandLineOptions options, string folder ) {
      _Options = options;
      _Folder = folder;
    }

    public int Execute() {
      if ( string.IsNullOrWhiteSpace( _Options.Folder ) ) return Runner.ERROR_NO_FOLDER_PROVIDED;
      foreach ( var directory in System.IO.Directory.GetDirectories( _Folder ) ) {
        using ( var folderIterator = new FolderIterator( _Options, directory ) ) {
          var result = folderIterator.Execute();
          if ( result != Runner.SUCCESS_EXECUTION ) return result;
        }
      }
      foreach ( var file in System.IO.Directory.GetFiles( _Folder, "AssemblyInfo.cs" ) ) {
        using ( var fileHandler = new FileHandler( _Options, file ) ) {
          var result = fileHandler.Execute();
          if ( result != Runner.SUCCESS_EXECUTION ) return result;
        }
      }
      return Runner.SUCCESS_EXECUTION;
    }

    public void Dispose() {
    }
  }
}
