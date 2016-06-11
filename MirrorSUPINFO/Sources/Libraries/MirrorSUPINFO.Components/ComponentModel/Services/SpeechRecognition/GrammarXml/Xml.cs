using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Storage;

namespace MirrorSUPINFO.Components.ComponentModel.Services.SpeechRecognition.GrammarXml
{
    public static class Xml<T>
    {
        public static StorageFile Serialize(T obj)
        {
            throw new NotImplementedException();
        }

        public static async Task<T> Deserialize(StorageFile grammarFile)
        {
            using (XmlReader reader = XmlReader.Create(await grammarFile.OpenStreamForReadAsync()))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                return (T)formatter.Deserialize(reader);
            }
        }
    }
}
