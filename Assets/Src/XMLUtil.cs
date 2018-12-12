using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;


namespace MyXML
{

    public class XMLUtil
    {

        public bool isPrintMsg = false;

        XmlDocument xml;

        public bool XMLIsNull
        {
            get { return xml == null; }
        }

        public void SetXML(XmlDocument xml)
        {
            this.xml = xml;
        }

        public XMLUtil(string path, bool disMsg = false)
        {
            isPrintMsg = disMsg;

            LoadXML(path);
        }
        public bool IsEmpty()
        {
            return xml == null ? true : false;
        }


        /// <summary>
        /// 获取xml
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void LoadXML(string path)
        {
            if (File.Exists(path))
            {
                xml = new XmlDocument();
                try
                {

                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreComments = true;
                    XmlReader reader = XmlReader.Create(path, settings);//xml文件只读取未加注释的部分

                    xml.Load(reader);
                    //xml.Load(path);
                }
                catch
                {
                    if (isPrintMsg)
                    {
                        Debug.Log("文件加载失败");
                    }
                    xml = null;
                }
            }
            else
            {
                xml = null;
                if (isPrintMsg)
                {
                    Debug.Log(path + "文件不存在");
                }
            }
        }

        /// <summary>
        /// 获取XmlNode
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlNode GetNode(string nodePath)
        {
            XmlNode node = null;
            if (xml != null)
            {
                node = xml.SelectSingleNode(nodePath);
            }
            return node;
        }

        /// <summary>
        /// 获取XmlElement
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlElement GetElement(string nodePath)
        {
            XmlElement e = null;
            XmlNode node = GetNode(nodePath);
            if (node != null)
            {
                e = (XmlElement)node;
            }
            return e;
        }


        public string GetInnerValueStr(XmlElement e)
        {
            string result = null;
            if (e != null)
            {
                result = e.InnerText;
            }
            return result;
        }

        public string GetInnerValueStr(string nodePath)
        {
            XmlElement e = GetElement(nodePath);
            if (e == null && isPrintMsg)
            {
                Debug.Log(nodePath + "节点不存在");
            }
            return GetInnerValueStr(e);
        }


        public string GetAttributeValueStr(XmlElement e, string attributeName)
        {
            string result = null;
            if (e != null)
            {
                result = e.GetAttribute(attributeName);
            }
            return result;
        }

        public string GetAttributeValueStr(string nodePath, string attributeName)
        {
            XmlElement e = GetElement(nodePath);
            if (e == null && isPrintMsg)
            {
                Debug.Log(nodePath + "节点不存在");
            }
            return GetAttributeValueStr(e, attributeName);
        }

    }
}
