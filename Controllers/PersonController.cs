using DockerKubernetesDemo.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DockerKubernetesDemo.Controllers
{
    public class PersonController : Controller
    {
        // person controller for CRUD operations
        private readonly DatabaseContext _ctx;
        public PersonController(DatabaseContext ctx)
        {

            _ctx = ctx;

        }
        public IActionResult Index()
        {
            ViewBag.greeting = "Hello Suresh";
            ViewData["greetingGM"] = "Good Morning";
            TempData["greetingTemp"] = "This is Temp data";
            return View();
        }

        public IActionResult AddPerson()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _ctx.Add(person);
                _ctx.SaveChanges();
                TempData["Status"] = "Person Added Sucessfully!!!";
                return RedirectToAction("AddPerson");
            }
            catch (Exception ex)
            {
                TempData["Status"] = "Person could not be Added";
                return View();
            }

        }

        public IActionResult DisplayPersons()
        {

            var persons = _ctx.Person.ToList();

            return View(persons);

        }

        public IActionResult EditPerson(int id)
        {
            var person = _ctx.Person.Find(id);

            return View(person);
        }
        [HttpPost]
        public ActionResult EditPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _ctx.Update(person);
                _ctx.SaveChanges();
                TempData["Status"] = "Person updated Sucessfully!!!";
                return RedirectToAction("DisplayPersons");
            }
            catch (Exception ex)
            {
                TempData["Status"] = "Person could not be Updated";
                return View();
            }
        }

        public IActionResult DeletePerson(int id)
        {
            try
            {
                var person = _ctx.Person.Find(id);

                if (person != null)
                {
                    _ctx.Person.Remove(person);
                    _ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("DisplayPersons");
        }
    }
}
