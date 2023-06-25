namespace BLL.Exceptions
{
    public class ModelValidationFailedException : Exception
    {
        public ModelValidationFailedException()
        {
        }

        public ModelValidationFailedException(string message)
            : base(message)
        {
        }
    }
}
