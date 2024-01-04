using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace _148075._148159.PhonesCatalog.DBFile
{
    internal class Serializer
    {
        public static void Serialize<T>(string fileName, List<T> producers)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(List<T>));
                serializer.WriteObject(fs, producers);
            }
        }

        public static List<T> Deserialize<T>(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(List<T>));
                return (List<T>)serializer.ReadObject(fs);
            }
        }
    }
}