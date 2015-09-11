namespace ZoomLa.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    public sealed class Serialize<T>
    {
        public T DeserializeField(string xmlField)
        {
            T local = default(T);
            if (!string.IsNullOrEmpty(xmlField))
            {
                try
                {
                    TextReader textReader = new StringReader(xmlField);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    local = (T)serializer.Deserialize(textReader);
                    textReader.Close();
                    return local;
                }
                catch (InvalidOperationException)
                {
                    return default(T);
                }
            }
            return default(T);
        }

        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<T> DeserializeFieldList(string xmlFieldList)
        {
            List<T> list = new List<T>();
            if (!string.IsNullOrEmpty(xmlFieldList))
            {
                TextReader textReader = new StringReader(xmlFieldList);
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                list = (List<T>)serializer.Deserialize(textReader);
                textReader.Close();
            }
            return list;
        }

        public string SerializeField(T t)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringWriter textWriter = new StringWriter(new StringBuilder());
            serializer.Serialize(textWriter, t, namespaces);
            string str = textWriter.ToString();
            textWriter.Close();
            return str;
        }

        public string SerializeFieldList(IList<T> list)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            StringWriter textWriter = new StringWriter(new StringBuilder());
            serializer.Serialize(textWriter, list, namespaces);
            string str = textWriter.ToString();
            textWriter.Close();
            return str;
        }
    }
}