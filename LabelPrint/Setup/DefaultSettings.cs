using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LabelPrint.Setup
{
    public static class DefaultSettings
    {
        private static string path = @"Settings\Options.xml";
        private static string nameNode = "options";

        public static void Initi()
        {
            bool existFile = File.Exists(path);
            if (!existFile)
            {
                XDocument doc = new XDocument(new XElement("Settings"));
                doc.Save(path);
            }
        }
        public static string Get(string nameOption)
        {
            string result = string.Empty;
            bool exists = File.Exists(path);
            if (exists)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
                XmlElement xRoot = xDoc.DocumentElement;
                if (xRoot.HasChildNodes)
                {
                    XmlNodeList childnodes = xRoot.ChildNodes;
                    foreach (XmlNode node in childnodes)
                    {
                        foreach (XmlNode attribute in node.Attributes)
                            if (attribute.LocalName.Equals(nameOption))
                                return attribute.InnerText;
                    }
                }
            }
            return result;
        }
        public static void Set(string name, string value)
        {
            bool changed = false;
            bool exist = false;
            Initi();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            if (xRoot.HasChildNodes)
            {
                XmlNodeList childnodes = xRoot.ChildNodes;
                foreach (XmlNode node in childnodes)
                {
                    foreach (XmlNode attribute in node.Attributes)
                        if (attribute.LocalName.Equals(name))
                        {
                            if (!attribute.InnerText.Equals(value))
                            {
                                attribute.InnerText = value;
                                changed = true;
                            }
                            exist = true;
                        }
                }
                if (changed)
                    xDoc.Save(path);
                else if(!exist)
                    AddNode(name, value);

            }
            else
            {
                AddNode(name, value);
            }

        }
        private static void AddNode(string name, string value)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement userElem = xDoc.CreateElement(nameNode);
            XmlAttribute nameAttr = xDoc.CreateAttribute(name);
            XmlText nameText = xDoc.CreateTextNode(value);
            nameAttr.AppendChild(nameText);
            userElem.Attributes.Append(nameAttr);
            xRoot.AppendChild(userElem);
            xDoc.Save(path);
        }
    }
}
