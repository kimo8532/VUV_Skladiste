using System;using System.Collections.Generic;using ConsoleTables;namespace VUV_skladiste{    internal class Izdatnica : Dokument    {        public Izdatnica(string datum, List<Artikl> artikl, List<decimal> kolicina) : base(datum, artikl, kolicina)        {            Tip = "IZD";            IznosUlaz = 0;            if (datum == "" || datum == null)            {                Datum = DateTime.Now.ToString("dd.MM.yyyy");            }            else            {                Datum = datum;            }            Artikl = artikl;            Kolicina = kolicina;            for (int i = 0; i < artikl.Count; i++)            {                IznosIzlaz += decimal.Round(artikl[i].Cijena * kolicina[i], 2);            }        }
        //Ispisuje zadanu izdatnicu koju odaberemo
        public static void Ispis()        {            List<Izdatnica> izdatnicaList = XML.ListaIzdatnica();            string[] opcije = new string[izdatnicaList.Count];            int i;            for (i = 0; i < izdatnicaList.Count; i++)            {                opcije[i] = $"{izdatnicaList[i].Datum} {izdatnicaList[i].IznosIzlaz}";            }            Skladiste Meni = new Skladiste("      Datum  IznosIzlaz", opcije);            int trenutniIndeks = Meni.Pokreni();            ConsoleTable table = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");            i = 0;            Console.WriteLine($"{izdatnicaList[trenutniIndeks].Tip} {izdatnicaList[trenutniIndeks].Datum} {izdatnicaList[trenutniIndeks].IznosUlaz}");            foreach (Artikl artikl in izdatnicaList[trenutniIndeks].Artikl)            {                table.AddRow(izdatnicaList[trenutniIndeks].Kolicina[i], artikl.NazivArtikla, artikl.Cijena, izdatnicaList[trenutniIndeks].Kolicina[i] * artikl.Cijena);                i++;            }            table.Write();            Console.ReadKey(true);        }
        //Dodaje zadanu izdatnicu
        public static Izdatnica Dodaj()        {            List<decimal> kolicinadostupna = XML.KolicinaArtikala();            List<Artikl> artiklizaizdatnicu = new List<Artikl>();            List<decimal> kolicinazaizdatnicu = new List<decimal>();            List<Artikl> artiklList = XML.StanjeSkladista();            decimal kolicina;
            do
            {
                string[] opcije = new string[artiklList.Count];
                for (int i = 0; i < artiklList.Count; i++)
                {
                    opcije[i] = $"{artiklList[i].SifraArtikla} {artiklList[i].NazivArtikla} {artiklList[i].Dimenzija} {kolicinadostupna[i]} {artiklList[i].Cijena}";
                }
                Skladiste Meni = new Skladiste("    Sifra artikla, naziv artikla, dimenzije, kolicina, cijena", opcije);
                int trenutniIndeks = Meni.Pokreni();
                Console.WriteLine("Koliko hocete tog artikla?");
                if (decimal.TryParse(Console.ReadLine(), out kolicina))
                {
                    if (!(kolicina <= 0))
                    {
                        if (kolicinadostupna[trenutniIndeks] >= kolicina)
                        {
                            artiklizaizdatnicu.Add(artiklList[trenutniIndeks]);
                            kolicinazaizdatnicu.Add(kolicina);
                            Console.WriteLine("Ako ste gotovi sa biranjem artikala, pritisnite F5, inace pritisnite bilo koji gumb");
                            ConsoleKeyInfo GumbPritisnuti = Console.ReadKey();
                            if (GumbPritisnuti.Key == ConsoleKey.F5)
                            {
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Ne mozete izdati {kolicina} {artiklList[trenutniIndeks]}, trenutno ima samo {kolicinadostupna[trenutniIndeks]}");
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
            } while (true);
            return new Izdatnica(DateTime.Now.ToString("dd.MM.yyyy"), artiklizaizdatnicu, kolicinazaizdatnicu);
        }        ~Izdatnica()
        {
        }
    }
}