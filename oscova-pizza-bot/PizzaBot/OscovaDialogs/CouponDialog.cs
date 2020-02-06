using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace PizzaBot.OscovaDialogs
{
    internal class CouponDialog: Dialog
    {
        [Action(ID = ActionName.CouponStart)]
        public void CouponStartAction(Context context, Result result)
        {
            result.SendResponse("Moving on, do you have a coupon with you? Usually the coupon is of the format ABC123.");
            context.Add(ContextName.ConfirmCoupon);
        }

        [Action(ID = ActionName.CouponAgain)]
        public void AskCouponAction(Context context, Result result)
        {
            result.SendResponse("Enter your coupon code that was provided to you.");
            context.Add(ContextName.WaitForCoupon);
        }

        [Expression("@sys.positive")]
        [Context(ContextName.ConfirmCoupon)]
        public void YesCoupon(Context context, Result result)
        {
            result.Bot.Actions[ActionName.CouponAgain].Invoke(context, result);
        }

        [Expression("@sys.negative")]
        [Context(ContextName.ConfirmCoupon)]
        public void NoCoupon(Context context, Result result)
        {
            result.Bot.Actions[ActionName.AddressRequest].Invoke(context, result);
        }

        [Fallback(Context = ContextName.ConfirmCoupon)]
        public void ConfirmCouponFallback(Context context, Result result)
        {
            var response = new Response();
            response.Text = "I was expecting a yes or no answer. So can you confirm if you've got a coupon?";
            response.Hint = "Yes|No"; //This time show the options.
            result.SendResponse(response);

            context.Add(ContextName.ConfirmCoupon);
        }

        [Expression("@pizza-coupon")]
        [Expression("@sys.positive it is @pizza-coupon")]
        [Context(ContextName.ConfirmCoupon)]
        public void YesWithCoupon(Context context, Result result)
        {
            var couponEntity = result.Entities.OfType("pizza-coupon");
            var pizzaHolder = context.SharedData.OfType<PizzaHolder>();
            pizzaHolder.Coupon = couponEntity.ToString();

            result.Bot.Actions[ActionName.AddressRequest].Invoke(context, result);
        }

        [Expression("@pizza-coupon")]
        [Context(ContextName.WaitForCoupon)]
        public void ValidCoupon(Context context, Result result)
        {
            var couponEntity = result.Entities.OfType("pizza-coupon");
            var pizzaHolder = context.SharedData.OfType<PizzaHolder>();
            pizzaHolder.Coupon = couponEntity.ToString();

            result.Bot.Actions[ActionName.AddressRequest].Invoke(context, result);
        }

        [Fallback(Context = ContextName.WaitForCoupon)]
        public void InvalidCoupon(Context context, Result result)
        {
            var response = new Response
            {
                Text = "That doesn't seem like a valid coupon. Would you like to try again?",
                Hint = "Yes|No"
            };

            result.SendResponse(response);
            context.Add(ContextName.ConfirmCoupon);
        }
    }
}