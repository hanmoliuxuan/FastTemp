using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
namespace 体温填报
{
    public partial class Form1 : Form
    {
        string path = Application.StartupPath + @"\1.txt";
        bool isopen = false;
        public Form1()
        {
            InitializeComponent();
            FileStream fs = File.Open(path, FileMode.OpenOrCreate);
            fs.Close();
            LoadInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isopen)
            {
                HtmlElement input_text = null;
                HtmlElementCollection input_list = webBrowser1.Document.GetElementsByTagName("input");
                input_text = input_list[1];
                input_text.SetAttribute("value", temp.Text);

                HtmlElementCollection button_list = webBrowser1.Document.GetElementsByTagName("button");
                HtmlElement btn_sub = button_list[0];
                btn_sub.InvokeMember("click");

                SaveInfo();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://yiqing.zyyj.com.cn/h5_body_tw?user_type=2&id_card=" + idnumber.Text + "&user_name=" + name.Text);
            isopen = true;
        }

        private void SaveInfo()
        {
            StreamWriter sw = new StreamWriter(path);

            sw.WriteLine(idnumber.Text);
            sw.WriteLine(name.Text);
            sw.WriteLine(temp.Text);
            sw.Close();
        }
        private void LoadInfo()
        {
            StreamReader sw = new StreamReader(path);
            idnumber.Text = sw.ReadLine();
            name.Text = sw.ReadLine();
            temp.Text = sw.ReadLine();
            sw.Close();
        }
    }
}
