using System.Linq;
using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;

namespace OscovaExcelBot.BotData.Dialogs
{
    internal class DatabaseSuperlativeDialog: Dialog
    {
        [Expression("Who is the {youngest} employee?")]
        [Expression("{Youngest} employee")]
        [Entity("pro-young")]
        public void YoungestEmployee(Context context, Result result)
        {
            var utility = context.SharedData.OfType<BotViewModel>();

            var minAge = utility.FullEmployeeList.Min(item => item.Age);
            var empByAge = utility.EmployeesBy(ColumnName.Age, minAge.ToString());
            utility.Display(empByAge);

            if (empByAge.Any())
            {
                result.SendResponse($"One of the youngest employee is {empByAge[0].Name}.");
            }
        }

        [Expression("Who is the {oldest} employee?")]
        [Expression("{Oldest} employee")]
        [Entity("prop-old")]
        public void OldestEmployee(Context context, Result result)
        {
            var utility = context.SharedData.OfType<BotViewModel>();

            var maxAge = utility.FullEmployeeList.Max(item => item.Age);
            var empByAge = utility.EmployeesBy(ColumnName.Age, maxAge.ToString());
            utility.Display(empByAge);

            if (empByAge.Any())
            {
                result.SendResponse($"One of the oldest employee is {empByAge[0].Name}.");
            }
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
            var utility = context.SharedData.OfType<BotViewModel>();

            var maxSalary = utility.FullEmployeeList.Max(item => item.Salary);
            var empByAge = utility.EmployeesBy(ColumnName.Salary, maxSalary.ToString());
            utility.Display(empByAge);

            if (empByAge.Any())
            {
                result.SendResponse($"One of the highest paid employee is {empByAge[0].Name}.");
            }
        }

        [Expression("{Least} paid employee")]
        [Expression("Who has the {least salary}?")]
        [Expression("Who gets paid the {least}")]
        [Expression("Who is paid the {least}")]
        [Expression("Employee with the {lowest} salary")]
        [Entity("prop-low-pay")]
        public void LeastPaidEmployee(Context context, Result result)
        {
            var utility = context.SharedData.OfType<BotViewModel>();

            var minSalary = utility.FullEmployeeList.Min(item => item.Salary);
            var empByAge = utility.EmployeesBy(ColumnName.Salary, minSalary.ToString());
            utility.Display(empByAge);

            if (empByAge.Any())
            {
                result.SendResponse($"One of the lowest paid employee is {empByAge[0].Name}.");
            }
        }
    }
}