using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFA
{
    public class Csapat
    {
        public string Orszag { get; set; }
        public int ReszvetelekSzama { get; set; }
        public int LegelsoReszvetel { get; set; }
        public int LegutobbiReszvetel { get; set; }
        public string LegjobbEredmeny { get; set; }

        public Csapat(string sor)
        {
            var buffer = sor.Split(';');

            Orszag = buffer[0];
            ReszvetelekSzama = int.Parse(buffer[1]);
            LegelsoReszvetel = int.Parse(buffer[2]);
            LegutobbiReszvetel = int.Parse(buffer[3]);
            LegjobbEredmeny = buffer[4];
        }
    }
    internal class Program
    {
        static List<Csapat> csapatok = new List<Csapat>();


        static void Main(string[] args)
        {
            Beolvas();
            F01();
            F02();
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

            using (var sw = new StreamWriter(@"..\..\RES\legtobbszor.txt", false, Encoding.UTF8))
            {
                foreach (var csapat in csapatok)
                {
                    if (csapat.ReszvetelekSzama >= 10)
                    {
                        sw.WriteLine($"{csapat.Orszag}: {DateTime.Today.Year - csapat.LegelsoReszvetel}");
                    }
                }
            }
            Console.WriteLine("legtobbszor.txt kész!");
        }

        private static void F07()
        {
            var legelsoVB = csapatok.Min(x => x.LegelsoReszvetel);
            Csapat magyarorszag = csapatok.Single(x => x.Orszag == "Magyarország");

            if(magyarorszag.LegelsoReszvetel != legelsoVB)
                Console.WriteLine("7. Feladat: Magyarország nem volt ott az első VB-n.");
            else
                Console.WriteLine("7. Feladat: Magyarország ott volt az első VB-n.");
            Console.WriteLine();
        }

        private static void F06()
        {
            Console.WriteLine($"6. Feladat: Szlovákia legjobb eredménye: {csapatok.Single(x => x.Orszag == "Szlovákia").LegjobbEredmeny}");
            Console.WriteLine();
        }

        private static void F05()
        {
            Console.WriteLine($"5. Feladat: Eddig {csapatok.Where(x => x.LegjobbEredmeny.Contains("Világbajnok")).Count()} ország csapata volt világbajnok");
            Console.WriteLine();
        }

        private static void F04()
        {
            Console.WriteLine($"4. Feladat: Az első VB-t {csapatok.Min(x => x.LegelsoReszvetel)}-ban rendezték");
            Console.WriteLine();
        }

        private static void F03()
        {
            int counter = 0;

            foreach (var csapat in csapatok)
            {
                if (csapat.Orszag == "Belgium" || csapat.Orszag == "Hollandia" || csapat.Orszag == "Luxemburg")
                    counter += csapat.ReszvetelekSzama;
            }

            Console.WriteLine($"3. Feladat: A BeNeLux országok összesen {counter} alkalommal vettek részt a VB-n");
            Console.WriteLine();
        }

        private static void F02()
        {
            List<Csapat> ketezerTizennyolc = csapatok.Where(x => x.LegutobbiReszvetel == 2018).ToList();
            Console.WriteLine("2. Feladat: 2018-as VB csapatai:");

            for (int i = 1; i <= ketezerTizennyolc.Count; i++)
            {
                Console.Write("\t{0, -10}", ketezerTizennyolc[i - 1].Orszag);
                if (i % 4 == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();

        }

        private static void F01()
        {
            Console.WriteLine($"1. Feladat: Csapatok száma: {csapatok.Count}");
            Console.WriteLine();
        }

        private static void Beolvas()
        {
            using (var sr = new StreamReader(@"..\..\RES\fociVBk.csv", Encoding.UTF8))
            {
                _ = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    csapatok.Add(new Csapat(sr.ReadLine()));
                }
            }
            
        }
    }
}
