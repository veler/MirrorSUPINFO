using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Media.SpeechRecognition;
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

        private SpeechRecognitionService()
        {
            _recognizer = new SpeechRecognizer();

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


        public void StartRecognition() => _recognizer.ContinuousRecognitionSession.StartAsync();

        public void StopRecognition() => _recognizer.ContinuousRecognitionSession.StopAsync();

        /// <summary>
        /// Ajoute un fichier de grammaire xml a la reconnaissance vocal
        /// </summary>
        /// <param name="filePath">chemin du fichier dans le projet</param>
        /// <returns>Si l'ajout et la compilation on était un succès</returns>
        public async Task<bool> AddGrammar(string filePath)
        {
            var grammarContentFile = await Package.Current.InstalledLocation.GetFileAsync(filePath);
            SpeechRecognitionGrammarFileConstraint grammarConstraint = new SpeechRecognitionGrammarFileConstraint(grammarContentFile);

            _recognizer.Constraints.Add(grammarConstraint);

            SpeechRecognitionCompilationResult compilationResult = await _recognizer.CompileConstraintsAsync();

            if (compilationResult.Status != SpeechRecognitionResultStatus.Success)
            {
                _recognizer.Constraints.Remove(grammarConstraint);
                return false;
            }
            return true;
        }

        #endregion
    }
}
