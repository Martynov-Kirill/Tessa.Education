namespace Tessa.Education.Entites.Entities
{
    /// <summary>
    /// Оценка
    /// </summary>
    public class Grade : Entity
    {
        /// <summary>
        /// Значение оценки
        /// </summary>
        public double? Value { get; set; } = -1.0;


        // Foreign Key для студента
        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }

        // Foreign Key для предмета
        public int? SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
