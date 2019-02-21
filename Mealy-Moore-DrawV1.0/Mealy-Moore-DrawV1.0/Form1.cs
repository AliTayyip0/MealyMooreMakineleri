using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mealy_Moore_DrawV1._0
{
    public partial class Form1 : Form
    {
        string[] istenenler;

        public Form1()
        {
            InitializeComponent();
            yapi.value = "";
            yapiListesi.Add(yapi);
            mooreYapisi.value = "";
            mooreYapisi.cikti = 0;
            mooreYapiListesi.Add(mooreYapisi);
        }




        #region Mealy

        YapiMealy yapi = new YapiMealy();
        protected List<YapiMealy> yapiListesi = new List<YapiMealy>();

        void DurumlarıOlustur(string dizi)
        {
            int m = 0;

            for (int i = 1; i < yapiListesi.Count; i++)
            {
                string yapi_degeri = yapiListesi[i].value;
                if (yapi_degeri.Length <= dizi.Length)
                {
                    if (yapi_degeri == dizi.Substring(0, yapi_degeri.Length))
                    {
                        m = yapi_degeri.Length;
                        yapi = yapiListesi[i];
                    }
                }
            }

            for (; m < dizi.Length; m++)
            {
                YapiMealy gecici = new YapiMealy();
                if (yapi.sifir == null && dizi[m] == '0')
                {
                    string yeni_deger = dizi.Substring(0, m + 1);
                    //if (IstenenMiControl(yeni_deger))
                    //{
                    if (checkBox1.Checked == false)
                    {
                        gecici.value = yeni_deger;
                        yapi.sifir = gecici;
                        yapi = yapi.sifir;
                        yapiListesi.Add(yapi);
                    }
                    else
                    {
                        gecici = yapiListesi[0];
                    }
                    //}
                    //else
                    //{
                    //    yapi.sifir = nereye_gideceğim(yeni_deger);

                    //}
                }
                else if (yapi.bir == null && dizi[m] == '1')
                {
                    string yeni_deger = dizi.Substring(0, m + 1);

                    //if (IstenenMiControl(yeni_deger))
                    //{
                    if (checkBox1.Checked == false)
                    {
                        gecici.value = yeni_deger;
                        yapi.bir = gecici;
                        yapi = yapi.bir;
                        yapiListesi.Add(yapi);
                    }
                    else
                    {
                        gecici = yapiListesi[0];
                    }
                    //}
                    //else
                    //{
                    //    yapi.sifir = nereye_gideceğim(yeni_deger);

                    //}


                }
            }
            yapi = yapiListesi[0];
        }

        Boolean IstenenMiControl(string metin)
        {
            for (int i = 0; i < istenenler.Length; i++)
            {
                if (metin == istenenler[i])
                {
                    return false;
                }
            }
            return true;
        }

        #region Boş Kalanları Doldur

        void BosDegerleriBelirleme()
        {
            for (int i = 0; i < yapiListesi.Count; i++)
            {
                if (yapiListesi[i].bir == null)
                {
                    string metin = yapiListesi[i].value + "1";
                    //if (IstenenMiControl(metin) == false) yapiListesi[i].cikti = 1;
                    yapiListesi[i].bir = nereye_gideceğim(metin);
                }
                if (yapiListesi[i].sifir == null)
                {
                    string metin = yapiListesi[i].value + "0";
                    //if (IstenenMiControl(metin) == false) yapiListesi[i].cikti = 1;
                    yapiListesi[i].sifir = nereye_gideceğim(metin);
                }
            }
        }

        YapiMealy nereye_gideceğim(string metin)
        {
            YapiMealy gecici = yapiListesi[0];
            int bayrak = 1;

            for (int i = 0; i < metin.Length; i++)
            {
                string kullanilacak_metin = metin.Substring(i, metin.Length - i);
                if (bayrak == 1)
                {
                    for (int j = 0; j < yapiListesi.Count; j++)
                    {
                        if (yapiListesi[j].value == kullanilacak_metin)
                        {
                            gecici = yapiListesi[j];
                            bayrak = 0;
                        }
                    }

                }

            }
            return gecici;
        }

        #endregion

        void CiktilariBelirleme()
        {
            for (int i = 0; i < yapiListesi.Count; i++)
            {
                string metin = yapiListesi[i].value + "1";
                if (IstenenMiControl(metin) == false)
                    yapiListesi[i].cikti1 = 1;

                metin = yapiListesi[i].value + "0";
                if (IstenenMiControl(metin) == false) yapiListesi[i].cikti0 = 1;

            }
        }

        
        #endregion




        #region Moore


        YapiMoore mooreYapisi = new YapiMoore();
        List<YapiMoore> mooreYapiListesi = new List<YapiMoore>();


        void MooreDurumlarıOlustur(string dizi)
        {
            int m = 0;

            for (int i = 1; i < mooreYapiListesi.Count; i++)
            {
                string yapi_degeri = mooreYapiListesi[i].value;
                int yapi_degeri_ciktisi = mooreYapiListesi[i].cikti;
                if (yapi_degeri.Length <= dizi.Length)
                {
                    if (yapi_degeri == dizi.Substring(0, yapi_degeri.Length))
                    {
                        m = yapi_degeri.Length;
                        mooreYapisi = mooreYapiListesi[i];
                    }
                }
            }

            for (; m < dizi.Length; m++)
            {
                YapiMoore gecici = new YapiMoore();
                if (mooreYapisi.sifir == null && dizi[m] == '0')
                {
                    string yeni_deger = dizi.Substring(0, m + 1);

                    gecici.value = yeni_deger;
                    if (!IstenenMiControl(yeni_deger)) gecici.cikti = 1;
                    else gecici.cikti = 0;
                    mooreYapisi.sifir = gecici;
                    mooreYapisi = mooreYapisi.sifir;
                    mooreYapiListesi.Add(mooreYapisi);

                }
                else if (mooreYapisi.bir == null && dizi[m] == '1')
                {
                    string yeni_deger = dizi.Substring(0, m + 1);

                    gecici.value = yeni_deger;
                    if (!IstenenMiControl(yeni_deger)) gecici.cikti = 1;
                    else gecici.cikti = 0;
                    mooreYapisi.bir = gecici;
                    mooreYapisi = mooreYapisi.bir;
                    mooreYapiListesi.Add(mooreYapisi);

                }
            }
            mooreYapisi = mooreYapiListesi[0];
        }

        void MooreBoslariDoldur()
        {
            for (int i = 0; i < mooreYapiListesi.Count; i++)
            {
                if (mooreYapiListesi[i].bir == null)
                {
                    string metin = mooreYapiListesi[i].value + "1";
                    mooreYapiListesi[i].bir = MooreGidilecekYer(metin);
                }
                if (mooreYapiListesi[i].sifir == null)
                {
                    string metin = mooreYapiListesi[i].value + "0";
                    mooreYapiListesi[i].sifir = MooreGidilecekYer(metin);
                }
            }
        }

        YapiMoore MooreGidilecekYer(string metin)
        {
            YapiMoore gecici = mooreYapiListesi[0];
            int bayrak = 1;

            for (int i = 0; i < metin.Length; i++)
            {
                string kullanilacak_metin = metin.Substring(i, metin.Length - i);
                if (bayrak == 1)
                {
                    for (int j = 0; j < mooreYapiListesi.Count; j++)
                    {
                        if (mooreYapiListesi[j].value == kullanilacak_metin)
                        {
                            gecici = mooreYapiListesi[j];
                            bayrak = 0;
                        }
                    }
                }
            }
            return gecici;
        }

        #endregion





        #region Ortak

        void clear()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            yapiListesi.Clear();
            yapi = new YapiMealy();
            yapi.value = "";
            yapiListesi.Add(yapi);
        }

        void CozmeyeBasla()
        {
            for (int i = 0; i < istenenler.Length; i++)
            {
                if (rbMealy.Checked == true)
                    DurumlarıOlustur(istenenler[i]);
                else
                    MooreDurumlarıOlustur(istenenler[i]);
            }
        }

        void yaz()
        {
            if (rbMealy.Checked == true)
            {
                for (int i = 0; i < yapiListesi.Count; i++)
                {
                    //listBox1.Items.Add(yapiListesi[i].value);
                    //listBox2.Items.Add(yapiListesi[i].sifir.value);
                    //listBox3.Items.Add(yapiListesi[i].bir.value);
                    listBox4.Items.Add(yapiListesi[i].value);
                    listBox1.Items.Add("S" + i);
                    listBox2.Items.Add("S" + yapiListesi.IndexOf(yapiListesi[i].sifir) + "/" + yapiListesi[i].cikti0);
                    listBox3.Items.Add("S" + yapiListesi.IndexOf(yapiListesi[i].bir) + "/" + yapiListesi[i].cikti1);

                }
            }
            else if (rbMoore.Checked == true)
            {
                for (int i = 0; i < mooreYapiListesi.Count; i++)
                {
                    listBox4.Items.Add(mooreYapiListesi[i].value);
                    listBox1.Items.Add("S" + i + "/" + mooreYapiListesi[i].cikti);
                    listBox2.Items.Add("S" + mooreYapiListesi.IndexOf(mooreYapiListesi[i].sifir));
                    listBox3.Items.Add("S" + mooreYapiListesi.IndexOf(mooreYapiListesi[i].bir));

                }
            }
        }

        #endregion

        Boolean text_control(string metin)
        {
            for (int i = 0; i < metin.Length; i++)
            {
                if (metin[i] == '1' || metin[i] == '0' || metin[i] == ',')
                {

                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (text_control(textBox1.Text))
            {
                clear();
                istenenler = textBox1.Text.Split(',');
                if (rbMealy.Checked == true)
                {
                    CozmeyeBasla();
                    BosDegerleriBelirleme();
                    CiktilariBelirleme();
                    yaz();
                }
                else if (rbMoore.Checked == true)
                {
                    CozmeyeBasla();
                    MooreBoslariDoldur();
                    yaz();
                }


                Cizim new_form;

                if (rbMealy.Checked == true) new_form = new Cizim(yapiListesi, mooreYapiListesi, 1);

                else new_form = new Cizim(yapiListesi, mooreYapiListesi, 0);

                new_form.Show();


            }
            else
            {
                MessageBox.Show("metinle ilgili bir hata var");
            }
        }



    }





    public class YapiMealy
    {
        public string value;
        public int cikti0 = 0;
        public int cikti1 = 0;
        public YapiMealy sifir;
        public YapiMealy bir;
    }

    public class YapiMoore
    {
        public string value;
        public int cikti = 0;
        public YapiMoore sifir;
        public YapiMoore bir;
    }

}