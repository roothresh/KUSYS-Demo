using KUSYS_Demo.Data;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Applicationdbcontext üzerinden db ye erişim sağlıyoruz. Tablolarımızda orada tanımlı.
        /// program.cs içerisine service tanımlamadığımız için biz servisi kullanmak istediğimiz zaman
        /// sanki bu obje oradaymış gibi her istediğimizde erişebiliyoruz.
        /// Constructor içerisinde service ten dependency injection yöntemi ile erişebiliyoruz.
        /// </summary>
        /// <param name="db"></param>
        public CourseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //select * from Courses. EF bizim için bunu yapıyor miss.
            //var kullanılabilir klasik fakat list geliyor zaten classlist oluşturup alabiliriz.
            //var courseList = _db.Courses.ToList();
            List<Course> courseList = _db.Courses.ToList();
            return View(courseList);
        }

        //GET
        public IActionResult CreateCourse()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCourse(Course course)
        {
            //Custom validation yapıldıktan sonra custom bir error göstermek için
            //ModelState.AddModelError("CustomError", "CustomMessage") kullanılabilir. 


            //ModelState model üzerinden validasyon kontrol işlemi yapar.
            if (ModelState.IsValid)
            {
                _db.Courses.Add(course);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
            
        }

        //GET
        public IActionResult EditCourse(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var courseFromDb = _db.Courses.Find(id);

            if (courseFromDb == null)
                return NotFound();

            return View(courseFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _db.Courses.Update(course);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        //GET
        public IActionResult DeleteCourse(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var courseFromDb = _db.Courses.Find(id);

            if (courseFromDb == null)
                return NotFound();

            return View(courseFromDb);
        }

        //POST
        //metod adı farklı olduğu için post olduğunda buraya uğramayacak. ActionName attribute u ile diğer metodu bunun ile bağlayabiliyoruz.
        [HttpPost,ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCoursePost(int? id)
        {
            var data = _db.Courses.Find(id);
            if(data == null)
            {
                return NotFound();
            }
            
            _db.Courses.Remove(data);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
