using System.Xml.Serialization;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.GrammarXml
{
    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/voicecommands/1.1")]
    public partial class VoiceCommandsCommandSetCommand
    {
        /// <remarks/>
        public string Example { get; set; }

        /// <remarks/>
        [XmlElement("ListenFor")]
        public string[] ListenFor { get; set; }

        /// <remarks/>
        public string Feedback { get; set; }

        /// <remarks/>
        public string Navigate { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public bool IsSecondAnswer { get; set; }

    }
}