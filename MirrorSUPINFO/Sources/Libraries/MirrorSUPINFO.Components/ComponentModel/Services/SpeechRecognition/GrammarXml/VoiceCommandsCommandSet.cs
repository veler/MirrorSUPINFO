using System.Xml.Schema;
using System.Xml.Serialization;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.GrammarXml
{
    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/voicecommands/1.1")]
    public partial class VoiceCommandsCommandSet
    {
        /// <remarks/>
        public string CommandPrefix { get; set; }

        /// <remarks/>
        public string Example { get; set; }

        /// <remarks/>
        [XmlElement("Command")]
        public VoiceCommandsCommandSetCommand[] Command { get; set; }

        /// <remarks/>
        [XmlElement("PhraseList")]
        public VoiceCommandsCommandSetPhraseList[] PhraseList { get; set; }

        /// <remarks/>
        [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Name { get; set; }
    }
}