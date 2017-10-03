using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;
namespace OscovaAndroidBot.Dialogs
{
    class HelloBotDialog : Dialog
    {
        [Expression("Hello Bot")]
        public void HelloBot(Context context, Result result)
        {
            result.SendResponse("Hello Bot developer!"); 
        }
    }
}