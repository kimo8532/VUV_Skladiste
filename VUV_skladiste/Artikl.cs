using System;using System.Collections.Generic;using ConsoleTables;namespace VUV_skladiste{    internal class Artikl    {        private string sifraArtikla;        private string nazivArtikla;        private string dimenzija;        private decimal cijena;        private decimal debljina;        private decimal sirina;        private decimal duzina;        private string mjernaJedinicaDimenzija;        private string cijenaSaMjerom;        private string jmj;        public decimal Debljina { get { return debljina; } set { debljina = value; } }        public decimal Sirina { get { return sirina; } set { sirina = value; } }        public decimal Duzina { get { return duzina; } set { duzina = value; } }        public string SifraArtikla { get { return sifraArtikla; } set { sifraArtikla = value; } }        public string NazivArtikla { get { return nazivArtikla; } set { nazivArtikla = value; } }        //Olaksava ispis!!!!        public string Dimenzija { get { return dimenzija; } set { dimenzija = $"{debljina}x{sirina}x{duzina} {mjernaJedinicaDimenzija}";} }         public decimal Cijena { get { return cijena; } set { cijena = value; } }        public string MjeraZaCijenu { get { return JMJ; } set { JMJ = value; } }        //Olaksava ispis!!!!        public string CijenaSaMjerom { get { return cijenaSaMjerom; } set { cijenaSaMjerom = $"{cijena}/{jmj}"; } }        public string JMJ { get { return jmj; } set { jmj = value; } }        public Artikl(string sifraArtikla, string nazivArtikla, string dimenzija, string cijenasaJMJ)        {            this.sifraArtikla = sifraArtikla;            this.nazivArtikla = nazivArtikla;            this.dimenzija = dimenzija;            Debljina = FDebljina(dimenzija);            Duzina = FDuzina(dimenzija);            Sirina = FSirina(dimenzija);            mjernaJedinicaDimenzija = FMjera(dimenzija);            cijenaSaMjerom = cijenasaJMJ;            Cijena = FCijena(cijenasaJMJ);            JMJ = FJMJ(cijenasaJMJ);        }        //Metode koje vuce iz stringa dimenzija dimenzije i njihovu mjernu jedinicu        public static decimal FDebljina(string dimenzija)        {            string temp = dimenzija;            return decimal.Parse(temp.Substring(0, temp.IndexOf('x')));        }        public static decimal FSirina(string dimenzija)        {            string temp = dimenzija;            temp = temp.Substring(temp.IndexOf('x') + 1, temp.Length - temp.IndexOf('x') - 1);            return decimal.Parse(temp.Substring(0, temp.IndexOf('x')));        }        public static decimal FDuzina(string dimenzija)        {            string temp = dimenzija;            temp = temp.Substring(temp.IndexOf('x') + 1, temp.Length - temp.IndexOf('x') - 1);            temp = temp.Substring(temp.IndexOf('x') + 1, temp.Length - temp.IndexOf('x') - 1);            return decimal.Parse(temp.Substring(0, temp.IndexOf(' ')));        }        public static string FMjera(string dimenzija)        {            string temp = dimenzija;            temp = temp.Substring(temp.IndexOf('x') + 1, temp.Length - temp.IndexOf('x') - 1);            temp = temp.Substring(temp.IndexOf('x') + 1, temp.Length - temp.IndexOf('x') - 1);            temp = temp.Substring(temp.IndexOf(' ') + 1, temp.Length - temp.IndexOf(' ') - 1);            return temp;        }        //metoda koja odvaja cijenu od jmj        public static decimal FCijena(string cijenasaJMJ)        {            string temp = cijenasaJMJ;            string temp2 = temp;            temp2 = temp2.Substring(temp.IndexOf('/'), temp.Length - temp.IndexOf('/'));            temp = temp.Substring(0, temp.Length - temp2.Length);            decimal value = decimal.Parse(temp);            return value;        }
        //metoda koja odvaja jmj od cijene
        public static string FJMJ(string cijenasaJMJ)        {            string temp = cijenasaJMJ;            temp = temp.Substring(temp.IndexOf('/')+ 1, temp.Length - temp.IndexOf('/') - 1);            return temp;        }
        //Metoda dohvaca artikl, koristimo ju da vidimo postoji li taj artikl ili ne
        public static Artikl DohvatiArtikl(string sifra)        {            List<Artikl> artikli = XML.ListaArtikala();            foreach (Artikl artikl in artikli)            {                if (artikl.SifraArtikla == sifra)                {                    return artikl;                }            }            return null;        }        //Izraduje artikl, tijekom netocnog unosa unosimo cijeli artikl ispocetka        public static void NoviArtikl()        {            try
            {
                Console.Clear();
                Console.WriteLine("Upisite sifru artikla");
                string sifraArtikla = Console.ReadLine();
                if (DohvatiArtikl(sifraArtikla) != null)
                {
                    throw new Iznimka("Postoji artikl sa zadanom sifrom");
                }
                if( sifraArtikla == null || !int.TryParse(sifraArtikla, out _) || sifraArtikla.Contains("-"))
                {
                    throw new Iznimka("Sifra netocno upisana, sadrzi slova, simbole ili je prazna!");
                }
                Console.WriteLine("Upisite naziv artikla");
                string nazivArtikla = Console.ReadLine();
                if (nazivArtikla == "")
                {
                    throw new Iznimka("Naziv ne moze biti prazan");
                }
                Console.WriteLine("Upisite debljinu");
                if (decimal.TryParse(Console.ReadLine(), out decimal debljina))
                {
                    if (debljina <= 0)
                    {
                        throw new Iznimka("Debljina ne mozez biti negativna i vrijednosti nula!");
                    }
                }
                else
                {
                    throw new Iznimka("Debljina ne moze sadrzavati simbole i slova");
                }
                Console.WriteLine("Upisite sirinu");
                if (decimal.TryParse(Console.ReadLine(), out decimal sirina))
                {
                    if (sirina <= 0)
                    {
                        throw new Iznimka("Sirina ne moze biti negativna i vrijednosti nula!");
                    }
                }
                else
                {
                    throw new Iznimka("Sirina ne moze sadrzavati simbole i slova");
                }
                Console.WriteLine("Upisite duzinu");
                if (decimal.TryParse(Console.ReadLine(), out decimal duzina))
                {
                    if (duzina <= 0)
                    {
                        throw new Iznimka("Duzina ne mozez biti negativna i vrijednosti nula!");
                    }
                }
                else
                {
                    throw new Iznimka("Duzina ne moze sadrzavati simbole i slova");
                }
                Console.WriteLine("Upisite mjernu jedinicu dimenzija");
                string mjernaJedinicaDimenzija = Console.ReadLine();
                Console.WriteLine("Upisite cijenu!");
                if (decimal.TryParse(Console.ReadLine(), out decimal cijena))
                {
                    if (cijena <= 0)
                    {
                        throw new Iznimka("Cijena ne moze biti negativna i vrijednosti nula!");
                    }
                }
                else
                {
                    throw new Iznimka("Cijena ne moze sadrzavati simbole i slova");
                }
                Console.WriteLine("Upisite JMJ po kojoj se odreduje cijena");
                Console.WriteLine("Ako se cijena odreduje po komadima pritisnite ENTER");
                string JMJ = Console.ReadLine();
                if (JMJ == "")
                {
                    JMJ = "kom";
                }
                Console.WriteLine("Jeste li sigurni da hocete napraviti ovaj artikl?");
                Console.WriteLine($"Sifra:{sifraArtikla}'\n'Naziv:{nazivArtikla}'\n'" +
                    $"Dimenzije:{debljina}x{duzina}x{sirina} {mjernaJedinicaDimenzija}'\n'Cijena:{cijena}/{JMJ}'\n'");
                Console.WriteLine("Pritisnite F5 za spremanje");
                ConsoleKeyInfo GumbPritisnuti = Console.ReadKey();
                if (GumbPritisnuti.Key == ConsoleKey.F5)
                {
                    XML.NoviArtikl(sifraArtikla, nazivArtikla, cijena, JMJ, debljina, sirina, duzina, mjernaJedinicaDimenzija);
                }
            }            catch (Iznimka i)            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(i.Message);
                Console.ResetColor();
                Console.WriteLine("Za izlaz pritisnite escape, za nastavak pritisnite bilo koji gumb");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    Skladiste.Start();
                }
                else
                {
                    Console.Clear();
                    NoviArtikl();
                }
            }        }        //Ispisuje sve artikle, dostupne i ne dostupne        public static void IspisSvihArtikala()        {            Console.Clear();            List<Artikl> lista = XML.ListaArtikala();            ConsoleTable table = new ConsoleTable("Sifra", "Naziv", "Dimenzije", "JMJ", "Cijena");            foreach (Artikl artikl in lista)            {                table.AddRow(artikl.sifraArtikla, artikl.nazivArtikla, artikl.Dimenzija, artikl.JMJ, artikl.Cijena);            }            table.Write();            Console.ReadKey(true);        }        //Azurira artikl i unosi ga u XML        public static void AzurirajArtikl()
        {
            Console.Clear();
            List<Artikl> artiklList = XML.ListaArtikala();
            string[] opcije = new string[artiklList.Count + 1];
            for (int i = 0; i < artiklList.Count; i++)
            {
                opcije[i] = $"{artiklList[i].SifraArtikla} {artiklList[i].NazivArtikla} {artiklList[i].Dimenzija} {artiklList[i].Cijena}";
            }
            opcije[artiklList.Count] = "Izlaz";
            Skladiste Meni = new Skladiste("Sifra artikla, naziv artikla, dimenzije, cijena", opcije);
            int trenutniIndeks = Meni.Pokreni();
            if(trenutniIndeks == artiklList.Count)
            {
                Console.Clear();
                Skladiste.PocniAplikaciju(Skladiste.VuvLogo());
            }
            Console.WriteLine("Koju stavku hocete promjeniti?");
            string[] opcije2 = new string[4];
            opcije2[0] = "Naziv artikla";
            opcije2[1] = "Dimenzije";
            opcije2[2] = "Cijena";
            opcije2[3] = "Odabir artikla";
            Skladiste Meni2 = new Skladiste("", opcije2);
            int trenutniIndeks2 = Meni2.Pokreni();
            string[] opcije3 = new string[4];
            string[] opcije4 = new string[2];
            Skladiste Meni3 = new Skladiste("", opcije3);
            Skladiste Meni4 = new Skladiste("", opcije4);
            int trenutniIndeks3;
            string promjena;
            switch (trenutniIndeks2)
            {
                case 0:
                    {
                        Console.WriteLine("Jeste li sigurni da hocete dimenzije azurirati? Ako niste pritisnite escape.");
                        if (Console.ReadKey().Key == ConsoleKey.Escape)
                        {
                            AzurirajArtikl();
                        }
                        else
                        {
                            while (true)
                            {
                                Console.WriteLine($"Trenutni naziv je {artiklList[trenutniIndeks].NazivArtikla}, u koji naziv hocete ju promjeniti");
                                promjena = Console.ReadLine();
                                if (promjena == "")
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Pokusajte ponovno, krivo ste unjeli naziv");
                                    Console.ResetColor();
                                    Console.WriteLine("Jeste li sigurni da hocete naziv azurirati? Ako niste pritisnite escape.");
                                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                                    {
                                        AzurirajArtikl();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Jeste li sigurni da hocete naziv azurirati? Ako niste pritisnite escape.");
                                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                                    {
                                        AzurirajArtikl();
                                    }
                                    break;
                                }
                            }
                            XML.AzurirajArtikl("Naziv", promjena, artiklList[trenutniIndeks].SifraArtikla);
                            break;
                        }
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("Jeste li sigurni da hocete dimenzije azurirati? Ako niste pritisnite escape.");
                        if (Console.ReadKey().Key == ConsoleKey.Escape)
                        {
                            AzurirajArtikl();
                        }
                        else
                        {
                            opcije3[0] = "Debljina";
                            opcije3[1] = "Sirina";
                            opcije3[2] = "Duzina";
                            opcije3[3] = "Mjerna jedinica";
                            trenutniIndeks3 = Meni3.Pokreni();
                            switch (trenutniIndeks3)
                            {
                                case 0:
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine($"Trenutna debljina je {artiklList[trenutniIndeks].Debljina}, u koju debljinu hocete ju promjeniti");
                                            promjena = Console.ReadLine();
                                            if (!decimal.TryParse(promjena, out _) || promjena == "" || promjena.Contains("-") || promjena == "0")
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Pokusaj ponovno! Krivo ste unijeli debljinu");
                                                Console.ResetColor();
                                                Console.WriteLine("Jeste li sigurni da hocete debljinu azurirati? Ako niste pritisnite escape.");
                                                if (Console.ReadKey().Key == ConsoleKey.Escape)
                                                {
                                                    AzurirajArtikl();
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Jeste li sigurni da hocete debljinu azurirati? Ako niste pritisnite escape.");
                                                if (Console.ReadKey().Key == ConsoleKey.Escape)
                                                {
                                                    AzurirajArtikl();
                                                }
                                                break;
                                            }
                                        }
                                        XML.AzurirajArtikl("Debljina", promjena, artiklList[trenutniIndeks].SifraArtikla);
                                        break;
                                    }
                                case 1:
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine($"Trenutna sirina je {artiklList[trenutniIndeks].Sirina}, u koju sirinu hocete ju promjeniti");
                                            promjena = Console.ReadLine();
                                            if (!decimal.TryParse(promjena, out _) || promjena == "" || promjena.Contains("-") || promjena == "0")
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Pokusaj ponovno! Krivo ste unijeli sirinu");
                                                Console.ResetColor();
                                                Console.WriteLine("Jeste li sigurni da hocete sirinu azurirati? Ako niste pritisnite escape.");
                                                if (Console.ReadKey().Key == ConsoleKey.Escape)
                                                {
                                                    AzurirajArtikl();
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Jeste li sigurni da hocete sirinu azurirati? Ako niste pritisnite escape.");
                                                if (Console.ReadKey().Key == ConsoleKey.Escape)
                                                {
                                                    AzurirajArtikl();
                                                }
                                                break;
                                            }
                                        }
                                        XML.AzurirajArtikl("Sirina", promjena, artiklList[trenutniIndeks].SifraArtikla);
                                        break;
                                    }
                                case 2:
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine($"Trenutna duzina je {artiklList[trenutniIndeks].Duzina}, u koju duzinu hocete ju promjeniti");
                                            promjena = Console.ReadLine();
                                            if (!decimal.TryParse(promjena, out _) || promjena == "" || promjena.Contains("-") || promjena == "0")
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Pokusaj ponovno! Krivo ste unijeli duzinu");
                                                Console.ResetColor();
                                                Console.WriteLine("Jeste li sigurni da hocete duzinu azurirati? Ako niste pritisnite escape.");
                                                if (Console.ReadKey().Key == ConsoleKey.Escape)
                                                {
                                                    AzurirajArtikl();
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Jeste li sigurni da hocete duzinu azurirati? Ako niste pritisnite escape.");
                                                if (Console.ReadKey().Key == ConsoleKey.Escape)
                                                {
                                                    AzurirajArtikl();
                                                }
                                                break;
                                            }
                                        }
                                        XML.AzurirajArtikl("Duzina", promjena, artiklList[trenutniIndeks].SifraArtikla);
                                        break;
                                    }
                                case 3:
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine($"Trenutni mjerna jedinica je {artiklList[trenutniIndeks].mjernaJedinicaDimenzija}," +
                                                $" u koju mjernu jedinicu hocete ju promjeniti");
                                            promjena = Console.ReadLine();
                                            if (promjena == "")
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Pokusajte ponovno, ostavili ste mjernu jedinicu praznom");
                                                Console.ResetColor();
                                                Console.WriteLine("Jeste li sigurni da hocete mjernu jedinicu azurirati? Ako niste pritisnite escape.");
                                                if (Console.ReadKey().Key == ConsoleKey.Escape)
                                                {
                                                    AzurirajArtikl();
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Jeste li sigurni da hocete mjernu jedinicu azurirati? Ako niste pritisnite escape.");
                                                if (Console.ReadKey().Key == ConsoleKey.Escape)
                                                {
                                                    AzurirajArtikl();
                                                }
                                                break;
                                            }
                                        }
                                    XML.AzurirajArtikl("MjernaJedinica", promjena, artiklList[trenutniIndeks].SifraArtikla);
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Jeste li sigurni da hocete cijenu azurirati? Ako niste pritisnite escape.");
                        if(Console.ReadKey().Key == ConsoleKey.Escape)
                        {
                            AzurirajArtikl();
                        }
                        else
                        {
                            opcije4[0] = "Cijena";
                            opcije4[1] = "JMJ";
                            trenutniIndeks3 = Meni4.Pokreni();
                            switch (trenutniIndeks3)
                            {
                                case 0:
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine($"Trenutna cijena je {artiklList[trenutniIndeks].Cijena}, u koju cijenu hocete ju promjeniti");
                                            promjena = Console.ReadLine();
                                            if (!decimal.TryParse(promjena, out _) || promjena == "" || promjena.Contains("-") || promjena == "0")
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Pokusaj ponovno! Krivo ste unijeli cijenu");
                                                Console.ResetColor();
                                                Console.WriteLine("Jeste li sigurni da hocete cijenu azurirati? Ako niste pritisnite escape.");
                                                if (Console.ReadKey().Key == ConsoleKey.Escape)
                                                {
                                                    AzurirajArtikl();
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Jeste li sigurni da hocete cijenu azurirati? Ako niste pritisnite escape.");
                                                if (Console.ReadKey().Key == ConsoleKey.Escape)
                                                {
                                                    AzurirajArtikl();
                                                }
                                                break;
                                            }
                                        }
                                        XML.AzurirajArtikl("Cijena", promjena, artiklList[trenutniIndeks].SifraArtikla);
                                        break;
                                    }
                                case 1:
                                    {
                                        Console.WriteLine($"Trenutni JMJ je {artiklList[trenutniIndeks].Duzina}, u koji JMJ hocete ju promjeniti");
                                        Console.WriteLine("Ako ostavite prazno JMJ ce automatski biti postavljeno u kom");
                                        promjena = Console.ReadLine();
                                        if (promjena == "")
                                        {
                                            promjena = "kom";
                                            Console.WriteLine("Jeste li sigurni da hocete JMJ azurirati? Ako niste pritisnite escape.");
                                            if (Console.ReadKey().Key == ConsoleKey.Escape)
                                            {
                                                AzurirajArtikl();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Jeste li sigurni da hocete JMJ azurirati? Ako niste pritisnite escape.");
                                            if (Console.ReadKey().Key == ConsoleKey.Escape)
                                            {
                                                AzurirajArtikl();
                                            }
                                        }
                                        XML.AzurirajArtikl("JMJ", promjena, artiklList[trenutniIndeks].SifraArtikla);
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        AzurirajArtikl();
                        break;
                    }
            }
        }    }}