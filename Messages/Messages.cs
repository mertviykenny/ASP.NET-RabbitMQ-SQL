using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Messages
{
    [Serializable]
    public class Messages 
    {
        public string name ;
        public string value;
        public Messages(string n, string v)
        {
            this.name = n;
            this.value = v;
        }
        public static byte[] SerializeToByte(Messages m)
        {
            IFormatter formatter = new BinaryFormatter();
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, m);
                bytes = ms.ToArray();
            }
            return bytes;
        }
        public static Messages DeserializeFromByte(byte[] bytes)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(bytes))
            {
                return formatter.Deserialize(ms) as Messages;
            }
        }
    }
    [Serializable]
    public class User
    {
        public string username;
        public string password;
        public string email;
    }
}
