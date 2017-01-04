using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace NLI_Database_Oscova.Dialogs
{
    class DatabaseGenericDialog: Dialog
    {
        [Expression]
        public void Default(Context context, Result result)
        {
            result.SendResponse("Please rephrase your query.");
        }

        [Expression("{clear}")]
        [Expression("{reset}")]
        [Entity("clear-command")]
        public void ResetTable(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DatabaseUtility>();
            result.SendResponse("Entire employees table.");
            utility.Evaluate("SELECT * FROM Employees");
        }
    }
}
