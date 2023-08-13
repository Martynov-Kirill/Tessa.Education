
namespace Tessa.Education.Entites.Models.Errors
{
    public class AlreadyExistsException : GeneralException
    {
        public AlreadyExistsException() : base("Entity already exists")
        {

        }
        public AlreadyExistsException(string message) : base(message)
        {

        }
    }
}
