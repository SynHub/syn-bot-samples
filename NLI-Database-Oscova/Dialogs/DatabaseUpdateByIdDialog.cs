using NLI_Database_Oscova.Contexts;
using Syn.Bot.Oscova;
using Syn.Bot.Oscova.Attributes;
using Syn.Bot.Oscova.Entities;

namespace NLI_Database_Oscova.Dialogs
{
    class DatabaseUpdateByIdDialog : Dialog
    {
        [Expression("@set @property of id @sys.number to @sys.number")]
        public void ChangePropertyOfId(Context context, Result result)
        {
            context.Add(DatabaseContext.ByIdConfirmation);

            var property = result.Entities.OfType("property");
            var numbers = result.Entities.AllOfType<NumberEntity>();

            context.SharedEntities.Add(property);
            context.SharedEntities.AddRange(numbers);

            var id = numbers[0].ToString();
            var number = numbers[1].ToString();

            var propertyString = property.Value.ToLower();
            if (propertyString != "age" && propertyString != "salary")
            {
                result.SendResponse($"{property} of ID: {id} is readonly.");
                return;
            }

            result.SendResponse($"Are you sure that you want to change {property} of ID: {id} to {number}?");
        }

        [Expression("{Yes}")]
        [Entity(Sys.Positive)]
        [Context(DatabaseContext.ByIdConfirmation)]
        public void ChangePropertyById(Context context, Result result)
        {
            var utility = context.SharedData.OfType<DatabaseUtility>();

            var property = context.SharedEntities.OfType("property");
            var numbers = context.SharedEntities.AllOfType<NumberEntity>();

            var id = numbers[0].ToString();
            var number = numbers[1].ToString();

            result.SendResponse($"{property} of ID: {id} changed to {number}");
            utility.UpdatePropertyById(id, property.Value, number);
        }

        [Expression("{no}")]
        [Entity(Sys.Negative)]
        [Context(DatabaseContext.ByIdConfirmation)]
        public void ChangePropertyByIdDeclined(Context context, Result result)
        {
            result.SendResponse("Operating canceled.");
        }
    }
}
