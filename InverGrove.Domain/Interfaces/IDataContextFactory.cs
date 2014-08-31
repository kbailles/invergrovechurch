namespace InverGrove.Domain.Interfaces
{
    public interface IDataContextFactory
    {
        /// <summary>
        /// Gets the object context.
        /// </summary>
        /// <returns></returns>
        object GetObjectContext();
    }
}