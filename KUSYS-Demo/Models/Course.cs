using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Course ID")]
        public string CourseId { get; set; }
        [Required]
        [DisplayName("Course Name")]
        public string CourseName { get; set; }
    }
}
