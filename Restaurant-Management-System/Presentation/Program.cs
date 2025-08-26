// See https://aka.ms/new-console-template for more information

using Figgle.Fonts;
using Restaurant_Management_System.Application.Services;
using Restaurant_Management_System.Domain.Entities;
using Restaurant_Management_System.Domain.Enums;
using Restaurant_Management_System.Domain.Interfaces;
using Restaurant_Management_System.Infrastructure;
using Restaurant_Management_System.Infrastructure.Exceptions;
using Restaurant_Management_System.Presentation;
using Sharprompt;
using Spectre.Console;
Console.OutputEncoding = System.Text.Encoding.UTF8;

IAuthentication authentication = new Authentication();
IRestaurantService restaurantService = new RestaurantService();
ICustomerService customerService = new CustomerService();
IOrderService orderService = new OrderService();
IAdminService adminService = new AdminService();







Console.WriteLine("نسخه اول رستوران".Reverse().ToArray());



await AnsiConsole.Progress()
    .Columns(new ProgressColumn[]
    {
        new TaskDescriptionColumn(),    
        new ProgressBarColumn(),        
        new PercentageColumn(),         
        new SpinnerColumn()           
    })
    .StartAsync(async ctx =>
    {
        var dbTask = ctx.AddTask("[red]Connecting to database[/]");
        var appTask = ctx.AddTask("[yellow]Loading application[/]");
        var uiTask = ctx.AddTask("[green]Building UI[/]");

        while (!ctx.IsFinished)
        {
            await Task.Delay(150);

            dbTask.Increment(2.5);
            appTask.Increment(2);
            uiTask.Increment(3.0);
        }
    });

