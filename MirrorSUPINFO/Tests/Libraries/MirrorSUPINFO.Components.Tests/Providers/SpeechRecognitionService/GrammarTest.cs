using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.GrammarXml;

namespace MirrorSUPINFO.Components.Tests.Providers.SpeechRecognitionService
{
    [TestClass]
    public class GrammarTest
    {
        [TestMethod]
        public async Task TestXmlDeserialization()
        {
            try
            {
                //j'arrive pas a executer les test je sais pas d'ou ca viens
                var grammarFile = await Package.Current.InstalledLocation.GetFileAsync(@"Providers\SpeechRecognitionService\voiceGramarTestFile.xml");
                var result = await Xml<VoiceCommands>.Deserialize(grammarFile);
                Assert.IsNotNull(result);
                Assert.AreEqual(2,result.CommandSet.Command.Length);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public async Task TestVoixEnDureVariableDeListe()
        {
            try
            {
                //j'arrive pas a executer les test je sais pas d'ou ca viens
                var speechService = ComponentModel.Services.SpeechRecognition.SpeechRecognitionService.GetService();
               speechService.AddGrammarFile(await Package.Current.InstalledLocation.GetFileAsync(@"Providers\SpeechRecognitionService\voiceGramarTestFile.xml"));
              var result =  speechService.FindCommand("search trip to london");
                    Assert.AreEqual(result.ResultGroups["destination0"],"london");
                    Assert.AreEqual(result.CommandSolver.Name, "searchDestination");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public async Task TestVoixEnDureVariableInconue()
        {
            try
            {
                //j'arrive pas a executer les test je sais pas d'ou ca viens
                var speechService = ComponentModel.Services.SpeechRecognition.SpeechRecognitionService.GetService();
                speechService.AddGrammarFile(await Package.Current.InstalledLocation.GetFileAsync(@"Providers\SpeechRecognitionService\voiceGramarTestFile.xml"));
                var result = speechService.FindCommand("search trip to ville inconue");
                Assert.AreEqual(result.ResultGroups["all0"], "ville inconue");
                Assert.AreEqual(result.CommandSolver.Name, "searchDestination");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

    }
}
