using System;namespace VUV_skladiste{    internal class Skladiste
    {
        //Podatkovni clan koji nam daje pristup logu bilo gdje u aplikaciji
        private static string vuvLogo = @"
                              _______  _        _        _______  ______  _________ _______ _________ _______ 
|\     /||\     /||\     /|  (  ____ \| \    /\( \      (  ___  )(  __  \ \__   __/(  ____ \\__   __/(  ____ \
| )   ( || )   ( || )   ( |  | (    \/|  \  / /| (      | (   ) || (  \  )   ) (   | (    \/   ) (   | (    \/
| |   | || |   | || |   | |  | (_____ |  (_/ / | |      | (___) || |   ) |   | |   | (_____    | |   | (__    
( (   ) )| |   | |( (   ) )  (_____  )|   _ (  | |      |  ___  || |   | |   | |   (_____  )   | |   |  __)   
 \ \_/ / | |   | | \ \_/ /         ) ||  ( \ \ | |      | (   ) || |   ) |   | |         ) |   | |   | (      
  \   /  | (___) |  \   /    /\____) ||  /  \ \| (____/\| )   ( || (__/  )___) (___/\____) |   | |   | (____/\
   \_/   (_______)   \_/     \_______)|_/    \/(_______/|/     \|(______/ \_______/\_______)   )_(   (_______/
                                                                                                              
";
        public static string VuvLogo()
        {
            return vuvLogo;
        }
        private int trenutniIndeks;
        private string[] opcije;
        private string pocetniTekst;
        public Skladiste(string pocetniTekst, string[] opcije)
        {
            this.pocetniTekst = pocetniTekst;
            this.opcije = opcije;
            trenutniIndeks = 0;
        }
        //Ispisuje meni i prikazuje trenutno odabranu opciju
        public void PrikaziMeni()
        {            Console.BackgroundColor = ConsoleColor.Black;            Console.ForegroundColor = ConsoleColor.White;            Console.WriteLine(pocetniTekst);            Console.ResetColor();            for (int i = 0; i < opcije.Length; i++)
            {
                string trenutnaOpcija = opcije[i];
                string prefix;
                if (i == trenutniIndeks)
                {
                    prefix = ">";
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"{prefix} < {trenutnaOpcija} >");
            }
            Console.ResetColor();
        }
        //Pokrece program, daje korisniku opciju navigirati kroz meni
        public int Pokreni()
        {
            ConsoleKey gumbPritisnuti;
            do
            {
                Console.Clear();
                PrikaziMeni();                do                {                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);                    gumbPritisnuti = keyInfo.Key;                    System.Threading.Thread.Sleep(10);                } while (Console.KeyAvailable);
                if (gumbPritisnuti == ConsoleKey.DownArrow)
                {
                    if (trenutniIndeks != opcije.Length - 1)
                    {
                        trenutniIndeks++;
                    }
                    else
                    {
                        trenutniIndeks = 0;
                    }
                }
                else if (gumbPritisnuti == ConsoleKey.UpArrow)
                {
                    if (trenutniIndeks != 0)
                    {
                        trenutniIndeks--;
                    }
                    else
                    {
                        trenutniIndeks = opcije.Length - 1;
                    }
                }
                else if (gumbPritisnuti == ConsoleKey.Escape)
                {
                    Start();
                }
            }
            while (gumbPritisnuti != ConsoleKey.Enter);
            return trenutniIndeks;
        }
        //Glavni meni, pokrece funkcije bitne za funkcioniranje programa kao sto je provjera xml datoteka i da automatski izracuna iznosulaz i iznosizlaz u datotekama
        public static void Start()        {            XML.IzracunajIznos();            XML.ProvjeraPrijePokretanja();            string ASCII = VuvLogo();            string[] opcije = { "Pokreni", "Izlaz" };            Skladiste glavniMeni = new Skladiste(ASCII, opcije);            int trenutniIndeks = glavniMeni.Pokreni();            switch (trenutniIndeks)            {                case 0:                    {                        PocniAplikaciju("");                        break;                    }                case 1:                    {                        Izlaz();                        break;                    }            }            Console.ReadKey(true);        }
        //Zapocinje drugi glavni meni koji sadrzi sve mogucnosti glavne mogucnosti programa na odabir
        public static void PocniAplikaciju(string ASCII)        {            string[] opcije = { "Dodaj artikl", "Trenutni artikli", "Dodaj izdatnicu", "Ispisi izdatnicu", "Dodaj primku", "Ispis primke", "Stanje", "5 najnovijih racuna", "Istoniraj racun", "Azuriraj artikl" };            Skladiste glavniMeni = new Skladiste(VuvLogo() + '\n' + ASCII, opcije);            int trenutniIndeks = glavniMeni.Pokreni();            switch (trenutniIndeks)            {                case 0:                    {                        Artikl.NoviArtikl();                        PocniAplikaciju($"Uspjesno kreiran artikl");                        break;                    }                case 1:                    {                        Artikl.IspisSvihArtikala();                        PocniAplikaciju("");                        break;                    }                case 2:                    {                        XML.NovaIzdatnica(Izdatnica.Dodaj());                        PocniAplikaciju("Uspjesno kreirana izdatnica");                        break;                    }                case 3:                    {                        Izdatnica.Ispis();                        PocniAplikaciju("");                        break;                    }                case 4:                    {                        XML.NovaPrimka(Primka.Dodaj());                        PocniAplikaciju("Uspjesno kreirana primka");                        break;                    }                case 5:                    {                        Primka.Ispis();                        PocniAplikaciju("");                        break;                    }                case 6:                    {                        XML.Ispis();                        PocniAplikaciju("");                        break;                    }
                case 7:                    {
                        Dokument.IspisNajnovijihDokumenata();
                        PocniAplikaciju("");
                        break;
                    }
                case 8:                    {
                        Dokument.IstonirajRacun();
                        PocniAplikaciju("Racun uspjesno istoniran");
                        break;
                    }
                case 9:                    {
                        Artikl.AzurirajArtikl();
                        PocniAplikaciju("Artikl uspjesno azuriran");
                        break;
                    }            }            Console.ReadKey(true);        }
        //Izlaz na desktop nakon zavrsteka koristenja aplikacije
        public static void Izlaz()        {            Console.WriteLine("Pritisnite bilo koji gumb za izlaz");            Console.ReadKey(true);            Environment.Exit(0);        }        ~Skladiste()
        {
        }    }}