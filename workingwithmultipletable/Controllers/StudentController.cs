using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using workingwithmultipletable.Data;
using workingwithmultipletable.Models;
using workingwithmultipletable.ViewModel;

namespace workingwithmultipletable.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationContext _context;

        public StudentController(ApplicationContext context)
        {
            _context = context;
        }
        /* method  */
        private static string EveryFirstCharacterCapital(string input)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(input))
            {
                var data = input.Split(' ');
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].First().ToString().ToUpper() + data[i].Substring(1) + " ");
                }
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }
        public IActionResult Index()
        {
            var data = (from e in _context.employees
                        join d in _context.departments
                        on e.DepartmentId equals d.DepartmentId

                        select new ViewModelSummary
                        {
                            EmpId = e.EmpId,
                            FirstName = (e.FirstName),
                            MiddleName = EveryFirstCharacterCapital(e.MiddleName),
                            LastName = EveryFirstCharacterCapital(e.LastName),
                            Gender = e.Gender,
                            DepartmentCode = e.Department.DepartmentCode.ToUpper(),
                            DepartmentName = e.Department.DepartmentName.ToLower(),
                            DepartmentId = e.Department.DepartmentId,
                        }).ToList();
            return View(data);
            //EmpListVM emp = new EmpListVM();
            //var empData = _context.employees.FromSqlRaw("Select * from employees").ToList();
            //var depData = _context.departments.FromSqlRaw("Select * from departments").ToList();

            //emp.employees = empData;
            //emp.departments = depData;

            //return View(emp);


        }
        [Route("employees")]
        //public IActionResult Add()
        //{
        //    return View();
        //}

        public IActionResult Add()
        {
            //if (ModelState.IsValid)
            //{
            // _context.employees.Add(model);
            //_context.SaveChanges();
            //return RedirectToAction("Index");

            //}
            ViewBag.Department = new SelectList(_context.departments, "DepartmentId", "DepartmentName");

            return View();

        }

        [Route("employees")]
        [HttpPost]
        public IActionResult Add(ViewModelSummary emp)
        {
            ViewBag.Department = new SelectList(_context.departments, "DepartmentId", "DepartmentName");

            ModelState.Remove("DepartmentName");
            ModelState.Remove("DepartmentCode");

            if (ModelState.IsValid)
            {
                // Map ViewModelSummary properties to Employee properties
                Employee employee = new Employee
                {
                    FirstName = emp.FirstName,
                    MiddleName = emp.MiddleName,
                    LastName = emp.LastName,
                    Gender = emp.Gender,
                    DepartmentId = emp.DepartmentId,
                    // Map other properties as needed
                };

                _context.employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

    
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    var result = _context.employees.FirstOrDefault(x => x.EmpId == id);
                    _context.Remove(result);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                throw;
            }


        }


        [Route("employees/edit/{id}")]

        public IActionResult Edit(int id)
        {
            var employee = _context.employees.FirstOrDefault(x => x.EmpId == id);

            if (employee == null)
            {
                return NotFound(); // Handle the case where the employee with the given id is not found
            }

            var viewModel = new ViewModelSummary
            {
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                DepartmentId = employee.DepartmentId,
                // Map other properties as needed
            };

            ViewBag.Department = new SelectList(_context.departments, "DepartmentId", "DepartmentName", viewModel.DepartmentId);

            return View(viewModel);
        }


        [Route("employees/edit/{id}")]
        [HttpPost]
        public IActionResult Edit(ViewModelSummary model)
        {
            ModelState.Remove("DepartmentName");
            ModelState.Remove("DepartmentCode");
            if (ModelState.IsValid)
            {
                Employee employee = new Employee();
                _context.employees.Update(employee);
                _context.SaveChanges(); 
                return RedirectToAction("Index");

            }
            return View();
        }


        [Route("employees/Details/{id}")]
        public IActionResult Details(int id)
        {
            ViewModelSummary viewModelSummary = new ViewModelSummary();
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    viewModelSummary = (from e in _context.employees.Where(e =>e.EmpId==id)
                                join d in _context.departments
                                on e.DepartmentId equals d.DepartmentId

                                select new ViewModelSummary
                                {
                                    EmpId = e.EmpId,
                                    FirstName = (e.FirstName),
                                    MiddleName = EveryFirstCharacterCapital(e.MiddleName),
                                    LastName = EveryFirstCharacterCapital(e.LastName),
                                    Gender = e.Gender,
                                    
                                    DepartmentCode = e.Department.DepartmentCode.ToUpper(),
                                    DepartmentName = e.Department.DepartmentName.ToLower(),
                                    DepartmentId = e.Department.DepartmentId,
                                }).First();

                    if (viewModelSummary == null)
                    {
                        return NotFound();
                    }
                   
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(viewModelSummary);


        }

    }

    
}