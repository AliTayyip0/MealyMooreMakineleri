using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Mealy_Moore_DrawV1._0
{
    public partial class Cizim : Form
    {
        List<YapiMealy> mealyYapiListesi;
        List<YapiMoore> mooreYapiListesi;
        int islem;




        public Cizim(List<YapiMealy> mealyYapiListesi1, List<YapiMoore> mooreYapiListesi1, int islem1)
        {
            InitializeComponent();
            mealyYapiListesi = mealyYapiListesi1;
            mooreYapiListesi = mooreYapiListesi1;
            islem = islem1;
            //yaz();
        }
        private void Cizim_Shown(object sender, EventArgs e)
        {
            
            Brush color = Brushes.Red;

            Graphics gr = this.CreateGraphics();
            Pen renk = new Pen(Color.LightSeaGreen, 5);
            gr.DrawLine(renk,4,4,10,4);
            gr.DrawString("\'1\'", DefaultFont,color,14,4);
            renk = new Pen(Color.Orange, 5);
            gr.DrawLine(renk, 4, 20, 10, 20);
            gr.DrawString("\'0\'", DefaultFont, color, 14, 20);


            if (islem == 1)//mealy
            {
                Daireleri_Ciz(1);
            }
            else if (islem == 0)// Moore
            {
                Daireleri_Ciz(0);
            }
        }









        ArrayList durumKonumları = new ArrayList();
        List<Konumlar> konumlar = new List<Konumlar>();



        #region MealyÇiz
        /*
        void dairelericiz1(int islem)
        {
            int durum_sayisi = 0;
            if (islem == 1)
            {
                durum_sayisi = mealyYapiListesi.Count;
            }
            else if (islem == 0)
            {
                durum_sayisi = mooreYapiListesi.Count;
            }


            Graphics gr = this.CreateGraphics();
            Pen renk = new Pen(Color.DarkRed, 5);
            int ortax = 450;
            int basy = 450;

            //int xartis = 120;
            //int yartis = 100;
            int cemberboyut = 60;



            // int cap = durum_sayisi * cemberboyut;
            int cap = 300;
            //int x = 0;
            //int y = Math.Abs((yaricap * yaricap) - (x * x));

            List<int[]> noktalar = konum_hesapla(ortax,basy,cap);

            for (int i = 0; i < durum_sayisi / 2; i++)
            {
                gr.DrawEllipse(renk, noktalar[i][0], noktalar[i][1], cemberboyut, cemberboyut);
                KonumlariDoldur(noktalar[i][0], noktalar[i][1]);
            }
        }

        List<int[]> konum_hesapla(int merkezx,int merkezy,int cap)
        {
            List<int[]> noktalar = new List<int[]>() ;
            int x = 0;
            
            do
            {

                var y = Math.Sqrt ((cap * cap / 4) - (x * x));

                //if (y.GetType()==typeof(int))
                //{
                    int[] eklenecek = { x+merkezx, Convert.ToInt32(y)+merkezy };
                    noktalar.Add(eklenecek);
                int[] eklenecek1 = { x + merkezx, Convert.ToInt32(y) - merkezy };
                noktalar.Add(eklenecek1);
                //}
                x = x + 1 ;

            } while (x*x<cap);
            
            

            return noktalar;
        }
        */


        void Daireleri_Ciz(int islem)
        {

            int durum_sayisi=0;
            if (islem==1)
            {
                durum_sayisi = mealyYapiListesi.Count;
            }
            else if(islem==0)
            {
                durum_sayisi = mooreYapiListesi.Count;
            }

            

            Graphics gr = this.CreateGraphics();
            Pen renk = new Pen(Color.DarkRed, 5);
            int ortax = 600 / 2;
            int basy = 30;

            int xartis = 120;
            int yartis = 100;
            int cemberboyut = 60;


            ortax -= xartis / 2;
            gr.DrawEllipse(renk, ortax, basy, cemberboyut, cemberboyut); // s0 durumu
            KonumlariDoldur(ortax, basy);

            
            basy += yartis;
            if (durum_sayisi % 2 == 0)
            {
                for (int i = 0; i < (durum_sayisi - 1) / 2; i++)
                {
                    gr.DrawEllipse(renk, ortax, basy, cemberboyut, cemberboyut);
                    KonumlariDoldur(ortax, basy);
                    ortax += xartis;
                    gr.DrawEllipse(renk, ortax, basy, cemberboyut, cemberboyut);
                    KonumlariDoldur(ortax, basy);
                    basy += yartis;
                    ortax -= xartis;
                }
                gr.DrawEllipse(renk, ortax, basy, cemberboyut, cemberboyut); // bunun sebebi s0 çıkınca tek sayıda düğüm kalıyor  o son düğümü ekleme amaçlı
                KonumlariDoldur(ortax, basy);
            }
            else if (durum_sayisi % 2 == 1)
            {
                for (int i = 0; i < (durum_sayisi) / 2; i++)
                {
                    gr.DrawEllipse(renk, ortax, basy, cemberboyut, cemberboyut);
                    KonumlariDoldur(ortax, basy);
                    ortax += xartis;
                    gr.DrawEllipse(renk, ortax, basy, cemberboyut, cemberboyut);
                    KonumlariDoldur(ortax, basy);
                    basy += yartis;
                    ortax -= xartis;
                }
            }

            CizgileriCiz(islem);
        }

        void KonumlariDoldur(int x, int y)
        {
            Konumlar gecici = new Konumlar();
            gecici.x = x;
            gecici.y = y;
            konumlar.Add(gecici);
            gecici = new Konumlar();
        }

        void CizgileriCiz(int islem)
        {
            Point point1 = new Point();
            Point point2 = new Point();
            Graphics gr = this.CreateGraphics();
            Brush color = Brushes.LightYellow;


            int cemberboyut = 40 / 2;
            int konbir = 0,konsifir=0;
            int forici = 0;
            if (islem==1)
            {
                forici = mealyYapiListesi.Count;
            }
            else if (islem==0)
            {
                forici= mooreYapiListesi.Count; ;
            }
            for (int i = 0; i < forici; i++)
            {
                
                if (islem==1)
                {
                    konbir = mealyYapiListesi.IndexOf(mealyYapiListesi[i].bir);
                    
                }
                else if (islem==0)
                {
                    konbir = mooreYapiListesi.IndexOf(mooreYapiListesi[i].bir);
                }

                point1.X = konumlar[i].x + cemberboyut;
                point1.Y = konumlar[i].y + cemberboyut;
                point2.X = konumlar[konbir].x + cemberboyut;
                point2.Y = konumlar[konbir].y + cemberboyut;
                Pen renk = new Pen(Color.LightSeaGreen, 3);
                
                CizgileriCiz2(point1, point2,renk);
                // Font = Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //gr.DrawString("s"+i, , konumlar[i].x, konumlar[i].y);
                if (islem==1)
                {
                    //point1.X += -7;
                    gr.DrawString("s" + i, DefaultFont, color, point1);
                    //point1.X += 7;

                    konsifir = mealyYapiListesi.IndexOf(mealyYapiListesi[i].sifir);
                }
                else if (islem==0)
                {
                    //point1.X += -7;
                    gr.DrawString("s" + i+"/"+mooreYapiListesi[i].cikti, DefaultFont, color, point1);
                    //point1.X += 7;

                    konsifir = mooreYapiListesi.IndexOf(mooreYapiListesi[i].sifir);
                }
                

                point2.X = konumlar[konsifir].x + cemberboyut;
                point2.Y = konumlar[konsifir].y + cemberboyut;
                renk = new Pen(Color.Orange, 3);
                CizgileriCiz2(point1, point2,renk);

            }
        }

        void CizgileriCiz2(Point point1, Point point2,Pen renk)
        {
            Graphics gr = this.CreateGraphics();
            Brush color = Brushes.Black;

            int cakismaonleme = 20;

            if (point2 == point1)
            {
                //renk = new Pen(Color.Brown, 3);
                int degisim = 25;
                if (point1.X == 260 || point1.X == 320)
                {
                    point1.X -= 5;
                    gr.DrawLine(renk, point1.X, point1.Y, point1.X - degisim, point1.Y);
                    gr.DrawLine(renk, point1.X - degisim, point1.Y, point1.X - degisim, point1.Y - degisim);
                    gr.DrawLine(renk, point1.X - degisim, point1.Y - degisim, point1.X, point1.Y - degisim);
                    gr.DrawLine(renk, point1.X, point1.Y - degisim, point1.X, point1.Y);

                }
                if (point1.X == 380)
                {
                    point1.X += 5;
                    gr.DrawLine(renk, point1.X, point1.Y, point1.X + degisim, point1.Y);
                    gr.DrawLine(renk, point1.X + degisim, point1.Y, point1.X + degisim, point1.Y - degisim);
                    gr.DrawLine(renk, point1.X + degisim, point1.Y - degisim, point1.X, point1.Y - degisim);
                    gr.DrawLine(renk, point1.X, point1.Y - degisim, point1.X, point1.Y);
                }
            }

            else if (point1.X == 380 && point2.X == 380)
            {
                //renk = new Pen(Color.Blue, 3);
                Point gecici = new Point(0, 0);
                Point gecici2 = new Point(0, 0);
                gecici.X = point1.X + (Math.Abs(point1.Y - point2.Y) / 3) + 10;
                gecici2.X = point1.X - (Math.Abs(point1.Y - point2.Y) / 3) + 10;
                gecici.Y = ((point1.Y + point2.Y) / 2) - 10;
                gecici2.Y = ((point1.Y + point2.Y) / 2) + 10;
                //point1.X += 20;
                //point1.Y += 20;
                gr.DrawBezier(renk, point1, gecici, gecici, point2);
                renk.Color = Color.Chocolate;
                gr.DrawRectangle(renk, point2.X, point2.Y, 3, 3);
            }
            else if (point1.X == 260 && point2.X == 260)
            {
                //renk = new Pen(Color.Blue, 3);
                Point gecici = new Point(0, 0);
                Point gecici2 = new Point(0, 0);
                gecici.X = point1.X - (Math.Abs(point1.Y - point2.Y) / 3) + 10;
                gecici2.X = point1.X - (Math.Abs(point1.Y - point2.Y) / 3) + 10;
                gecici.Y = ((point1.Y + point2.Y) / 2) - 10;
                gecici2.Y = ((point1.Y + point2.Y) / 2) + 10;
                //point1.X -= 10;
                //point1.Y += 20;
                gr.DrawBezier(renk, point1, gecici, gecici, point2);
                renk.Color = Color.Chocolate;
                gr.DrawRectangle(renk, point2.X, point2.Y, 3, 3);
            }
            else
            {
                if (point1.X < point2.X)
                {
                    if (point1.Y>point2.Y)
                    {

                        point1.X += 27;
                        //point2.X -= 17;
                        point2.Y += cakismaonleme;
                        gr.DrawBezier(renk, point1, point1, point2, point2);
                        renk.Color = Color.Chocolate;
                        gr.DrawRectangle(renk, point2.X, point2.Y, 3, 3);
                    }
                    else if(point1.Y == point2.Y)
                    {
                        point1.X += 27;
                        point2.X -= 27;
                        gr.DrawBezier(renk, point1, point1, point2, point2);
                        renk.Color = Color.Chocolate;
                        gr.DrawRectangle(renk, point2.X, point2.Y, 3, 3);
                    }
                    else
                    {
                        point1.X += 27;
                        //point2.X -= 17;
                        point2.Y -= cakismaonleme;
                        gr.DrawBezier(renk, point1, point1, point2, point2);
                        renk.Color = Color.Chocolate;
                        gr.DrawRectangle(renk, point2.X, point2.Y, 3, 3);
                    }
                }       
                else
                {
                    if (point1.Y > point2.Y)
                    {

                        point1.X -= 27;
                        //point2.X += 17;
                        point2.Y += cakismaonleme;
                        gr.DrawBezier(renk, point1, point1, point2, point2);
                        renk.Color = Color.Chocolate;
                        gr.DrawRectangle(renk, point2.X, point2.Y, 3, 3);
                    }
                    else if (point1.Y == point2.Y)
                    {
                        point1.X -= 27;
                        point2.X += 27;
                        gr.DrawBezier(renk, point1, point1, point2, point2);
                        renk.Color = Color.Chocolate;
                        gr.DrawRectangle(renk, point2.X, point2.Y, 3, 3);
                    }
                    else
                    {
                        point1.X -= 27;
                        //point2.X += 17;
                        point2.Y -= cakismaonleme;
                        gr.DrawBezier(renk, point1, point1, point2, point2);
                        renk.Color = Color.Chartreuse;
                        gr.DrawRectangle(renk, point2.X, point2.Y, 3, 3);
                    }


                    //point1.X -= 17;
                    //point2.X += 17;
                    ////point2.Y += cakismaonleme;
                    //gr.DrawBezier(renk, point1, point1, point2, point2);
                    //gr.DrawRectangle(renk, point2.X, point2.Y, 3, 3);
                }

            }

        }
        

        #endregion
    }


    public class Konumlar
    {
        public int x;
        public int y;
    }
    
}