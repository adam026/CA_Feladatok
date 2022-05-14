using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanic
{
    public class Kategoria
    {
        public string KategoriNev { get; set; }
        public int TuleloUtasok { get; set; }
        public int EltuntUtasok { get; set; }

        public Kategoria(string sor)
        {
            var buffer = sor.Split(';');
            KategoriNev = buffer[0];
            TuleloUtasok = int.Parse(buffer[1]);
            EltuntUtasok = int.Parse(buffer[2]);
        }
    }
    internal class Program
    {
        static List<Kategoria> kategoriak = new List<Kategoria>();
        static Dictionary<string, int> talalatok = new Dictionary<string, int>();
        static List<string> eltuntek = new List<string>();
        static void Main(string[] args)
        {
            Beolvas();
            F02();
            F03();
            F04();
            F06();
            F07();
            Console.ReadLine();
        }

        private static void F07()
        {
            var legtobbTulelo = kategoriak.OrderByDescending(x => x.TuleloUtasok).First();
            Console.WriteLine($"7. feladat: {legtobbTulelo.KategoriNev}");
        }

        private static void F06()
        {
            foreach (var kategoria in kategoriak)
            {
                if ((kategoria.EltuntUtasok * 100 / (kategoria.TuleloUtasok + kategoria.EltuntUtasok)) > 60)
                {
                    eltuntek.Add(kategoria.KategoriNev);
                }
            }

            Console.WriteLine("6. feladat: ");
            foreach (var kategoria in eltuntek)
            {
                Console.WriteLine($"\t{kategoria}");
            }
        }

        private static void F04()
        {
            Console.Write("4. feladat: Kulcsszó: ");
            var kulcsszo = Console.ReadLine();
            var talalat = 0;
            foreach (var kategoria in kategoriak)
            {
                if (kategoria.KategoriNev.Contains(kulcsszo.ToLower()))
                {
                    if (!talalatok.ContainsKey(kategoria.KategoriNev))
                    {
                        talalatok.Add(kategoria.KategoriNev, kategoria.TuleloUtasok + kategoria.EltuntUtasok);
                    }
                    else
                    {
                        talalatok[kategoria.KategoriNev] += kategoria.TuleloUtasok + kategoria.EltuntUtasok;
                    }
                    talalat++;
                }
                    
            }

            if (talalat != 0)
            {
                Console.WriteLine("\tVan találat!");

                Console.WriteLine("5. feladat:");

                foreach (var kategoria in talalatok)
                {
                    Console.WriteLine($"\t{kategoria.Key} {kategoria.Value} fő");
                }
            }
            else
                Console.WriteLine("\tNincs találat!");

            

        }

        private static void F03()
        {
            var osszutasLetszam = kategoriak.Sum(x => x.EltuntUtasok) + kategoriak.Sum(y => y.TuleloUtasok);
            Console.WriteLine($"3. feladat: {osszutasLetszam} fő");
        }

        private static void F02()
        {
            Console.WriteLine($"2. Feladat: {kategoriak.Count} db");
        }

        private static void Beolvas()
        {
            using (var sr = new StreamReader(@"..\..\RES\titanic.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    kategoriak.Add(new Kategoria(sr.ReadLine()));
                }
                
            }
        }
    }
}
