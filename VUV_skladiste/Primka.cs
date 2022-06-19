﻿using System;
        //Ispisuje jednu primku po nasem odabiru
        public static void Ispis()
            }
        //Kreira objekt primka
        public static Primka Dodaj()
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
            } while (true);