using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client; // dopolaczenia z baza oracle
using Oracle.DataAccess.Types; // do polaczenia z baza oracle 
using ClassLibrary3_polaczenie; // klasa statyczna s³u¿ca jako kontener przecowuj¹ca user baza pasword
using ClassLibrary3_formatki; // klasa z polami statycznymi sluzaca do przechowywania id formatki klieknietej i id_biblioteki;


namespace Kontrolkatemp1
{
    public partial class Okno_zegara : Form
    {
        OracleConnection con;

        bool flaga_temp = false;

        static Okno_zegara instance = null; //  dla wzorca singleton

        string user = "";
        string pasword = "";
        string baza = "";

        bool polaczeni_z_baza = false; // status polaczenia z baza

        string id_biblioteki;
        string id_formatki;

        string stare_id_pomiaru;
        string stare_wartosc_pomiaru;

        string nowe_id_pomiaru;
        string nowe_wartosc_pomiaru;

        OracleDataAdapter ODA;
        String cmdQuery;
        DataSet DS;
        DataRow DR;
        DataTable DT;

        bool flaga_wykonania = true;

        int progess_watku = 0;




        //metoda statyczna implementujaca wzorzec singletona
        public static Okno_zegara Instance()
        {
            if (instance == null)
                instance = new Okno_zegara(); //tworznie obiektu 
            return Okno_zegara.instance;

        }


        public OracleConnection Con
        {
            get { return con; }
            set { con = value; }
        }


        Graphics grafika;
        GraphicsPath GP1;
        Size S1;
        Point P1;
        
        float wartosc_aktualna = 0;
        private float kierunek=1;
        private float wartosc_zadana;

        GraphicsPath grafika_skali;
        Matrix matriks1_skali;
        Graphics g_skali;
        Color tla_skali;
        bool flaga_zera = true; // przy inicjalizowaniu tarczy wskazowka jest ustawiana na zero przy pierwsyzm odrysowaniu 
          


        public Okno_zegara()
        {

            InitializeComponent();
            txtbxwartosc_aktualna.Text = wartosc_aktualna.ToString();


        }

        private void rysuj_tarcze(bool flaga_ustaw_na_zero)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // podwojny bufor kontrolki
            grafika = panel1.CreateGraphics(); // przygotowanie powierzchni do rysowania 
            grafika.SmoothingMode = SmoothingMode.HighQuality; //wyg³adzanie 
            grafika.PixelOffsetMode = PixelOffsetMode.HighQuality; // przesuniecie o pol pixela 

            S1 = new Size(panel1.Width, panel1.Height); // rozmiar na caly panel 

            SolidBrush B1 = new SolidBrush(Color.FromArgb(154, 177, 163)); // kolor pedzla 
            P1 = new Point(panel1.Width / 2 - S1.Width / 2, panel1.Height / 2 - S1.Height / 2); //zaczynamy rysowac w srodku z  przesuniecie  o polowe szerokosci i polowe wysokosci
            Rectangle R1 = new Rectangle(P1, S1);
            grafika.FillEllipse(B1, R1);

            int s2_rozmiar = 20;
            Size S2 = new Size(S1.Width - 2 * s2_rozmiar, S1.Height - 2 * s2_rozmiar);
            Point P2 = new Point(P1.X + s2_rozmiar, P1.Y + s2_rozmiar);
            Rectangle R2 = new Rectangle(P2, S2);
            SolidBrush B2 = new SolidBrush(Color.FromArgb(223, 223, 223));
            grafika.FillEllipse(B2, R2);

            Point srodek = new Point(panel1.Width / 2, panel1.Height / 2);

            SolidBrush B3 = new SolidBrush(Color.Green);
            int mniejsze_o = 50;
            grafika.FillPie(B3, 0 + mniejsze_o, 0 + mniejsze_o, panel1.Width - 2 * mniejsze_o, panel1.Height - 2 * mniejsze_o, 180, 30);

            SolidBrush B31 = new SolidBrush(Color.Yellow);
            grafika.FillPie(B31, 0 + mniejsze_o, 0 + mniejsze_o, panel1.Width - 2 * mniejsze_o, panel1.Height - 2 * mniejsze_o, 180 + 30, 30);

