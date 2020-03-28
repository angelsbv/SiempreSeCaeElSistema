using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiempreSeCaeElSistema.Models;

namespace SiempreSeCaeElSistema.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AirlineContext _context;

        public EmployeeController(AirlineContext context)
        {
            _context = context;
        }

        [Route("empleados"),Route("empleados/index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }
        
        [Route("empleado/info/{id}")]
        public async Task<IActionResult> EmpInfo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmpID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        [Route("empleado/agregar")]
        public IActionResult AddEmp()
        {
            return View();
        }

        [Route("empleado/agregar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmp([Bind("EmpID,EmpName,EmpLastName,EmpGender,EmpHomeAdrs,EmpPhoneNumber,EmpEmail,EmpBirthdate,EmpHireDate,EmpModifiedDate,EmpCardID,EmpSalary,EmpType")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.EmpHireDate = DateTime.Now;
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [Route("empleado/editar/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditEmp(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [Route("empleado/editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmp(int id, [Bind("EmpID,EmpName,EmpLastName,EmpGender,EmpHomeAdrs,EmpPhoneNumber,EmpEmail,EmpBirthdate,EmpHireDate,EmpModifiedDate,EmpCardID,EmpSalary,EmpType")] Employee employee)
        {
            if (id != employee.EmpID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employee.EmpModifiedDate = DateTime.Now;
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmpID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        [Route("empleado/eliminar/{id}")]
        public async Task<IActionResult> DeleteEmp(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmpID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        [Route("empleado/eliminar/{id}")]
        [HttpPost, ActionName("DeleteEmp")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SBCDeleteEmp(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpID == id);
        }
    }
}
