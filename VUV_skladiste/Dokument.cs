﻿using System.Collections.Generic;
        //Podatkovni clanovi
        private string tip;
        //Metode
        public string Tip { get { return tip; } set { tip = value; } }
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
                if(dokuments[i].iznosIzlaz == 0)
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
                        if(Convert.ToInt32(dokuments[j].Datum.Substring(3, 2)) < Convert.ToInt32(dokuments[j + 1].Datum.Substring(3, 2)))
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
            ConsoleTable table1 = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");
            ConsoleTable table2 = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");
            ConsoleTable table3 = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");
            ConsoleTable table4 = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");
            ConsoleTable table5 = new ConsoleTable("Kolicina", "Artikli", "Cijena", "Iznos");
            Console.ReadKey();
        }
    }
}