            SolidBrush B4 = new SolidBrush(Color.Red);
            grafika.FillPie(B4, 0 + mniejsze_o, 0 + mniejsze_o, panel1.Width - 2 * mniejsze_o, panel1.Height - 2 * mniejsze_o, 180 + 30 + 30, 120);

            SolidBrush B41 = new SolidBrush(Color.FromArgb(119, 190, 224));
            grafika.FillPie(B41, 0 + mniejsze_o, 0 + mniejsze_o, panel1.Width - 2 * mniejsze_o, panel1.Height - 2 * mniejsze_o, 180, -30);

            SolidBrush B5 = new SolidBrush(Color.FromArgb(32, 132, 168));
            grafika.FillPie(B5, 0 + mniejsze_o, 0 + mniejsze_o, panel1.Width - 2 * mniejsze_o, panel1.Height - 2 * mniejsze_o, 180 - 30, -60);

            int ramka = 40;
            SolidBrush B51 = new SolidBrush(Color.White);
            grafika.FillPie(B51, 0 + mniejsze_o + ramka, 0 + mniejsze_o +ramka, panel1.Width - (2 * mniejsze_o) -(2 * ramka), panel1.Height - (2 * mniejsze_o) - (2 * ramka), 180 - 30-60, 180+90);
  
            //rysuj_tarcze(ref S1, ref P1, Color.GreenYellow, 90);
            odswiez_skale();
            rysuj_szara_tarcze_1(ref S1, ref P1);

           // grafika.DrawString("dupa", new Font("Microsoft Sans Serif", 30), new SolidBrush(Color.Black), new Point(0, 0));

            Rysuj_czarny_guzik();

            //rysowanie wskazowki 
            Point[] wskazowka ={ new Point(panel1.Width / 2 - 19, panel1.Height / 2), new Point(panel1.Width / 2 + 19, panel1.Height / 2), new Point(panel1.Width / 2, panel1.Height / 2 - 170) };
            GP1 = new GraphicsPath();
            GP1.AddPolygon(wskazowka);
            if (flaga_ustaw_na_zero == true)
            {
                flaga_zera = false;
                ustaw_na_zero();
            }
            else  // przy kolejnym odrysowaniu
            {
                ustaw_na_wartosc_aktualna(wartosc_aktualna); //odrysowuje wartosc
            }

        }

        private void rysuj_szara_tarcze_1(ref Size S1, ref Point P1)
        {
            rysuj_tarcze(ref S1, ref P1,Color.FromArgb(223,223,223),110);

        }

        private void rysuj_tarcze(ref Size S1, ref Point P1, Color color, int s3_rozmiar)
        {
            
            Size S3 = new Size(S1.Width - 2 * s3_rozmiar, S1.Height - 2 * s3_rozmiar);
            Point P21 = new Point(P1.X + s3_rozmiar, P1.Y + s3_rozmiar);
            Rectangle R21 = new Rectangle(P21, S3);
            SolidBrush B21 = new SolidBrush(color);
            //SolidBrush B21 = new SolidBrush(Color.Yellow);
            grafika.FillEllipse(B21, R21);
        }

        private void Rysuj_czarny_guzik()
        {
            //rysowanie na srodku czarnego koleczka
            Rectangle R3 = new Rectangle(new Point(panel1.Width / 2 - 20, panel1.Height / 2 - 20), new Size(40, 40));
            grafika.FillEllipse(Brushes.Black, R3);
        }

 
        private void button1_Click(object sender, EventArgs e)
        {


            grafika.DrawPath(new Pen(Brushes.Black, 2), GP1);
        }

 

        private void button1_Click_1(object sender, EventArgs e)
        {
            grafika.DrawPath(new Pen(Color.Black, 2), GP1);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            GP1.Reset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
     

            
            
        }


        private void button3_Clik2(object sender, EventArgs e)
        {
            Matrix matriks = new Matrix(); 
            //obrot o kat wzgledem srodka panelu 
            matriks.RotateAt(kierunek*(float)1.5, new Point(panel1.Width / 2, panel1.Height / 2)); // skok co pol stopnia 
            GP1.Transform(matriks);
            
            grafika.DrawPath(new Pen(Brushes.Black, 2), GP1);
            grafika.FillPath(Brushes.Black, GP1);
            Rysuj_czarny_guzik();
            
        }

