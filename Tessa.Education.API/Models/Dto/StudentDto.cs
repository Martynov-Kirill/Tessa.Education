namespace Tessa.Education.API.Models.Dto
{
    public class StudentDto : PersonDto
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

    }
}
