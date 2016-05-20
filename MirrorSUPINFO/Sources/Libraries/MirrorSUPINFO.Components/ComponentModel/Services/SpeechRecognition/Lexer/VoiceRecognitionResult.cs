using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.Lexer
{
   public class VoiceRecognitionResult
    {
       public CommandSolver CommandSolver { get; set; }
       public Dictionary<string,string> ResultGroups { get; set; }
    }
}
