using System;
using System.Collections.Generic;
using System.Linq;
using Syn.Utilities;

namespace PizzaBot
{
    public class PizzaHolder
    {
        internal PizzaHolder()
        {
            Toppings = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Size = string.Empty;
            Crust = string.Empty;
            Address = string.Empty;

            Sauce = "No Sauce";
            Cheese = "No Cheese";
            Coupon = string.Empty;
        }

        public string Size { get; set; }
        public string Cheese { get; set; }
        public string Coupon { get; set; }

        public string Crust { get; set; }
        public string Sauce { get; set; }
        public string Address { get; set; }

        public HashSet<string> Toppings { get; }

        public override string ToString()
        {
            var sizeName = Size.ToLower().Replace("size", string.Empty);
            var crustName = Crust.ToLower().Replace("crust", string.Empty);

            var sauceName = Sauce.ToLower().Replace("sauce", string.Empty) + " sauce";
            var cheeseName = Cheese.ToLower().Replace("cheese", string.Empty) + " cheese";

            return $"It is of {sizeName} size with {crustName} crust, {sauceName}, {cheeseName}, {ToppingsToString()} as toppings.";
        }

        public string ToppingsToString()
        {
            return Utility.Text.GetFormattedSentence(Toppings.ToList());
        }
    }
}