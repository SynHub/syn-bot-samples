using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace PizzaBot.OscovaDialogs
{
    internal class CrustDialog : Dialog
    {
        //User selected a crust option.
        [Expression("@pizza-crust")]
        [Context(ContextName.WaitForCrust)]
        public void CrustSelected(Context context, Result result)
        {
            var pizzaHolder = context.SharedData.OfType<PizzaHolder>();
           
            var crust = result.Entities.OfType("pizza-crust");
            pizzaHolder.Crust = crust.ToString();

            var response = new Response();
            response.Text = $"{crust} is a good crust preference. Moving on, would you like to add some sauce on your pizza?";

            result.SendResponse(response);
            context.Add(ContextName.ConfirmSauce);
        }

        //User did not select or mention any known crust option.
        [Fallback(Context = ContextName.WaitForCrust)]
        public void CrustFallback(Context context, Result result)
        {
            var response = new Response();
            response.Text = "Ouch! that doesn't seem like a valid crust option. Could you please select one from the options I have provided.";
            response.Hint = EntitiesCreator.GetCrustHint;
            result.SendResponse(response);

            context.Add(ContextName.WaitForCrust);
        }
    }
}