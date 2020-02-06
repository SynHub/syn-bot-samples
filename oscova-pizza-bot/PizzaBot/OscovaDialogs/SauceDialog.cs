using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace PizzaBot.OscovaDialogs
{
    internal class SauceDialog : Dialog
    {
        //User wants Sauce
        [Expression("@sys.positive")]
        [Context(ContextName.ConfirmSauce)]
        public void YesSauce(Context context, Result result)
        {
            var response = new Response
            {
                Text = "Here are the sauce options for you. Please select the one you prefer.",
                Hint = EntitiesCreator.GetSauceHint
            };

            result.SendResponse(response);
            context.Add(ContextName.WaitForSauce);
        }

        //User doesn't want any sauce
        [Expression("@sys.negative")]
        [Context(ContextName.ConfirmSauce)]
        public void NoSauce(Context context, Result result)
        {
            var response = new Response();
            response.Text = "As you wish no sauce on your pizza. Would you like to add some cheese on your pizza?";
            result.SendResponse(response);

            //Move on to Cheese confirmation...
            context.Add(ContextName.ConfirmCheese);
        }

        //User selected Sauce
        [Expression("@pizza-sauce")]
        [Context(ContextName.WaitForSauce)]
        public void SelectedSauce(Context context, Result result)
        {
            var sauceEntity = result.Entities.OfType("pizza-sauce");
            var pizzaHolder = context.SharedData.OfType<PizzaHolder>();
            pizzaHolder.Sauce = sauceEntity.ToString();

            var response = new Response();
            response.Text = $"Cool so you've selected \"{sauceEntity}\". Would you like to add some cheese on your pizza?";
            result.SendResponse(response);

            //Sauce selected. Move on to Cheese confirmation..
            context.Add(ContextName.ConfirmCheese);
        }


        //User mentiooned invalid sauce type.
        [Fallback(Context = ContextName.WaitForSauce)]
        public void SizeFallback(Context context, Result result)
        {
            var response = new Response();
            response.Text = "That doesn't seem like a valid sauce type that I am aware of. Please do specify a valid sauce option to continue.";
            response.Hint = EntitiesCreator.GetSauceHint;
            result.SendResponse(response);

            context.Add(ContextName.WaitForSauce);
        }

        //User didn't properly confirm if he wants sauce or not.
        [Fallback(Context = ContextName.ConfirmSauce)]
        public void SauceConfirmationFallback(Context context, Result result)
        {
            var response = new Response();
            response.Text = "I was expecting a yes or no answer. So could you confirm if you would like sauce or not?";
            response.Hint = "Yes|No"; //This time show the options.
            result.SendResponse(response);

            context.Add(ContextName.ConfirmSauce);
        }

       
    }
}