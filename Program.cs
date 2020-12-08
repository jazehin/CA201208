using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA201208
{
    class Program
    {
        struct Jatekos
        {
            public int Mezszam;
            public string Nev;
            public string Nemzetiseg;
            public string Poszt;
            public int Szuletett;
            public int Kor; //hogy ne kelljen többször is kiszámolni

            public Jatekos(string sor)
            {
                var t = sor.Split(';');
                Mezszam = int.Parse(t[0]);
                Nev = t[1];
                Nemzetiseg = t[2];
                Poszt = t[3];
                Szuletett = int.Parse(t[4]);
                Kor = DateTime.Now.Year - Szuletett;
        }
        }
        static List<Jatekos> jatekosok = new List<Jatekos>();
        static void Main()
        {
            Beolvas();
            Console.WriteLine("A feladarész");
            A1();
            A2();
            A3();
            A4();
            A5();
            A6();
            A7();
            A8();
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine("B feladarész");
            B();
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine("C feladarész");
            C();
            Console.ReadKey(true);
        }

        private static void C()
        {
            var sw = new StreamWriter(@"..\..\Res\hatvedek.txt", false, Encoding.UTF8);

            foreach (var jatekos in jatekosok)
            {
                if (jatekos.Poszt == "hátvéd") 
                {
                    string csaladnev = jatekos.Nev.Split(' ')[1];
                    sw.WriteLine(csaladnev + (csaladnev.Length > 7 ? "\t" : "\t\t") + jatekos.Kor);

                }
            }

            sw.Flush();
            sw.Close();

            Console.WriteLine("A hátvédek kiírása a hatvedek.txt fájlba megtörtént.");
        }

        private static void B()
        {
            Console.Write("Adjon meg egy mezszámot: ");
            int mezszam = int.Parse(Console.ReadLine());

            int i = 0;
            while (i < jatekosok.Count && jatekosok[i].Mezszam != mezszam) i++;

            if (i < jatekosok.Count) Console.WriteLine($"{jatekosok[i].Nev}");
            else Console.WriteLine("A fájl létrejöttének idejében nincsen ilyen mezszámú játékos a Juventusban.");
        }

        private static void A8()
        {
            Dictionary<int, int> szuletesek = new Dictionary<int, int>();
            foreach (var jatekos in jatekosok)
            {
                if (!szuletesek.ContainsKey(jatekos.Szuletett)) szuletesek.Add(jatekos.Szuletett, 1);
                else szuletesek[jatekos.Szuletett]++;
            }

            Console.WriteLine("8.: A fájl létrejöttének idejében a következő években született pontosan 3 játékos:");
            foreach (var szuletes in szuletesek)
            {
                if (szuletes.Value == 3) Console.WriteLine($"\t{szuletes.Key}");
            }
        }

        private static void A7()
        {
            int mini = 0;
            for (int i = 1; i < jatekosok.Count; i++)
            {
                if (jatekosok[i].Szuletett < jatekosok[mini].Szuletett && jatekosok[i].Poszt == "csatár") mini = i;
            }

            Console.WriteLine($"7.: A fájl létrejöttének idejében {jatekosok[mini].Nev} a legidősebb csatára a Juventusnak.");
        }

        private static void A6()
        {
            Dictionary<string, int> posztok = new Dictionary<string, int>();
            foreach (var jatekos in jatekosok)
            {
                if (!posztok.ContainsKey(jatekos.Poszt)) posztok.Add(jatekos.Poszt, 1);
                else posztok[jatekos.Poszt]++;
            }

            Console.WriteLine("6.: A fájl létrejöttének idejében a posztokon ennyi játékos volt:");
            foreach (var poszt in posztok)
            {
                Console.WriteLine($"\t{poszt.Key} : {poszt.Value}");
            }
        }

        private static void A5()
        {
            int s = 0;
            foreach (var jatekos in jatekosok)
            {
                s += jatekos.Kor;
            }

            Console.WriteLine($"5.: A fájl létrejöttének idejében {(double)s/jatekosok.Count:0.00} az átlagéletkor a Juventusban.");
        }

        private static void A4()
        {
            int maxi = 0;
            for (int i = 1; i < jatekosok.Count; i++)
            {
                if (jatekosok[i].Szuletett > jatekosok[maxi].Szuletett) maxi = i;
            }

            Console.WriteLine($"4.: A fájl létrejöttének idejében {jatekosok[maxi].Nev} a legfiatalabb játékosa a Juventusnak.");
        }

        private static void A3()
        {
            int c = 0;
            foreach (var jatekos in jatekosok)
            {
                if (jatekos.Nemzetiseg == "olasz") c++;
            }

            Console.WriteLine($"3.: A fájl létrejöttének idejében {c} db olasz játékosa van a Juventusnak.");
        }

        private static void A2()
        {
            Console.Write("2.: A fájl létrejöttének idejében a csapatban ");

            int i = 0;
            while (i < jatekosok.Count && jatekosok[i].Nemzetiseg != "magyar") i++;

            if (i < jatekosok.Count) Console.WriteLine("van magyar játékos.");
            else Console.WriteLine("nincs magyar játékos.");
        }

        private static void A1()
        {
            Console.WriteLine($"1.: A fájl létrejöttének idejében {jatekosok.Count} játékosa van a Juventusnak.");
        }

        private static void Beolvas()
        {
            var sr = new StreamReader(@"..\..\Res\juve.txt", Encoding.UTF8, true);

            while (!sr.EndOfStream)
            {
                jatekosok.Add(new Jatekos(sr.ReadLine()));
            }

            sr.Close();
        }
    }
}
