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
        public const string ASTERIX_REGEX_GROUP_NAME = "all";

        private readonly VoiceCommands _voiceCommand;

        private Dictionary<string, List<string>> _valuesList;
        public List<CommandSolver> Regexes { get; set; }

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

            Regexes = new List<CommandSolver>();
            foreach (var command in _voiceCommand.CommandSet.Command)
            {
                Regexes.Add(new CommandSolver { MatchRegex = new Regex(GenerateRegexForCommand(command), RegexOptions.IgnoreCase), Name = command.Name, Navigation = command.Navigate, Answer = command.Feedback });
            }
        }


        public string GenerateRegexForCommand(VoiceCommandsCommandSetCommand command)
        {
            for (int i = 0; i < command.ListenFor.Length; i++)
            {
                string baseString = ParseCrochet(command.ListenFor[i].Trim());
                command.ListenFor[i] = $"({ParseAcolade(baseString)})";
            }
            return string.Join("|", command.ListenFor.Select(lf => lf.Trim()));
        }

        private string ParseCrochet(string value)
        {
            return new Regex(@" ?\[(.*?)\]").Replace(value, "( $1)?");
        }

        private string ParseAcolade(string value)
        {
            var regexAcolade = new Regex(@"\{(.*?)\}");
            var result = regexAcolade.Matches(value);
            var groupNameIndex = new Dictionary<string, int>();
            foreach (Match match in result)
            {
                if (_valuesList.ContainsKey(match.Groups[1].Value))
                {
                    value = value.Remove(match.Index, match.Length);
                    value = value.Insert(match.Index, $"(?<{GenerateGroupName(groupNameIndex, match.Value)}>{string.Join("|", _valuesList[match.Groups[1].Value])})");
                }
                else if (match.Groups[1].Value == "*")
                {
                    value = value.Remove(match.Index, match.Length);
                    value = value.Insert(match.Index, $"(?<{GenerateGroupName(groupNameIndex, ASTERIX_REGEX_GROUP_NAME)}>.*?)");
                }
            }
            return value;
        }

        private string GenerateGroupName(Dictionary<string, int> groupNameIndex, string name)
        {
            if (groupNameIndex.ContainsKey(name))
            {
                groupNameIndex[name]++;
            }
            else
            {
                groupNameIndex.Add(name, 0);
            }
            return $"{name}[{groupNameIndex[name]}]";
        }
    }
}
