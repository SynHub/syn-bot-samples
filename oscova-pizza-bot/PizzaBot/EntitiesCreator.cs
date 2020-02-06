using System.Collections.Generic;
using System.Text.RegularExpressions;
using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Recognizers;

namespace PizzaBot
{
    public static class EntitiesCreator
    {
        public static void Initialize()
        {
            OscovaBot.Instance.CreateRecognizer("start", new Regex(@"(?<!/)(/(?:start))(?(?<=\w)\b)", RegexOptions.IgnoreCase));

            CreateSizeEntities();
            CreateCrustEntities();
            CreateSauceEntities();
            CreateCheeseEntities();
            CreateToppingsEntities();

            OscovaBot.Instance.CreateRecognizer("pizza-coupon", new Regex(@"\b([a-zA-Z]{3}\d{3})\b"));
        }

        private static void CreateSizeEntities()
        {
            var bot = OscovaBot.Instance;
            SizeEntityRecognizer = bot.CreateRecognizer("pizza-size", 
                new[]
                {
                    "Small", "Medium", "Large", "Extra Large"
                });

            SizeEntityRecognizer.Entries.AddSynonyms("Small", new[] { "Indie", "Indee" });
            SizeEntityRecognizer.Entries.AddSynonyms("Medium", new[] { "Normal", "Any Size", "Your Wish", "Anything" });
            SizeEntityRecognizer.Entries.AddSynonyms("Large", new[] { "Big", "Bigger" });
            SizeEntityRecognizer.Entries.AddSynonyms("Extra Large", new[] { "X-Large", "X Large", "Huge", "xtra large" });
        }

        private static void CreateCrustEntities()
        {
            var bot = OscovaBot.Instance;
            CrustEntityRecognizer = bot.CreateRecognizer("pizza-crust",
                new[]
                {
                    "Stuffed Crust", "Classic Crust", "Thin & Crispy", "Italian Style",
                    "Double Decandence", "Wheat Multigrain", "Gluten-Free"
                });

            CrustEntityRecognizer.Entries.AddSynonyms("Stuffed Crust", new[] { "stuffed" });
            CrustEntityRecognizer.Entries.AddSynonyms("Classic Crust", new[] { "classic" });
            CrustEntityRecognizer.Entries.AddSynonyms("Thin & Crispy", new[] { "thin", "crispy", "thin and crispy", "thin n crispy" });
            CrustEntityRecognizer.Entries.AddSynonyms("Italian Style", new[] { "italian" });
            CrustEntityRecognizer.Entries.AddSynonyms("Double Decandence", new[] { "double", "decandence" });
            CrustEntityRecognizer.Entries.AddSynonyms("Wheat Multigrain", new[] { "wheat", "multigrain" });
            CrustEntityRecognizer.Entries.AddSynonyms("Gluten-Free", new[] { "gluten", "gluten free", "no gluten" });
        }

        private static void CreateSauceEntities()
        {
            var bot = OscovaBot.Instance;
            SauceEntityRecognizer = bot.CreateRecognizer("pizza-sauce",
                new[] 
                {
                    "Tomato", "Garlic", "BBQ", "Olive Oil",
                    "Balsamic Glaze", "Tangy Ranch", "Pesto", "Bean and Salsa"
                });

            SauceEntityRecognizer.Entries.AddSynonyms("Tomato", new[] { "tomatoe", "tomato sauce", "tomatoe sauce" });
            SauceEntityRecognizer.Entries.AddSynonyms("Garlic", new[] { "garlic sauce" });
            SauceEntityRecognizer.Entries.AddSynonyms("BBQ", new[] { "bbq sauce", "barbecue", "barbeque sauce" });
            SauceEntityRecognizer.Entries.AddSynonyms("Olive Oil", new[] { "olive", "oil", "olives", "olive sauce" });
            SauceEntityRecognizer.Entries.AddSynonyms("Balsamic Glaze", new[] { "balsamic", "balsamic sauce" });
            SauceEntityRecognizer.Entries.AddSynonyms("Tangy Ranch", new[] { "tangy", "ranch", "ranch sauce" });
            SauceEntityRecognizer.Entries.AddSynonyms("Pesto", new[] { "pesto sauce" });
            SauceEntityRecognizer.Entries.AddSynonyms("Bean and Salsa", new[] { "bean", "beans", "salsa", "bean n salsa", "bean & salsa" });
        }

