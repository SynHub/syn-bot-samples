using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace PizzaBot.OscovaDialogs
{
    internal class AddressDialog : Dialog
    {
        [Action(ID = ActionName.AddressRequest)]
        public void AddressRequestAction(Context context, Result result)
        {
            var pizzaHolder = context.SharedData.OfType<PizzaHolder>();

            var couponAppliedMsg = string.Empty;
            if (!string.IsNullOrEmpty(pizzaHolder.Coupon))
            {
                couponAppliedMsg = $"Yay! {pizzaHolder.Coupon} coupon applied. ";
            }

            result.SendResponse($"{couponAppliedMsg}Your Pizza is now ready! \n{pizzaHolder}\n Please type in the pizza delivery address.");

            context.Add(ContextName.WaitForAddress);
        }

        [Expression("@sys.text")]
        [Context(ContextName.WaitForAddress)]
        public void StoreAddress(Context context, Result result)
        {
            var addressText = result.Request.Text;
            var response = new Response
            {
                Text = $"Thank you! Your pizza will be delivered to \"{addressText}\". This concludes the Pizza Mock up :) Press /start to restart.",
                Hint = "/start"
            };
            result.SendResponse(response);
        }
    }
}