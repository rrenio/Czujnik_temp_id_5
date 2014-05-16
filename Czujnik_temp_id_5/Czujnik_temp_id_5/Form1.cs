using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client; // dopolaczenia z baza oracle
using Oracle.DataAccess.Types; // do polaczenia z baza oracle 
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;   // dla klienta tcp
using ClassLibrary3_polaczenie; // klasa statyczna przechowujaca id user pas itd 
using ClassLibrary3_formatki;// zawiera id formatki i id biblioteki;
using System.IO; // strumienei 
using System.Threading; //watki

namespace Kontrolkatemp1
{
    public partial class Polaczenie : Form
    {

        OracleConnection con;
        string user = "";
        string pasword = "";
        string baza = "";
        bool polaczeni_z_baza = false;
        string ip = "";
        string port = "";
        TcpClient klient;
        NetworkStream strumien_net;
        System.IO.StreamReader strumien_odczytu;
        System.IO.StreamWriter strumien_zapisu;

        string id_biblioteki = "";
        string id_formatki = "";
        string id_czujnika = "";
        char[] bufor_odczytu = new char[6];
        

        static Polaczenie instance = null; //  dla wzorca singleton

        string dana = "";
        char[] tab = { '+', '-' };
        int znak;
        double liczba = 0;
        string dana1;
        

        //oracle
        String cmdQuery;
        OracleCommand cmd;
        //dodanie parametrow do zapytania;
        OracleParameter p1;
        OracleParameter p2;

        bool flaga_oracle = false;

        //logi 
        FileStream plik;
        StreamWriter zapis;

        bool flaga_wyslania=false;
        bool flaga_kompletna_temp = false;
        string dana_do_wys³ania = "";
        
        string pomocniczy="";
        int dlugosc = -1;
        int gdzie = -1;
        int ilosc_blednych_odczytow = 0;
     
        


        //metoda statyczna implementujaca wzorzec singletona
        public static Polaczenie Instance ()
        {
           if (instance == null)
           instance = new Polaczenie(); //tworznie obiektu 
           return Polaczenie.instance; 
                         
        }

        //wlasnoœæ do ustawienia 
        public OracleConnection Con
        {
            get { return con; }
            set { con = value; }
        }
        
        public Polaczenie()
        {
            InitializeComponent();

        }

        // po zaladowaniu ona 
        private void Polaczenie_Load(object sender, EventArgs e)
        {
            //pobranie z klasy statycznej uztykownika haslo i password  w formie glownej sa do neij przekazywane 
            this.user = Class1.Uzytkownik;
            this.pasword = Class1.Haslo;
            this.baza = Class1.Baza;

            //tworzeni polaczenia zbaza
            utworz_polaczenie_z_baza();

            //pobierz  parametry czujnika
            id_biblioteki =Formatki.Id_biblioteki;
            id_formatki = Formatki.Id_formatki;

            //pobierz ip i pobierz nr portu 
            pobierz_parametry_czujnika();
            
            //otwarcie pliku 
            plik = new FileStream(@"C:\Users\renio\Documents\Visual Studio 2005\Projects\WindowsApplication7\WindowsApplication7\bin\Debug\log.txt", FileMode.OpenOrCreate, FileAccess.Write);

            zapis = new StreamWriter(plik);



        }

