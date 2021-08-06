using System;
using CsvHelper;


namespace JurassicPark
{

    class Program
    {
        static void DisplayGreeting()
        {
            // Credit to https://www.ascii-art-generator.org/ for ascii art
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("       #                                                ######                       ");
            Console.WriteLine("       # #    # #####    ##    ####   ####  #  ####     #     #   ##   #####  #    # ");
            Console.WriteLine("       # #    # #    #  #  #  #      #      # #    #    #     #  #  #  #    # #   #  ");
            Console.WriteLine("       # #    # #    # #    #  ####   ####  # #         ######  #    # #    # ####   ");
            Console.WriteLine(" #     # #    # #####  ######      #      # # #         #       ###### #####  #  #   ");
            Console.WriteLine(" #     # #    # #   #  #    # #    # #    # # #    #    #       #    # #   #  #   #  ");
            Console.WriteLine("  #####   ####  #    # #    #  ####   ####  #  ####     #       #    # #    # #    # ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            return userInput;
        }

        static double PromptForDouble(string prompt)
        {
            Console.Write(prompt);
            double userInput;
            var isThisGoodInput = Double.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0.0 as your answer.");
                return 0;
            }
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }
        }

        private static void AddDino(DinoDatabase database)
        {
            // CREATE (out of CREATE - READ - UPDATE - DELETE)

            // Make a new dino object
            var dino = new Dino();
            var realDietType = false;

            Console.ForegroundColor = ConsoleColor.Cyan;
            // Prompt for values and save them in the dino's properties
            dino.Name = PromptForString("\nWhat is the dinos name? ");
            dino.DinoType = PromptForString($"What type of dino is {dino.Name}? ");
            while (!realDietType)
            {
                dino.DietType = PromptForString($"What {dino.Name}'s diet? (Idk for help) ");

                if (dino.DietType == "Carnivore" || dino.DietType == "Omnivore" || dino.DietType == "Herbivore")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nYour answer was invalid. Please try again!");
                    Console.WriteLine("Your choice must be one of the following:\n");
                    Console.WriteLine("Carnivore: An animal that eats meat");
                    Console.WriteLine("Omnivore: An animal that eats meat and plants");
                    Console.WriteLine("Herbivore: An animal that eats plants\n");
                }
            }
            dino.WhenAcquired = DateTime.Now;
            dino.Weight = PromptForDouble($"How much does {dino.Name} weigh? ");
            dino.EnclosureNumber = PromptForInteger($"What is {dino.Name}'s enclosure number? ");

