using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiempreSeCaeElSistema.Models;

namespace Airline.Controllers
{
    [Route("aviones")]
    public class AircraftController : Controller
    {
        private readonly AirlineContext ctx;

        public AircraftController(AirlineContext context)
        {
            ctx = context;
        }

        [Route("index"), Route("")]
        public async Task<IActionResult> Index()
        {
            return View(await ctx.Aircrafts.ToListAsync());
        }


        [Route("agregar")]
        [HttpGet]
        public IActionResult AddAc()
        {
            return View();
        }

        [Route("agregar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAc([Bind("AcID,AcModel,AcType,AcCapacity,AcRegisterDate,AcModifiedDate")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                aircraft.AcRegisterDate = DateTime.Now;
                ctx.Add(aircraft);
                await ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aircraft);
        }

        [Route("editar/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditAc(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await ctx.Aircrafts.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }
            return View(aircraft);
        }

        [Route("editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAc(int id, [Bind("AcID,AcModel,AcType,AcCapacity,AcRegisterDate,AcModifiedDate")] Aircraft aircraft)
        {
            if (id != aircraft.AcID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    aircraft.AcModifiedDate = DateTime.Now;
                    ctx.Update(aircraft);
                    await ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ctx.Aircrafts.Any(e => e.AcID == aircraft.AcID))
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
            return View(aircraft);
        }

        [Route("eliminar/{id}")]
        [HttpGet]
        public async Task<IActionResult> DelAc(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await ctx.Aircrafts
                .FirstOrDefaultAsync(m => m.AcID == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        [Route("eliminar/{id}")]
        [HttpPost, ActionName("DelAc")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SBCDelAc(int id)
        {
            var aircraft = await ctx.Aircrafts.FindAsync(id);
            ctx.Aircrafts.Remove(aircraft);
            await ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
