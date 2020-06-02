using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{

    public class dateToUser
    {
        
        public List<string> userList = new List<string>();
    }

    public class data
    {
        //public class dateInfo
        //{
        //    public string date="ddd";
        //    public string count="aaa";
        //    public string room = "0";
        //    public int callTick = 0;
        //}

        public List<user> userList = new List<user>();
        public class user
        {
            public string name;
            public string key;
            public List<string> dateList = new List<string>();

        }

        static public void savetest()
        {
            var d = new data();
            d.userList.Add(new user());
            d.userList[0].name = "tt";
            d.userList[0].dateList.Add("sdsd");

            data.save(d);
        }

        static public void save(data d)
        {
            XmlSerializer x = new XmlSerializer(typeof(data));
            //TextWriter writer = new StreamWriter(@"data.xml");
            TextWriter writer2 = new StreamWriter(@"data_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml");
            //x.Serialize(writer, d);
            x.Serialize(writer2, d);
        }

        static public data load()
        {

            //savetest();

            var x = new XmlSerializer(typeof(data));
            var r = new StreamReader(@"data.xml");
            return x.Deserialize(r) as data;
        }
    }
}
