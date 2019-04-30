using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace AppConfigOper
{
    /*利用Xml方法读写App.Config
     */
    class XmlOper
    {
        /// <summary>
        /// 读取App.Config中的结点
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string ReadAppSettings(string strKey)
        {
            var doc = new XmlDocument();
            //获得配置文件的全路径  
            var strFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            doc.Load(strFileName);

            var appSettingNode = doc.GetElementsByTagName("appSettings");

            if (appSettingNode.Count > 0)
            {
                //找出名称为“add”的所有元素  
                var nodes = appSettingNode[0].ChildNodes;
                for (int i = 0; i < nodes.Count; i++)
                {
                    //获得将当前元素的key属性  
                    var xmlAttributeCollection = nodes[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        var att = xmlAttributeCollection["key"];
                        if (att.Value == strKey)
                        {
                            return xmlAttributeCollection["value"].Value;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 修改App.Config中的结点
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="value"></param>
        public static void ModifyOrAddAppSettings(string strKey, string value)
        {
            var doc = new XmlDocument();
            //获得配置文件的全路径  
            var strFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            doc.Load(strFileName);

            var appSettingNode = doc.GetElementsByTagName("appSettings");
            var notFind = true;
            //修改元素
            if (appSettingNode.Count > 0)
            {
                //找出名称为“add”的所有元素  
                var nodes = appSettingNode[0].ChildNodes;

                for (int i = 0; i < nodes.Count; i++)
                {
                    //获得将当前元素的key属性  
                    var xmlAttributeCollection = nodes[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        var att = xmlAttributeCollection["key"];

                        if (att.Value == strKey)
                        {
                            //对目标元素中的第二个属性赋值  
                            att = xmlAttributeCollection["value"];
                            att.Value = value;
                            notFind = false;
                            break;
                        }

                    }
                }
            }
            //添加元素
            if (notFind)
            {
                XmlElement xn = doc.CreateElement("add");
                xn.SetAttribute("key", strKey);
                xn.SetAttribute("value", value);
                appSettingNode[0].AppendChild(xn);
            }

            //保存上面的修改  
            doc.Save(strFileName);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
