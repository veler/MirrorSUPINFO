using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.GrammarXml;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.Lexer
{
    public class RegexCreator
    {
        private readonly VoiceCommands _voiceCommand;

        private Dictionary<string, List<string>> _valuesList;

        public RegexCreator(VoiceCommands voiceCommand)
        {
            _voiceCommand = voiceCommand;
            _valuesList = new Dictionary<string, List<string>>();
            StartExtract();
        }

        private void StartExtract()
        {
            foreach (var phraseList in _voiceCommand.CommandSet.PhraseList)
            {
                _valuesList.Add(phraseList.Label, new List<string>(phraseList.Item.Select(i => i.Trim())));
            }

            Regexes = new Dictionary<string, CommandSolver>();
            foreach (var command in _voiceCommand.CommandSet.Command)
            {
                Regexes.Add(command.Name, new CommandSolver() { MatchRegex = new Regex(GenerateRegexForCommand(command)), Name = command.Name, Navigation = command.Navigate, Answer = command.Feedback });
            }
        }

        public Dictionary<string, CommandSolver> Regexes { get; set; }

        public string GenerateRegexForCommand(VoiceCommandsCommandSetCommand command)
        {
            for (int i = 0; i < command.ListenFor.Length; i++)
            {
                string baseString = ParseCrochet(command.ListenFor[i]);
                command.ListenFor[i] = $"({ParseAcolade(baseString)})";
            }
            return string.Join("|", command.ListenFor.Select(lf=>lf.Trim()));
        }

        private string ParseCrochet(string value)
        {
            return new Regex(@" ?\[(.*?)\]").Replace(value, "( $1)?");
        }

        private string ParseAcolade(string value)
        {
            var regexAcolade = new Regex(@"\{(.*?)\}");
            var result = regexAcolade.Matches(value);
            foreach (Match match in result)
            {
                if (_valuesList.ContainsKey(match.Groups[1].Value))
                {
                    value = value.Remove(match.Index, match.Length);
                    value = value.Insert(match.Index, $"({string.Join("|", _valuesList[match.Groups[1].Value])})");
                }
                else if (match.Groups[1].Value == "*")
                {
                    value = value.Remove(match.Index, match.Length);
                    value = value.Insert(match.Index, "(.*?)");
                }
            }
            return value;
        }


    }
}