        //funkcja wykorzystywana prfzy odrysowaniu kojenym wskazowki 
        private void ustaw_na_wartosc_aktualna(float wartosc)
        {

            //wyliczenie o jaki kat nalezy odrysowac wskazowke przy inizjalizacji wskazowka jest pionowo do gory 90 stopni bo latwiej byloi rysowoac 
            float kat =wartosc_aktualna-30;
            kat = (float) kat * (float) 3;

            grafika.DrawPath(new Pen(Color.FromArgb(223, 223, 223), 2), GP1);
            Matrix matriks = new Matrix();
            //obrot o kat wzgledem srodka panelu  ustawienie na 0
            matriks.RotateAt(kat, new Point(panel1.Width / 2, panel1.Height / 2));
            GP1.Transform(matriks);
            SolidBrush sb1 = new SolidBrush(Color.Black);
            grafika.FillPath(sb1, GP1);
            Rysuj_czarny_guzik();
      
        }

        //funckjs wykorzystywana przy odrysowaniu wskazowki p[rzy inicajlizacji 
        private void ustaw_na_zero()
        {           
            grafika.DrawPath(new Pen(Color.FromArgb(223, 223, 223), 2), GP1);
            Matrix matriks = new Matrix();
            //obrot o kat wzgledem srodka panelu  ustawienie na 0
            matriks.RotateAt(-90, new Point(panel1.Width / 2, panel1.Height / 2));
            GP1.Transform(matriks);
            SolidBrush sb1 = new SolidBrush(Color.Black);
            grafika.FillPath(sb1, GP1);
            Rysuj_czarny_guzik();
            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            
            try
            {
                //wartosc_zadana = float.TryParse(txtbxwartosc_zadana.Text,out wart_zadana);

                if (float.TryParse(txtbxwartosc_zadana.Text, out wartosc_zadana) == true)
                {


                    if (wartosc_zadana > 60)
                    {
                        wartosc_zadana = wartosc_aktualna;
                        MessageBox.Show("Zadana temperatura jest za du¿a wartosc zadana nie zostanie zmieniona");
                    }
                    if (wartosc_zadana < -30)
                    {
                        wartosc_zadana = wartosc_aktualna;
                        MessageBox.Show("Zadana temperatura jest za niska wartosc zadana nie zostanie zmieniona");
                    }



                    if (wartosc_aktualna != wartosc_zadana)
                    {
                        bttnUstaw.Enabled = false;
                        timer1.Enabled = true;
                        timer1.Start();
                    }
                }
                else
                {
                    MessageBox.Show("B³¹d konwersji temperatury");
                    txtbxwartosc_zadana.Text = wartosc_aktualna.ToString();
                }
            }
            catch
            {
                MessageBox.Show("B³¹d konwersji zadanej temperatury");
                txtbxwartosc_zadana.Text = txtbxwartosc_aktualna.Text;
                txtbxwartosc_aktualna.ForeColor = Color.YellowGreen;
                
            }
        
           

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (wartosc_aktualna != wartosc_zadana)
            {
                if (wartosc_aktualna < wartosc_zadana)
                {
                    kierunek = 1;

                }
                else
                {
                    kierunek = -1;

                }



                    //button3_Click(sender, e);
                    rysuj_szara_tarcze_1(ref S1, ref P1); // odrysowuje w srodku tarcze zeby przykryc stara wskazowke 
                    
                    button3_Clik2(sender, e);

                    if (kierunek == 1)
                        wartosc_aktualna += (float)0.5;
                    else
                        wartosc_aktualna -= (float)0.5;

                    string war_pom_aktualna = wartosc_aktualna.ToString();
                    if (war_pom_aktualna.IndexOf(',') == -1) // gdy nei znaleziono kropki tzn calkowito liczbowa to dadaje zebyzaly czas byla wiswietlana jako 23.0 a nie 23 
                    {
                        txtbxwartosc_aktualna.Text = wartosc_aktualna.ToString() + ",0";
                    }
                    else
                    {
                        txtbxwartosc_aktualna.Text = wartosc_aktualna.ToString();
                    }
                 
              
            }
            else
            {

                timer1.Stop();
                bttnUstaw.Enabled = true;
            }





        }




