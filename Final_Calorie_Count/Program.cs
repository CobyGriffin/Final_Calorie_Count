using System;

public class FoodItem
{
    public string Name { get; set; }
    public int Category { get; set; }
    public int Calories { get; set; }
    public int Quantity { get; set; }

    public FoodItem(string name, int category, int calories, int quantity)
    {
        Name = name;
        Category = category;
        Calories = calories;
        Quantity = quantity;
    }

    public FoodItem()
    {
        Name = "No Item Listed";
        Category = -1;
        Calories = -1;
        Quantity = -1;
    }

    public double TotalCalories()
    {
        return Calories * Quantity;
    }

    public string CategoryName()
    {
        return Category switch
        {
            0 => "Fruit",
            1 => "Vegetable",
            2 => "Protein",
            3 => "Grain",
            4 => "Dairy",
            _ => "No Category Chosen"
        };
    }

    public string DisplayInformation()
    {
        return $"Name: {Name}\nCategory: {CategoryName()}\nCalories: {Calories}\nQuantity: {Quantity}\nTotal Calories: {TotalCalories()}\n";
    }
}

public class Program
{
    static FoodItem[] foodItems = new FoodItem[4];

    public static void Main(string[] args)
    {
        Preload();
        Menu();
    }

    static void Preload()
    {
        foodItems[0] = new FoodItem("Apple", 0, 95, 1);
        foodItems[1] = new FoodItem("Banana", 0, 105, 2);
    }

    static void Menu()
    {
        int choice;
        do
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Display all the food items");
            Console.WriteLine("2. Add New Items");
            Console.WriteLine("3. Calculate your total calories eaten");
            Console.WriteLine("4. Calculate the average calories of an item you've eaten");
            Console.WriteLine("5. Display all food eaten of a certain category");
            Console.WriteLine("6. Search for a food item by name");
            Console.WriteLine("7. Exit");
            Console.Write("Please select an option (1-7): ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    DisplayAllFoodItems();
                    break;
                case 2:
                    AddItem();
                    break;
                case 3:
                    double totalCalories = TotalCaloriesEaten();
                    Console.WriteLine($"Your total calories are {totalCalories}");
                    break;
                case 4:
                    double averageCalories = AverageCalorieEaten();
                    Console.WriteLine($"Your average calories are {averageCalories}");
                    break;
                case 5:
                    DisplayByCategory();
                    break;
                case 6:
                    Console.Write("Please enter a food name: ");
                    string? foodName = Console.ReadLine();
                    DisplayItemWithName(foodName);
                    break;
                case 7:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        } while (choice != 7);
    }

    static void DisplayAllFoodItems()
    {
        foreach (var item in foodItems)
        {
            if (item != null)
            {
                Console.WriteLine(item.DisplayInformation());
            }
        }
    }

    static void AddItem()
    {
        FoodItem newItem = MakeNewItem();
        int firstIndex = FindEmptyIndex();

        if (firstIndex == -1)
        {
            IncreaseArraySize();
            firstIndex = FindEmptyIndex();
        }

        foodItems[firstIndex] = newItem;
    }

    static FoodItem MakeNewItem()
    {
        Console.Write("Enter Name: ");
        string? name = Console.ReadLine();

        int category;
        do
        {
            Console.Write("Enter Category (0: Fruit, 1: Vegetable, 2: Protein, 3: Grain, 4: Dairy): ");
        } while (!int.TryParse(Console.ReadLine(), out category) || category < 0 || category > 4);

        int calories;
        do
        {
            Console.Write("Enter Calories: ");
        } while (!int.TryParse(Console.ReadLine(), out calories));

        int quantity;
        do
        {
            Console.Write("Enter Quantity: ");
        } while (!int.TryParse(Console.ReadLine(), out quantity));

        return new FoodItem(name, category, calories, quantity);
    }

    static int FindEmptyIndex()
    {
        for (int i = 0; i < foodItems.Length; i++)
        {
            if (foodItems[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    static void IncreaseArraySize()
    {
        int newSize = foodItems.Length * 2;
        FoodItem[] newArray = new FoodItem[newSize];
        for (int i = 0; i < foodItems.Length; i++)
        {
            newArray[i] = foodItems[i];
        }
        foodItems = newArray;
    }

    static double TotalCaloriesEaten()
    {
        double totalCalories = 0;
        foreach (var item in foodItems)
        {
            if (item != null)
            {
                totalCalories += item.TotalCalories();
            }
        }
        return totalCalories;
    }

    static double AverageCalorieEaten()
    {
        double totalCalories = 0;
        int count = 0;
        foreach (var item in foodItems)
        {
            if (item != null)
            {
                totalCalories += item.TotalCalories();
                count++;
            }
        }
        return count == 0 ? 0 : totalCalories / count;
    }

    static void DisplayByCategory()
    {
        Console.Write("Please select a category (0: Fruit, 1: Vegetable, 2: Protein, 3: Grain, 4: Dairy): ");
        int category;
        while (!int.TryParse(Console.ReadLine(), out category) || category < 0 || category > 4)
        {
            Console.WriteLine("Invalid input. Please try again.");
        }

        foreach (var item in foodItems)
        {
            if (item != null && item.Category == category)
            {
                Console.WriteLine(item.DisplayInformation());
            }
        }
    }

    static void DisplayItemWithName(string foodName)
    {
        bool found = false;
        foreach (var item in foodItems)
        {
            if (item != null && item.Name.Equals(foodName, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(item.DisplayInformation());
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine($"The name '{foodName}' doesn't exist.");
        }
    }
}