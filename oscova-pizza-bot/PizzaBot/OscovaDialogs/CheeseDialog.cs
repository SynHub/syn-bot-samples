using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace PizzaBot.OscovaDialogs
{
    internal class CheeseDialog : Dialog
    {
        //User wants cheese
        [Expression("@sys.positive")]
        [Context(ContextName.ConfirmCheese)]
        public void YesSauce(Context context, Result result)
        {
            var response = new Response
            {
                Text = "Here are the cheese types we've got for you. Please select the one you prefer.",
                Hint = EntitiesCreator.GetCheeseHint
            };

            result.SendResponse(response);
            context.Add(ContextName.WaitForCheese);
        }

        //User doesn't want any cheese
        [Expression("@sys.negative")]
        [Context(ContextName.ConfirmCheese)]
        public void NoSauce(Context context, Result result)
        {
            var response = new Response
            {
                Text = "As you wish no cheese on your pizza. What would you like to add as your toppings?",
                Hint = EntitiesCreator.GetToppingsHint
            };

            context.Add(ContextName.WaitForToppings);

            //cheese selected. Start toppings...
            result.SendResponse(response);
        }

        //User selected cheese
        [Expression("@pizza-cheese")]
        [Context(ContextName.WaitForCheese)]
        public void SelectedSauce(Context context, Result result)
        {
            var cheeseEntity = result.Entities.OfType("pizza-cheese");
            var pizzaHolder = context.SharedData.OfType<PizzaHolder>();
            pizzaHolder.Cheese = cheeseEntity.ToString();

            var response = new Response
            {
                Text = $"Okay so your pizza will have \"{cheeseEntity}\" cheese. What would you like to add as your toppings?",
                Hint = EntitiesCreator.GetToppingsHint
            };

            context.Add(ContextName.WaitForToppings);
            result.SendResponse(response);
        }

        //User mentioned invalid cheese type.
        [Fallback(Context = ContextName.WaitForCheese)]
        public void SizeFallback(Context context, Result result)
        {
            var response = new Response();
            response.Text = "The mentioned cheese option is not available or is invalid. Please do specify a valid cheese type to continue.";
            response.Hint = EntitiesCreator.GetCheeseHint;
            result.SendResponse(response);

            context.Add(ContextName.WaitForCheese);
        }

        //User didn't properly confirm if he wants cheese or not.
        [Fallback(Context = ContextName.ConfirmCheese)]
        public void SauceConfirmationFallback(Context context, Result result)
        {
            var response = new Response();
            response.Text = "I was expecting a yes or no answer. So could you confirm if you would like cheese or not?";
            response.Hint = "Yes|No";
            result.SendResponse(response);

            context.Add(ContextName.ConfirmCheese);
        }
    }
}