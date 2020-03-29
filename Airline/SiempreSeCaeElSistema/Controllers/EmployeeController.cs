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
        readonly AirlineContext ctx;

        public EmployeeController(AirlineContext context)
        {
            ctx = context;
        }

        [Route("empleados"),Route("empleados/index")]
        public async Task<IActionResult> Index()
        {
            return View(await ctx.Employees.Include(r => r.FlightsAssigned).ToListAsync());
        }
        
        [Route("empleado/info/{id}")]
        public async Task<IActionResult> EmpInfo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await ctx.Employees
                .FirstOrDefaultAsync(m => m.EmpID == id);
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }
        [Route("empleado/agregar")]
        public IActionResult AddEmp()
        {
            return View();
        }

        [Route("empleado/agregar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmp([Bind("EmpID,EmpName,EmpLastName,EmpGender,EmpHomeAdrs,EmpPhoneNumber,EmpEmail,EmpBirthdate,EmpHireDate,EmpModifiedDate,EmpCardID,EmpSalary,EmpType")] Employee emp)
        {
            if (ModelState.IsValid)
            {
                emp.EmpHireDate = DateTime.Now;
                ctx.Add(emp);
                await ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        [Route("empleado/editar/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditEmp(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await ctx.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
        [Route("empleado/editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmp(int id, [Bind("EmpID,EmpName,EmpLastName,EmpGender,EmpHomeAdrs,EmpPhoneNumber,EmpEmail,EmpBirthdate,EmpHireDate,EmpModifiedDate,EmpCardID,EmpSalary,EmpType")] Employee emp)
        {
            if (id != emp.EmpID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    emp.EmpModifiedDate = DateTime.Now;
                    ctx.Update(emp);
                    await ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ctx.Employees.Any(e => e.EmpID == emp.EmpID))
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
            return View(emp);
        }
        [Route("empleado/eliminar/{id}")]
        public async Task<IActionResult> DeleteEmp(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await ctx.Employees
                .FirstOrDefaultAsync(m => m.EmpID == id);
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }
        [Route("empleado/eliminar/{id}")]
        [HttpPost, ActionName("DeleteEmp")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SBCDeleteEmp(int id)
        {
            var emp = await ctx.Employees.FindAsync(id);
            ctx.Employees.Remove(emp);
            await ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("empleado/asignar/{id}")]
        [HttpGet]
        public async Task<IActionResult> AssignFlg(int id)
        {
            ViewData["FlgID"] = new SelectList(ctx.Flights, "FlgID", "FlgID");
            var emp = await ctx.Employees.FindAsync(id);
            ViewBag.EmpNombreCompleto = emp.NombreCompleto;
            ViewBag.EmpID = emp.EmpID;
            return View();
        }

        [Route("empleado/asignar/{FlgID}")]
        [HttpPost]
        public async Task<IActionResult> AssignFlg(int FlgID, [Bind("FlgID, EmpID")] FlightAssignedTo FlgEmp)
        {
            
            if (ModelState.IsValid)
            {

                //ModelState.AddModelError("one", "Este empleado ya tiene este vuelo asignado.");
                //ViewData["FlgID"] = new SelectList(ctx.Flights, "FlgID", "FlgID");
                //var emp = await ctx.Employees.FindAsync(FlgEmp.EmpID);
                //ViewBag.EmpNombreCompleto = emp.NombreCompleto;
                //ViewBag.EmpID = emp.EmpID;

                ctx.FlightAssignedEmps.Add(FlgEmp);
                await ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [Route("empleado/asignaciones/{id}")]
        [HttpGet]
        public async Task<IActionResult> EmpFlgTbl(int id)
        {
            
            var flgEmp = from i in ctx.FlightAssignedEmps
                        where i.EmpID == id
                        select i;

            var emp = await ctx.Employees
                .FindAsync(id);
            if (flgEmp == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EmpNombreCompleto = emp.NombreCompleto;
            return View(await flgEmp.ToListAsync());
        }

        [Route("empleado/delassign/{id}")]
        [HttpGet]
        public async Task<IActionResult> DelEmpFlg(int id)
        {
            var flgEmp = await ctx.FlightAssignedEmps.FindAsync(id);

            if(flgEmp == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ctx.FlightAssignedEmps.Remove(flgEmp);
            await ctx.SaveChangesAsync();
            return RedirectToAction(nameof(EmpFlgTbl), new { id = flgEmp.EmpID });
        }

    }
}
