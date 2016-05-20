using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Media.SpeechRecognition;
using Windows.Storage;
using MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.GrammarXml;
using MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.Lexer;
using MirrorSUPINFO.SDK.Tools;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition
{
    public class SpeechRecognitionService : ISpeechRecognition
    {
        #region static methods

        private static SpeechRecognitionService _speechRecognition;
        public static SpeechRecognitionService GetService() => _speechRecognition ?? (_speechRecognition = new SpeechRecognitionService());

        #endregion

        private readonly SpeechRecognizer _recognizer;
        public SpeechRecognizer Recognizer => _recognizer;

        private readonly List<VoiceCommands> _gramarList = new List<VoiceCommands>();
        private readonly List<CommandSolver> _commandSolvers = new List<CommandSolver>();

        private SpeechRecognitionService()
        {
            _recognizer = new SpeechRecognizer();
            _recognizer.Constraints.Add(new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.WebSearch,
                "webSearch"));
            _recognizer.CompileConstraintsAsync().AsTask().Wait();
            _recognizer.ContinuousRecognitionSession.ResultGenerated += RecognitionFound;
        }


        #region event handlers

        /// <summary>
        /// Lancer a la fin de la dicté depar l'utilisateur
        /// </summary>
        public event TypedEventHandler<SpeechContinuousRecognitionSession, SpeechContinuousRecognitionResultGeneratedEventArgs>
            RecognitionResultGenerated
        {
            add { _recognizer.ContinuousRecognitionSession.ResultGenerated += value; }
            remove { _recognizer.ContinuousRecognitionSession.ResultGenerated -= value; }
        }

        /// <summary>
        /// lancer lors des changement de status du speach recognizer
        /// </summary>
        public event TypedEventHandler<SpeechRecognizer, SpeechRecognizerStateChangedEventArgs> RecognizerStateChanged
        {
            add { _recognizer.StateChanged += value; }
            remove { _recognizer.StateChanged -= value; }
        }

        /// <summary>
        /// Lancer lors d'une hypothese de dicté intermédiaire
        /// </summary>
        public event TypedEventHandler<SpeechRecognizer, SpeechRecognitionHypothesisGeneratedEventArgs> RecognizerHypothesisGenerated
        {
            add { _recognizer.HypothesisGenerated += value; }
            remove { _recognizer.HypothesisGenerated -= value; }
        }

        public event TypedEventHandler<SpeechRecognitionResult, VoiceRecognitionResult> RecognitionCommandFound;

        #endregion

        #region Methods


        public async Task CompileRecognition() => await _recognizer.CompileConstraintsAsync();

        public void StartRecognition() => _recognizer.ContinuousRecognitionSession.StartAsync();

        public void StopRecognition() => _recognizer.ContinuousRecognitionSession.StopAsync();

        /// <summary>
        /// Ajouter un fichier de grammaire a la reconnaissance
        /// </summary>
        /// <param name="grammarFile"></param>
        public async void AddGrammarFile(StorageFile grammarFile)
        {
            var grammar = await Xml<VoiceCommands>.Deserialize(grammarFile);
            _gramarList.Add(grammar);
            RegexCreator creator = new RegexCreator(grammar);
            _commandSolvers.AddRange(creator.Regexes);
        }

        /// <summary>
        /// event de reconnaissance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void RecognitionFound(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            if (args.Result.Status == SpeechRecognitionResultStatus.Success)
            {
                var command = FindCommand(args.Result.Text);
                OnRecognitionCommandFound(args.Result, command);
            }
        }

        /// <summary>
        /// Trouve la commande correspondant au text
        /// </summary>
        /// <param name="text"></param>
        /// <returns>null if not found</returns>
        public VoiceRecognitionResult FindCommand(string text)
        {
            foreach (var commandSolver in _commandSolvers)
            {
                var match = commandSolver.MatchRegex.Match(text);
                if (match.Success)
                {
                    var result = new Dictionary<string, string>();
                    foreach (var groupName in commandSolver.MatchRegex.GetGroupNames())
                    {
                        result.Add(groupName,match.Groups[groupName].Value);
                    }
                    return new VoiceRecognitionResult()
                    {
                        CommandSolver = commandSolver,
                        ResultGroups = result
                    };
                }
            }
            return null;
        }

        protected virtual void OnRecognitionCommandFound(SpeechRecognitionResult sender, VoiceRecognitionResult args)
        {
            RecognitionCommandFound?.Invoke(sender, args);
        }

        #endregion
    }
}
