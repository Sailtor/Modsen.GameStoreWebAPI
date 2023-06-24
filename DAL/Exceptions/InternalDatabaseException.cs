namespace DAL.Exceptions
{
    public class DatabaseSaveFailedException : Exception
    {
        public DatabaseSaveFailedException()
        {
        }
        public DatabaseSaveFailedException(string message)
            : base(message)
        {
        }
    }
}
