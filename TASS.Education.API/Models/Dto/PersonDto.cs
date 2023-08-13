namespace Tessa.Education.API.Models.Dto
{
    public class PersonDto
    {
        /// <summary>
        /// Полное имя человека
        /// </summary>
        public string? Name { get; set; } = String.Empty;

        /// <summary>
        /// Контактные данные
        /// </summary>
        public string? Contact { get; set; } = String.Empty;

        /// <summary>
        /// Почта
        /// </summary>
        public string? Email { get; set; } = String.Empty;

        /// <summary>
        /// Возраст
        /// </summary>
        public int? Age { get; set; } = 0;

        /// <summary>
        /// Адрес проживания
        /// </summary>
        public string? Address { get; set; } = String.Empty;

        /// <summary>
        /// Номер и Серия Пасспорта
        /// </summary>
        public string? PassportID { get; set; } = String.Empty;
    }
}
