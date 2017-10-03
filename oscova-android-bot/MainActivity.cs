using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Syn.Bot.Oscova;
using OscovaAndroidBot.Dialogs;
namespace OscovaAndroidBot
{
    [Activity(Label = "OscovaAndroidBot", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var bot = new OscovaBot();
            bot.Dialogs.Add(new HelloBotDialog());
            bot.Trainer.StartTraining();

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            var input = FindViewById<EditText>(Resource.Id.editText1);
            var output = FindViewById<EditText>(Resource.Id.editText2);

            bot.MainUser.ResponseReceived += (sender, args) =>
            {
                output.Text = $"Bot: {args.Response.Text}";
            };

            button.Click += delegate {
                var result = bot.Evaluate(input.Text);
                result.Invoke();
            };
        }
    }
}
