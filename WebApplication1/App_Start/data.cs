using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;


namespace WebApplication1.App_Start
{
    
    public class data
    {
        public List<user> userList = new List<user>();
        public class user
        {
            public string name;
            public List<string> dateList = new List<string>();

        }

        static public void savetest()
        {
            var d = new data();
            d.userList.Add(new user());
            d.userList[0].name = "tt";
            d.userList[0].dateList.Add("dsfd");

            data.save(d);
        }

        static public void save(data d)
        {
            XmlSerializer x = new XmlSerializer(typeof(data));
            TextWriter writer = new StreamWriter(@"d:/data.xml");
            TextWriter writer2 = new StreamWriter(@"d:/data_"+DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml");
            x.Serialize(writer, d);
            x.Serialize(writer2, d);
        }

        static public data load()
        {
            var x = new XmlSerializer(typeof(data));
            var r = new StreamReader(@"c:/data.xml");
            return x.Deserialize(r) as data;
        }
    }
}