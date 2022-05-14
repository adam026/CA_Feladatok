using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultrabalaton
{
    public class Versenyzo
    {
        public string Nev { get; set; }
        public int Rajtszam { get; set; }
        public string Kategoria { get; set; }
        public TimeSpan IdoEredmeny { get; set; }
        public int Tavszazalek { get; set; }

        public double IdoOraban => IdoEredmeny.TotalHours;

        public Versenyzo(string sor)
        {
            var buffer = sor.Split(';');

            Nev = buffer[0];
            Rajtszam = int.Parse(buffer[1]);
            Kategoria = buffer[2];
            var ido = buffer[3].Split(':');
            IdoEredmeny = new TimeSpan(
                hours: int.Parse(ido[0]),
                minutes: int.Parse(ido[1]),
                seconds: int.Parse(ido[2]));
            Tavszazalek = int.Parse(buffer[4]);

        }
    }
    internal class Program
    {
        static List<Versenyzo> versenyzok = new List<Versenyzo>();

        static void Main(string[] args)
        {
            Beolvasas();
            F03();
            F04();
            F05();
            F07();
            F08();
            Console.ReadKey();

        }

        private static void F08()
        {
            Console.WriteLine("8. Feladat: Verseny győztesei:");

            var gyoztesNo = versenyzok.Where(x => x.Tavszazalek == 100 && x.Kategoria == "Noi").OrderBy(x => x.IdoEredmeny).First();
            var gyoztesFerfi = versenyzok.Where(x => x.Tavszazalek == 100 && x.Kategoria == "Ferfi").OrderBy(x => x.IdoEredmeny).First();

            Console.WriteLine($"\tNők: {gyoztesNo.Nev} ({gyoztesNo.Rajtszam}) - {gyoztesNo.IdoEredmeny}");
            Console.WriteLine($"\tFérfiak: {gyoztesFerfi.Nev} ({gyoztesFerfi.Rajtszam}) - {gyoztesFerfi.IdoEredmeny}");

        }

        private static void F07()
        {
            var tTTFerfiSportolok = versenyzok.Where(x => x.Kategoria == "Ferfi" && x.Tavszazalek == 100).Average(t => t.IdoOraban);
            Console.WriteLine($"7. Feladat: Átlagos idő: {tTTFerfiSportolok} óra");
        }

        private static void F05()
        {
            Console.Write("5. Feladat: Kérlek add meg a sportoló nevét: ");
            var megadottNev = Console.ReadLine();

            var indultE = versenyzok.Where(x => x.Nev.ToLower() == megadottNev.ToLower()).FirstOrDefault();
            bool indult = !(indultE is null);

            Console.Write("\tIndult egyéniben a sportoló? ");
            if (indult)
            {
                Console.WriteLine("\tIgen");
                Console.Write("\tTeljesítette a teljes távot? ");
                if(indultE.Tavszazalek == 100)
                    Console.WriteLine("Igen");
                else
                    Console.WriteLine("Nem");
            }
                
            else
                Console.WriteLine("\t Nem");

        }

        private static void F04()
        {
            var teljesTavotTeljesítoNok = versenyzok.Where(k => k.Kategoria == "Noi" && k.Tavszazalek == 100).Count();
            Console.WriteLine($"4. Feladat: Célba érkező női sportolók: {teljesTavotTeljesítoNok} fő");
        }

        private static void F03()
        {
            var versenyzokSzama = versenyzok.Count();
            Console.WriteLine($"3. Feladat: Egyéni indulók: {versenyzokSzama} fő");
        }

        private static void Beolvasas()
        {
            using (var sr = new StreamReader(@"..\..\RES\ub2017egyeni.txt", Encoding.UTF8))
            {
                _ = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    versenyzok.Add(new Versenyzo(sr.ReadLine()));
                }

            }
        }
    }
}
