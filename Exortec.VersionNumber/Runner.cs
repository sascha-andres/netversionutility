//*******************************************************************************************
// Exortec.VersionNumber - Runner.cs
//*******************************************************************************************
// (c) 10-Dez-2014
//*******************************************************************************************

using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Exortec.VersionNumber {
  public class Runner : IDisposable {
    #region constants
    internal const int ERROR_NO_FOLDER_PROVIDED = -3;
    internal const int SUCCESS_EXECUTION = 0;
    internal const int ERROR_NO_FOLDER = -2;
    #endregion

    private readonly string[] _Args = null;
    private CommandLineOptions _Options = null;

    private bool validate() {
      _Options = new CommandLineOptions();
      return Parser.Default.ParseArguments( _Args, _Options );
    }

    #region console write helper
    internal static void writeLine( string lineToWrite ) {
      Console.WriteLine( lineToWrite );
    }

    internal static void writeError( string lineToWrite ) {
      var foregroundColor = Console.ForegroundColor;
      var backgroundColor = Console.BackgroundColor;
      Console.ForegroundColor = ConsoleColor.Red;
      Console.BackgroundColor = ConsoleColor.White;
      writeLine( lineToWrite );
      Console.ForegroundColor = foregroundColor;
      Console.BackgroundColor = backgroundColor;
    }
    #endregion

    public int Execute() {
      if ( validate() ) {
        if ( !_Options.Quiet ) {
          var header = string.Format( "versioning compiled on {0}", VersionHelper.GetBuildDateTime() );
          writeLine( header );
        }
        if ( Directory.Exists( _Options.Folder ) ) {
          using ( var folderIterator = new FolderIterator( _Options ) ) {
            return folderIterator.Execute();
          }
        } else {
          writeError( "Folder provided does not exist" );
          return ERROR_NO_FOLDER;
        }
      } else {
        writeError( "Options could not be parsed" );
        return Parser.DefaultExitCodeFail;
      }
    }

    public Runner( string[] args ) {
      _Args = args;
    }

    public void Dispose() { }
  }
}
