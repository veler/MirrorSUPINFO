using System.Collections.Generic;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.Lexer
{
   public class VoiceRecognitionResult
    {
       public CommandSolver CommandSolver { get; set; }
       public Dictionary<string,string> ResultGroups { get; set; }
    }
}
