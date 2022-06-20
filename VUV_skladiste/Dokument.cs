using System.Collections.Generic;using System;using ConsoleTables;namespace VUV_skladiste{    internal abstract class Dokument : IDokument    {
        //Podatkovni clanovi
        private string tip;        private string datum;        private List<Artikl> artikl;        private List<decimal> kolicina;        private decimal iznosUlaz;        private decimal iznosIzlaz;
        //Metode
        public string Tip { get { return tip; } set { tip = value; } }        public string Datum { get { return datum; } set { datum = value; } }        public List<Artikl> Artikl { get { return artikl; } set { artikl = value; } }        public List<decimal> Kolicina { get { return kolicina; } set { kolicina = value; } }        public decimal IznosUlaz { get { return iznosUlaz; } set { iznosUlaz = value; } }        public decimal IznosIzlaz { get { return iznosIzlaz; } set { iznosIzlaz = value; } }        public Dokument(string datum, List<Artikl> artikl, List<decimal> kolicina)        {            Datum = datum;            Artikl = artikl;            Kolicina = kolicina;        }        public Dokument()        {        }
        //Funkcija istonira racun, ne brise ga iz base nego dodaje atribut da je istoniran, tom racunu nikad vise ne mozemo pristupiti
        //osim ako promjenimo to rucno u nasoj bazi
        public static void IstonirajRacun()
        {
            ConsoleTable consoleTable = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");
            List<Dokument> dokuments = new List<Dokument>();
            foreach (Primka primka in XML.ListaPrimka())
            {
                dokuments.Add(primka);
            }
            foreach (Izdatnica izdatnica in XML.ListaIzdatnica())
            {
                dokuments.Add(izdatnica);
            }
            string[] opcije = new string[dokuments.Count];
            int i;
            for (i = 0; i < dokuments.Count; i++)
            {
                if (dokuments[i].iznosIzlaz == 0)
                {
                    opcije[i] = $"{dokuments[i].Tip} {dokuments[i].Datum} {dokuments[i].IznosUlaz}";
                }
                else
                {
                    opcije[i] = $"{dokuments[i].Tip} {dokuments[i].Datum} {dokuments[i].IznosIzlaz}";
                }
            }
            Console.WriteLine("Koji racun hocete istonirati?");
            Skladiste Meni = new Skladiste("", opcije);
            int trenutniIndeks = Meni.Pokreni();
            Console.WriteLine("Jeste li sigurni da hocete istonirati ovaj racun? Ako jeste pritisnite enter, inace bilo koji gumb");
            foreach (Artikl artikl in dokuments[trenutniIndeks].Artikl)
            {
                consoleTable.AddRow(dokuments[trenutniIndeks].Kolicina[i], artikl.NazivArtikla, artikl.Cijena, dokuments[trenutniIndeks].Kolicina[i] * artikl.Cijena);
                i++;
            }
            consoleTable.Write();
            ConsoleKeyInfo pritisnutigumb = Console.ReadKey();
            if (ConsoleKey.Enter == pritisnutigumb.Key)
            {
                if (dokuments[trenutniIndeks].iznosIzlaz == 0)
                {
                    XML.DodajAtributIstoniranPrimki((Primka)dokuments[trenutniIndeks]);
                }
                else
                {
                    XML.DodajAtributIstoniranIzdatnici((Izdatnica)dokuments[trenutniIndeks]);
                }
            }
            else
            {
                IstonirajRacun();
            }
        }
        //Ispisuje 5 najnovijih dokumenata napravljenih, ubrajaju se primke i izdatnice
        public static void IspisNajnovijihDokumenata()
        {
            List<Dokument> dokuments = new List<Dokument>();
            foreach (Primka primka in XML.ListaPrimka())
            {
                dokuments.Add(primka);
            }
            foreach (Izdatnica izdatnica in XML.ListaIzdatnica())
            {
                dokuments.Add(izdatnica);
            }
            int n = dokuments.Count;
            int i;
            Random r = new Random();
            for (i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (Convert.ToInt32(dokuments[j].Datum.Substring(6, 4)) < Convert.ToInt32(dokuments[j + 1].Datum.Substring(6, 4)))
                    {
                        Dokument temp = dokuments[j];
                        dokuments[j] = dokuments[j + 1];
                        dokuments[j + 1] = temp;
                    }
                    else if (Convert.ToInt32(dokuments[j].Datum.Substring(6, 4)) == Convert.ToInt32(dokuments[j + 1].Datum.Substring(6, 4)))
                    {
                        if (Convert.ToInt32(dokuments[j].Datum.Substring(3, 2)) < Convert.ToInt32(dokuments[j + 1].Datum.Substring(3, 2)))
                        {
                            Dokument temp = dokuments[j];
                            dokuments[j] = dokuments[j + 1];
                            dokuments[j + 1] = temp;
                        }
                        else if (Convert.ToInt32(dokuments[j].Datum.Substring(3, 2)) == Convert.ToInt32(dokuments[j + 1].Datum.Substring(3, 2)))
                        {
                            if (Convert.ToInt32(dokuments[j].Datum.Substring(0, 2)) < Convert.ToInt32(dokuments[j + 1].Datum.Substring(0, 2)))
                            {
                                Dokument temp = dokuments[j];
                                dokuments[j] = dokuments[j + 1];
                                dokuments[j + 1] = temp;
                            }
                        }
                    }
                }
            }
            ConsoleTable table1 = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");            Console.WriteLine($"{dokuments[0].Tip} {dokuments[0].Datum} {dokuments[0].IznosUlaz} {dokuments[0].IznosIzlaz}");            i = 0;            foreach (Artikl artikl in dokuments[0].Artikl)            {                table1.AddRow((decimal)dokuments[0].Kolicina[i], artikl.NazivArtikla, (decimal)artikl.Cijena, (decimal)dokuments[0].Kolicina[i] * artikl.Cijena);                i++;            }            table1.Write();
            ConsoleTable table2 = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");            Console.WriteLine($"{dokuments[1].Tip} {dokuments[1].Datum} {dokuments[1].IznosUlaz} {dokuments[1].IznosIzlaz}");            i = 0;            foreach (Artikl artikl in dokuments[1].Artikl)            {                table2.AddRow((decimal)dokuments[1].Kolicina[i], artikl.NazivArtikla, (decimal)artikl.Cijena, (decimal)dokuments[1].Kolicina[i] * artikl.Cijena);                i++;            }            table2.Write();
            ConsoleTable table3 = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");            Console.WriteLine($"{dokuments[2].Tip} {dokuments[2].Datum} {dokuments[2].IznosUlaz} {dokuments[2].IznosIzlaz}");            i = 0;            foreach (Artikl artikl in dokuments[2].Artikl)            {                table3.AddRow((decimal)dokuments[2].Kolicina[i], artikl.NazivArtikla, (decimal)artikl.Cijena, (decimal)dokuments[2].Kolicina[i] * artikl.Cijena);                i++;            }            table3.Write();
            ConsoleTable table4 = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");            Console.WriteLine($"{dokuments[3].Tip} {dokuments[3].Datum} {dokuments[3].IznosUlaz} {dokuments[3].IznosIzlaz}");            i = 0;            foreach (Artikl artikl in dokuments[3].Artikl)            {                table4.AddRow((decimal)dokuments[3].Kolicina[i], artikl.NazivArtikla, (decimal)artikl.Cijena, (decimal)dokuments[3].Kolicina[i] * artikl.Cijena);                i++;            }            table4.Write();
            ConsoleTable table5 = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");            Console.WriteLine($"{dokuments[4].Tip} {dokuments[4].Datum} {dokuments[4].IznosUlaz} {dokuments[4].IznosIzlaz}");            i = 0;            foreach (Artikl artikl in dokuments[4].Artikl)            {                table5.AddRow((decimal)dokuments[4].Kolicina[i], artikl.NazivArtikla, (decimal)artikl.Cijena, (decimal)dokuments[4].Kolicina[i] * artikl.Cijena);                i++;            }            table5.Write();
            Console.ReadKey();
        }
    }
}