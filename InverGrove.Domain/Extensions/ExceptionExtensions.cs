using System.Data.Entity.Validation;
using System.Text;

namespace InverGrove.Domain.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Builds the validation error message.
        /// </summary>
        /// <param name="dbe">The dbe.</param>
        /// <returns></returns>
        public static string ToValidationErrorMessage(this DbEntityValidationException dbe)
        {
            var sb = new StringBuilder();

            foreach (var error in dbe.EntityValidationErrors)
            {
                var count = 0;

                foreach (var ve in error.ValidationErrors)
                {
                    sb.Append(ve.ErrorMessage);

                    if (count < error.ValidationErrors.Count)
                    {
                        sb.Append(", ");
                    }

                    count++;
                }
            }

            return sb.ToString();
        }
    }
}