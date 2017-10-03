using Syn.Bot.Oscova;

namespace OscovaConsoleBot
{
    internal class MyDialog : Dialog
    {
        public MyDialog()
        {
            QuickReplies.Add("How are you?", "I am fine.");
            QuickReplies.Add("How is life?","Life is going great!");
        }
    }
}