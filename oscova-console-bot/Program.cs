using System;
using Syn.Bot.Oscova;

namespace OscovaConsoleBot
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var bot = new OscovaBot();
            bot.Dialogs.Add(new HelloBotDialog());
            bot.Dialogs.Add(new AppDialog());
            bot.Trainer.StartTraining();

            bot.MainUser.ResponseReceived += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.Response.Text);
            };

            while (true)
            {
                var request = Console.ReadLine();
                var evaluationResult = bot.Evaluate(request);
                evaluationResult.Invoke();
            }
        }
    }
}