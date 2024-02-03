using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using workingwithmultipletable.Data;
using workingwithmultipletable.Models;

namespace workingwithmultipletable.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationContext _context;

        public DepartmentController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            var result = await _context.departments.ToListAsync();
            return View(result);
        }


        public IActionResult Add(Department model)
        {
            if(ModelState.IsValid)
            {
                _context.departments.Add(model);
                _context.SaveChanges();
                return RedirectToAction ("Index");
            }
            return View();
     
        
        }
        [Route("departments")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Department department = new Department();
            department = _context.departments.SingleOrDefault(x => x.DepartmentId == id);
            return View(department);
        }

        [Route("departments")]
        public IActionResult Edit(Department model)
        {
            if(ModelState.IsValid)
            {
                _context.departments.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var result= _context.departments.FirstOrDefault(y => y.DepartmentId == id);
            _context.departments.Remove(result);
            _context.SaveChanges();
            return RedirectToAction("Index");   
        }
       
    }
}