        #region skala // funkcje od skali 
        private void Inicjalizacja_skali()
        {
            g_skali = grafika;   //panel1.CreateGraphics();
            tla_skali = panel1.BackColor; // zapamietuje tlo skali
            //g_skali.SmoothingMode = SmoothingMode.HighQuality; //wygladzanie 
            //g_skali.PixelOffsetMode = PixelOffsetMode.HighQuality; //przesuniecie o pol pixela 
            grafika_skali = new GraphicsPath();
            matriks1_skali =new Matrix();
            grafika_skali.AddLine(panel1.Width / 2, panel1.Height / 2 + 183, panel1.Width / 2, panel1.Height / 2 + 195);
            g_skali.DrawPath(new Pen(Color.Black, 2), grafika_skali);
            matriks1_skali.RotateAt(3, new Point(panel1.Width / 2, panel1.Height / 2));
        }

        private void wyrysuj_skale()
        {
            for (int i = 1; i < 91; i++)
            {
                grafika_skali.Transform(matriks1_skali);
                if (i % 5 == 0)
                {
                    g_skali.DrawPath(new Pen(Color.Black, 2), grafika_skali);
                }

                g_skali.DrawPath(new Pen(Color.Black, 1), grafika_skali);
            }
        }


        private void odswiez_skale()
        {
            Inicjalizacja_skali();
            wyrysuj_skale();
        }
        #endregion


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
            rysuj_tarcze(flaga_zera);  // dodane 02.03.2011
        }

        private void bttnUstaw_Click(object sender, EventArgs e)
        {
            try
            {

                //wartosc_zadana = float.TryParse(txtbxwartosc_zadana.Text,out wart_zadana);      
                if (float.TryParse(txtbxwartosc_zadana.Text, out wartosc_zadana) == true)
                {


                    if (wartosc_zadana > 60)
                    {
                        wartosc_zadana = wartosc_aktualna;
                        MessageBox.Show("Zadana temperatura jest za du¿a wartosc zadana nie zostanie zmieniona");
                    }
                    if (wartosc_zadana < -30)
                    {
                        wartosc_zadana = wartosc_aktualna;
                        MessageBox.Show("Zadana temperatura jest za niska wartosc zadana nie zostanie zmieniona");
                    }



                    if (wartosc_aktualna != wartosc_zadana)
                    {
                        bttnUstaw.Enabled = false;
                        timer1.Enabled = true;
                        timer1.Start();
                    }
                }
                else
                {
                    MessageBox.Show("B³¹d konwersji temperatury");
                    txtbxwartosc_zadana.Text = wartosc_aktualna.ToString();
                }
            }
            catch
            {
                MessageBox.Show("B³¹d konwersji zadanej temperatury");
                txtbxwartosc_zadana.Text = txtbxwartosc_aktualna.Text;
                txtbxwartosc_aktualna.ForeColor = Color.YellowGreen;

            }
        
        }

        //inijalizacja labeli
        private void Okno_zegara_Load(object sender, EventArgs e)
        {
            label3.BackColor = Color.FromArgb(223, 223, 223);
            label4.BackColor = Color.FromArgb(223, 223, 223);
            label5.BackColor = Color.FromArgb(223, 223, 223);
            label6.BackColor = Color.FromArgb(223, 223, 223);
            label7.BackColor = Color.FromArgb(223, 223, 223);
            label8.BackColor = Color.FromArgb(223, 223, 223);
            label9.BackColor = Color.FromArgb(223, 223, 223);
            label10.BackColor = Color.FromArgb(223, 223, 223);
            label11.BackColor = Color.FromArgb(223, 223, 223);
            label12.BackColor = Color.FromArgb(223, 223, 223);
            label13.BackColor = Color.FromArgb(223, 223, 223);
            label14.BackColor = Color.FromArgb(223, 223, 223);
            label15.BackColor = Color.FromArgb(223, 223, 223);
            label16.BackColor = Color.FromArgb(223, 223, 223);
            label17.BackColor = Color.FromArgb(223, 223, 223);
            label18.BackColor = Color.FromArgb(223, 223, 223);
            label19.BackColor = Color.FromArgb(223, 223, 223);
            label20.BackColor = Color.FromArgb(223, 223, 223);
            label21.BackColor = Color.FromArgb(223, 223, 223);

            // z klasy statyczna dolacozna z dll statycznie przechowuje user halo i baze :) 
            this.user = Class1.Uzytkownik;
            this.pasword = Class1.Haslo;
            this.baza = Class1.Baza;

            // tworzy po³¹cznei z baza 
            utworz_polaczenie_z_baza();

            //
            //pobierz  parametry czujnika
            id_biblioteki = Formatki.Id_biblioteki;
            id_formatki = Formatki.Id_formatki;


        }

        private void Okno_zegara_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null; // usuwanie referencji na siebie dla singleton
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (backgroundWorker1.CancellationPending == true) // przerwanie petli neiskonczonej 
                {
                    break;
                }

                if (flaga_temp)
                {
                    flaga_temp = false;
                    
                    // jaka temp 

                    pobierz_temp();

                    if (stare_wartosc_pomiaru != nowe_wartosc_pomiaru)  // odrysowuje wskazowke gdy nowa wartosc sie inn aod wczesniejszej 
                    {
                        stare_id_pomiaru = nowe_id_pomiaru;  //przepisanie dla kolejnego pomiaru 
                        stare_wartosc_pomiaru=nowe_wartosc_pomiaru;
                        if (progess_watku == 0)
                        {
                            progess_watku = 1;
                            backgroundWorker1.ReportProgress(0);
                        }
                        else if (progess_watku==1)
                        {
                            progess_watku = 0;
                            backgroundWorker1.ReportProgress(1);
                        }

                                                
                    }

                }
     
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Interval = (int)numericUpDown1.Value * 1000;
            timer2.Start();  
            button2.Enabled = false;
            button1.Enabled = true;
            backgroundWorker1.RunWorkerAsync(); // uruchomienie w¹tku
        }

        private void button1_Click_3(object sender, EventArgs e)
        {

            backgroundWorker1.CancelAsync(); // przerwanie 

            button2.Enabled = true;
            button1.Enabled = false;
            flaga_temp = false; // flaga dla watku kasowanie flagi 

        }


        // przerwanie od licznika 
        private void timer2_Tick(object sender, EventArgs e)
        {
            flaga_temp = true; // ustawiam flage dla watku 

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Koniec pomiarów w czasie rzeczywistym");
        }


        private void utworz_polaczenie_z_baza()
        {
            //tworze nowe po³¹czenie na formatce           
            try
            {
                con = new OracleConnection();
                con.ConnectionString = "User ID =" + this.user + "; Password =" + this.pasword + "; Data Source =" + this.baza;
                con.Open();
                polaczeni_z_baza = true; // ustawiam flage polaczneia z baza 
                odswiez_status_polaczenia();
            }
            catch (Exception ex)
            {
                MessageBox.Show("B³¹d po³¹czenia \n" + ex);
            }

        }

        private void odswiez_status_polaczenia()
        {
            //status polaczenia
            if (polaczeni_z_baza == true)
            {
                toolStripStatusLabel2.Text = "Aktywne po³¹czenie";  //status polaczenia
                toolStripStatusLabel4.Text = baza.ToUpper();  // baza
                toolStripStatusLabel6.Text = user.ToUpper(); // uzytkowni
                
            }
            else
            {
                toolStripStatusLabel2.Text = "Nieaktywne po³¹czenie"; // status polaczenia
                toolStripStatusLabel4.Text = "...";  // baza
                toolStripStatusLabel6.Text = "..."; // uzytkowni
                
            }
        }

        //pobierz dane ip i nr portu z czujnika 
        private void pobierz_temp()
        {

            if (flaga_wykonania)
            {
                cmdQuery = "select p.id,P.WARTOSC from pomiary p where P.ID_CZUJNIKA=" + Formatki.Id_czujnika + " and  rownum=1 order by id desc";

                ODA = new OracleDataAdapter(cmdQuery, Con);
                DS = new DataSet(); // tworzedataset
            }

            ODA.Fill(DS);  // wypelniam dataseta
            DT = DS.Tables[0]; // tablica 0
            DR = DT.Rows[0]; // pierwszy rekord

            nowe_id_pomiaru = DR[0].ToString();
            nowe_wartosc_pomiaru = DR[1].ToString();

        }

        //zmiana progresu tu mam dostep do kontrole ktore sa stworzone w glownym watku 
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtbxwartosc_zadana.Text = nowe_wartosc_pomiaru; // ustawiam wartoœc zadana
            bttnUstaw_Click(sender, e);
        }


    }
}