using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace PizzaBot.OscovaDialogs
{
    internal class StartDialog : Dialog
    {
        [Expression("@start")]
        public void Greetings(Context context, Result result)
        {
            result.User.Context.Clear();

            //Make sure a PizzaHolder instance is added for this user.
            context.SharedData.Add(new PizzaHolder());

            var userName = result.User.Settings["name"].Value;

            var response = new Response();
            response.Text = $"Well hello there {userName}! Let's build your own (BYO) pizza. To begin with I will need to know what size of Pizza would you prefer?";
            response.Hint = EntitiesCreator.GetSizeHint;
            result.SendResponse(response);

            context.Add(ContextName.WaitForSize);
        }
    }
}