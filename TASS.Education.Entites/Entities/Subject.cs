namespace Tessa.Education.Entites.Entities
{
    /// <summary>
    /// Академический Предмет
    /// </summary>
    public class Subject : Entity
    {
        /// <summary>
        /// Название предмета
        /// </summary>
        public string? Title { get; set; } = String.Empty;

        /// <summary>
        /// Направление предметной области
        /// </summary>
        public string? Area { get; set; } = String.Empty;

        /// <summary>
        /// Преподователи, персонал
        /// </summary>
        public virtual ICollection<Person>? Teachers { get; set; }

        // Связь один ко многим с оценками
        public virtual ICollection<Grade>? Grades { get; set; }
    }
}
