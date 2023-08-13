
namespace Tessa.Education.Entites.Models.Errors
{
    public class DoesNotExistException : GeneralException
    {
        public DoesNotExistException() : base("Entity does not exists")
        {

        }
        public DoesNotExistException(string message) : base(message)
        {

        }
    }
}
