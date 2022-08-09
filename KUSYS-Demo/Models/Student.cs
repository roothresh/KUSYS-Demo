using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class Student
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        [DisplayName("Student Number")]
        public long StudentNumber{ get; set; }
        [Required]
        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        [Required]
        [DisplayName("Student Surname")]
        public string StudentSurname { get; set; }
        [DisplayName("Birth Date")]
        public DateTime BirthDate{ get; set; }

    }
}