AnsiConsole.MarkupLine("[bold cyan]✔ Application started successfully![/]");
Console.ReadKey();








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
    var currentUser = (Admin)Storage.CurrentUser!;
    Console.Clear();
    foreach (var currentUserRestaurant in currentUser.Restaurants)
    {
        ConsolePainter.CyanMessage($"{currentUserRestaurant.Id}. {currentUserRestaurant.Name}");
    }

    Console.Write("\nSelect a restaurant by id: ");
    int restaurantId = int.Parse(Console.ReadLine()!);
    adminService.ChangeRestaurant(adminService.GetRestaurant(currentUser, restaurantId));
    Console.ReadKey();
    
    var currentRestaurant = Storage.CurrentRestaurant;
    Customer currentCustomer = null!;


    while (true)
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
                "6. 🍴 View Orders",
                "7. 🥰 Add Order",
                "8. 🍽️ Add Restaurant",
                "9. ⚙️ Change Restaurant",
                "10.🥲 Logout"
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

                ConsolePainter.WriteTable(restaurantService.ShowMenu(Storage.CurrentRestaurant) ?? throw new InvalidOperationException(), ConsoleColor.Yellow, ConsoleColor.Cyan);
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
                ConsolePainter.WriteTable(currentRestaurant.Menu!);
                Console.Write("Enter an id to delete from menu: ");
                int foodIdToRemove = int.Parse(Console.ReadLine()!);
                restaurantService.RemoveFood(currentRestaurant, restaurantService.SearchFood(currentRestaurant, foodIdToRemove));

                break;

            case "4. 😊 Add Customer":
                Console.Write("FirsName: ");
                string fName = Console.ReadLine()!;

                Console.Write("LastName: ");
                string lName = Console.ReadLine()!;

                Console.Write("Email: ");
                string email = Console.ReadLine()!;

                Console.Write("Password: ");
                string password = Console.ReadLine()!;

                adminService.AddCustomer(new Customer(fName, lName, email, password, RoleEnum.Customer, null!));
                break;


            case "5. 😡 Remove Customer":

                ConsolePainter.WriteTable(adminService.GetAllCustomers(Storage.UserList), ConsoleColor.Yellow, ConsoleColor.Cyan);
                Console.Write("Enter an id to remove the customer: ");
                int customerIdToDelete = int.Parse(Console.ReadLine()!);
                var customerToDelete = adminService.SearchCustomer(customerIdToDelete);
                if (customerToDelete == null)
                {
                   ConsolePainter.RedMessage("Failed action"); 
                }
                else
                {
                    adminService.RemoveCustomer(customerToDelete);
                    ConsolePainter.GreenMessage("deleted successfully");
                }

                Console.ReadKey();
                break;


            case "6. 🍴 View Orders":
                var orders = orderService.GetOrders(Storage.CurrentRestaurant);

                if (orders.Count == 0)
                {
                    ConsolePainter.RedMessage("No orders found for this restaurant.");
                }
                else
                {
                    foreach (var order in orders)
                    {
                        Console.WriteLine($"\n📌 Order ID: {order.Id}    {order.Customer?.FirstName} {order.Customer?.LastName}");
                        Console.WriteLine("\n🍕 Items:");

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

                                Console.WriteLine($"    ✅ {food.Name} {extraInfo} | Price: {food.Price:N0}");
                            }
                        }
                        else
                        {
                            ConsolePainter.RedMessage(" - No foods in this order.");
                        }


                        if (order.Food != null)
                        {
                            ConsolePainter.CyanMessage(
                                $"\n💲 Total Price: {orderService.CalculateTotalPrice(order.Food):N0}");
                            ConsolePainter.CyanMessage(
                                $"💰 Total Discount: {orderService.CalculateTotalDiscount(order.Food):N0}");
                            ConsolePainter.CyanMessage(
                                $"💵 Final Price: {orderService.CalculateFinalPrice(order.Food):N0}");
                        }

                        Console.WriteLine(new string('-', 40));
                    }
                }

                Console.ReadKey();
                break;
            case "7. 🥰 Add Order":
                Console.Write("add new customer or user old: ");
                var selectCustomer = Prompt.Select("Select an option", new[]
                {
                    "1. New Customer",
                    "2. Old Customer",
                });
                if (selectCustomer == "2. Old Customer")
                {
                    ConsolePainter.WriteTable(adminService.GetAllCustomers(Storage.UserList), ConsoleColor.Yellow, ConsoleColor.Cyan);
                    Console.Write("Enter an id: ");
                    currentCustomer = adminService.SearchCustomer(int.Parse(Console.ReadLine()!));
                }
                else
                {
                    Console.Write("FirsName: "); 
                    fName = Console.ReadLine()!;

                    Console.Write("LastName: "); 
                    lName = Console.ReadLine()!;

                    Console.Write("Email: ");
                    email = Console.ReadLine()!;

                    Console.Write("Password: ");
                    password = Console.ReadLine()!;
                    currentCustomer = new Customer(fName, lName, email, password, RoleEnum.Customer, null!);
                    adminService.AddCustomer(currentCustomer);

                }
                bool addItemToOrder = true;
                List<Food> orderFood = new();
                while (addItemToOrder)
                {
                    Console.Clear();

                    ConsolePainter.WriteTable(currentRestaurant.Menu!);
                    Console.Write("enter an item id to add: ");
                    int foodId = int.Parse(Console.ReadLine()!);
                    var food = restaurantService.SearchFood(currentRestaurant, foodId);
                    orderFood.Add(food);

                    

                    var loop = Prompt.Select("Do you want to counting", new[]
                    {
                        "Yes",
                        "No",
                    });
                    if (loop == "No")
                        addItemToOrder = false;
                    

                }
                Storage.Orders.Add(new Order(currentCustomer, currentRestaurant, orderFood));
                break;

            case "8. 🍽️ Add Restaurant":
                Console.Write("\nEnter restaurant name: ");
                string restaurantName = Console.ReadLine()!;

                Console.Write("Description: ");
                string restaurantDescription = Console.ReadLine()!;
                
                adminService.AddRestaurant(new Restaurant(restaurantName, restaurantDescription), currentUser);
                ConsolePainter.WriteTable(adminService.GetAllRestaurants(currentUser));
                Console.ReadKey();
                break;
            case "9. ⚙️ Change Restaurant":
                foreach (var currentUserRestaurant in currentUser.Restaurants)
                {
                    ConsolePainter.CyanMessage($"{currentUserRestaurant.Id}. {currentUserRestaurant.Name}");
                }

                Console.Write("\nSelect a restaurant by id: ");
                restaurantId = int.Parse(Console.ReadLine()!);
                adminService.ChangeRestaurant(adminService.GetRestaurant(currentUser, restaurantId));


                break;
            case "10.🥲 Logout":
                Authentication(true);
                break;


        }
    }
}


Console.ReadKey();