            // Add it to the list
            database.AddDino(dino);
            database.SaveDinos();
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ShowDino(DinoDatabase database)
        {
            // - Prompt for the name
            Console.ForegroundColor = ConsoleColor.Green;
            var nameToSearchFor = PromptForString("\nWhat name are you looking for? ");

            Dino foundDino = database.FindOneDino(nameToSearchFor);

            // - After the loop, `foundDino` is either `null` (not found) or refers to the matching item
            if (foundDino == null)
            {
                // - Show a message if `null`
                Console.WriteLine("No such dino!");
            }
            else
            {
                // - otherwise show the details.

                Console.WriteLine($"\n\nHere is the requested data for {foundDino.Name}");
                Console.WriteLine($"{foundDino}");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ListAllDinos(DinoDatabase database)
        {
            // READ (out of CREATE - READ - UPDATE - DELETE)
            foreach (var dino in database.GetAllDinos())
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(dino);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void UpdateDino(DinoDatabase database)
        {
            // UPDATE - from CREATE, READ, UPDATE, DELETE

            Console.ForegroundColor = ConsoleColor.Yellow;
            // Get the employee name we are searchign for
            var nameToSearchFor = PromptForString("\nWhat name are you looking for? ");

            // Search the database to see if they exist!
            Dino foundDino = database.FindOneDino(nameToSearchFor);

            // If we didn't find anyone
            if (foundDino == null)
            {
                //  Show that the person doesn't exist
                Console.WriteLine("No such dino!");
            }
            // If we found an employee
            else
            {
                Console.WriteLine($"{foundDino}");
                var changeChoice = PromptForString("\nWhat do you want to change [Name/Type/Diet/Weight/Enclosure]? ").ToUpper();

                // -- What do we want to change?
                //    - if name
                if (changeChoice == "NAME")
                {
                    //      - prompt for a new name
                    foundDino.Name = PromptForString("What is the new name? ");
                }
                if (changeChoice == "TYPE")
                {
                    //      - prompt for a new name
                    foundDino.DinoType = PromptForString("What is the new type? ");
                }
                //    - if the diet
                if (changeChoice == "DIET")
                {
                    //      - prompt for a new diet
                    foundDino.DietType = PromptForString("What is the new diet? ");
                }
                //   - if weight
                if (changeChoice == "WEIGHT")
                {
                    //      - prompt for new weight
                    foundDino.Weight = PromptForDouble("What is the new weight? ");
                }
            }
            database.SaveDinos();
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void TransferDino(DinoDatabase database)
        {
            // - Prompt for the name
            Console.ForegroundColor = ConsoleColor.Magenta;
            var nameToSearchFor = PromptForString("\nWhat name are you looking for? ");

            Dino foundDino = database.FindOneDino(nameToSearchFor);

            database.TransferDino(nameToSearchFor);

            database.SaveDinos();
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void SummaryOfDinos(DinoDatabase database)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            var summaryOf = PromptForString("\nDo you want a summary of all dinos? (Yes/No) ");
            database.DinoData(summaryOf);
            Console.ForegroundColor = ConsoleColor.White;

        }

        private static void DeleteDino(DinoDatabase database)
        {
            // DELETE - out of (CREATE, READ, UPDATE, DELETE)

            Console.ForegroundColor = ConsoleColor.DarkRed;
            // Get the dino name we are searching for
            var nameToSearchFor = PromptForString("\nWhat name are you looking for? ");

            // Search the database to see if they exist!
            Dino foundDino = database.FindOneDino(nameToSearchFor);

            // If we didn't find anyone
            if (foundDino == null)
            {
                //  Show that the person doesn't exist
                Console.WriteLine("No such dino!");
            }
            // If we found a dino
            else
            {
                //  - We did find the employee
                //  - Show the details
                Console.WriteLine($"{foundDino}");

                //  - Ask to confirm "Are you sure you want to delete them?"
                var confirm = PromptForString("Are you sure? [Y/N] ").ToUpper();

                if (confirm == "Y")
                {
                    //    - Delete them
                    database.DeleteDino(foundDino);
                    database.SaveDinos();
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Welcome to C#");

            // Make a new database
            var database = new DinoDatabase();
            database.LoadDinos();

            Console.Clear();
            DisplayGreeting();

            // Should we keep showing the menu?
            var keepGoing = true;

            // While the user hasn't said QUIT yet
            while (keepGoing)
            {
                // Insert a blank line then prompt them and get their answer (force uppercase)
                Console.WriteLine();
                Console.Write("What do you want to do?\n(A)dd a dino\n(D)elete a dino\n(F)ind a dino\n(L)ist all the dinos\n(U)pdate a dino\n(T)ransfer a dino\n(S)ummary of dinos\n(Q)uit\n: ");
                var choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "A":
                        AddDino(database);
                        break;
                    case "D":
                        DeleteDino(database);
                        break;
                    case "F":
                        ShowDino(database);
                        break;
                    case "L":
                        ListAllDinos(database);
                        break;
                    case "U":
                        UpdateDino(database);
                        break;
                    case "T":
                        TransferDino(database);
                        break;
                    case "S":
                        SummaryOfDinos(database);
                        break;
                    case "Q":
                        keepGoing = false;
                        break;
                    default:
                        Console.WriteLine("\nYour answer was invalid. Please try again!");
                        break;
                }
            }

        }
    }
}
