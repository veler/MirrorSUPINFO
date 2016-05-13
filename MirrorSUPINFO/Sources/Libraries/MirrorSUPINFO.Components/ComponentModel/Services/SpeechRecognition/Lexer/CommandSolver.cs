using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.Lexer
{
    public class CommandSolver
    {
        public string Name { get; set; }
        public Regex MatchRegex { get; set; }
        public string Navigation { get; set; }
        public string Answer { get; set; }
        public bool IsSecondAnwser { get; set; }
    }
}
