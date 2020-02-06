using System;
using System.IO;
using Syn.Bot.Channels.Telegram;
using Syn.Bot.Channels.Web;
using Syn.Bot.Oscova;
using Syn.Log;
using File = System.IO.File;

namespace PizzaBot
{
    internal class Program
    {
        private static WebApiChannel<OscovaBot> WebApiChannel { get; set; }
        private static OscovaBot Bot => OscovaBot.Instance;
        private static Logger Logger => OscovaBot.Logger;

        private static readonly string LogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "pizzabot-log.txt");

        private static void Main()
        {
            try
            {
                OscovaBot.Logger.LogLevelThreshold = LogLevel.Info;

                var telegramChannel = new TelegramChannel<OscovaBot>("TELEGRAM_BOT_TOKEN", OscovaBot.Instance);
                WebApiChannel = new WebApiChannel<OscovaBot>(OscovaBot.Instance, "HOST_ADDRESS");

                Action<LogReceivedEventArgs> logAction = receivedEventArgs =>
                {
                    File.AppendAllText(LogFilePath, receivedEventArgs.Log.ToString() + Environment.NewLine);
                    Console.WriteLine(receivedEventArgs.Log);
                };

                Logger.LogReceived += (sender, receivedEventArgs) =>
                {
                    logAction.Invoke(receivedEventArgs);
                };

                telegramChannel.Logger.LogReceived += (sender, args) =>
                {
                    logAction.Invoke(args);
                };

                WebApiChannel.Logger.LogReceived += (sender, args) =>
                {
                    logAction.Invoke(args);
                };
              

                LogEvent("Start Saga Bot");
                Logger.Info("Initializing Saga Bot...");

                Bot.Configuration.RequiredRecognizersOnly = true;
                Bot.Configuration.RemoveContextOnFallback = false;
                Bot.Configuration.ContextLifespan = 1;
                Bot.Configuration.Scoring.MinimumScore = 0.4;

                Bot.Users.UserAdded += (sender, userAddedEventArgs) =>
                {
                    LogEvent($"USER {userAddedEventArgs.User.ID}");
                };

                EntitiesCreator.Initialize();
                DialogCreator.Initialize();

                //Load Word Vectors
                //Logger.Info("Loading word vectors...");
                //Saga.Language.WordVectors.Load(@"D:\Word Vectors\FastText\wiki-en.vec", VectorDataFormat.Text);
                //Saga.Language.WordVectors.Load(@"D:\pizzabot-wordvectors.vec", VectorDataFormat.Text);

                Bot.Language.WordVectors.Logger.LogReceived += (sender, logReceivedEventArgs) =>
                {
                    Logger.Log(logReceivedEventArgs.Log);
                };

                Bot.Trainer.StartTraining();

                //Save optimized Word Vectors
                //var optimizedWordVector = Bot.Language.WordVectors.Optimize(Saga);
                //optimizedWordVector.Save(@"/*D:\pizzabot-wordvectors.vec*/", VectorDataFormat.Text);
                //Logger.Info("Saved optimized Word Vectors.");

                telegramChannel.Start();
                WebApiChannel.Start();

                Console.WriteLine("Press any key to stop Saga bot server.");
                Console.ReadLine();

                telegramChannel.Stop();
                Logger.Info("Telegram channel stopped.");

                WebApiChannel.Stop();
                Logger.Info("Web API channel stopped.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }

        private static void LogEvent(string title)
        {
            File.AppendAllText(LogFilePath, Environment.NewLine + $"====={title.ToUpper()}=====" + Environment.NewLine);
        }
    }
}