        //pobierz dane ip i nr portu z czujnika 
        private  void pobierz_parametry_czujnika()
        {
            String cmdQuery = "select C.IP,C.PORT,C.id from FORMATKI_CZUJNIK fc,CZUJNIKI c where  FC.ID_CZUJNIKA=c.id and FC.ID="+id_formatki;

            OracleDataAdapter ODA = new OracleDataAdapter(cmdQuery, Con);
            DataSet DS = new DataSet(); // tworzedataset
            ODA.Fill(DS);  // wypelniam dataseta
            DataRow DR;
            DataTable DT;
            DT = DS.Tables[0]; // tablica 0
            DR = DT.Rows[0]; // pierwszy rekord

            //ustaiwam iP czujnika i nr portu id czujnika 
            txt_ip.Text = DR[0].ToString();
            txt_nr_portu.Text = DR[1].ToString();
            id_czujnika = DR[2].ToString();

            ip = txt_ip.Text;
            port = txt_nr_portu.Text;

            

            
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
                status_polaczenia_tlstrp.Text = "Aktywne po³¹czenie";  //status polaczenia
                baza_tlstrp.Text = baza.ToUpper();  // baza
                uzytkownik_tlstrp.Text = user.ToUpper(); // uzytkowni
                txt_ip.Text = ip;
                txt_nr_portu.Text = port;
            }
            else
            {
                status_polaczenia_tlstrp.Text = "Nieaktywne po³¹czenie"; // status polaczenia
                baza_tlstrp.Text = "...";  // baza
                uzytkownik_tlstrp.Text = "..."; // uzytkowni
                txt_ip.Text = ip;
                txt_nr_portu.Text = port;
            }
        }

