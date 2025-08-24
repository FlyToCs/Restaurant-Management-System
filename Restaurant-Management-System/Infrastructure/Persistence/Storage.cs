using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Management_System.Domain.Entities;
using Restaurant_Management_System.Domain.Enums;


namespace Restaurant_Management_System.Infrastructure.Persistence
{
    public static class Storage
    {
        public static List<User> UserList = new();
        public static User? CurrentUser;

        public static List<Restaurant> RestaurantList = new();
        public static Restaurant? CurrentRestaurant;

        public static List<Order> Orders = new();
        public static Order? CurrentOrder;

        static Storage()
        {
            // ------------------ Users ------------------
            UserList.Add(new Customer("ali", "Akbari","a@gmail.com", "123", RoleEnum.Customer,
                new ContactInfo() { Address = "Simin bolivar, Punak", City = "Tehran", Country = "Iran", Phone = "0215555555", PhoneNumber = "09123333333" }));
            UserList.Add(new Customer("Baran", "Sadeghi","b@gmail.com", "123", RoleEnum.Customer,
                new ContactInfo() { Address = "Enghelab Square", City = "Karaj", Country = "Iran", Phone = "0215555555", PhoneNumber = "09123333333" }));
            UserList.Add(new Customer("Kamran", "Mooavi","k@gmail.com", "123", RoleEnum.Customer,
                new ContactInfo() { Address = "Sis Street, k50", City = "Mashhad", Country = "Iran", Phone = "0215555555", PhoneNumber = "09123333333" }));
            // UserList.Add(new Admin("Mohammad", "Dehghani","m@gmail.com", "123456", RoleEnum.Admin,
            //     new ContactInfo() { Address = "Qhods street, Valiasr", City = "Qom", Country = "Iran", Phone = "0215555555", PhoneNumber = "09123333333" }));

            var admin1 = new Admin("Mohammad", "Dehghani", "m@gmail.com", "123456", RoleEnum.Admin,
                new ContactInfo()
                {
                    Address = "Qhods street, Valiasr", City = "Qom", Country = "Iran", Phone = "0215555555",
                    PhoneNumber = "09123333333"
                });
            UserList.Add(admin1);

            // ------------------ Restaurants ------------------
            var toranjMenu = new List<Food>()
        {
            new MainDish("Cheeseburger", FoodTypeEnum.MainDish, false, 180_000) { Description = "Juicy beef with cheese" },
            new MainDish("Pepperoni Pizza", FoodTypeEnum.MainDish, true, 250_000) { Description = "Thin crust pizza with spicy pepperoni" },
            new Appetizer("French Fries", FoodTypeEnum.Appetizer, false, 80_000) { Description = "Crispy golden fries" },
            new Drink("Coca Cola", FoodTypeEnum.Drink, false, 40_000) { Description = "Refreshing soda" },
            new Dessert("Chocolate Cake", FoodTypeEnum.Dessert, 8, false, 95_000) { Description = "Rich dark chocolate" }
        };
            var toranj = new Restaurant("Toranj", "Best fast food restaurant", toranjMenu);
            toranj.Owner = admin1;

            var shandizMenu = new List<Food>()
        {
            new MainDish("Chelow Kebab Koobideh", FoodTypeEnum.MainDish, false, 190_000) { Description = "Grilled minced lamb with rice" },
            new MainDish("Joojeh Kebab", FoodTypeEnum.MainDish, false, 210_000) { Description = "Saffron grilled chicken with rice" },
            new Appetizer("Shirazi Salad", FoodTypeEnum.Appetizer, false, 70_000) { Description = "Cucumber, tomato, onion salad" },
            new Drink("Doogh", FoodTypeEnum.Drink, false, 45_000) { Description = "Yogurt-based cold drink" },
            new Dessert("Shole Zard", FoodTypeEnum.Dessert, 9, false, 85_000) { Description = "Saffron rice pudding" }
        };
            var shandiz = new Restaurant("Shandiz", "Authentic Persian kebabs and stews", shandizMenu);
            shandiz.Owner = admin1;
            var naderiMenu = new List<Food>()
        {
            new MainDish("Kotlet Sandwich", FoodTypeEnum.MainDish, false, 120_000) { Description = "Homemade kotlet in bread" },
            new Appetizer("Soup Jo", FoodTypeEnum.Appetizer, true, 80_000) { Description = "Creamy barley soup" },
            new Drink("Persian Tea with Nabat", FoodTypeEnum.Drink, false, 35_000) { Description = "Black tea with rock candy" },
            new Dessert("Napoleon Pastry", FoodTypeEnum.Dessert, 7, false, 85_000) { Description = "Layered puff pastry with cream" }
        };
            var naderi = new Restaurant("Naderi Cafe", "Famous Persian café with pastries and drinks", naderiMenu);
            naderi.Owner = admin1;



            RestaurantList.Add(toranj);
            RestaurantList.Add(shandiz);
            RestaurantList.Add(naderi);


            admin1.Restaurants.Add(toranj);
            admin1.Restaurants.Add(shandiz);
            admin1.Restaurants.Add(naderi);

            // ------------------ Orders ------------------
            var customer1 = (Customer)UserList[0];
            var customer2 = (Customer)UserList[1];
            var customer3 = (Customer)UserList[2];

            // Order1: Toranj, چند غذا
            List<Food> order1Foods = new List<Food>();
            
            order1Foods.Add(toranjMenu[0]); // Cheeseburger
            order1Foods.Add(toranjMenu[3]); // Coca Cola
            
            Orders.Add(new Order(customer1, toranj, order1Foods));

            // Order2: Shandiz
            List<Food> order2Foods = new List<Food>();
            order2Foods.Add(shandizMenu[0]); // Chelow Kebab
            order2Foods.Add(shandizMenu[4]); // Shole Zard
            Orders.Add(new Order(customer2, shandiz, order2Foods));

            // Order3: Naderi Cafe
            List<Food> order3Foods = new List<Food>();
            order3Foods.Add(naderiMenu[0]); // Kotlet Sandwich
            order3Foods.Add(naderiMenu[3]); // Napoleon Pastry
            Orders.Add(new Order(customer3, naderi, order3Foods));

            
        }
    }

}
