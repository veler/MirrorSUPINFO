using System.Collections.Generic;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.Grammar.Interfaces
{
    public interface IMirrorGrammar
    {
        string Topic { get; set; }
        string AppName { get; set; }
        string StartValue { get; set; }
        List<IMirrorGramarObject> MirrorGramarObjects { get; set; }

    }
}