        //pingowanie 
        private void txt_testuj_polaczenie_Click(object sender, EventArgs e)
        {
            Ping p1 = new Ping();
            try
            {
                PingReply odpowiedz = p1.Send(ip);

                if (odpowiedz.Status == IPStatus.Success)
                {
                    MessageBox.Show("OdpowiedŸ z adresu: "+ip+"\n" +"Czas odpowiedzi: "+odpowiedz.RoundtripTime+ " ms \n"+"TTL"+odpowiedz.Options.Ttl, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show("B³ad pinga", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("B³¹d:" + ip + " " + ex.Message);
            }

        }

        //start odczytu
        private void button2_Click(object sender, EventArgs e)
        {
            bttn_start.Enabled = false; // wylaczneie przycisku start
            bttn_stop.Enabled = true; //wlaczenie  przycisku start 

            //blokowanie mozliwosci zmienianie okresu 
            okres_nmrc.Enabled = false;
            //ustawienia timera 
            
            timer2.Interval = (int)okres_nmrc.Value * 1000; // ustawienie okresu
            timer2.Start();    // odliczanie 

            zapis_do_log("start watku zapis a");

            backgroundWorker1_odczyt.RunWorkerAsync();  // uruchomienie watku wysylajace go co n sekund zapytania o temp 
            zapis_do_log("start watku odczyt temp");
            backgroundWorker2_zapis.RunWorkerAsync(); // zapisywanie do bazy 

            zapis_do_log("koniec clikc start");

        }

        private void zapis_do_log(string log)
        {
            zapis.WriteLine(log);
            zapis.Flush();
        }

        //nawi¹zanie po³aczenia po tcp/ip z modulem tibbo
        private void zestaw_polaczenie_tcp_ip()
        {
            try
            {
                klient = new TcpClient(ip, int.Parse(port));

                strumien_net = new NetworkStream(klient.Client);
                strumien_zapisu = new System.IO.StreamWriter(strumien_net);
                strumien_odczytu = new System.IO.StreamReader(strumien_net);

                char[] bufor_odczytu = new char[30];

                strumien_odczytu.ReadBlock(bufor_odczytu, 0, 30);


                string dana = "";
                foreach (char c in bufor_odczytu)
                {
                    dana += c.ToString();
                }
                //Nastapilo polaczenie z modulem

                if (dana == "Nastapilo polaczenie z modulem") // taki komunikatem modu³ tiboo zg³asza sie przy podlaczeniudo niego
                {
                    MessageBox.Show("Nastapi³o po³¹cznei z modu³em o ip:" + ip);
                    bttn_polaczenie.Enabled = false;
                }
                else
                {
                    MessageBox.Show("B³ad przedstawienia sie modu³u");
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("B³ad zapisu do strumienia" + ex.Message);
            }
        }


        private string pobierz_temp()
        {


            strumien_odczytu.BaseStream.ReadTimeout = 2 * 1000 * (int)okres_nmrc.Value; //podwojny okres dla bezpieczenstwa
            try
            {
                strumien_odczytu.ReadBlock(bufor_odczytu, 0, 6); // czytaz az bufor bedzie pelny bo 6 ramek wysylam 
                
                //kasowanie ilosci blednych odczytow 
                ilosc_blednych_odczytow = 0;


                foreach (char b in bufor_odczytu)
                {
                    dana += b.ToString();
                }

                zapis_do_log("odebrana dana:" + dana);
                gdzie = dana.LastIndexOf('+'); // patrze na ktory miejscu jest +
                dana = dana.Remove(0, dana.LastIndexOf('+')); // przsuneicie plusa na poczatek 
                zapis_do_log("dana po przesunieciu dana:" + dana);

                dlugosc = dana.Length;

                if (dlugosc == 6)
                    return dana;
                else
                {
                    for (int j = 0; j < 6; j++) // czyszcenie bufora ile 
                    {
                        bufor_odczytu[j] = '\0';
                    }
                    strumien_odczytu.ReadBlock(bufor_odczytu, 0, gdzie); // doczytuje brakujace znaki 

                }

                foreach (char c in bufor_odczytu)
                {
                    dana += c.ToString();
                }
                zapis_do_log("dana po skorygowaniu :" + dana);
                return dana;
            }
            catch (Exception Ex)
            {
                zapis_do_log("B³ad metody pobierz_temp:" + Ex);
                ilosc_blednych_odczytow++;
                
                //jesli pod rz¹d bylo 3 b ledne konwersje mozliwe ze 
                if (ilosc_blednych_odczytow == 3)
                {
                    przerwij_pomiary();
                }
            }
            
            


            return "+ 22,6";
        }

        //stop pomiarom
        private void bttn_stop_Click(object sender, EventArgs e)
        {
            timer2.Stop(); // zatrzymanie timera
            bttn_stop.Enabled = false; // wy³¹czenie przycisku stop
            bttn_start.Enabled = true; //za³¹czneie przycisku start
            okres_nmrc.Enabled = true;

            flaga_oracle = false;
        }

        //polacznei tcp/ip
        private void bttn_polaczenie_Click(object sender, EventArgs e)
        {
            zestaw_polaczenie_tcp_ip();
            bttn_start.Enabled = true; //wlacz przycisk start
        }

        //zpaisuje dane pomiarowe do bazy 
        private void zapisz_dane_do_bazy(string dana)
        {

            if (flaga_oracle == false)  // raz wykonanie
            {
                // int id_czujnika1;
                // float wartosc1;
                cmdQuery = "insert into POMIARY(id_czujnika,wartosc) values (:id_czujnika,:wartosc)";

                cmd = new OracleCommand(cmdQuery, con);


                //dodanie parametrow do zapytania;
                p1 = new OracleParameter(":id_czujnika", OracleDbType.Int32);
                p2 = new OracleParameter(":wartosc", OracleDbType.Double);

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);

                flaga_oracle = true;

            }
           
            

            try
            {
                //obroka stringa do prawidlowego formatu 
                
                znak = dana.LastIndexOfAny(tab);

                dana= dana.Remove(znak, 1);  //  usuniecie wybranej pozycji 


                dana1 = dana.Trim();  // usuniecie bialej spacji np dla moja liczba +xx.x  wiec jak mam + 5.0  lub +25.0 
                
                
                liczba = double.Parse(dana1); // zamiana stringa na double 
                //ustawienie wartosci zmiennych 
                p1.Value = int.Parse(id_czujnika);
                p2.Value = liczba;



                try
                {
                    cmd.ExecuteReader(CommandBehavior.SingleRow);
                }
                catch (Exception ex)
                {
                    zapis_do_log("Blad zglaszany przez baze podczas zapisu ex:" + ex.Message);

                    //gdy nastapilo rozlaczenie proba ponowna polaczenia
                    if (con.State == ConnectionState.Closed)
                    {
                        zapis_do_log("Blad zerwane po³¹czenie z baz¹ nast¹pi próba ponownego po³aczenia");
                        try
                        {
                            con.Open();
                            
                        }
                        catch
                        {
                            zapis_do_log("Ponowne po³¹czenie do bazy XE wywo³a³o b³ad con.state "+con.State.ToString());
                            
                        }
                    }
                }



            }
            catch (Exception ex1)
            {
                //MessageBox.Show("B³ad  konwersji temperatury" + ex1.ToString()+"temp:"+dana1);
                zapis_do_log("B³ad konwersji temp dana:" + dana+", "+ex1);
            }

         
            

          
        }

        private void Polaczenie_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;  // zerowanie  usuwanie referencji na siebie dla singeltona
        }


        //odpytanie bazy
        private void backgroundWorker1_odczyt_DoWork(object sender, DoWorkEventArgs e)
        {
            zapis_do_log("wewnatrz watku wyslania a");
            while (true)
            {
                try
                {
                    if (backgroundWorker1_odczyt.CancellationPending == true)
                    {
                        break;
                    }

                    if (flaga_wyslania == true)
                    {
                        //wyslanie daniej 
                        zapis_do_log("zapis a do strumienia net");
                        strumien_zapisu.Write("a"); // zapisuje komendte 96h do strumienia net na to czujnik odpowiada 
                        strumien_zapisu.Flush();
                        zapis_do_log("wyslanie a");
                        flaga_wyslania = false; // kasowanie flagi
                    }
                }
                catch (Exception Ex)
                {
                    zapis_do_log("B³ad zapisu do strumienia backgroundWorker1_odczyt_DoWork "+Ex);
                    
                }
            }
        }

        private void backgroundWorker1_odczyt_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            //MessageBox.Show("Pomiar temperatury zakoñczony watek1 zapis do strumienia");
            zapis_do_log("Pomiar temperatury zakoñczony watek1 zapis do strumienia");
            bttn_start.Enabled = true; // wylaczneie przycisku start
            bttn_stop.Enabled = false; //wlaczenie  przycisku stop 

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            flaga_wyslania = true;  // usatw flage wyslanie 
        }

        private void backgroundWorker2_zapis_DoWork(object sender, DoWorkEventArgs e)
        {
            string dana12 = "";
            while (true)
            {
                try
                {
                    if (backgroundWorker2_zapis.CancellationPending == true)
                    {
                        break;
                    }

                    zapis_do_log("przed odczytem temp" + dana12);
                    dana12 = pobierz_temp(); // pobiera temp 
                    dana = ""; // dana  z pobierz_temp
                    zapis_do_log("po odczycie temp:" + dana12);

                    czy_dana_kompletna(dana12);
                    dana12 = "";

                    if (flaga_kompletna_temp)
                    {
                        zapis_do_log("przed zapisem do bazy");
                        zapisz_dane_do_bazy(dana_do_wys³ania);
                        zapis_do_log("po zapisnie do bazy");
                        flaga_kompletna_temp = false;  // blokuje zapis do bazy  odczyt przed przybyciem nastepnej danej 
                    }
                }
                catch (Exception Ex)
                {
                    zapis_do_log("B³ad w backgroundWorker2_zapis_DoWork odczyt ze strumienia "+Ex);
                }
                


            }

        }

        public void czy_dana_kompletna(string dana)
        {
 
            try
            {
                pomocniczy = dana.Substring(dana.IndexOf('+'), 6);
                dana_do_wys³ania = pomocniczy;
                if (dana_do_wys³ania!="+ 22,6")
                flaga_kompletna_temp=true; //ustawiam dla  waktu ze moze zapisac do bazy temp
                
            }
            catch
            {
                // wygaszam nie ma kompletnej danej 
                zapis_do_log("B³ad odczytu temp ze strumeinia net dana:" + dana);
            }


        }

        private void backgroundWorker2_zapis_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            zapis_do_log("Pomiar temperatury zakoñczony watek2 odczyt ze strumienia");
        }

        private void przerwij_pomiary()
        {
            backgroundWorker1_odczyt.CancelAsync();
            backgroundWorker2_zapis.CancelAsync();
            zapis_do_log("Wymuszenie zamkniêcia w¹tków");

        }


       

      

        

       
    }
}