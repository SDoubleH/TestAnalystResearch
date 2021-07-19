using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace ReadXmlAsObject
{
    class Program
    {
        static void Main(string[] args)
        {
            using var xmlFile = HttpGet(@"http://xa-at-sys/TestReport/Common/TestResult_2021-07-19 06-29-274562.xml");
            XmlDocument document = new XmlDocument();
            document.Load(xmlFile);
            XmlNodeList nodeList = document.SelectNodes("//control");
            List<XmlNode> failedList = new List<XmlNode>();
            foreach (XmlNode node in nodeList)
            {
                var failed = node.Attributes.GetNamedItem("failedcases");
                if (failed != null && failed.Value != "0")
                {
                    failedList.Add(node);
                }
            }
        }

        public static StreamReader HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //设置发送请求的类型
            request.ContentType = "text/html;charset=UTF-8";// "application/json";
            //设置请求超时时间
            request.Timeout = 60 * 1000;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return new StreamReader(response.GetResponseStream(), Encoding.UTF8);
        }
    }
}
