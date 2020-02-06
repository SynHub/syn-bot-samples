using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace PizzaBot.OscovaDialogs
{
    internal class ToppingsDialog : Dialog
    {
        [Action(ID = ActionName.ToppingsAgain)]
        public void ToppingsAgainAction(Context context, Result result)
        {
            var response = new Response
            {
                Text = "What would you like to add as your toppings?",
                Hint = EntitiesCreator.GetToppingsHint
            };
            result.SendResponse(response);
            context.Add(ContextName.WaitForToppings);
        }

        [Expression("@sys.positive")]
        [Context(ContextName.ConfirmMoreToppings)]
        public void MoreToppings(Context context, Result result)
        {
            result.Bot.Actions[ActionName.ToppingsAgain].Invoke(context, result);
        }

        [Expression("@sys.negative")]
        [Context(ContextName.ConfirmMoreToppings)]
        public void NoToppings(Context context, Result result)
        {
            result.Bot.Actions[ActionName.CouponStart].Invoke(context, result);
        }

        //User selected just 1 topping. Ask if he wants to add more toppings.
        [Expression("@pizza-toppings")]
        [Context(ContextName.WaitForToppings)]
        public void OneToppingsSelected(Context context, Result result)
        {
            var toppingsEntity = result.Entities.OfType("pizza-toppings");
            var pizzaHolder = context.SharedData.OfType<PizzaHolder>();

            if (pizzaHolder.Toppings.Contains(toppingsEntity.ToString()))
            {
                var presentResponse = new Response
                {
                    Text = $"{toppingsEntity} is already present in your order. Would you like to add some other toppings?",
                    Hint = "Yes|No"
                };

                result.SendResponse(presentResponse);
                context.Add(ContextName.ConfirmMoreToppings);
                return;
            }

            pizzaHolder.Toppings.Add(toppingsEntity.ToString());
            var response = new Response
            {
                Text = $"You selected {pizzaHolder.ToppingsToString()}. Would you like to add more toppings?",
                Hint = "Yes|No"
            };

            result.SendResponse(response);
            context.Add(ContextName.ConfirmMoreToppings);
        }

        [Fallback(Context = ContextName.WaitForToppings)]
        public void InvalidToppings(Context context, Result result)
        {
            result.Bot.Actions[ActionName.ToppingsAgain].Invoke(context, result);
        }
    }
}