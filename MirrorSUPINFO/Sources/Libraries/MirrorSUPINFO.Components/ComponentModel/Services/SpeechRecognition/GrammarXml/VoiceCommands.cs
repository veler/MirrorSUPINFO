using System.Xml.Serialization;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.GrammarXml
{

    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/voicecommands/1.1")]
    [XmlRoot(Namespace = "http://schemas.microsoft.com/voicecommands/1.1", IsNullable = false)]
    public partial class VoiceCommands
    {
        /// <remarks/>
        public VoiceCommandsCommandSet CommandSet { get; set; }
    }
}