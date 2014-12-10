//*******************************************************************************************
// Exortec.VersionNumber - Program.cs
//*******************************************************************************************
// (c) 10-Dez-2014
//*******************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exortec.VersionNumber {
  class Program {
    static int Main( string[] args ) {
      int result = -100;
      using (var runner = new Runner(args) ) {
        result = runner.Execute();
      }
      return result;
    }
  }
}
