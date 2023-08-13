namespace Tessa.Education.Entites.Models.Errors
{
    public class IncorrectCredentialException : GeneralException
    {
        public IncorrectCredentialException() : base("Credentials is incorrect")
        {

        }
        public IncorrectCredentialException(string message) : base(message)
        {

        }
    }
}
