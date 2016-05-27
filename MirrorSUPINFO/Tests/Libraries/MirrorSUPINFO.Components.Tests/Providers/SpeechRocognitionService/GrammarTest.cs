using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.GrammarXml;

namespace MirrorSUPINFO.Components.Tests.Providers.SpeechRocognitionService
{
    [TestClass]
    public class GrammarTest
    {
        [TestMethod]
        public async Task TestXmlDeserialization()
        {
            //j'arrive pas a executer les test je sais pas d'ou ca viens
            var grammarFile = await Package.Current.InstalledLocation.GetFileAsync("voiceGramarTestFile.xml");
            var result = await Xml<VoiceCommands>.Deserialize(grammarFile);
            Assert.IsNotNull(result);
        }
    }
}
