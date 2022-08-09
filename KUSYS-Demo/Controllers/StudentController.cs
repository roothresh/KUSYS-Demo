using KUSYS_Demo.Data;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Student> students = _db.Students.ToList();
            return View(students);
        }

        //GET
        public IActionResult CreateStudent()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);

        }

        //GET
        public IActionResult EditStudent(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var studentFromDb = _db.Students.Find(id);

            if (studentFromDb == null)
                return NotFound();

            return View(studentFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        //GET
        public IActionResult DeleteStudent(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var studentFromDb = _db.Students.Find(id);

            if (studentFromDb == null)
                return NotFound();

            return View(studentFromDb);
        }

        [HttpPost, ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStudentPost(int? id)
        {
            var data = _db.Students.Find(id);
            if (data == null)
            {
                return NotFound();
            }

            _db.Students.Remove(data);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult StudentDetail(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var student = _db.Students.Find(id);
            return PartialView("_ShowStudentDetail", student);
        }

    }
}
