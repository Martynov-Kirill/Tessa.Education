using System.ComponentModel.DataAnnotations.Schema;

namespace Tessa.Education.Entites.Entities
{
    [Table("Student")]
    public class Student : Person
    {
        /// <summary>
        /// Направление предметной области студента
        /// </summary>
        public string? SubjectArea { get; set; } = String.Empty;

        /// <summary>
        /// Факультет студента
        /// </summary>
        public string? Faculty { get; set; } = String.Empty;

        /// <summary>
        /// Академическая успеваемость в %
        /// </summary>
        public double? AcademicPerformance { get; set; } = -1.0;

        /// <summary>
        /// Посещение занятий в %
        /// </summary>
        public double? ClassAttendance { get; set; } = -1.0;


        // Foreign Key для группы
        public int? GroupId { get; set; }
        public virtual Group? Group { get; set; } = new();

        // Связь один ко многим с оценками
        public virtual ICollection<Grade>? Grades { get; set; }

        public override string ToString()
        {
            return base.ToString() + string.Join(" ", SubjectArea, Faculty, AcademicPerformance, ClassAttendance);
        }
    }
}
