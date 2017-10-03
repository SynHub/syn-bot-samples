using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace OscovaConsoleBot
{
    internal class HelloBotDialog : Dialog
    {
        [Expression("Hello Bot")]
        public void OpenDoor(Context context, Result result)
        {
            result.SendResponse("Hello Bot developer!");
        }
    }
}