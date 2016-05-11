using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.SpeechRecognition;
using Windows.Storage;

namespace MirrorSUPINFO.SDK.Tools
{
    public interface ISpeechRecognition
    {
        void StartRecognition();
        void StopRecognition();

        void AddGrammarFile(StorageFile grammarFile);

        #region events

        event TypedEventHandler<SpeechRecognizer, SpeechRecognitionHypothesisGeneratedEventArgs>
            RecognizerHypothesisGenerated;

        event TypedEventHandler<SpeechRecognizer, SpeechRecognizerStateChangedEventArgs> RecognizerStateChanged;

        event TypedEventHandler<SpeechContinuousRecognitionSession, SpeechContinuousRecognitionResultGeneratedEventArgs> RecognitionResultGenerated;
        
        #endregion
    }
}
