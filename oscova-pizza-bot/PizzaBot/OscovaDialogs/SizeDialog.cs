using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace PizzaBot.OscovaDialogs
{
    internal class SizeDialog : Dialog
    {
        [Expression("@pizza-size")]
        [Expression("I want a @pizza-size one")]
        [Context(ContextName.WaitForSize)]
        public void SizeSelected(Context context, Result result)
        {
            var pizzaSize = result.Entities.OfType("pizza-size");

            var pizza = context.SharedData.OfType<PizzaHolder>();
            pizza.Size = pizzaSize.ToString();

            var response = new Response();
            response.Text = $"So \"{pizzaSize}\" it is. Now that you've selected the size. What would be your crust preference? You may scroll down for more options.";
            response.Hint = EntitiesCreator.GetCrustHint;
            result.SendResponse(response);

            context.Add(ContextName.WaitForCrust);

        }

        [Fallback(Context = ContextName.WaitForSize)]
        public void SizeFallback(Context context, Result result)
        {
            var response = new Response();
            response.Text ="That doesn't seem like a pizza size I am aware of. Please do specify a valid size so we can continue.";
            response.Hint = EntitiesCreator.GetSizeHint;
            result.SendResponse(response);

            context.Add(ContextName.WaitForSize);
        }
    }
}