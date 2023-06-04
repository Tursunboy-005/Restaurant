namespace Restaurant;
using System;
using System.Collections.Generic;

// Base class for User
class User
{
    public string Name { get; set; }
    public string Role { get; set; }

    public User(string name, string role)
    {
        Name = name;
        Role = role;
    }

    public virtual void PerformAction()
    {
        Console.WriteLine("Performing generic user action...");
    }
}

// Derived class for Waiter
class Waiter : User
{
    public Waiter(string name) : base(name, "Waiter")
    {
    }

    public override void PerformAction()
    {
        Console.WriteLine("Performing waiter action...");
        TakeOrder();
        ServeFood();
    }

    private void TakeOrder()
    {
        Console.WriteLine("Taking order from customer...");
    }

    private void ServeFood()
    {
        Console.WriteLine("Serving food to customer...");
    }
}

// Derived class for Chef
class Chef : User
{
    public Chef(string name) : base(name, "Chef")
    {
    }

    public override void PerformAction()
    {
        Console.WriteLine("Performing chef action...");
        PrepareFood();
        CookFood();
    }

    private void PrepareFood()
    {
        Console.WriteLine("Preparing food...");
    }

    private void CookFood()
    {
        Console.WriteLine("Cooking food...");
    }
}

// Food class
class Food
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Food(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"Food: {Name}");
        Console.WriteLine($"Price: {Price:C}");
    }
}

// Delegate for restaurant events
delegate void RestaurantEvent();

// Restaurant class
class Restaurant
{
    private List<User> users;
    private List<Food> menu;

    public Restaurant()
    {
        users = new List<User>();
        menu = new List<Food>();
    }

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public void RemoveUser(User user)
    {
        users.Remove(user);
    }

    public void AddFoodToMenu(Food food)
    {
        menu.Add(food);
    }

    public void RemoveFoodFromMenu(Food food)
    {
        menu.Remove(food);
    }

    public void Start()
    {
        Console.WriteLine("Welcome to the Restaurant Management System!");
        Console.WriteLine("---------------------------------------------");

        while (true)
        {
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Remove User");
            Console.WriteLine("3. Add Food to Menu");
            Console.WriteLine("4. Remove Food from Menu");
            Console.WriteLine("5. Perform Actions");
            Console.WriteLine("6. View Menu");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddUserMenu();
                    break;
                case 2:
                    RemoveUserMenu();
                    break;
                case 3:
                    AddFoodToMenu();
                    break;
                case 4:
                    RemoveFoodFromMenu();
                    break;
                case 5:
                    PerformActions();
                    break;
                case 6:
                    ViewMenu();
                    break;
                case 7:
                    Console.WriteLine("Exiting the application...");
                    return;
                default:
                    Console.WriteLine("Invalid choice! Please try again.");
                    break;
            }
        }
    }

    private void AddUserMenu()
    {
        Console.WriteLine("Select the user type:");
        Console.WriteLine("1. Waiter");
        Console.WriteLine("2. Chef");
        Console.Write("Enter your choice: ");
        int userType = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the user's name: ");
        string userName = Console.ReadLine();

        switch (userType)
        {
            case 1:
                User waiter = new Waiter(userName);
                AddUser(waiter);
                Console.WriteLine($"Waiter {userName} added successfully!");
                break;
            case 2:
                User chef = new Chef(userName);
                AddUser(chef);
                Console.WriteLine($"Chef {userName} added successfully!");
                break;
            default:
                Console.WriteLine("Invalid user type! Please try again.");
                break;
        }
    }

    private void RemoveUserMenu()
    {
        if (users.Count == 0)
        {
            Console.WriteLine("No users to remove!");
            return;
        }

        Console.WriteLine("Select the user to remove:");
        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {users[i].Name} ({users[i].Role})");
        }

        Console.Write("Enter your choice: ");
        int userIndex = Convert.ToInt32(Console.ReadLine()) - 1;

        if (userIndex >= 0 && userIndex < users.Count)
        {
            User removedUser = users[userIndex];
            RemoveUser(removedUser);
            Console.WriteLine($"{removedUser.Role} {removedUser.Name} removed successfully!");
        }
        else
        {
            Console.WriteLine("Invalid user choice! Please try again.");
        }
    }

    private void AddFoodToMenu()
    {
        Console.Write("Enter the food name: ");
        string foodName = Console.ReadLine();

        Console.Write("Enter the food price: ");
        decimal foodPrice = Convert.ToDecimal(Console.ReadLine());

        Food food = new Food(foodName, foodPrice);
        AddFoodToMenu(food);
        Console.WriteLine($"Food {food.Name} added to the menu successfully!");
    }

    private void RemoveFoodFromMenu()
    {
        if (menu.Count == 0)
        {
            Console.WriteLine("No food items in the menu!");
            return;
        }

        Console.WriteLine("Select the food item to remove:");
        for (int i = 0; i < menu.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {menu[i].Name}");
        }

        Console.Write("Enter your choice: ");
        int foodIndex = Convert.ToInt32(Console.ReadLine()) - 1;

        if (foodIndex >= 0 && foodIndex < menu.Count)
        {
            Food removedFood = menu[foodIndex];
            RemoveFoodFromMenu(removedFood);
            Console.WriteLine($"{removedFood.Name} removed from the menu successfully!");
        }
        else
        {
            Console.WriteLine("Invalid food choice! Please try again.");
        }
    }

    private void PerformActions()
    {
        if (users.Count == 0)
        {
            Console.WriteLine("No users available to perform actions!");
            return;
        }

        Console.WriteLine("Select the user to perform actions:");
        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {users[i].Name} ({users[i].Role})");
        }

        Console.Write("Enter your choice: ");
        int userIndex = Convert.ToInt32(Console.ReadLine()) - 1;

        if (userIndex >= 0 && userIndex < users.Count)
        {
            User selectedUser = users[userIndex];
            Console.WriteLine($"Selected user: {selectedUser.Name} ({selectedUser.Role})");

            // Perform user actions
            selectedUser.PerformAction();
        }
        else
        {
            Console.WriteLine("Invalid user choice! Please try again.");
        }
    }

    private void ViewMenu()
    {
        if (menu.Count == 0)
        {
            Console.WriteLine("No food items in the menu!");
            return;
        }

        Console.WriteLine("Menu:");
        foreach (Food food in menu)
        {
            food.DisplayDetails();
            Console.WriteLine();
        }
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        Restaurant restaurant = new Restaurant();
        restaurant.Start();
    }
}
