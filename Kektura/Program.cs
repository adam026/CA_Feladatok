using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kektura
{
    public class Tura
    {
        public string Kezdopont { get; set; }
        public string Vegpont { get; set; }
        public double Hossz { get; set; }
        public int Emelkedes { get; set; }
        public int Lejtes { get; set; }
        public char Pecsetelohely { get; set; }
        public bool Hianyosnev => Pecsetelohely == 'i' && !Vegpont.Contains("pecsetelohely") ? true : false;

        public Tura(string sor)
        {
            var buffer = sor.Split(';');
            Kezdopont = buffer[0];
            Vegpont = buffer[1];
            Hossz = double.Parse(buffer[2]);
            Emelkedes = int.Parse(buffer[3]);
            Lejtes = int.Parse(buffer[4]);
            Pecsetelohely = char.Parse(buffer[5]);
        }
    }
    internal class Program
    {
        static int kezdetTSZFM = 0;
        static List<Tura> turak = new List<Tura>();
        static Dictionary<string, double> magassagokSzerint = new Dictionary<string, double>();
        static void Main(string[] args)
        {
            Beolvas();
            F03();
            F04();
            F05();
            F07();
            F08();

        }

        private static void F08()
        {
            var aktualisTSZFM = kezdetTSZFM;
            foreach (var tura in turak)
            {
                if(!magassagokSzerint.ContainsKey(tura.Vegpont))
                {
                    aktualisTSZFM = aktualisTSZFM + tura.Emelkedes - tura.Lejtes;
                    magassagokSzerint.Add(tura.Vegpont, aktualisTSZFM);
                }
            }

            var legmagasabb = magassagokSzerint.OrderByDescending(x => x.Value).First();

            Console.WriteLine($"8. Feladat: A túra legmagasabban fekvő pontja: \n\tA végpont neve: {legmagasabb.Key}\n\tA végpont tengerszint feletti magassága: {legmagasabb.Value} m");
        }

        private static void F07()
        {
            Console.WriteLine("7. feladat: Hiányos állomásnevek: ");
            foreach (var allomas in turak)
            {
                if (allomas.Hianyosnev)
                    Console.WriteLine("\t" + allomas.Vegpont);
            }

            
        }

        private static void F05()
        {
            var legrovidebb = turak.OrderByDescending(x => x.Hossz).Last();
            Console.WriteLine($"5. Feladat: A legrövidebb szakasz adatai:\n\tKezdete: {legrovidebb.Kezdopont}\n\tVége: {legrovidebb.Vegpont}\n\tTávolság: {legrovidebb.Hossz}");
        }

        private static void F04()
        {
            Console.WriteLine($"4. Feladat: A túra teljes hossza: {turak.Sum(x => x.Hossz)} km");
        }

        private static void F03()
        {
            Console.WriteLine($"3. Feladat: Szakaszok száma: {turak.Count} db");
        }

        private static void Beolvas()
        {
            using (var sr = new StreamReader(@"..\..\RES\kektura.csv", Encoding.UTF8))
            {
                kezdetTSZFM = int.Parse(sr.ReadLine());
                while (!sr.EndOfStream)
                {
                    turak.Add(new Tura(sr.ReadLine()));
                }
            }
        }
    }
}
