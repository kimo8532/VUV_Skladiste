using System;using System.Collections.Generic;using ConsoleTables;namespace VUV_skladiste{    internal class Primka : Dokument    {        public Primka(string datum, List<Artikl> artikl, List<decimal> kolicina) : base(datum, artikl, kolicina)        {            IznosIzlaz = 0;            Tip = "PRM";            if (datum == "" || datum == null)            {                base.Datum = DateTime.Now.ToString("dd.MM.yyyy");            }            else            {                base.Datum = datum;            }            Artikl = artikl;            Kolicina = kolicina;            for (int i = 0; i < artikl.Count; i++)            {                IznosUlaz += (decimal)artikl[i].Cijena * (decimal)kolicina[i];            }        }
        //Ispisuje jednu primku po nasem odabiru
        public static void Ispis()        {            List<Primka> primkaList = XML.ListaPrimka();            string[] opcije = new string[primkaList.Count];            int i;            for (i = 0; i < primkaList.Count; i++)            {                opcije[i] = $"{primkaList[i].Datum} {(decimal)primkaList[i].IznosUlaz}";            }            Skladiste Meni = new Skladiste("      Datum   IznosUlaz", opcije);            int trenutniIndeks = Meni.Pokreni();            ConsoleTable table = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");            i = 0;            Console.WriteLine($"{primkaList[trenutniIndeks].Tip} {primkaList[trenutniIndeks].Datum} {primkaList[trenutniIndeks].IznosUlaz}");            foreach (Artikl artikl in primkaList[trenutniIndeks].Artikl)            {                table.AddRow((decimal)primkaList[trenutniIndeks].Kolicina[i], artikl.NazivArtikla, (decimal)artikl.Cijena, (decimal)primkaList[trenutniIndeks].Kolicina[i] * artikl.Cijena);                i++;
            }            table.Write();            Console.ReadKey(true);        }
        //Kreira objekt primka
        public static Primka Dodaj()        {            List<Artikl> artiklizaprimku = new List<Artikl>();            List<decimal> kolicinazaprimku = new List<decimal>();            List<Artikl> artiklList = XML.ListaArtikala();            decimal kolicina;
            do
            {
                string[] opcije = new string[artiklList.Count];
                for (int i = 0; i < artiklList.Count; i++)
                {
                    opcije[i] = $"{artiklList[i].SifraArtikla} {artiklList[i].NazivArtikla} {artiklList[i].Dimenzija} {artiklList[i].Cijena}";
                }
                Skladiste Meni = new Skladiste("Sifra artikla, naziv artikla, dimenzije, cijena", opcije);
                int trenutniIndeks = Meni.Pokreni();
                Console.WriteLine("Koliko hocete tog artikla?");
                if (decimal.TryParse(Console.ReadLine(), out kolicina))
                {
                    if (!(kolicina <= 0))
                    {
                        artiklizaprimku.Add(artiklList[trenutniIndeks]);
                        kolicinazaprimku.Add(kolicina);
                        Console.WriteLine("Ako ste gotovi sa biranjem artikala, pritisnite F5, inace pritisnite bilo koji gumb");
                        ConsoleKeyInfo GumbPritisnuti = Console.ReadKey();
                        if (GumbPritisnuti.Key == ConsoleKey.F5)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Kolicina ne moze biti negativna pokusajte ponovno");
                        Console.WriteLine("Ako ste gotovi sa biranjem artikala, pritisnite F5, inace pritisnite bilo koji gumb");
                        ConsoleKeyInfo GumbPritisnuti = Console.ReadKey();
                        if (GumbPritisnuti.Key == ConsoleKey.F5)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Kolicina ne moze biti u slovima");
                    Console.WriteLine("Ako ste gotovi sa biranjem artikala, pritisnite F5, inace pritisnite bilo koji gumb");
                    ConsoleKeyInfo GumbPritisnuti = Console.ReadKey();
                    if (GumbPritisnuti.Key == ConsoleKey.F5)
                    {
                        break;
                    }
                }
            } while (true);            return new Primka(DateTime.Now.ToString(), artiklizaprimku, kolicinazaprimku);        }    }}