using System.ComponentModel.DataAnnotations.Schema;

namespace Tessa.Education.Entites.Entities
{
    public class Error : Entity
    {
        /// <summary>
        /// Наименование ошибки
        /// </summary>
        public string Name { get; set; } = String.Empty;
        /// <summary>
        /// Короткое Описание ошибки
        /// </summary>
        public string Description { get; set; } = String.Empty;
        /// <summary>
        /// Мнемоник код для вызова или поиска конкретной ошибки
        /// </summary>
        public string MnemonicCode { get; set; } = String.Empty;

        /// <summary>
        /// Детализация ошибки
        /// </summary>
        public ErrorDetails ErrorDetails { get; set; } = new();

        /// <summary>
        /// Дополнительные параметры
        /// </summary>
        [NotMapped]
        public string[] Params { get; set; }
    }

}
