using System.Globalization;
using NLI_Database_Oscova.Contexts;
using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;
using Syn.Bot.Oscova.Entities;

namespace NLI_Database_Oscova.Dialogs
{
    internal class DatabaseUpdateByNameDialog : Dialog
    {
        [Expression("@set @property of @name to @sys.number")]
        public void ChangePropertyOfName(Context context, Result result)
        {
            context.Add(DatabaseContext.ByNameConfirmation);

            var property = result.Entities.OfType("property");
            var name = result.Entities.OfType("name");
            var number = result.Entities.OfType<NumberEntity>();

            context.SharedEntities.Add(property);
            context.SharedEntities.Add(name);
            context.SharedEntities.Add(number);

            var propertyString = property.Value.ToLower();
            if (propertyString != "age" && propertyString != "salary")
            {
                result.SendResponse($"{property} of {name} is readonly.");
                return;
            }

            result.SendResponse($"Are you sure that you want to change {property} of {name} to {number}?");
        }

        [Expression("{Yes}")]
        [Entity(Sys.Positive)]
        [Context(DatabaseContext.ByNameConfirmation)]
        public void ChangePropertyConfirmed(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DatabaseUtility>();

            var property = context.SharedEntities.OfType("property");
            var name = context.SharedEntities.OfType("name").Value;
            var number = context.SharedEntities.OfType<NumberEntity>().Value;

            result.SendResponse($"{property} of {name} changed to {number}");
            utility.UpdatePropertyByName(name, property.Value, number.ToString(CultureInfo.InvariantCulture));
        }

        [Expression("{no}")]
        [Entity(Sys.Negative)]
        [Context(DatabaseContext.ByNameConfirmation)]
        public void ChangePropertyByNameDeclined(Context context, Result result)
        {
            result.SendResponse("Operating canceled.");
        }        
    }
}