namespace Tessa.Education.Entites.Entities
{
    public class ErrorDetails : Entity
    {
        /// <summary>
        /// Детализация Ошибки
        /// </summary>
        public string Details { get; set; } = String.Empty;
        /// <summary>
        /// Тип ошибки
        /// </summary>
        public string ErrorType { get; set; } = String.Empty;
        /// <summary>
        /// Стак трейс ошибки
        /// </summary>
        public string? StackTrace { get; set; } = String.Empty;
    }
}
