using System.Diagnostics;
using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace OscovaConsoleBot
{
    internal class AppDialog : Dialog
    {
        [Expression("open {calc}")]
        [Entity("app")]
        [Prompt("app", "Please specify an App name.")]
        public void OpenApp(Context context, Result result)
        {
            var entity = result.Entities.OfType("app");
            Process.Start(entity.Value);
            result.SendResponse("App opened");
        }
    }
}