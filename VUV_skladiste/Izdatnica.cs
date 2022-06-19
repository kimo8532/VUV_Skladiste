﻿using System;
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
            }