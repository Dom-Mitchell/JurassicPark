using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace JurassicPark
{
    class DinoDatabase
    {
        private List<Dino> Dinos { get; set; } = new List<Dino>();

        private string FileName = "dinos.csv";

        public void LoadDinos()
        {
            if (File.Exists(FileName))
            {
                var fileReader = new StreamReader(FileName);

                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);

                // Replace our BLANK list of employees with the ones that are in the CSV file
                Dinos = csvReader.GetRecords<Dino>().ToList();

                fileReader.Close();
            }
        }

        // Write the list Dinos to a file
        public void SaveDinos()
        {
            var fileWriter = new StreamWriter(FileName);

            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(Dinos);

            fileWriter.Close();
        }

        // CREATE Add Dino
        public void AddDino(Dino newDino)
        {
            Dinos.Add(newDino);
        }

        // READ Get all the Dinos
        public List<Dino> GetAllDinos()
        {
            return Dinos;
        }

        // READ Find One Dino
        public Dino FindOneDino(string nameToFind)
        {
            Dino foundDino = Dinos.FirstOrDefault(dino => dino.Name.ToUpper().Contains(nameToFind.ToUpper()));

            return foundDino;
        }

        public void TransferDino(string nameToFind)
        {
            // var foundDino = FindOneDino(nameToFind);
            // var numNames = Dinos.Count(dino => dino.Name == nameToFind);

            // if (foundDino == null)
            // {
            //     // - Show a message if `null`
            //     Console.WriteLine("No such dino!");
            // }
            // else if (foundDino != null && Int32.foundDino != numNames)
        }


        public void DinoData(string summaryOf)
        {
            var validChoice = false;

            while (!validChoice)
            {
                var numHerbivores = Dinos.Count(dino => dino.DietType == "Herbivore");
                var numCarnivores = Dinos.Count(dino => dino.DietType == "Carnivore");
                var numOmnivores = Dinos.Count(dino => dino.DietType == "Omnivore");

                // - After the loop, `foundDino` is either `null` (not found) or refers to the matching item
                if (summaryOf.ToUpper() == "Y" || summaryOf.ToUpper() == "YES")
                {
                    Console.WriteLine("\nDino Summary: ");
                    validChoice = true;
                    Console.WriteLine($"Carnivores: {numCarnivores}\nOmnivores: {numOmnivores}\nHerbivores: {numHerbivores}\n");
                }
                else if (summaryOf.ToUpper() == "N" || summaryOf.ToUpper() == "NO")
                {
                    Console.WriteLine("Which do you want a summary of?\n");
                    Console.WriteLine("Carnivore: An animal that eats meat");
                    Console.WriteLine("Omnivore: An animal that eats meat and plants");
                    Console.WriteLine("Herbivore: An animal that eats plants\n");

                    var sumOf = Console.ReadLine().ToLower();

                    if (sumOf == "c" || sumOf == "carnivore")
                    {
                        Console.WriteLine("\nDino Summary: ");
                        Console.WriteLine($"Carnivores: {numCarnivores}\n");
                    }
                    else if (sumOf == "o" || sumOf == "omnivore")
                    {
                        Console.WriteLine("\nDino Summary: ");
                        Console.WriteLine($"Omnivores: {numOmnivores}\n");
                    }
                    else if (sumOf == "h" || sumOf == "herbivore")
                    {
                        Console.WriteLine("\nDino Summary: ");
                        Console.WriteLine($"Herbivores: {numHerbivores}\n");
                    }
                    else
                    {
                        Console.WriteLine("\nYour answer was invalid. Please try again!");
                        Console.WriteLine("Your choice must be one of the following:\n");
                        Console.WriteLine("Carnivore: An animal that eats meat");
                        Console.WriteLine("Omnivore: An animal that eats meat and plants");
                        Console.WriteLine("Herbivore: An animal that eats plants\n");
                        validChoice = false;
                    }

                }
                else
                {
                    Console.WriteLine("\nYour answer was invalid. Please try again!");
                    Console.WriteLine("Your choice must be one of the following:\n");
                    Console.WriteLine("Carnivore: An animal that eats meat");
                    Console.WriteLine("Omnivore: An animal that eats meat and plants");
                    Console.WriteLine("Herbivore: An animal that eats plants\n");
                    validChoice = false;
                }

            }

            // return "There was no data found!";
        }

        // DELETE Dino
        public void DeleteDino(Dino dinoToDelete)
        {
            Dinos.Remove(dinoToDelete);
        }


    }
}
