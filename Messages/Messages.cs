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
    public class Message
    {
        public string name ;
        public string value;
        public Message(string n, string v)
        {
            this.name = n;
            this.value = v;
        }
        public static byte[] SerializeToByte(Message m)
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
        public static Message DeserializeFromByte(byte[] bytes)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(bytes))
            {
                return formatter.Deserialize(ms) as Message;
            }
        }
    }
    [Serializable]
    public class User
    {
        public string username;
        public string password;

        public static byte[] SerializeToByte(User u)
        {
            IFormatter formatter = new BinaryFormatter();
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, u);
                bytes = ms.ToArray();
            }
            return bytes;
        }
        public static User DeserializeFromByte(byte[] bytes)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(bytes))
            {
                return formatter.Deserialize(ms) as User;
            }
        }
    }
}
