using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.ApplicationModel;
using Windows.Storage;
using MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.Grammar.Interfaces;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.Grammar
{
   public class MirrorFileGramar:IMirrorGrammar
    {
       public string Topic { get; set; }
       public string AppName { get; set; }
       public string StartValue { get; set; }
       public List<IMirrorGramarObject> MirrorGramarObjects { get; set; }

       public static async MirrorFileGramar DeSerialize(StorageFile grammarFile)
       {
            using (XmlReader reader = XmlReader.Create( await grammarFile.OpenStreamForReadAsync()))
            {
                DataContractSerializer formatter0 =
                    new DataContractSerializer(typeof(MirrorFileGramar));
                return (MirrorFileGramar)formatter0.ReadObject(reader);
            }
       }

       private  MirrorFileGramar(StorageFile grammarFile)
       {
       }
    }
}
