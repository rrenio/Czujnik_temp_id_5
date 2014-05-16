using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ClassLibrary3_formatki; // klasa z polami statycznymi sluzaca do przechowywania id formatki klieknietej i id_biblioteki;
using Oracle.DataAccess.Client; // dopolaczenia z baza oracle
using Oracle.DataAccess.Types; // do polaczenia z baza oracle 
using ClassLibrary3_polaczenie; // klasa statyczna s³u¿ca jako kontener przecowuj¹ca user baza pasword

namespace Kontrolkatemp1
{
    public partial class Archiwum : Form
    {

        static Archiwum instance = null; //  dla wzorca singleton

        Graphics G; // plutno do rysowania

        OracleDataAdapter ODA;
        String cmdQuery;
        DataSet DS;
        DataRow DR;
        DataTable DT;
        OracleConnection con;

        string user ="";
        string pasword ="";
        string baza = "";

        string  wartosc_pomiaru=""; // jak byla to wartosc w danej godzinie srednia gaussa
        string ilosc_pomiaru =""; // ile bylo danej wartosci sredniej gaussa w danej godzinie

        //wysokosc i szerokosc dla danego slupka
        int wysokosc = 0;
        int szerokosc = 0; 



        public Archiwum()
        {
            InitializeComponent();

        }

      


        //metoda statyczna implementujaca wzorzec singletona
        public static Archiwum Instance()
        {
            if (instance == null)
                instance = new Archiwum(); //tworznie obiektu 
            return Archiwum.instance;

        }


        public void rysuj_skale()
        {
            G = panel1.CreateGraphics();
            //skala
            Pen P1 = new Pen(Color.Black, 2);

            for (int i = 30; i <= 780; i += 30)
            {
                G.DrawLine(P1, new Point(i, panel1.Height), new Point(i, panel1.Height - 10));

            }

            G.DrawLine(P1, new Point(0, panel1.Height-10), new Point(panel1.Width, panel1.Height-10));
        }

        private void Archiwum_Paint(object sender, PaintEventArgs e)
        {
            rysuj_skale();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rysuj_wykres();
        }


        void rysuj_wykres()
        {
            G.Clear(panel1.BackColor); // czyszcze obszar wylresu przy przerysowaniu 
            rysuj_skale();
            int godzina_od=0;
            int godzina_do=1;
            int dzien;
            string s_dzien;
            DateTime D = data.Value;
            dzien = D.Day;
            
            for (int i = 30; i <= 720; i += 30)
            {
                string s_godzina_od = string.Format("{0:D2}", godzina_od); // godzina zawsze na dwoch pozycjach zapisana
                string s_godzina_do = string.Format("{0:D2}", godzina_do); // godzina zawsze na dwoch pozycjach zapisana
                s_dzien = string.Format("{0:D2}", dzien); // dzien na dwoch pozycjach wykoszystywany tylko jak dojdziemy do przedzialu 23 dnia poprzedniego i 00 dnia nastepnego
                cmdQuery = " select a.wartosc,a.ilosc from (select w.wartosc, w.ilosc  from (" +
                //" select p1.id, 
                " select P1.WARTOSC,count(*) ilosc"+ //P1.DATA_WPROWADZENIA, to_char(P1.DATA_WPROWADZENIA," + "\'" + "YYYY-MM-DD HH24:MI:SS" + "\'" +
                //" ) as data1 
                " from pomiary p1 where P1.DATA_WPROWADZENIA between  to_date(" + "\'" + data.Value.Year.ToString() + "-" + data.Value.Month.ToString()+"-"+
                data.Value.Day.ToString()+" "+ s_godzina_od+    //+ "2011-04-04 02" 
                "\'" + "," + "\'" + "YYYY-MM-DD HH24" + "\'" + ") and to_date(" + "\'"+
                data.Value.Year.ToString()+"-"+data.Value.Month.ToString()+"-"+dzien+" "+s_godzina_do+ //"2011-04-04 03" + 
                "\'" + "," + "\'" + "YYYY-MM-DD HH24" + "\'" + ")" +
                " and P1.ID_CZUJNIKA=" + Formatki.Id_czujnika + " group by p1.wartosc )w  order by w.ilosc desc) a where rownum=1";

                ODA = new OracleDataAdapter(cmdQuery, con);
                DS = new DataSet(); // tworzedataset

                ODA.Fill(DS);  // wypelniam dataseta
                DT = DS.Tables[0]; // tablica 0
                //sprawdzam czy istnieja jakies pomiary dla danego przedzialu godzinoweg  rysuje jak jest 
                if (DT.Rows.Count > 0)
                {

                    DR = DT.Rows[0]; // pierwszy rekord

                    wartosc_pomiaru = DR[0].ToString();
                    ilosc_pomiaru = DR[1].ToString();

                    wysokosc = int.Parse(ilosc_pomiaru) * 350 / 720;

                    SolidBrush SB = new SolidBrush(Color.Yellow);
                    Rectangle R = new Rectangle(i, panel1.Height - wysokosc - 20, 30, wysokosc);
                    G.FillRectangle(SB, R);
                }

                godzina_od++;
                godzina_do++;
                if (godzina_do == 24)
                {
                    godzina_do = 0;

                }
             
            }

        }

        private void Archiwum_Load(object sender, EventArgs e)
        {
            // z klasy statyczna dolacozna z dll statycznie przechowuje user halo i baze :) 
            this.user = Class1.Uzytkownik;
            this.pasword = Class1.Haslo;
            this.baza = Class1.Baza;

            con = new OracleConnection();
            con.ConnectionString = "User ID =" + this.user + "; Password =" + this.pasword + "; Data Source =" + this.baza;
            con.Open();

            //ustawiam jaki typ wykresu
            RBgauss.Checked = true;


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            rysuj_wykres(); // odœwie¿a po zas³oniêciu 
        }

    


    }
}