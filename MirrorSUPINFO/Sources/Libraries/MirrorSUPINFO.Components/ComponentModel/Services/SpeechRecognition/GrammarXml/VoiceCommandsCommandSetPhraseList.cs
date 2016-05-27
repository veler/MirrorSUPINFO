using System.Xml.Serialization;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.GrammarXml
{
    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/voicecommands/1.1")]
    public partial class VoiceCommandsCommandSetPhraseList
    {
        /// <remarks/>
        [XmlElement("Item")]
        public string[] Item { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Label { get; set; }
    }
}