namespace TaskManager
{
    class Program
    {
        static List<Task> tasks = new List<Task>();

        static void Main(string[] args)
        {
            int choice;

            do
            {
                ShowMenu();
                Console.Write("Unesite izbor (0-6): ");
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 6)
                {
                    Console.WriteLine("Nevažeći unos. Pokušajte ponovo.");
                    continue;
                }

                Console.Clear();
                switch (choice)
                {
                    case 1: AddTask(); break;
                    case 2: ShowAllTasks(); break;
                    case 3: MarkTaskAsCompleted(); break;
                    case 4: DeleteTask(); break;
                    case 5: SearchTasks(); break;
                    case 6: ShowTasksByPriority(); break;
                    case 0: Console.WriteLine("Izlaz iz programa..."); break;
                }
            } while (choice != 0);
        }

        static void ShowMenu()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("SISTEM ZA UPRAVLJANJE ZADACIMA");
            Console.WriteLine("=============================================");
            Console.WriteLine("1. Dodaj novi zadatak");
            Console.WriteLine("2. Prikaži sve zadatke");
            Console.WriteLine("3. Označi zadatak kao završen");
            Console.WriteLine("4. Obriši zadatak");
            Console.WriteLine("5. Pretraži zadatke po nazivu");
            Console.WriteLine("6. Prikaži zadatke po prioritetu");
            Console.WriteLine("0. Izlaz iz programa");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Aktivnih zadataka: {tasks.Count(t => !t.Completed)} | Završenih zadataka: {tasks.Count(t => t.Completed)}");
            Console.WriteLine("---------------------------------------------");
        }

        static void AddTask()
        {
            Console.WriteLine("Unesite naziv zadatka:");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Naziv ne može biti prazan.");
                return;
            }

            Console.WriteLine("Unesite opis zadatka:");
            string description = Console.ReadLine();

            Console.WriteLine("Unesite prioritet (1 - Nizak, 2 - Srednji, 3 - Visok):");
            if (!int.TryParse(Console.ReadLine(), out int priority) || priority < 1 || priority > 3)
            {
                Console.WriteLine("Nevažeći prioritet.");
                return;
            }

            tasks.Add(new Task(name, description, priority));
            Console.WriteLine("Zadatak uspješno dodat!");
        }

        static void ShowAllTasks()
        {
            if (!tasks.Any())
            {
                Console.WriteLine("Nema zadataka.");
                return;
            }

            Console.WriteLine("Svi zadaci:");
            int index = 1;
            foreach (var task in tasks)
            {
                Console.WriteLine($"{index++}. {task}");
            }
        }

        static void MarkTaskAsCompleted()
        {
            ShowAllTasks();
            if (!tasks.Any()) return;

            Console.Write("Unesite broj zadatka koji želite označiti kao završen: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > tasks.Count)
            {
                Console.WriteLine("Nevažeći unos.");
                return;
            }

            tasks[index - 1].Completed = true;
            Console.WriteLine("Zadatak označen kao završen.");
        }

        static void DeleteTask()
        {
            ShowAllTasks();
            if (!tasks.Any()) return;

            Console.Write("Unesite broj zadatka koji želite obrisati: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > tasks.Count)
            {
                Console.WriteLine("Nevažeći unos.");
                return;
            }

            tasks.RemoveAt(index - 1);
            Console.WriteLine("Zadatak obrisan.");
        }

        static void SearchTasks()
        {
            Console.Write("Unesite naziv za pretragu: ");
            string keyword = Console.ReadLine().ToLower();

            var foundTasks = tasks.Where(t => t.Name.ToLower().Contains(keyword)).ToList();

            if (!foundTasks.Any())
            {
                Console.WriteLine("Nema rezultata za pretragu.");
                return;
            }

            Console.WriteLine("Rezultati pretrage:");
            int index = 1;
            foreach (var task in foundTasks)
            {
                Console.WriteLine($"{index++}. {task}");
            }
        }

        static void ShowTasksByPriority()
        {
            var sortedTasks = tasks.OrderByDescending(t => t.Priority).ToList();

            if (!sortedTasks.Any())
            {
                Console.WriteLine("Nema zadataka.");
                return;
            }

            Console.WriteLine("Zadaci po prioritetu:");
            int index = 1;
            foreach (var task in sortedTasks)
            {
                Console.WriteLine($"{index++}. {task}");
            }
        }
    }

    class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; } // 1 - nizak, 2 - srednji, 3 - visok
        public bool Completed { get; set; }

        public Task(string name, string description, int priority)
        {
            Name = name;
            Description = description;
            Priority = priority;
            Completed = false;
        }

        public override string ToString()
        {
            string status = Completed ? "Završen" : "Aktivan";
            string priorityStr = Priority switch
            {
                1 => "Nizak",
                2 => "Srednji",
                3 => "Visok",
                _ => "Nepoznat"
            };

            return $"Naziv: {Name}, Opis: {Description}, Prioritet: {priorityStr}, Status: {status}";
        }
    }
}
