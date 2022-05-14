using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helsinki
{
    public class PontszerzoHelyezes
    {
        public int Helyezes { get; set; }
        public int SportolokSzama { get; set; }
        public string SportagNeve { get; set; }
        public string VersenyszamNeve { get; set; }

        public PontszerzoHelyezes(string sor)
        {
            var buffer = sor.Split(' ');
            Helyezes = int.Parse(buffer[0]);
            SportolokSzama = int.Parse(buffer[1]);
            SportagNeve = buffer[2];
            VersenyszamNeve = buffer[3];
        }
    }
    internal class Program
    {
        static List<PontszerzoHelyezes> helyezesek = new List<PontszerzoHelyezes>();
        static Dictionary<string, int> megszerzettErmek = new Dictionary<string, int>();
        

        static void Main(string[] args)
        {
            Beolvas();
            F03();
            F04();
            F05();
            F06();
            F07();
            F08();
            Console.ReadKey();
        }

        private static void F08()
        {
            var legtobbSportolo = helyezesek.OrderByDescending(x => x.SportolokSzama).First();
            Console.WriteLine($"8. Feladat:\n\tSportág: {legtobbSportolo.SportagNeve}\n\tVersenyszám: {legtobbSportolo.VersenyszamNeve}\n\tSportolók száma: {legtobbSportolo.SportolokSzama}");
        }

        private static void F07()
        {
            using (var sw = new StreamWriter(@"..\..\RES\helsinki2.txt", false, Encoding.UTF8))
            {
                foreach (var helyezes in helyezesek)
                {
                    var olimpiaiPont = 0;

                    switch (helyezes.Helyezes)
                    {
                        case (1):
                            olimpiaiPont = 7;
                            break;
                        case (2):
                            olimpiaiPont = 5;
                            break;
                        case (3):
                            olimpiaiPont = 4;
                            break;
                        case (4):
                            olimpiaiPont = 3;
                            break;
                        case (5):
                            olimpiaiPont = 2;
                            break;
                        case (6):
                            olimpiaiPont= 1;
                            break;
                    }


                    var sportagNeve = "";
                    if (helyezes.SportagNeve == "kajakkenu")
                        sportagNeve = "kajak-kenu";
                    else
                        sportagNeve = helyezes.SportagNeve;

                    sw.WriteLine($"{helyezes.Helyezes} {helyezes.SportolokSzama} {olimpiaiPont} {sportagNeve} {helyezes.VersenyszamNeve}");
                }

                Console.WriteLine("7. Feladat: \n\tKiírás befejezve");
            }
        }

        private static void F06()
        {
            Console.WriteLine("6. Feladat: ");
            var uszas = 0;
            var torna = 0;

            foreach (var sportag in helyezesek)
            {
                if (sportag.SportagNeve == "uszas")
                    uszas++;
                else if (sportag.SportagNeve == "torna")
                    torna++;
            }

            if (torna > uszas)
                Console.WriteLine("\tTorna sportágban szereztek több érmet");
            else if (uszas > torna)
                Console.WriteLine("\tÚszás sportágban szereztek több érmet");
            else if (torna == uszas)
                Console.WriteLine("\tEgyenlő volt az érmek száma");

        }

        private static void F05()
        {
            var pontOsszeg = 0;

            foreach (var helyezes in helyezesek)
            {
                switch (helyezes.Helyezes)
                {
                    case (1):
                        pontOsszeg += 7;
                        break;
                    case (2):
                        pontOsszeg += 5;
                        break;
                    case (3):
                        pontOsszeg += 4;
                        break;
                    case (4):
                        pontOsszeg += 3;
                        break;
                    case (5):
                        pontOsszeg += 2;
                        break;
                    case (6):
                        pontOsszeg += 1;
                        break;
                }
            }

            Console.WriteLine($"5. Feladat: \n\tOlimpiai pontok száma: {pontOsszeg}");
        }

        private static void F04()
        {
            foreach (var helyezes in helyezesek)
            {
                var megnevezes = "";
                if (helyezes.Helyezes == 1)
                    megnevezes = "Arany";
                else if (helyezes.Helyezes == 2)
                    megnevezes = "Ezüst";
                else if (helyezes.Helyezes == 3)
                    megnevezes = "Bronz";
                else
                    continue;

                if (!megszerzettErmek.ContainsKey(megnevezes))
                    megszerzettErmek.Add(megnevezes, 1);
                else
                    megszerzettErmek[megnevezes]++;
            }

            Console.WriteLine("4. Feladat: ");
            foreach (var erme in megszerzettErmek)
            {
                Console.WriteLine($"\t{erme.Key}: {erme.Value}");
            }
            Console.WriteLine($"\tÖsszesen: {megszerzettErmek.Sum(x => x.Value)}");
        }

        private static void F03()
        {
            Console.WriteLine($"3. Feladat: \n\tPontszerző helyzések száma: {helyezesek.Count}");
        }

        private static void Beolvas()
        {
            using (var sr = new StreamReader(@"..\..\RES\helsinki.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    helyezesek.Add(new PontszerzoHelyezes(sr.ReadLine()));
                }
            }
        }
    }
}
