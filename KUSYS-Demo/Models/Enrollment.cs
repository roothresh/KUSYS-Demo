using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CourseId{ get; set; }
        [Required]
        public long StudentId { get; set; }
    }
}
