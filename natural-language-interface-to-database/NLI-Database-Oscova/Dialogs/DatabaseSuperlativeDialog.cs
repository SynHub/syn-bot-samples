using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace NLI_Database_Oscova.Dialogs
{
    internal class DatabaseSuperlativeDialog: Dialog
    {
        [Expression("Who is the {youngest} employee?")]
        [Expression("{Youngest} employee")]
        [Entity("pro-young")]
        public void YoungestEmployee(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DatabaseUtility>();
            utility.Evaluate("SELECT *, MIN(Age) FROM Employees;");
            result.SendResponse("The youngest employee.");
        }

        [Expression("Who is the {oldest} employee?")]
        [Expression("{Oldest} employee")]
        [Entity("prop-old")]
        public void OldestEmployee(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DatabaseUtility>();
            utility.Evaluate("SELECT *, MAX(Age) FROM Employees;");
            result.SendResponse("The oldest employee.");
        }

        [Expression("Who is the {highest} paid employee?")]
        [Expression("{Highest} paid employee")]
        [Expression("Employee with the {highest} salary")]
        [Expression("Who gets {paid the most}?")]
        [Expression("Who gets {paid the higest}?")]
        [Expression("Who is the {richest} employee?")]
        [Entity("prop-high-pay")]
        public void HighestPaidEmployee(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DatabaseUtility>();
            utility.Evaluate("SELECT * , MAX(Salary) From Employees;");
            result.SendResponse("Highest paid employee.");
        }

        [Expression("{Least} paid employee")]
        [Expression("Who has the {least salary}?")]
        [Expression("Who gets paid the {least}")]
        [Expression("Who is paid the {least}")]
        [Expression("Employee with the {lowest} salary")]
        [Entity("prop-low-pay")]
        public void LeastPaidEmployee(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DatabaseUtility>();
            utility.Evaluate("SELECT * , MIN(Salary) From Employees;");
            result.SendResponse("Least paid employee.");
        }
    }
}