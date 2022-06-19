﻿using System;
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
        {
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
                PrikaziMeni();

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
                case 7:
                        Dokument.IspisNajnovijihDokumenata();
                        PocniAplikaciju("");
                        break;
                    }
                case 8:
                        Dokument.IstonirajRacun();
                        PocniAplikaciju("Racun uspjesno istoniran");
                        break;
                    }
                case 9:
                        Artikl.AzurirajArtikl();
                        PocniAplikaciju("Artikl uspjesno azuriran");
                        break;
                    }