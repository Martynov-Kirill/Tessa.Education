namespace Tessa.Education.Entites.Models.Errors
{
    public class InvalidFormatException : GeneralException
    {
        public InvalidFormatException() : base("Value format is invalid")
        {

        }
        public InvalidFormatException(string message) : base(message)
        {

        }
    }
}
