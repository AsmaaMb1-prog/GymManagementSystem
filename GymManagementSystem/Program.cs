
using System;
using System.Collections.Generic;

namespace Gym_Management_System
{
    // ====================== PROGRAM (MAIN) ======================
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Personal Trainer Management System ===");
                Console.WriteLine("1- Manage clients and trainers");
                Console.WriteLine("2- Manage exercises and workout plans");
                Console.WriteLine("3- Manage nutrition and progress");
                Console.WriteLine("4- Manage gym and memberships");
                Console.WriteLine("5- Reports and statistics");
                Console.WriteLine("0- Exit");
                Console.Write("Choose: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                if (input == "1")
                {
                    ClientTrainerManager.Run();
                }
                else if (input == "2")
                {
                    WorkoutManager.Run();
                }
                else if (input == "3")
                {
                    NutritionProgressManager.Run();
                }
                else if (input == "4")
                {
                    GymMembershipManager.Run();
                }
                else if (input == "5")
                {
                    ReportManager.Run();
                }
                else if (input == "0")
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }

    // ====================== PERSON / CLIENT / TRAINER ======================

    public class Person
    {
        private static int nextId = 1;

        private int id;
        private string name;
        private int age;
        private string email;

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value > 0)
                    age = value;
            }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public Person(string name, int age, string email)
        {
            id = nextId++;
            Name = name;
            Age = age;
            Email = email;
        }
    }

    public class Client : Person
    {
        public string Goal { get; set; }

        public Client(string name, int age, string email, string goal)
            : base(name, age, email)
        {
            Goal = goal;
        }
    }

    public class Trainer : Person
    {
        public string Specialty { get; set; }

        public Trainer(string name, int age, string email, string specialty)
            : base(name, age, email)
        {
            Specialty = specialty;
        }
    }

    public static class ClientTrainerManager
    {
        private static List<Client> clients = new List<Client>();
        private static List<Trainer> trainers = new List<Trainer>();

        public static int TotalClients
        {
            get { return clients.Count; }
        }

        public static int TotalTrainers
        {
            get { return trainers.Count; }
        }

        public static void Run()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Clients and Trainers ===");
                Console.WriteLine("1- Add new client");
                Console.WriteLine("2- Add new trainer");
                Console.WriteLine("3- Show all clients");
                Console.WriteLine("4- Show all trainers");
                Console.WriteLine("0- Back");
                Console.Write("Choose: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                if (input == "1")
                {
                    AddClient();
                }
                else if (input == "2")
                {
                    AddTrainer();
                }
                else if (input == "3")
                {
                    ShowClients();
                }
                else if (input == "4")
                {
                    ShowTrainers();
                }
                else if (input == "0")
                {
                    back = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }

                if (!back)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void AddClient()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Age: ");
            int age;
            int.TryParse(Console.ReadLine(), out age);

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Goal (lose weight / build muscle / etc.): ");
            string goal = Console.ReadLine();

            Client c = new Client(name, age, email, goal);
            clients.Add(c);

            Console.WriteLine("Client added.");
        }

        private static void AddTrainer()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Age: ");
            int age;
            int.TryParse(Console.ReadLine(), out age);

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Specialty: ");
            string spec = Console.ReadLine();

            Trainer t = new Trainer(name, age, email, spec);
            trainers.Add(t);

            Console.WriteLine("Trainer added.");
        }

        private static void ShowClients()
        {
            if (clients.Count == 0)
            {
                Console.WriteLine("No clients yet.");
                return;
            }

            Console.WriteLine("Clients:");
            Console.WriteLine("--------------------------------");
            foreach (var c in clients)
            {
                Console.WriteLine("Id: " + c.Id + ", Name: " + c.Name + ", Age: " + c.Age + ", Goal: " + c.Goal);
            }
        }

        private static void ShowTrainers()
        {
            if (trainers.Count == 0)
            {
                Console.WriteLine("No trainers yet.");
                return;
            }

            Console.WriteLine("Trainers:");
            Console.WriteLine("--------------------------------");
            foreach (var t in trainers)
            {
                Console.WriteLine("Id: " + t.Id + ", Name: " + t.Name + ", Age: " + t.Age + ", Specialty: " + t.Specialty);
            }
        }
    }

    // ====================== EXERCISES / WORKOUT PLANS ======================

    public class Exercise
    {
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }

        public Exercise(string name, string muscleGroup, int sets, int reps)
        {
            Name = name;
            MuscleGroup = muscleGroup;
            Sets = sets;
            Reps = reps;
        }
    }

    public class WorkoutPlan
    {
        public string Name { get; set; }
        public List<Exercise> Exercises { get; private set; }

        public WorkoutPlan(string name)
        {
            Name = name;
            Exercises = new List<Exercise>();
        }

        public void AddExercise(Exercise ex)
        {
            if (ex != null)
                Exercises.Add(ex);
        }

        public void Print()
        {
            Console.WriteLine("Workout plan: " + Name);
            if (Exercises.Count == 0)
            {
                Console.WriteLine("- No exercises yet.");
            }
            else
            {
                foreach (var ex in Exercises)
                {
                    Console.WriteLine("- " + ex.Name + " (" + ex.MuscleGroup + ") " + ex.Sets + "x" + ex.Reps);
                }
            }
        }
    }

    public static class WorkoutManager
    {
        private static List<Exercise> allExercises = new List<Exercise>();
        private static List<WorkoutPlan> plans = new List<WorkoutPlan>();

        public static int TotalExercises
        {
            get { return allExercises.Count; }
        }

        public static int TotalWorkoutPlans
        {
            get { return plans.Count; }
        }

        public static void Run()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Exercises and Workout Plans ===");
                Console.WriteLine("1- Add new exercise");
                Console.WriteLine("2- Show all exercises");
                Console.WriteLine("3- Create workout plan");
                Console.WriteLine("4- Show workout plans");
                Console.WriteLine("0- Back");
                Console.Write("Choose: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                if (input == "1")
                {
                    AddExercise();
                }
                else if (input == "2")
                {
                    ShowExercises();
                }
                else if (input == "3")
                {
                    CreateWorkoutPlan();
                }
                else if (input == "4")
                {
                    ShowWorkoutPlans();
                }
                else if (input == "0")
                {
                    back = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }

                if (!back)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void AddExercise()
        {
            Console.Write("Exercise name: ");
            string name = Console.ReadLine();

            Console.Write("Muscle group: ");
            string group = Console.ReadLine();

            Console.Write("Sets: ");
            int sets;
            int.TryParse(Console.ReadLine(), out sets);

            Console.Write("Reps: ");
            int reps;
            int.TryParse(Console.ReadLine(), out reps);

            Exercise ex = new Exercise(name, group, sets, reps);
            allExercises.Add(ex);

            Console.WriteLine("Exercise added.");
        }

        private static void ShowExercises()
        {
            if (allExercises.Count == 0)
            {
                Console.WriteLine("No exercises yet.");
                return;
            }

            Console.WriteLine("Exercises:");
            Console.WriteLine("--------------------------------");
            foreach (var ex in allExercises)
            {
                Console.WriteLine("- " + ex.Name + " (" + ex.MuscleGroup + ") " + ex.Sets + "x" + ex.Reps);
            }
        }

        private static void CreateWorkoutPlan()
        {
            Console.Write("Workout plan name: ");
            string name = Console.ReadLine();

            WorkoutPlan plan = new WorkoutPlan(name);

            Console.WriteLine("Add exercises to this plan (by name). Leave empty to finish.");
            while (true)
            {
                Console.Write("Exercise name: ");
                string exName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(exName))
                    break;

                Exercise found = null;
                foreach (var ex in allExercises)
                {
                    if (ex.Name.Equals(exName, StringComparison.OrdinalIgnoreCase))
                    {
                        found = ex;
                        break;
                    }
                }

                if (found != null)
                {
                    plan.AddExercise(found);
                    Console.WriteLine("Added.");
                }
                else
                {
                    Console.WriteLine("Exercise not found.");
                }
            }

            plans.Add(plan);
            Console.WriteLine("Workout plan created.");
        }

        private static void ShowWorkoutPlans()
        {
            if (plans.Count == 0)
            {
                Console.WriteLine("No workout plans yet.");
                return;
            }

            foreach (var p in plans)
            {
                p.Print();
                Console.WriteLine();
            }
        }
    }

    // ====================== NUTRITION / PROGRESS ======================

    public class NutritionPlan
    {
        private static int nextId = 1;
        private static int totalPlans = 0;

        private int id;
        private string clientName;
        private int dailyCalories;
        private List<string> meals;
        private string notes;

        public int Id
        {
            get { return id; }
        }

        public static int TotalPlans
        {
            get { return totalPlans; }
        }

        public string ClientName
        {
            get { return clientName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    clientName = value;
            }
        }

        public int DailyCalories
        {
            get { return dailyCalories; }
            set
            {
                if (value > 0)
                    dailyCalories = value;
            }
        }

        public List<string> Meals
        {
            get { return meals; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public NutritionPlan(string clientName, int dailyCalories)
        {
            id = nextId++;
            totalPlans++;

            this.clientName = clientName;
            this.dailyCalories = dailyCalories;
            this.meals = new List<string>();
            this.notes = string.Empty;
        }

        public void AddMeal(string mealDescription)
        {
            if (!string.IsNullOrWhiteSpace(mealDescription))
            {
                meals.Add(mealDescription);
            }
        }

        public void PrintDetails()
        {
            Console.WriteLine("------------ Nutrition Plan ------------");
            Console.WriteLine("Plan Id: " + Id);
            Console.WriteLine("Client: " + ClientName);
            Console.WriteLine("Daily calories: " + DailyCalories);
            Console.WriteLine("Meals:");

            if (meals.Count == 0)
            {
                Console.WriteLine("- No meals added yet.");
            }
            else
            {
                for (int i = 0; i < meals.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + meals[i]);
                }
            }

            if (!string.IsNullOrWhiteSpace(Notes))
            {
                Console.WriteLine("Notes: " + Notes);
            }

            Console.WriteLine("----------------------------------------");
        }
    }

    public class Progress
    {
        private static int totalRecords = 0;

        private string clientName;
        private DateTime date;
        private double weight;
        private double bodyFat;
        private string notes;

        public static int TotalRecords
        {
            get { return totalRecords; }
        }

        public string ClientName
        {
            get { return clientName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    clientName = value;
            }
        }

        public DateTime Date
        {
            get { return date; }
            private set { date = value; }
        }

        public double Weight
        {
            get { return weight; }
            set
            {
                if (value > 0)
                    weight = value;
            }
        }

        public double BodyFat
        {
            get { return bodyFat; }
            set
            {
                if (value >= 0)
                    bodyFat = value;
            }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public Progress(string clientName, double weight, double bodyFat, string notes)
        {
            totalRecords++;

            ClientName = clientName;
            Weight = weight;
            BodyFat = bodyFat;
            Notes = notes;
            Date = DateTime.Now;
        }

        public void Print()
        {
            Console.WriteLine("Date:   " + Date);
            Console.WriteLine("Client: " + ClientName);
            Console.WriteLine("Weight: " + Weight + " kg");
            Console.WriteLine("Body fat: " + BodyFat + " %");
            if (!string.IsNullOrWhiteSpace(Notes))
            {
                Console.WriteLine("Notes: " + Notes);
            }
            Console.WriteLine("----------------------------------------");
        }
    }

    public static class NutritionProgressManager
    {
        private static List<NutritionPlan> plans = new List<NutritionPlan>();
        private static List<Progress> progressList = new List<Progress>();

        public static void Run()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Nutrition and Progress ===");
                Console.WriteLine("1- Create nutrition plan");
                Console.WriteLine("2- Enter client progress");
                Console.WriteLine("3- Display client progress");
                Console.WriteLine("4- Edit nutrition plan");
                Console.WriteLine("5- Show statistics");
                Console.WriteLine("0- Back");
                Console.Write("Choose: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                if (input == "1")
                {
                    CreatePlan();
                }
                else if (input == "2")
                {
                    EnterProgress();
                }
                else if (input == "3")
                {
                    DisplayClientProgress();
                }
                else if (input == "4")
                {
                    EditPlan();
                }
                else if (input == "5")
                {
                    ShowStatistics();
                }
                else if (input == "0")
                {
                    back = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }

                if (!back)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void CreatePlan()
        {
            Console.Write("Client name: ");
            string name = Console.ReadLine();

            Console.Write("Target daily calories: ");
            int cal;
            int.TryParse(Console.ReadLine(), out cal);

            NutritionPlan plan = new NutritionPlan(name, cal);

            Console.WriteLine("Enter meals (empty line to finish):");
            while (true)
            {
                string meal = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(meal))
                    break;

                plan.AddMeal(meal);
            }

            Console.Write("Notes (optional): ");
            plan.Notes = Console.ReadLine();

            plans.Add(plan);

            Console.WriteLine();
            Console.WriteLine("Nutrition plan created.");
        }

        private static NutritionPlan FindPlanByClient(string clientName)
        {
            foreach (var p in plans)
            {
                if (p.ClientName.Equals(clientName, StringComparison.OrdinalIgnoreCase))
                {
                    return p;
                }
            }
            return null;
        }

        private static void EnterProgress()
        {
            Console.Write("Client name: ");
            string name = Console.ReadLine();

            Console.Write("Weight (kg): ");
            double weight;
            double.TryParse(Console.ReadLine(), out weight);

            Console.Write("Body fat (%): ");
            double fat;
            double.TryParse(Console.ReadLine(), out fat);

            Console.Write("Notes (optional): ");
            string notes = Console.ReadLine();

            Progress p = new Progress(name, weight, fat, notes);
            progressList.Add(p);

            Console.WriteLine();
            Console.WriteLine("Progress saved.");
        }

        private static void DisplayClientProgress()
        {
            Console.Write("Client name: ");
            string name = Console.ReadLine();

            bool found = false;

            foreach (var p in progressList)
            {
                if (p.ClientName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    if (!found)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Progress history for " + name + ":");
                        Console.WriteLine("----------------------------------------");
                    }
                    p.Print();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No progress found for this client.");
            }
        }

        private static void EditPlan()
        {
            Console.Write("Client name: ");
            string name = Console.ReadLine();

            NutritionPlan plan = FindPlanByClient(name);

            if (plan == null)
            {
                Console.WriteLine("No nutrition plan found for this client.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Current plan:");
            plan.PrintDetails();

            Console.WriteLine();
            Console.Write("New daily calories (leave empty to keep current): ");
            string calText = Console.ReadLine();
            int cal;
            if (!string.IsNullOrWhiteSpace(calText) && int.TryParse(calText, out cal))
            {
                plan.DailyCalories = cal;
            }

            Console.Write("Do you want to replace meals? (y/n): ");
            string answer = Console.ReadLine();

            if (answer == "y" || answer == "Y")
            {
                while (plan.Meals.Count > 0)
                {
                    plan.Meals.RemoveAt(0);
                }

                Console.WriteLine("Enter new meals (empty line to finish):");
                while (true)
                {
                    string meal = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(meal))
                        break;

                    plan.AddMeal(meal);
                }
            }

            Console.Write("New notes (leave empty to keep current): ");
            string newNotes = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newNotes))
            {
                plan.Notes = newNotes;
            }

            Console.WriteLine();
            Console.WriteLine("Plan updated.");
        }

        private static void ShowStatistics()
        {
            Console.WriteLine("Total nutrition plans: " + NutritionPlan.TotalPlans);
            Console.WriteLine("Total progress records: " + Progress.TotalRecords);

            if (progressList.Count > 0)
            {
                double sum = 0;
                for (int i = 0; i < progressList.Count; i++)
                {
                    sum += progressList[i].Weight;
                }
                double average = sum / progressList.Count;

                Console.WriteLine("Average recorded weight: " + average + " kg");
            }
        }
    }

    // ====================== GYM / MEMBERSHIP ======================

    public class Gym
    {
        public string Name { get; set; }
        public string Location { get; set; }

        public Gym(string name, string location)
        {
            Name = name;
            Location = location;
        }

        public void PrintInfo()
        {
            Console.WriteLine("Gym name: " + Name);
            Console.WriteLine("Location: " + Location);
        }
    }

    public class Membership
    {
        public string TypeName { get; set; }
        public double PricePerMonth { get; set; }
        public int DurationMonths { get; set; }

        public Membership(string typeName, double pricePerMonth, int durationMonths)
        {
            TypeName = typeName;
            PricePerMonth = pricePerMonth;
            DurationMonths = durationMonths;
        }
    }

    public static class GymMembershipManager
    {
        private static Gym gym = new Gym("My Fitness Club", "Main Street");
        private static List<Membership> memberships = new List<Membership>();

        public static int TotalMembershipTypes
        {
            get { return memberships.Count; }
        }

        public static void Run()
        {
            bool back = false;

            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Gym and Memberships ===");
                Console.WriteLine("1- Show gym information");
                Console.WriteLine("2- Add membership type");
                Console.WriteLine("3- Show membership types");
                Console.WriteLine("0- Back");
                Console.Write("Choose: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                if (input == "1")
                {
                    gym.PrintInfo();
                }
                else if (input == "2")
                {
                    AddMembership();
                }
                else if (input == "3")
                {
                    ShowMemberships();
                }
                else if (input == "0")
                {
                    back = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }

                if (!back)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void AddMembership()
        {
            Console.Write("Membership name: ");
            string name = Console.ReadLine();

            Console.Write("Price per month: ");
            double price;
            double.TryParse(Console.ReadLine(), out price);

            Console.Write("Duration (months): ");
            int months;
            int.TryParse(Console.ReadLine(), out months);

            Membership m = new Membership(name, price, months);
            memberships.Add(m);

            Console.WriteLine("Membership type added.");
        }

        private static void ShowMemberships()
        {
            if (memberships.Count == 0)
            {
                Console.WriteLine("No membership types yet.");
                return;
            }

            Console.WriteLine("Membership types:");
            Console.WriteLine("--------------------------------");
            foreach (var m in memberships)
            {
                Console.WriteLine("- " + m.TypeName + " | " + m.PricePerMonth + " per month | " + m.DurationMonths + " months");
            }
        }
    }

    // ====================== REPORTS ======================

    public static class ReportManager
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("=== Reports and Statistics ===");
            Console.WriteLine();

            Console.WriteLine("Total clients: " + ClientTrainerManager.TotalClients);
            Console.WriteLine("Total trainers: " + ClientTrainerManager.TotalTrainers);
            Console.WriteLine("Total exercises: " + WorkoutManager.TotalExercises);
            Console.WriteLine("Total workout plans: " + WorkoutManager.TotalWorkoutPlans);
            Console.WriteLine("Total nutrition plans: " + NutritionPlan.TotalPlans);
            Console.WriteLine("Total progress records: " + Progress.TotalRecords);
            Console.WriteLine("Total membership types: " + GymMembershipManager.TotalMembershipTypes);

            Console.WriteLine();
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
    }
}
