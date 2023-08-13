namespace Tessa.Education.Entites.Models.Errors
{
    public class NullArgumentException : GeneralException
    {
        public NullArgumentException() : base("Some of arguments has empty or null value")
        {

        }
        public NullArgumentException(string message) : base(message)
        {

        }
    }
}
