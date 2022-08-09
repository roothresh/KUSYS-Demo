using KUSYS_Demo.Data;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Dynamic;

namespace KUSYS_Demo.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EnrollmentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //bütün dbler çağrılıyor.
            var enrollments = _db.Enrollments.ToList();
            var courses = _db.Courses.ToList();
            var students = _db.Students.ToList();

            //course lar parent olacak şekilde öğrenciler child olacak şekilde bir dictionary oluşturuluyor.
            Dictionary<Course,List < Student >> enrollmentDictionary = new Dictionary<Course, List<Student>>();

            //her bir course için döngüye giriyoruz.
            //enrollments içerisinde tek tek bu course a bağlı olan öğrencileri çekiyoruz.
            //çektiğimiz öğrencileri de linq ile ayrıştırırarak listeye atıyoruz.
            //daha sonra dictimizi key course class ve value de öğrenci listimiz olarak dolduruyoruz.
            foreach(Course course in courses)
            {
                Course courseDict = course;
                var dictStudents = new List<Student>();
                var findStudents = enrollments.FindAll(x => x.CourseId == courseDict.Id).ToList();

                if(findStudents.Count > 0)
                {
                    //select * from students where Id in (select studentId from enrollments) 
                    dictStudents = (from p in students
                                       where findStudents.Any(x => x.StudentId == p.Id)
                                       select p).ToList();
                }
                

                enrollmentDictionary.Add(courseDict, dictStudents.ToList());
            }
            return View(enrollmentDictionary);
        }

        //GET
        public IActionResult AddEnrollment()
        {
            //ExpandoObject .net 4.0 ile gelmiş. Runtime esnasında dinamik olarak olarak prop ekleyip çıkartabiliyormuşuz.
            //Farklı yöntemlerde var... Test edilesi bir yanı var...

            dynamic resultModel = new ExpandoObject();
            resultModel.Students = _db.Students.ToList();
            resultModel.Courses = _db.Courses.ToList();

            return View(resultModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEnrollment(IFormCollection formCollection)
        {
            /*
             * Form collection olayı da formdan postlanan dataları name ve value larını 
             * key value pair yaparak alıyor. formcollection ile keyleri çektiğimizde valuelara erişim sağlıyoruz.
             * Kullanma sebebi expandoobject kullanmam. Farklı bir yol vardır belki...
             */
            if (ModelState.IsValid)
            {
                Enrollment enrollment = new Enrollment();

                enrollment.CourseId = Convert.ToInt32(formCollection["Course"]);
                enrollment.StudentId = Convert.ToInt32(formCollection["Student"]);

                var isStudentAlreadyExist = _db.Enrollments.Any(x => x.StudentId == enrollment.StudentId && x.CourseId == enrollment.CourseId);
                if (!isStudentAlreadyExist)
                {
                    _db.Enrollments.Add(enrollment);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["failed"] = "This student already enrolled this course !";
                    return RedirectToAction("Index", TempData["failed"]);
                }

            }
            return View();

        }
    }
}
