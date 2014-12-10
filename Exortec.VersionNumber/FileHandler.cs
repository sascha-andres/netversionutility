using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exortec.VersionNumber {
  internal class FileHandler : IDisposable {
    private readonly string _File = string.Empty;
    private readonly CommandLineOptions _Options = null;
    private readonly System.Text.RegularExpressions.Regex _AssemblyVersion = new System.Text.RegularExpressions.Regex(@"^\[assembly:\s*AssemblyVersion\s*\(\s*""(?<major>\d+)\.(?<minor>\d+)");
    private readonly System.Text.RegularExpressions.Regex _AssemblyFileVersion = new System.Text.RegularExpressions.Regex( @"^\[assembly:\s*AssemblyFileVersion\s*\(\s*""(?<major>\d+)\.(?<minor>\d+)" );

    public int Execute() {
      var contentBuilder = new System.Text.StringBuilder();
      var contentNeedsToBeWritten = false;
      using ( var stream = System.IO.File.OpenRead( _File ) ) {
        using ( var textReader = new System.IO.StreamReader( stream ) ) {
          var line = string.Empty;
          while ( null != (line = textReader.ReadLine()) ) {
            if ( _Options.AssemblyFileVersion && _AssemblyFileVersion.IsMatch( line.Trim() ) ) {
              var match = _AssemblyFileVersion.Match( line.Trim() );
              if ( match.Success ) {
                var major = match.Groups["major"].Value;
                var minor = match.Groups["minor"].Value;
                line = string.Format( @"[assembly: AssemblyFileVersion( ""{0}.{1}.{2}.{3}"" )]", major, minor, DateTime.Now.Year, string.Format( "{0}{1}", DateTime.Now.Month, DateTime.Now.Day ) );
                contentNeedsToBeWritten = true;
              }
            } else if ( _Options.AssemblyVersion && _AssemblyVersion.IsMatch( line.Trim() ) ) {
              var match = _AssemblyVersion.Match( line.Trim() );
              if ( match.Success ) {
                var major = match.Groups["major"].Value;
                var minor = match.Groups["minor"].Value;
                line = string.Format( @"[assembly: AssemblyVersion( ""{0}.{1}.{2}.{3}"" )]", major, minor, DateTime.Now.Year, string.Format( "{0}{1}", DateTime.Now.Month, DateTime.Now.Day ) );
                contentNeedsToBeWritten = true;
              }
            }
            contentBuilder.AppendLine( line );
          }
        }
      }
      if ( contentNeedsToBeWritten ) {
        System.IO.File.WriteAllText( _File, contentBuilder.ToString() );
      }
      return Runner.SUCCESS_EXECUTION;
    }

    public FileHandler( CommandLineOptions options, string file ) {
      _Options = options;
      _File = file;
    }

    public void Dispose() {
    }
  }
}
