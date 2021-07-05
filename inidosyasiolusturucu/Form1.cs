using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inidosyasiolusturucu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class inikaydet
        {
            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section, string key, string value, string filepath);
            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filepath);


            public inikaydet(string dosyayolu)
            {
                dosyol = dosyayolu;
            }
            private string dosyol = string.Empty;
            public string varsayilan { get; set; }

            public string Oku(string bolum, string ayaradi)
            {
                varsayilan = varsayilan ?? string.Empty;
                StringBuilder strBuild = new StringBuilder(256);
                GetPrivateProfileString(bolum, ayaradi, varsayilan, strBuild, 255, dosyol);
                return strBuild.ToString();

            }
            public long yaz(string bolum, string ayaradi, string deger)
            {
                return WritePrivateProfileString(bolum, ayaradi, deger, dosyol);

            }





        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            inikaydet ini = new inikaydet(Application.StartupPath + @"\Ayarlar.ini");
            ini.yaz(textBox1.Text, textBox2.Text, textBox3.Text);

            MessageBox.Show("Ayarlar kaydedildi");
        }

        private void okubtn_Click(object sender, EventArgs e)
        {

            try
            {
                if (File.Exists(Application.StartupPath + @"\Ayarlar.ini"))
                {
                    inikaydet ini = new inikaydet(Application.StartupPath + @"\Ayarlar.ini");

                    textBox3.Text = ini.Oku(textBox1.Text, textBox2.Text);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("ini dosyası hasarlı " + hata);
            }
            if (textBox3.Text == "")
            {
                inikaydet ini = new inikaydet(Application.StartupPath + @"\Ayarlar.ini");
                ini.yaz(textBox1.Text, textBox2.Text, textBox3.Text);
            }
        }

        private void islembtn_Click(object sender, EventArgs e)
        {
            inikaydet ini = new inikaydet(Application.StartupPath + @"\settings.ini");
            if (File.Exists("credentials.txt") == true)
            {
                string[] lines = File.ReadAllLines("credentials.txt");


                if (lines.Length > 0)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (i == 0)
                        {

                            ini.yaz("sistem", "hotelid", lines[i]);
                        }
                        else if (i == 1)
                        {

                            ini.yaz("sistem", "username", lines[i]);
                        }
                        else if (i == 2)
                        {

                            ini.yaz("sistem", "password", lines[i]);
                        }
                    }

                }
            }
            else
            {
                for (int i = 0; i < 1; i++)
                {


                    ini.yaz("sistem", "hotelid", "");



                    ini.yaz("sistem", "username", "");



                    ini.yaz("sistem", "password", "");

                }
            }
        }
    }
}
