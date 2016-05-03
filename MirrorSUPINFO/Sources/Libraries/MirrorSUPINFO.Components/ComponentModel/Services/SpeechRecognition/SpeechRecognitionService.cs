using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Media.SpeechRecognition;
using MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.Grammar;
using MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.Grammar.Interfaces;
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

        private List<IMirrorGrammar> MirrorGrammars { get; set; }

        private SpeechRecognitionService(params string[] topics)
        {
            _recognizer = new SpeechRecognizer();
            MirrorGrammars = new List<IMirrorGrammar>();
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

        #endregion

        #region Methods


        public async Task CompileRecognition() => await _recognizer.CompileConstraintsAsync();

        public void StartRecognition() => _recognizer.ContinuousRecognitionSession.StartAsync();

        public void StopRecognition() => _recognizer.ContinuousRecognitionSession.StopAsync();


        public void AddGrammar(string filePath)
        {

            _recognizer.Constraints.Add(new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.Dictation, ""));

        }

        #endregion
    }
}
