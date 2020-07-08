using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME
    {
        public short wYear;
        public short wMonth;
        public short wDayOfWeek;
        public short wDay;
        public short wHour;
        public short wMinute;
        public short wSecond;
        public short wMilliseconds;
    }

   

    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);

        public class dateInfo
        {
            public string date;
            public string count;
            public string room = "";
            public int callTick = 0;
        }

        Dictionary<string, dateInfo> _dateList = new Dictionary<string, dateInfo>();
        Dictionary<string, dateToUser> _date2UserList = new Dictionary<string, dateToUser>();
        
        public Form1()
        {
            InitializeComponent();
        }

        data _data;
        SYSTEMTIME _st = new SYSTEMTIME();

        private void Form1_Load(object sender, EventArgs e)
        {

            _st.wYear = 2017; // must be short
            _st.wMonth = 7;
            _st.wDay = 10;
            _st.wHour = 0;
            _st.wMinute = 0;
            _st.wSecond = 0;


            webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            webBrowser1.Navigate("http://forest.maketicket.co.kr/ticket/GD41");
            _data = data.load();

            foreach (var item in _data.userList)
            {
                dateToUser val;
                foreach (var item2 in item.dateList)
                {
                    if (!_date2UserList.TryGetValue(item2, out val))
                    {
                        val = new dateToUser();
                        _date2UserList.Add(item2, val);
                    }

                    val.userList.Add(item.key);

                }
            }
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Console.WriteLine("WebBrowser1_DocumentCompleted");
        }

        public static object[] ExtractFormatParameters(string sourceString, string formatString)
        {
            Regex placeHolderRegex = new Regex(@"\{(\d+)\}");
            Regex formatRegex = new Regex(placeHolderRegex.Replace(formatString, m => "(<" + m.Groups[1].Value + ">.*?)"));
            Match match = formatRegex.Match(sourceString);
            if (match.Success)
            {
                var output = new object[match.Groups.Count - 1];
                for (int i = 0; i < output.Length; i++)
                    output[i] = match.Groups[i + 1].Value;
                return output;
            }
            return new object[] { };
        }
        private static readonly HttpClient client = new HttpClient();
        int state = 0;
        private async void timer1_Tick(object sender, EventArgs e)
        {
            //var form = new Form2();
            //form.Show(this);

            

            try
            {


            //webBrowser1.Document.InvokeScript("calendar_move('20020706')");
                //webBrowser1.Document.InvokeScript("javascript:calendar_move('20200706')");
              
                
                //    var r = webBrowser1.Document.GetElementsByTagName("HTML");

                //var a = r[0].OuterHtml;

                

                //var sr = new System.IO.StreamReader(webBrowser1.DocumentStream);

                //_dateList.Clear();
                
                //webBrowser1.Document.InvokeScript("calendar_move(\"20020706\")");
                //doclist.Add(webBrowser1.DocumentText);
                //while (sr.EndOfStream)

                switch(state)
                {
                    case 0:
                        state = 1;
                        break;
                    case 1:
                        var list = webBrowser1.Document.GetElementsByTagName("a");
                        foreach (var item in list)
                        {
                            var aa = item as HtmlElement;
                            if (aa != null && aa.InnerText == "다음달")
                                aa.InvokeMember("Click");
                        }
                        state = 2;
                        return;
                    case 2:
                        state = 0;
                        break;

                }

                foreach (var line in webBrowser1.DocumentText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {

                    if (line.Contains("f_SelectDateZone") == false)
                        continue;

                    var objList = line.Split(new string[] { "line = ","zero", "href='javascript:f_SelectDateZone(", "\"", " ", ",", ");'" }, StringSplitOptions.RemoveEmptyEntries);
                    //var objList = ExtractFormatParameters(line2, "f_SelectDateZone( {0}, {1}, {2} , {3} , {4} )");

                    if (objList.Count() < 10)
                        continue;

                    if (objList[0] != "<LI")
                        continue;


                    dateInfo d = new dateInfo();
                    d.date = objList[5];
                    d.room = objList[8];
                    d.count = objList[9];
                    
                    // 모두가능 저장
                    if (_dateList.Keys.Contains(d.date) == false )
                    {
                        _dateList[d.date] = d;
                    }
                    //특정룸 저장
                    if (_dateList.Keys.Contains(d.date+d.room) == false)
                    {
                        _dateList[d.date + d.room] = d;
                    }

                    if( d.count == "0")
                    {
                        continue;
                    }

                    if (listBox1.Items.Count > 1000)
                    {
                        listBox1.Items.Clear();
                    }

                    if ((Environment.TickCount - _dateList[d.date].callTick) < 60 * 1000 * 10)
                        continue;

                    string dd = string.Empty;

                    if (_date2UserList.Keys.Contains(d.date) == true)
                    {
                        dd = d.date;
                    }

                    if (_date2UserList.Keys.Contains(d.date + d.room) == true)
                    {
                        dd = d.date + d.room;
                    }

                    if (dd == string.Empty)
                        continue;

                    var du = _date2UserList[dd];

                    if( checkBox1.Checked)
                        Process.Start("IExplore.exe", "http://forest.maketicket.co.kr/ticket/GD41");

                    foreach (var item in du.userList)
                    {

                        _dateList[d.date].callTick = Environment.TickCount;



                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", item);

                        var content3 = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string, string>("message", d.date),
                                new KeyValuePair<string, string>("message", "http://forest.maketicket.co.kr/ticket/GD41")
                            });

                        var response = await client.PostAsync("https://notify-api.line.me/api/notify", content3);
                        var responseString = await response.Content.ReadAsStringAsync();
                    }


                }

                listBox1.Items.Clear();
                foreach (var item in _dateList)
                {
                    listBox1.Items.Add(item);
                }

                //if (_st.wMonth == 6)
                //    _st.wMonth = 7;
                //else _st.wMonth = 6;

                //SetSystemTime(ref _st); // invoke this method.

                if (listBox2.Items.Count > 1000)
                    listBox2.Items.Clear();
                listBox2.Items.Insert(0, Environment.TickCount.ToString());

                webBrowser1.Navigate("http://forest.maketicket.co.kr/ticket/GD41");
            }
            catch (Exception ex)
            {

                
            }


        }
    }
}
