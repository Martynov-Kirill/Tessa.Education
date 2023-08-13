namespace Tessa.Education.Entites.Entities
{
    public class Group : Entity
    {
        /// <summary>
        /// Номер группы
        /// </summary>
        public string? Number { get; set; } = String.Empty;
        /// <summary>
        /// Код группы
        /// </summary>
        public string? Code { get; set; } = String.Empty;


        // Зависимость Многие к Одному : Many to One
        // Много студентов могут быть одной группе
        public virtual ICollection<Student>? Students { get; set; }
    }
}
