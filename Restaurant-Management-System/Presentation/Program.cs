// See https://aka.ms/new-console-template for more information

using Figgle.Fonts;
using Restaurant_Management_System.Application.Interfaces;
using Restaurant_Management_System.Domain.Entities;
using Restaurant_Management_System.Domain.Enums;
using Restaurant_Management_System.Infrastructure.Exceptions;
using Restaurant_Management_System.Infrastructure.Implementations;
using Restaurant_Management_System.Infrastructure.Persistence;
using Restaurant_Management_System.Presentation;
using Sharprompt;
using Spectre.Console;

IAuthentication authentication = new Authentication();
IRestaurantService restaurantService = new RestaurantService();



Console.OutputEncoding = System.Text.Encoding.UTF8;



Console.WriteLine("نسخه اول رستوران".Reverse().ToArray());
//
// await AnsiConsole.Progress()
//     .StartAsync(async ctx =>
//     {
//         // Define tasks
//         var task1 = ctx.AddTask("[red]Connecting to database[/]");
//         var task2 = ctx.AddTask("[aqua]Loading application[/]");
//
//         while (!ctx.IsFinished)
//         {
//             // Simulate some work
//             await Task.Delay(150);
//
//             // Increment
//             task1.Increment(5.5);
//             task2.Increment(2.5);
//         }
//     });




Authentication(true);




void Authentication(bool flag)
{
    while (flag)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(FiggleFonts.Standard.Render("Authentication"));
        Console.ResetColor();

        var select = Prompt.Select("Select an option", new[]
        {
            "1. Login",
            "2. Register",
            "3. Exit"
        });
        Console.WriteLine("------------------");



        switch (select)
        {
            case "1. Login":
                Console.Write("\nEmail: ");
                string email = Console.ReadLine()!;

                Console.Write("Password: ");
                string password = Console.ReadLine()!;

                try
                {
                    Storage.CurrentUser = authentication.Login(email, password);
                    ConsolePainter.GreenMessage("Login successfully");
                    Console.ReadKey();
                    if (Storage.CurrentUser != null) MainMenu(Storage.CurrentUser.Role);
                }
                catch (InvalidEmailException ex)
                {
                    ConsolePainter.RedMessage(ex.Message);
                    Console.ReadKey();
                }
                catch (UserNotFoundException ex)
                {
                    ConsolePainter.RedMessage(ex.Message);
                    Console.ReadKey();
                }
                catch (InvalidPasswordException ex)
                {
                    ConsolePainter.RedMessage(ex.Message);
                    Console.ReadKey();
                }
                break;

            case "2. Register":
                Console.Write("\nEmail: ");
                string newEmail = Console.ReadLine()!;

                Console.Write("Password: ");
                string newPassword = Console.ReadLine()!;

                RoleEnum newRole;
                var role = Prompt.Select("Select a Role", new[] { "Customer", "Admin" });
                newRole = role == "Customer" ? RoleEnum.Customer : RoleEnum.Admin;

                try
                {
                    Storage.CurrentUser = authentication.Register(newEmail, newPassword, newRole);
                    ConsolePainter.GreenMessage("Register successfully");
                    Console.ReadKey();
                    if (Storage.CurrentUser != null) MainMenu(Storage.CurrentUser.Role);
                }
                catch (InvalidEmailException ex)
                {
                    ConsolePainter.RedMessage(ex.Message);
                    Console.ReadKey();
                }
                catch (InvalidPasswordException ex)
                {
                    ConsolePainter.RedMessage(ex.Message);
                    Console.ReadKey();
                }
                break;

            case "3. Exit":
                flag = false;
                break;
        }
    }
}

