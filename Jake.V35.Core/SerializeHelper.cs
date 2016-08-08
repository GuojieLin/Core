using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Jake.V35.Core
{
    public static class SerializeHelper
    {
        private static readonly Encoding DefalutEnocding = Encoding.UTF8;
        /// <summary>
        /// 序列化为XmlString
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string SerializeToXmlString<T>(this object entity,Encoding encoding = null)
        {
            if (encoding == null) encoding = DefalutEnocding;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings {Encoding = encoding};
                using (XmlWriter writer = XmlWriter.Create(ms, settings))
                {
                    //去除默认命名空间xmlns:xsd和xmlns:xsi
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    xmlSerializer.Serialize(writer, entity, ns);
                }
                return encoding.GetString(ms.GetBuffer());
            }
        }

        /// <summary>
        /// XML String 反序列化成对象
        /// </summary>
        public static T DeserializeToEntityFromXmlString<T>(string xmlString,Encoding encoding = null)
        {
            if (encoding == null) encoding = DefalutEnocding;
            T t;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream xmlStream = new MemoryStream(encoding.GetBytes(xmlString)))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    Object obj = xmlSerializer.Deserialize(xmlReader);
                    t = (T) obj;
                }
            }
            return t;
        }
        /// <summary>
        /// 序列化成Xml文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void SerializeToXmlFile<T>(this object entity, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(fs, entity);
            }
        }

        /// <summary>
        /// 序列化成Xml文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T DeserializeToEntityFromXmlFile<T>(string filePath)
        {
            T t;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                // Deserialize 
                t = (T)serializer.Deserialize(fs);
            }
            return t;
        }
        public static object DeserializeToEntityFromXmlFile(string filePath,Type type)
        {
            if (type == null) throw new Exception("类型为空");
            using (FileStream fs = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(type);
                // Deserialize 
                object o  = serializer.Deserialize(fs);
                return o;
            }
        }

        public static byte[] SerializeToBytes<T>(T entity)
        {
            using (MemoryStream memorystream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(memorystream, entity);
                return memorystream.ToArray();
            }
        }
        public static byte[] SerializeToBytes(this object entity)
        {
            using (MemoryStream memorystream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(memorystream, entity);
                return memorystream.ToArray();
            }
        }
        public static T DeserializeFromBytes<T>(byte[] bytes)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(bytes))
            {
                return (T)formatter.Deserialize(stream);
            }
        }

        
    }

}