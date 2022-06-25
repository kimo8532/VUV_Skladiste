using System;using System.Collections.Generic;using System.Xml;using System.IO;using ConsoleTables;namespace VUV_skladiste{    internal class XML    {
        //Kreira novi artikl koristeci ulazne parametre
        public static void NoviArtikl(string sifraArtikla, string nazivArtikla, decimal cijena, string mjera, decimal debljina, decimal sirina, decimal duzina, string mjernaJedinicaDimenzija)        {            XmlDocument xmlDocument = new XmlDocument();            xmlDocument.Load("Artikl.xml");            XmlNode mainNode = xmlDocument.SelectSingleNode("//Artikli");            XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "Artikl", null);            XmlAttribute sifra = xmlDocument.CreateAttribute("Sifra");            sifra.Value = sifraArtikla;            XmlAttribute naziv = xmlDocument.CreateAttribute("Naziv");            naziv.Value = nazivArtikla;            XmlAttribute cijenaa = xmlDocument.CreateAttribute("Cijena");            XmlAttribute mjeraa = xmlDocument.CreateAttribute("JMJ");            cijenaa.Value = cijena.ToString();            mjeraa.Value = mjera;            XmlNode xmlChildNode = xmlDocument.CreateNode(XmlNodeType.Element, "Dimenzija", null);            XmlAttribute debljinaa = xmlDocument.CreateAttribute("Debljina");            debljinaa.Value = debljina.ToString();            XmlAttribute sirinaa = xmlDocument.CreateAttribute("Sirina");            sirinaa.Value = sirina.ToString();            XmlAttribute duzinaa = xmlDocument.CreateAttribute("Duzina");            duzinaa.Value = duzina.ToString();            XmlAttribute mjernaJedinica = xmlDocument.CreateAttribute("MjernaJedinica");            mjernaJedinica.Value = mjernaJedinicaDimenzija;            xmlChildNode.Attributes.Append(debljinaa);            xmlChildNode.Attributes.Append(sirinaa);            xmlChildNode.Attributes.Append(duzinaa);            xmlChildNode.Attributes.Append(mjernaJedinica);            xmlNode.Attributes.Append(sifra);            xmlNode.Attributes.Append(naziv);            xmlNode.Attributes.Append(cijenaa);            xmlNode.Attributes.Append(mjeraa);            xmlNode.AppendChild(xmlChildNode);            mainNode.AppendChild(xmlNode);            xmlDocument.Save("Artikl.xml");        }
        //Vuce sve artikle iz Artikl.XML
        public static List<Artikl> ListaArtikala()        {            List<Artikl> artikli = new List<Artikl>();            string xml = "";            StreamReader sr = new StreamReader("Artikl.xml");            using (sr)            {                xml = sr.ReadToEnd();            }            XmlDocument xmlDocument = new XmlDocument();            xmlDocument.LoadXml(xml);            XmlNodeList Xmlartikli = xmlDocument.SelectNodes("//Artikli/Artikl");            foreach (XmlNode artikl in Xmlartikli)            {
                artikli.Add(new Artikl(artikl.Attributes["Sifra"].Value, artikl.Attributes["Naziv"].Value, $"{artikl.LastChild.Attributes["Debljina"].Value}x{artikl.LastChild.Attributes["Sirina"].Value}x{artikl.LastChild.Attributes["Duzina"].Value} " +
                $"{artikl.LastChild.Attributes["MjernaJedinica"].Value}", $"{artikl.Attributes["Cijena"].Value}/{artikl.Attributes["JMJ"].Value}"));            }            return artikli;        }
        //Bitna provjera svih XMLova, ako je ne ispravan ne mozemo pokrenuti program
        public static void ProvjeraPrijePokretanja()        {            string xml = "";            StreamReader sr = new StreamReader("Artikl.xml");            using (sr)            {                xml = sr.ReadToEnd();            }            XmlDocument xmlDocument = new XmlDocument();            xmlDocument.LoadXml(xml);            XmlNodeList Xmlartikli = xmlDocument.SelectNodes("//Artikli/Artikl");            foreach (XmlNode XmlartikliNode in Xmlartikli)            {
                if (XmlartikliNode.Attributes["Naziv"].Value == "")                {                    throw new Iznimka("Naziv ne moze biti prazan! Datoteka Artikl.XML");                }                if (decimal.TryParse(XmlartikliNode.LastChild.Attributes["Debljina"].Value, out decimal debljina))                {                    if (debljina <= 0)                    {                        throw new Iznimka("Debljina ne mozez biti negativna i vrijednosti nula! Datoteka Artikl.XML");                    }                }                else                {                    throw new Iznimka("Debljina ne moze sadrzavati simbole i slova! Datoteka Artikl.XML");                }                if (decimal.TryParse(XmlartikliNode.LastChild.Attributes["Sirina"].Value, out decimal sirina))                {                    if (sirina <= 0)                    {                        throw new Iznimka("Sirina ne moze biti negativna i vrijednosti nula! Datoteka Artikl.XML");                    }                }                else                {                    throw new Iznimka("Sirina ne moze sadrzavati simbole i slova! Datoteka Artikl.XML");                }                if (decimal.TryParse(XmlartikliNode.LastChild.Attributes["Duzina"].Value, out decimal duzina))                {                    if (duzina <= 0)                    {                        throw new Iznimka("Duzina ne mozez biti negativna i vrijednosti nula! Datoteka Artikl.XML");                    }                }                else                {                    throw new Iznimka("Duzina ne moze sadrzavati simbole i slova! Datoteka Artikl.XML");                }                if (decimal.TryParse(XmlartikliNode.Attributes["Cijena"].Value, out decimal cijena))                {                    if (cijena <= 0)                    {                        throw new Iznimka("Cijena ne moze biti negativna i vrijednosti nula! Datoteka Artikl.XML");                    }                }                else                {                    throw new Iznimka("Cijena ne moze sadrzavati simbole i slova! Datoteka Artikl.XML");                }                if (XmlartikliNode.Attributes["Sifra"].Value == "")                {                    throw new Iznimka("Sifra ne moze biti neispunjena! Datoteka Artikl.XML");                }                if (XmlartikliNode.Attributes["JMJ"].Value == "")                {                    throw new Iznimka("JMJ ne moze biti neispunjen! Datoteka Artikl.XML");                }                foreach (XmlNode XmlArtikliNode2 in Xmlartikli)                {                    if (!XmlartikliNode.Equals(XmlArtikliNode2))                    {                        if (XmlartikliNode.Attributes["Sifra"].Value == XmlArtikliNode2.Attributes["Sifra"].Value)                        {                            throw new Iznimka("Program nece pokrenuti, datoteka ima iste sifre: Artikl.XML");                        }                    }                }            }            sr.Close();            xml = "";            StreamReader sr2 = new StreamReader("Primka.xml");            using (sr2)            {                xml = sr2.ReadToEnd();            }            xmlDocument.LoadXml(xml);            XmlNodeList Xmlprimke = xmlDocument.SelectNodes("//Primke/Primka");            foreach (XmlNode XmlPrimkaNode in Xmlprimke)            {                if (XmlPrimkaNode.Attributes["Tip"].Value != "PRM")                {                    throw new Iznimka("Tip mora biti PRM! Primka.xml");                }                if (!decimal.TryParse(XmlPrimkaNode.Attributes["IznosUlaz"].Value, out decimal iznos))                {                    throw new Iznimka("Iznos ulaz nesmije sadrzavati slova i simbole! Primka.xml");                }                if (XmlPrimkaNode.ChildNodes.Count == 0)
                {
                    throw new Iznimka("Izdatnica nema artikle! Izdatnica.xml");
                }                foreach (XmlNode XmlChildPrimka in XmlPrimkaNode.ChildNodes)                {                    if (Artikl.DohvatiArtikl(XmlChildPrimka.Attributes["Sifra"].Value) == null)                    {                        throw new Iznimka("Artikl ne postoji! Primka.xml");                    }                    if (decimal.TryParse(XmlChildPrimka.Attributes["Kolicina"].Value, out decimal kolicina))                    {                        if (kolicina < 0)                        {                            throw new Iznimka("Kolicina ne moze biti prazna ili nula! Primka.xml");                        }                    }                    else                    {                        throw new Iznimka("Kolicina ne smije sadrzavati slova i simbole! Primka.xml");                    }                }            }            sr.Close();            xml = "";            StreamReader sr3 = new StreamReader("Izdatnica.xml");            using (sr3)            {                xml = sr3.ReadToEnd();            }            xmlDocument.LoadXml(xml);            XmlNodeList Xmlizdatnice = xmlDocument.SelectNodes("//Izdatnice/Izdatnica");            foreach (XmlNode XmlIzdatnicaNode in Xmlizdatnice)            {                if (XmlIzdatnicaNode.Attributes["Tip"].Value != "IZD")                {                    throw new Iznimka("Tip mora biti IZD! Izdatnica.xml");                }                if (!decimal.TryParse(XmlIzdatnicaNode.Attributes["IznosIzlaz"].Value, out decimal iznos))                {                    throw new Iznimka("Iznos izlaz nesmije sadrzavati slova i simbole! Izdatnica.xml");                }                else                {                    if (iznos < 0)                    {                        throw new Iznimka("Iznos izlaz ne smije biti manji od 0! Izdatnica.xml");                    }                }                if (XmlIzdatnicaNode.ChildNodes.Count == 0)
                {
                    throw new Iznimka("Izdatnica nema artikle! Izdatnica.xml");
                }                foreach (XmlNode XmlChildIzdatnica in XmlIzdatnicaNode.ChildNodes)                {                    if (Artikl.DohvatiArtikl(XmlChildIzdatnica.Attributes["Sifra"].Value) == null)                    {                        throw new Iznimka("Artikl ne postoji");                    }                    if (decimal.TryParse(XmlChildIzdatnica.Attributes["Kolicina"].Value, out decimal kolicina))                    {                        if (kolicina < 0)                        {                            throw new Iznimka("Kolicina ne moze biti prazna ili nula! Izdatnica.xml");                        }                    }                    else                    {                        throw new Iznimka("Kolicina ne smije sadrzavati slova i simbole! Izdatnica.xml");                    }                }            }        }
        //Vraca listu svih primka koje nisu istonirane, istonirane racune ne smijemo vise koristiti jer se smatraju izbrisanim
        public static List<Primka> ListaPrimka()        {            try
            {
                List<Primka> primkaList = new List<Primka>();
                string xmlPrimka = "";
                StreamReader sr = new StreamReader("Primka.xml");
                using (sr)
                {
                    xmlPrimka = sr.ReadToEnd();
                }
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlPrimka);
                XmlNodeList Xmlprimka = xmlDocument.SelectNodes("//Primke/Primka");
                List<Artikl> noviartikli = new List<Artikl>();
                List<decimal> kolicina = new List<decimal>();
                foreach (XmlNode primka in Xmlprimka)
                {
                    if (primka.Attributes["Istonirano"] == null)
                    {
                        noviartikli.Clear();
                        foreach (XmlNode xmlartikl in primka.ChildNodes)
                        {
                            if (Artikl.DohvatiArtikl(xmlartikl.Attributes["Sifra"].Value) == null)
                            {
                                throw new Iznimka("Artikl ne postoji");
                            }
                            else
                            {
                                noviartikli.Add(Artikl.DohvatiArtikl(xmlartikl.Attributes["Sifra"].Value));
                                kolicina.Add(Convert.ToInt32(xmlartikl.Attributes["Kolicina"].Value));
                            }
                        }
                        primkaList.Add(new Primka(primka.Attributes["Datum"].Value, noviartikli, kolicina));
                    }
                }
                return primkaList;
            }
            catch (Iznimka i)            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(i.Message);
                Console.ResetColor();
                return null;
            }        }
        //Vraca listu izdatnica, ne ispisuje istonirane racune
        public static List<Izdatnica> ListaIzdatnica()        {            try
            {
                List<Izdatnica> IzdatnicaList = new List<Izdatnica>();
                string xmlIzdatnica = "";
                StreamReader sr = new StreamReader("Izdatnica.xml");
                using (sr)
                {
                    xmlIzdatnica = sr.ReadToEnd();
                }
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlIzdatnica);
                XmlNodeList XmlIzdatnica = xmlDocument.SelectNodes("//Izdatnice/Izdatnica");
                List<Artikl> noviartikli = new List<Artikl>();
                List<decimal> kolicina = new List<decimal>();
                foreach (XmlNode izdatnica in XmlIzdatnica)
                {
                    if (izdatnica.Attributes["Istonirano"] == null)
                    {
                        noviartikli.Clear();
                        foreach (XmlNode xmlartikl in izdatnica.ChildNodes)
                        {
                            if (Artikl.DohvatiArtikl(xmlartikl.Attributes["Sifra"].Value) == null)
                            {
                                throw new Iznimka("Artikl ne postoji");
                            }
                            else
                            {
                                noviartikli.Add(Artikl.DohvatiArtikl(xmlartikl.Attributes["Sifra"].Value));
                                kolicina.Add(Convert.ToInt32(xmlartikl.Attributes["Kolicina"].Value));
                            }
                        }
                        IzdatnicaList.Add(new Izdatnica(izdatnica.Attributes["Datum"].Value, noviartikli, kolicina));
                    }
                }
                return IzdatnicaList;
            }            catch (Iznimka i)            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(i.Message);
                Console.ResetColor();
                return null;
            }        }
        //Funkcija koja izdraduje novu primku u xml
        public static void NovaPrimka(Primka p)        {            XmlDocument xmlDocument = new XmlDocument();            xmlDocument.Load("Primka.xml");            XmlNode mainNode = xmlDocument.SelectSingleNode("//Primke");            XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "Primka", null);            XmlAttribute tip = xmlDocument.CreateAttribute("Tip");            tip.Value = p.Tip;            XmlAttribute datum = xmlDocument.CreateAttribute("Datum");            datum.Value = p.Datum;            XmlAttribute iznosulaz = xmlDocument.CreateAttribute("IznosUlaz");            iznosulaz.Value = p.IznosUlaz.ToString();            xmlNode.Attributes.Append(tip);            xmlNode.Attributes.Append(datum);            xmlNode.Attributes.Append(iznosulaz);            Primka temp = p;            while (DodajSveArtikleUPrimku(xmlDocument, xmlNode, temp).Artikl.Count > 0) ;            mainNode.AppendChild(xmlNode);            xmlDocument.Save("Primka.xml");        }
        //Postavlja artikle u primku
        public static Primka DodajSveArtikleUPrimku(XmlDocument xmlDocument, XmlNode xmlNode, Primka temp)        {            XmlNode xmlChildNode = xmlDocument.CreateNode(XmlNodeType.Element, "Artikl", null);            XmlAttribute sifra = xmlDocument.CreateAttribute("Sifra");            sifra.Value = temp.Artikl[0].SifraArtikla;            XmlAttribute kolicina = xmlDocument.CreateAttribute("Kolicina");            kolicina.Value = temp.Kolicina[0].ToString();            xmlChildNode.Attributes.Append(sifra);            xmlChildNode.Attributes.Append(kolicina);            xmlNode.AppendChild(xmlChildNode);            temp.Artikl.RemoveRange(0, 1);            temp.Kolicina.RemoveRange(0, 1);            return temp;        }
        //Kreira novu izdatnicu u XML 
        public static void NovaIzdatnica(Izdatnica i)        {            XmlDocument xmlDocument = new XmlDocument();            xmlDocument.Load("Izdatnica.xml");            XmlNode mainNode = xmlDocument.SelectSingleNode("//Izdatnice");            XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "Izdatnica", null);            XmlAttribute tip = xmlDocument.CreateAttribute("Tip");            tip.Value = i.Tip;            XmlAttribute datum = xmlDocument.CreateAttribute("Datum");            datum.Value = i.Datum;            XmlAttribute iznosulaz = xmlDocument.CreateAttribute("IznosIzlaz");            iznosulaz.Value = i.IznosIzlaz.ToString();            xmlNode.Attributes.Append(tip);            xmlNode.Attributes.Append(datum);            xmlNode.Attributes.Append(iznosulaz);            Izdatnica temp = i;            while (DodajSveArtikleUIzdatnicu(xmlDocument, xmlNode, temp).Artikl.Count > 0) ;            mainNode.AppendChild(xmlNode);            xmlDocument.Save("Izdatnica.xml");        }
        //Dodajte izdatnici navedene artikle
        public static Izdatnica DodajSveArtikleUIzdatnicu(XmlDocument xmlDocument, XmlNode xmlNode, Izdatnica temp)        {            XmlNode xmlChildNode = xmlDocument.CreateNode(XmlNodeType.Element, "Artikl", null);            XmlAttribute sifra = xmlDocument.CreateAttribute("Sifra");            sifra.Value = temp.Artikl[0].SifraArtikla;            XmlAttribute kolicina = xmlDocument.CreateAttribute("Kolicina");            kolicina.Value = temp.Kolicina[0].ToString();            xmlChildNode.Attributes.Append(sifra);            xmlChildNode.Attributes.Append(kolicina);            xmlNode.AppendChild(xmlChildNode);            temp.Artikl.RemoveRange(0, 1);            temp.Kolicina.RemoveRange(0, 1);            return temp;        }
        //Automatski dodaje vrijednost svim primkama i izdatnicama, takoder i datum.
        public static void IzracunajIznos()        {            XmlDocument xmlDocument = new XmlDocument();            xmlDocument.Load("Primka.xml");            XmlNodeList XmlPrimka = xmlDocument.SelectNodes("//Primke/Primka");            Artikl temp;            decimal Iznos = 0.00m;            int i = 0;            foreach (XmlNode Primka in XmlPrimka)            {                foreach (XmlNode artikl in Primka.ChildNodes)                {                    temp = Artikl.DohvatiArtikl(artikl.Attributes["Sifra"].Value);                    Iznos += temp.Cijena * decimal.Parse(artikl.Attributes["Kolicina"].Value);                    i++;                }                if (Primka.Attributes["Datum"].Value == "")                {                    Primka.Attributes["Datum"].Value = DateTime.Now.ToString("dd.MM.yyyy");                }                Primka.Attributes["IznosUlaz"].Value = decimal.Round(Iznos, 2).ToString();                Iznos = 0.00m;            }            xmlDocument.Save("Primka.xml");            xmlDocument.Load("Izdatnica.xml");            XmlNodeList Xmlizdatnica = xmlDocument.SelectNodes("//Izdatnice/Izdatnica");            Iznos = 0.00M;            i = 0;            foreach (XmlNode Izdatnica in Xmlizdatnica)            {                foreach (XmlNode artikl in Izdatnica.ChildNodes)                {                    temp = Artikl.DohvatiArtikl(artikl.Attributes["Sifra"].Value);                    Iznos += temp.Cijena * decimal.Parse(artikl.Attributes["Kolicina"].Value);                    i++;                }                if (Izdatnica.Attributes["Datum"].Value == "")                {                    Izdatnica.Attributes["Datum"].Value = DateTime.Now.ToString("dd.MM.yyyy");                }                Izdatnica.Attributes["IznosIzlaz"].Value = decimal.Round(Iznos, 2).ToString();                Iznos = 0.00m;            }            xmlDocument.Save("Izdatnica.xml");        }
        //Ispisuje sve artikle 
        public static void Ispis()        {            ConsoleTable table = new ConsoleTable("Sifra", "Naziv", "JMJ", "Cijena",                "Kol. ulaz", "Izn. ulaz", "Kol. izlaz", "Izn. izlaz", "Stanje kol.", "Stanje cj.");            XmlDocument xmlartiklidoc = new XmlDocument();            XmlDocument xmlizdatnicadoc = new XmlDocument();            XmlDocument xmlprimkadoc = new XmlDocument();            xmlartiklidoc.Load("Artikl.xml");            xmlizdatnicadoc.Load("Izdatnica.xml");            xmlprimkadoc.Load("Primka.xml");            decimal kolicinaulaz;            decimal iznosulaz;            decimal kolicinaizlaz;            decimal iznosizlaz;            XmlNodeList xmlartikli = xmlartiklidoc.SelectNodes("//Artikli/Artikl");            XmlNodeList Xmlizdatnica = xmlizdatnicadoc.SelectNodes("//Izdatnice/Izdatnica");            XmlNodeList xmlprimka = xmlprimkadoc.SelectNodes("//Primke/Primka");            foreach (XmlNode artikl in xmlartikli)            {                kolicinaulaz = 0;                iznosulaz = 0;                kolicinaizlaz = 0;                iznosizlaz = 0;
                foreach (XmlNode primka in xmlprimka)                {                    foreach (XmlNode artikluprimki in primka.ChildNodes)                    {                        if (artikluprimki.Attributes["Sifra"].Value == artikl.Attributes["Sifra"].Value)                        {
                            kolicinaulaz += decimal.Parse(artikluprimki.Attributes["Kolicina"].Value);                            iznosulaz += decimal.Parse(artikl.Attributes["Cijena"].Value) * decimal.Parse(artikluprimki.Attributes["Kolicina"].Value);                        }                    }                }                foreach (XmlNode izdatnica in Xmlizdatnica)                {                    foreach (XmlNode artikluizdatnici in izdatnica.ChildNodes)                    {                        if (artikluizdatnici.Attributes["Sifra"].Value == artikl.Attributes["Sifra"].Value)                        {                            kolicinaizlaz += decimal.Parse(artikluizdatnici.Attributes["Kolicina"].Value);                            iznosizlaz += decimal.Parse(artikl.Attributes["Cijena"].Value) * decimal.Parse(artikluizdatnici.Attributes["Kolicina"].Value);                        }                    }                }                table.AddRow(artikl.Attributes["Sifra"].Value,                    $"{artikl.Attributes["Naziv"].Value} {artikl.LastChild.Attributes["Debljina"].Value}x{artikl.LastChild.Attributes["Sirina"].Value}x{artikl.LastChild.Attributes["Duzina"].Value} {artikl.LastChild.Attributes["MjernaJedinica"].Value}"                    , artikl.Attributes["JMJ"].Value, artikl.Attributes["Cijena"].Value, kolicinaulaz,                    iznosulaz, kolicinaizlaz, iznosizlaz, kolicinaulaz - kolicinaizlaz, iznosulaz - iznosizlaz);            }            table.Write();            Console.ReadKey();        }
        //Dodaje atribut istonirano primki Istonirano, tim podatkima ne mozemo vise pristupiti osim modifikacije baze
        public static void DodajAtributIstoniranPrimki(Primka p)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("Primka.xml");
            XmlNodeList Primke = xmlDocument.SelectNodes("/Primke/Primka");
            XmlAttribute atribut = xmlDocument.CreateAttribute("Istonirano");
            atribut.Value = "true";
            foreach (XmlNode PrimkeNode in Primke)
            {
                if ((PrimkeNode.Attributes["Datum"].Value == p.Datum) && (PrimkeNode.Attributes["IznosUlaz"].Value == p.IznosUlaz.ToString()))
                {
                    PrimkeNode.Attributes.Append(atribut);
                }
            }
            xmlDocument.Save("Primka.xml");
        }
        //Dodaje atribut istonirano izdatnici Istonirano, tim podatkima ne mozemo vise pristupiti osim modifikacije baze
        public static void DodajAtributIstoniranIzdatnici(Izdatnica p)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("Izdatnica.xml");
            XmlNodeList Izdatnice = xmlDocument.SelectNodes("/Izdatnice/Izdatnica");
            XmlAttribute atribut = xmlDocument.CreateAttribute("Istonirano");
            atribut.Value = true.ToString();
            foreach (XmlNode IzdatnicaNode in Izdatnice)
            {
                if ((IzdatnicaNode.Attributes["Datum"].Value == p.Datum) && (IzdatnicaNode.Attributes["IznosIzlaz"].Value == p.IznosIzlaz.ToString()))
                {
                    IzdatnicaNode.Attributes.Append(atribut);
                }
            }
            xmlDocument.Save("Izdatnica.xml");
        }
        //Ispisuje trenutno stanje skladista
        public static List<Artikl> StanjeSkladista()        {            List<Artikl> dostupni = new List<Artikl>();            XmlDocument xmlartiklidoc = new XmlDocument();            XmlDocument xmlizdatnicadoc = new XmlDocument();            XmlDocument xmlprimkadoc = new XmlDocument();            xmlartiklidoc.Load("Artikl.xml");            xmlizdatnicadoc.Load("Izdatnica.xml");            xmlprimkadoc.Load("Primka.xml");            decimal kolicinaulaz;            decimal kolicinaizlaz;            XmlNodeList xmlartikli = xmlartiklidoc.SelectNodes("//Artikli/Artikl");            XmlNodeList Xmlizdatnica = xmlizdatnicadoc.SelectNodes("//Izdatnice/Izdatnica");            XmlNodeList xmlprimka = xmlprimkadoc.SelectNodes("//Primke/Primka");            foreach (XmlNode artikl in xmlartikli)            {                kolicinaulaz = 0;                kolicinaizlaz = 0;
                foreach (XmlNode primka in xmlprimka)                {                    foreach (XmlNode artikluprimki in primka.ChildNodes)                    {                        if (artikluprimki.Attributes["Sifra"].Value == artikl.Attributes["Sifra"].Value)                        {
                            kolicinaulaz += decimal.Parse(artikluprimki.Attributes["Kolicina"].Value);                        }                    }                }                foreach (XmlNode izdatnica in Xmlizdatnica)                {                    foreach (XmlNode artikluizdatnici in izdatnica.ChildNodes)                    {                        if (artikluizdatnici.Attributes["Sifra"].Value == artikl.Attributes["Sifra"].Value)                        {                            kolicinaizlaz += decimal.Parse(artikluizdatnici.Attributes["Kolicina"].Value);                        }                    }                }                if ((kolicinaulaz >= kolicinaizlaz))
                {
                    if (artikl.Attributes["JMJ"].Value == "")
                    {
                        dostupni.Add(new Artikl(artikl.Attributes["Sifra"].Value, artikl.Attributes["Naziv"].Value, $"{artikl.LastChild.Attributes["Debljina"].Value}x{artikl.LastChild.Attributes["Sirina"].Value}x{artikl.LastChild.Attributes["Duzina"].Value} " +
                        $"{artikl.LastChild.Attributes["MjernaJedinica"].Value}", $"{artikl.Attributes["Cijena"].Value}"));
                    }
                    else
                    {
                        dostupni.Add(new Artikl(artikl.Attributes["Sifra"].Value, artikl.Attributes["Naziv"].Value, $"{artikl.LastChild.Attributes["Debljina"].Value}x{artikl.LastChild.Attributes["Sirina"].Value}x{artikl.LastChild.Attributes["Duzina"].Value} " +
                        $"{artikl.LastChild.Attributes["MjernaJedinica"].Value}", $"{artikl.Attributes["Cijena"].Value}/{artikl.Attributes["JMJ"].Value}"));
                    }
                }            }            return dostupni;
        }
        //Daje nam pristup trenutnoj kolicini svakog artikla
        public static List<decimal> KolicinaArtikala()
        {
            List<decimal> dostupni = new List<decimal>();            XmlDocument xmlartiklidoc = new XmlDocument();            XmlDocument xmlizdatnicadoc = new XmlDocument();            XmlDocument xmlprimkadoc = new XmlDocument();            xmlartiklidoc.Load("Artikl.xml");            xmlizdatnicadoc.Load("Izdatnica.xml");            xmlprimkadoc.Load("Primka.xml");            decimal kolicinaulaz;            decimal kolicinaizlaz;            XmlNodeList xmlartikli = xmlartiklidoc.SelectNodes("//Artikli/Artikl");            XmlNodeList Xmlizdatnica = xmlizdatnicadoc.SelectNodes("//Izdatnice/Izdatnica");            XmlNodeList xmlprimka = xmlprimkadoc.SelectNodes("//Primke/Primka");            foreach (XmlNode artikl in xmlartikli)            {                kolicinaulaz = 0;                kolicinaizlaz = 0;
                foreach (XmlNode primka in xmlprimka)                {                    foreach (XmlNode artikluprimki in primka.ChildNodes)                    {                        if (artikluprimki.Attributes["Sifra"].Value == artikl.Attributes["Sifra"].Value)                        {
                            kolicinaulaz += decimal.Parse(artikluprimki.Attributes["Kolicina"].Value);                        }                    }                }                foreach (XmlNode izdatnica in Xmlizdatnica)                {                    foreach (XmlNode artikluizdatnici in izdatnica.ChildNodes)                    {                        if (artikluizdatnici.Attributes["Sifra"].Value == artikl.Attributes["Sifra"].Value)                        {                            kolicinaizlaz += decimal.Parse(artikluizdatnici.Attributes["Kolicina"].Value);                        }                    }                }                if ((kolicinaulaz >= kolicinaizlaz))
                {
                    dostupni.Add(kolicinaulaz - kolicinaizlaz);
                }            }            return dostupni;
        }
        //Azurira artikl
        public static void AzurirajArtikl(string keyword, string promjena, string sifra)
        {
            XmlDocument dokument = new XmlDocument();
            dokument.Load("Artikl.xml");
            XmlNodeList xmlartikli = dokument.SelectNodes("//Artikli/Artikl");
            foreach (XmlNode artikl in xmlartikli)
            {
                if (artikl.Attributes["Sifra"].Value == sifra)
                {
                    artikl.Attributes[keyword].Value = promjena;
                    dokument.Save("Artikl.xml");
                }
            }
            XML.IzracunajIznos();
        }
        ~XML()
        {
        }
    }
}