        public static void CreateCheeseEntities()
        {
            var bot = OscovaBot.Instance;
            CheeseEntityRecognizer = bot.CreateRecognizer("pizza-cheese",
                new[]
                {
                    "Mozzarella", "Swiss", "Vegan", "Gorgonzola",
                    "Reduced Fat", "Goat", "Parmesan", "Cheddar",
                    "Ricotta"
                });

            CheeseEntityRecognizer.Entries.AddSynonyms("Reduced Fat", new[] { "no fat", "fat free", "without fat" });
            CheeseEntityRecognizer.Entries.AddSynonyms("Vegan", new[] { "vegetarian" });
        }

        private static void CreateToppingsEntities()
        {
            var bot = OscovaBot.Instance;
            ToppingsEntityRecognizer = bot.CreateRecognizer("pizza-toppings", 
                new[]
                {
                    "Anchovies", "Salami", "Pepperoni", "Meatballs",
                    "Mushrooms", "Bacon", "Smoked Ham", "Italian Sausage",
                    "Broccoli","Tomatoes","Garlic","Olives","Pineapple",
                    "Onions","Pepper","Spinach"
                });

            ToppingsEntityRecognizer.Entries.AddSynonyms("Anchovies", new[] { "Anchovy" });
            ToppingsEntityRecognizer.Entries.AddSynonyms("Meatballs", new[] { "Meatball" });
            ToppingsEntityRecognizer.Entries.AddSynonyms("Mushrooms", new []{"Mushroom"});
            ToppingsEntityRecognizer.Entries.AddSynonyms("Smoked Ham", new[] { "Ham", "Hams" });
            ToppingsEntityRecognizer.Entries.AddSynonyms("Bacon", new[] { "Bacons" });
            ToppingsEntityRecognizer.Entries.AddSynonyms("Italian Sausage", new[] { "Sausage", "Sausages" });

            ToppingsEntityRecognizer.Entries.AddSynonyms("Broccoli", new[] { "brocolis" });
            ToppingsEntityRecognizer.Entries.AddSynonyms("Tomatoes", new[] { "tomatoe", "tomato" });
            ToppingsEntityRecognizer.Entries.AddSynonyms("Garlic", new[] { "garlics" });
            ToppingsEntityRecognizer.Entries.AddSynonyms("Olives", new[] { "olive" });
            ToppingsEntityRecognizer.Entries.AddSynonyms("Pineapple", new[] { "pineapples" });
            ToppingsEntityRecognizer.Entries.AddSynonyms("Onions", new[] { "onion" });
            ToppingsEntityRecognizer.Entries.AddSynonyms("Pepper", new[] { "peppers" });
            ToppingsEntityRecognizer.Entries.AddSynonyms("Spinach", new[] { "spinaches" });
        }

        private static EntityRecognizer SizeEntityRecognizer { get; set; }
        private static EntityRecognizer CrustEntityRecognizer { get; set; }
        private static EntityRecognizer SauceEntityRecognizer { get; set; }
        private static EntityRecognizer CheeseEntityRecognizer { get; set; }
        private static EntityRecognizer ToppingsEntityRecognizer { get; set; }

        public static string GetSizeHint => ConvertToHint(SizeEntityRecognizer);
        public static string GetCrustHint => ConvertToHint(CrustEntityRecognizer);
        public static string GetSauceHint => ConvertToHint(SauceEntityRecognizer);
        public static string GetCheeseHint => ConvertToHint(CheeseEntityRecognizer);
        public static string GetToppingsHint => ConvertToHint(ToppingsEntityRecognizer);

        private static string ConvertToHint(EntityRecognizer recognizer)
        {
            var entityList = new List<string>();

            foreach (var item in recognizer.Entries)
            {
                //Avoid giving synonyms in Hints.
                if (string.IsNullOrEmpty(item.Reference)) entityList.Add(item.Value);
            }

            return string.Join("|", entityList);
        }
    }
}