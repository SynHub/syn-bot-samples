using PizzaBot.OscovaDialogs;
using Syn.Bot.Oscova;

namespace PizzaBot
{
    public static class DialogCreator
    {
        public static void Initialize()
        {
            //OscovaBot.Instance.Dialogs.Add(new TestDialog());
            OscovaBot.Instance.Dialogs.Add(new StartDialog());
            OscovaBot.Instance.Dialogs.Add(new SizeDialog());
            OscovaBot.Instance.Dialogs.Add(new CrustDialog());   
            OscovaBot.Instance.Dialogs.Add(new SauceDialog());
            OscovaBot.Instance.Dialogs.Add(new CheeseDialog());
            OscovaBot.Instance.Dialogs.Add(new ToppingsDialog());
            OscovaBot.Instance.Dialogs.Add(new CouponDialog());
            OscovaBot.Instance.Dialogs.Add(new AddressDialog());
        }
    }
}