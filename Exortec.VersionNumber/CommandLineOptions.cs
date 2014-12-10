//*******************************************************************************************
// Exortec.VersionNumber - CommandLineOptions.cs
//*******************************************************************************************
// (c) 10-Dez-2014
//*******************************************************************************************

using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exortec.VersionNumber {
  class CommandLineOptions {
    [Option( "assemblyfileversion", Required = false, HelpText = "Change assembly file version", DefaultValue = true )]
    public bool AssemblyFileVersion { get; set; }
    [Option( "assemblyversion", Required = false, HelpText = "Change assembly version", DefaultValue = true )]
    public bool AssemblyVersion { get; set; }
    [Option( 'f', "folder", Required = true, HelpText = "Look here (recursivly) for AssemblyInfo.cs" )]
    public string Folder { get; set; }
    [HelpOption]
    public string GetUsage() {
      return HelpText.AutoBuild( this,
        ( HelpText current ) => HelpText.DefaultParsingErrorsHandler( this, current ) );
    }
    [ParserState]
    public IParserState LastParserState { get; set; }
  }
}
