using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace DataExport
{
	// Token: 0x0200000E RID: 14
	public class TableAssociationUtil
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00006DE8 File Offset: 0x00004FE8
		public static Dictionary<string, List<TableAssociationInfo>> GetAllAssociation()
		{
			string text = Path.Combine(Path.GetDirectoryName(typeof(TableAssociationUtil).Assembly.Location), "Config\\tableAssociations.xml");
			Dictionary<string, List<TableAssociationInfo>> dictionary = new Dictionary<string, List<TableAssociationInfo>>(StringComparer.OrdinalIgnoreCase);
			bool flag = !File.Exists(text);
			Dictionary<string, List<TableAssociationInfo>> result;
			if (flag)
			{
				result = dictionary;
			}
			else
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(text);
				XmlNode documentElement = xmlDocument.DocumentElement;
				XmlNodeList childNodes = documentElement.ChildNodes;
				XmlNodeList xmlNodeList = null;
				foreach (object obj in childNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string name = xmlNode.Name;
					bool flag2 = name.ToUpper().Equals("tableAssociations".ToUpper());
					if (flag2)
					{
						xmlNodeList = xmlNode.ChildNodes;
						break;
					}
				}
				bool flag3 = xmlNodeList == null;
				if (flag3)
				{
					result = dictionary;
				}
				else
				{
					foreach (object obj2 in xmlNodeList)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						string key = null;
						try
						{
							key = xmlNode2.Attributes["tableName"].Value;
						}
						catch
						{
							throw new Exception(xmlNode2.ToString());
						}
						List<TableAssociationInfo> list = new List<TableAssociationInfo>();
						foreach (object obj3 in xmlNode2.ChildNodes)
						{
							XmlNode xmlNode3 = (XmlNode)obj3;
							TableAssociationInfo tableAssociationInfo = new TableAssociationInfo();
							tableAssociationInfo.Name = xmlNode3.Attributes["tableName"].Value;
							tableAssociationInfo.WhereCondition = xmlNode3.Attributes["whereCondition"].Value;
							try
							{
								tableAssociationInfo.SqlCondition = xmlNode3.Attributes["sqlCondition"].Value;
							}
							catch
							{
							}
							list.Add(tableAssociationInfo);
						}
						dictionary.Add(key, list);
					}
					result = dictionary;
				}
			}
			return result;
		}
	}
}
