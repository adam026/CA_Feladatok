using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juventus
{
    public class Jatekos
    {
        public int Mezszam { get; set; }
        public string JatekosNeve { get; set; }
        public string JatekosNemzetisege { get; set; }
        public string Poszt { get; set; }
        public int SzuletesiEv { get; set; }

        public Jatekos(string sor)
        {
            var buffer = sor.Split(';');
            Mezszam = int.Parse(buffer[0]);
            JatekosNeve = buffer[1];
            JatekosNemzetisege = buffer[2];
            Poszt = buffer[3];
            SzuletesiEv = int.Parse(buffer[4]);
        }
    }
    internal class Program
    {
        public static List<Jatekos> jatekosok = new List<Jatekos>();
        public static Dictionary<string, int> posztokSzerint = new Dictionary<string, int>();
        public static Dictionary<int, int> evekSzerint = new Dictionary<int, int>();
        static void Main(string[] args)
        {
            Betolt();
            F01();
            F02();
            F03();
            F04();
            F05();
            F06();
            F07();
            F08();
            F09();
            F10();
            Console.ReadKey();
        }

        private static void F10()
        {
            Console.WriteLine("10. Feladat: Fájlbaírás");
        }

        private static void F09()
        {
            Console.Write("9. Feladat: Kérek egy mezszámot: ");
            var mezszam = int.Parse(Console.ReadLine());
            var talalat = jatekosok.SingleOrDefault(x => x.Mezszam == mezszam);
            if(talalat is null)
                Console.WriteLine("\tNincs ilyen játékos!");
            else
                Console.WriteLine("\t" + talalat.JatekosNeve);
        }

        private static void F08()
        {
            Console.WriteLine("8. Feladat: Évek, amikor pontosan 3 játékos született:");
            foreach (var jatekos in jatekosok)
            {
                if (!evekSzerint.ContainsKey(jatekos.SzuletesiEv))
                    evekSzerint.Add(jatekos.SzuletesiEv, 1);
                else
                    evekSzerint[jatekos.SzuletesiEv]++;
            }
            var pontosanHarom = evekSzerint.Where(x => x.Value == 3).ToList();
            foreach (var jatekos in pontosanHarom)
            {
                Console.WriteLine("\t" + jatekos.Key);
            }
            Console.WriteLine();
        }

        private static void F07()
        {
            Console.WriteLine($"7. Feladat: A legidősebb csatár: {jatekosok.Where(x => x.Poszt == "csatár").OrderByDescending(y => y.SzuletesiEv).Last().JatekosNeve}");
            Console.WriteLine();
        }

        private static void F06()
        {
            Console.WriteLine("6. Feladat: ");
            foreach (var jatekos in jatekosok)
            {
                if (!posztokSzerint.ContainsKey(jatekos.Poszt))
                    posztokSzerint.Add(jatekos.Poszt, 1);
                else
                    posztokSzerint[jatekos.Poszt]++;
            }

            foreach (var poszt in posztokSzerint)
            {
                Console.WriteLine($"\t{poszt.Key} poszt : {poszt.Value} személy");
            }
            Console.WriteLine();
        }

        private static void F05()
        {
            Console.WriteLine($"5. Feladat: A csapat átlagéletkora {jatekosok.Average(x => DateTime.Today.Year - x.SzuletesiEv)} év");
            Console.WriteLine();
        }

        private static void F04()
        {
            var legfiatalabb = jatekosok.OrderByDescending(x => DateTime.Today.Year - x.SzuletesiEv).Last();
            Console.WriteLine($"4. Feladat: A legfiatalabb játékos neve: {legfiatalabb.JatekosNeve}");
            Console.WriteLine();
        }

        private static void F03()
        {
            var counter = jatekosok.Where(x => x.JatekosNemzetisege == "olasz").Count();
            Console.WriteLine($"3. Feladat: A csapatnak {counter} db Olasz nemzetiségű jétékosa van.");
            Console.WriteLine();
        }

        private static void F02()
        {
            var counter = jatekosok.Where(x => x.JatekosNemzetisege == "magyar").Count();
            if (counter != 0)
                Console.WriteLine($"2. Feladat: A csapatnak {counter} db Magyar nemzetiségű játékosa van.");
            else
                Console.WriteLine("2. Feladat: A csapatnak nincs Magyar nemzetiségű játékosa.");
            Console.WriteLine();
        }

        private static void F01()
        {
            Console.WriteLine($"1. Feladat: A csapatnak jelenleg {jatekosok.Count} igazolt játékosa van.");
            Console.WriteLine();
        }

        private static void Betolt()
        {
            using (var sr = new StreamReader(@"..\..\RES\juve.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    jatekosok.Add(new Jatekos(sr.ReadLine()));
                }
                
            }
        }
    }
}