void MainMenu(RoleEnum role)
{

    bool exit = false;

    while (!exit)
    {
        string[] options;
        Console.Clear();
        if (role == RoleEnum.Admin)
        {
            //m@gmail.com 
            options = new[]
            {
                "0. 🍳 Show Menu",
                "1. 😋 Add Food",
                "2. 🔄️ Update Food",
                "3. ❌ Remove Food",
                "4. 😊 Add Customer",
                "5. 😡 Remove Customer",
                "6. 🥰 Add Order",
                "7. 🍴 View Orders",
                "8. 🍽️ Add Restaurant",
                "9. 🥲 Logout"
            };
        }
        else // Customer
        {
            options = new[]
            {
                "1. Add Food 😋",
                "2. Place Order",
                "3. View My Orders",
                "4. Exit"
            };
        }

        var select = Prompt.Select("Select an option", options);

        switch (select)
        {
            // --- ADMIN OPTIONS ---
            case "0. 🍳 Show Menu":

                ConsolePainter.WriteTable(restaurantService.ShowMenu(Storage.CurrentRestaurant), ConsoleColor.Yellow, ConsoleColor.Cyan);
                Console.ReadKey();
                break;
            case "1. 😋 Add Food":
                Console.Write("Enter food name: ");
                string name = Console.ReadLine()!;


                var typeSelect = Prompt.Select("Select food type", new[]
                {
                    "MainDish",
                    "Appetizer",
                    "Drink",
                    "Dessert"
                });

                FoodTypeEnum type;

                if (typeSelect == "MainDish") type = FoodTypeEnum.MainDish;
                else if (typeSelect == "Appetizer") type = FoodTypeEnum.Appetizer;
                else if (typeSelect == "Drink") type = FoodTypeEnum.Drink;
                else if (typeSelect == "Dessert") type = FoodTypeEnum.Dessert;
                else throw new Exception("Invalid selection");

                Console.Write("Enter price: ");
                decimal price = decimal.Parse(Console.ReadLine()!);

                Food newFood;
                switch (type)
                {
                    case FoodTypeEnum.MainDish:
                        var spicySelect = Prompt.Select("Is it spicy?", new[] { "No", "Yes" });
                        bool isSpicy = spicySelect == "Yes";
                        newFood = new MainDish(name, type, isSpicy, price);
                        break;

                    case FoodTypeEnum.Appetizer:
                        var warmSelect = Prompt.Select("Is it warm?", new[] { "No", "Yes" });
                        bool isWarm = warmSelect == "Yes";
                        newFood = new Appetizer(name, type, isWarm, price);
                        break;

                    case FoodTypeEnum.Drink:
                        var alcoholicSelect = Prompt.Select("Is it alcoholic?", new[] { "No", "Yes" });
                        bool isAlcoholic = alcoholicSelect == "Yes";
                        newFood = new Drink(name, type, isAlcoholic, price);
                        break;

                    case FoodTypeEnum.Dessert:
                        var sweetnessSelect = Prompt.Select("Sweetness level", new[]
                        {
                            "1","2","3","4","5","6","7","8","9","10"
                        });
                        int sweetness = int.Parse(sweetnessSelect);
                        var frozenSelect = Prompt.Select("Is it frozen?", new[] { "No", "Yes" });
                        bool isFrozen = frozenSelect == "Yes";
                        newFood = new Dessert(name, type, sweetness, isFrozen, price);
                        break;

                    default:
                        throw new Exception("Invalid food type");
                }

                Storage.CurrentRestaurant!.Menu!.Add(newFood);
                Console.WriteLine($"{newFood.Name} added to {Storage.CurrentRestaurant.Name} menu successfully.");
                break;



            case "2. 🔄️ Update Food":

                break;

            case "3. ❌ Remove Food":

                break;

            case "4. 😊 Add Customer":

                break;


            case "5. 😡 Remove Customer":
                
                ConsolePainter.WriteTable(restaurantService.GetCustomers(), ConsoleColor.Yellow, ConsoleColor.Cyan);
                Console.ReadKey();
                break;
            case "6. 🥰 Add Order":

                break;

            case "7. 🍴 View Orders":
                Storage.CurrentRestaurant = Storage.RestaurantList[1];
                var orders = restaurantService.GetOrders(Storage.CurrentRestaurant);

                if (orders.Count == 0)
                {
                    Console.WriteLine("No orders found for this restaurant.");
                }
                else
                {
                    foreach (var order in orders)
                    {
                        Console.WriteLine($"Order ID: {order.Id}");
                        Console.WriteLine($"Customer: {order.Customer?.FirstName} {order.Customer?.LastName}");
                        Console.WriteLine("Foods:");

                        if (order.Food != null && order.Food.Count > 0)
                        {
                            foreach (var food in order.Food)
                            {
                                string extraInfo = "";

                                if (food is MainDish mainDish)
                                    extraInfo = mainDish.IsSpicy ? "(Spicy)" : "(Not spicy)";
                                else if (food is Appetizer appetizer)
                                    extraInfo = appetizer.IsWarm ? "(Warm)" : "(Cold)";
                                else if (food is Drink drink)
                                    extraInfo = drink.IsAlcoholic ? "(Alcoholic)" : "(Non-alcoholic)";
                                else if (food is Dessert dessert)
                                    extraInfo = dessert.IsFrozen ? $"(Frozen, Sweetness {dessert.SweetnessLevel})" : $"(Sweetness {dessert.SweetnessLevel})";

                                Console.WriteLine($" - {food.Name} {extraInfo} | Price: {food.Price:N0}");
                            }
                        }
                        else
                        {
                            ConsolePainter.RedMessage(" - No foods in this order.");
                        }


                        ConsolePainter.CyanMessage($"Total Price: {restaurantService.CalculateTotalPrice(order.Food):N0}");
                        ConsolePainter.CyanMessage($"Total Discount: {restaurantService.CalculateTotalDiscount(order.Food):N0}");
                        ConsolePainter.CyanMessage($"Final Price: {restaurantService.CalculateFinalPrice(order.Food):N0}");
                        Console.WriteLine(new string('-', 40));
                    }
                }

                Console.ReadKey();
                break;

            case "8. 🍽️ Add Restaurant":
                break;
            case "9. 🥲 Logout":
                exit = true;
                break;


        }
    }
}


Console.ReadKey();