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
    public class FlightController : Controller
    {
        private readonly AirlineContext ctx;
        public FlightController(AirlineContext context)
        {
            ctx = context;
        }
        [Route("vuelos"), Route("vuelos/index")]
        public async Task<IActionResult> Index()
        {
            var model = ctx.Flights.Include(f => f.aircraft);
            return View(await model.ToListAsync());
        }

        [Route("vuelos/agregar")]
        [HttpGet]
        public IActionResult AddFlg()
        {
            ViewData["AcID"] = new SelectList(ctx.Aircrafts, "AcID", "AcModel");
            return View();
        }

        [Route("vuelos/agregar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFlg(Flight vmFr)
        {
            if (ModelState.IsValid) 
            {
                ctx.Flights.Add(vmFr);
                await ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcID"] = new SelectList(ctx.Aircrafts, "AcID", "AcModel", vmFr.AcID);
            return View(vmFr);
        }

        [Route("vuelos/editar/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditFlg (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await ctx.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["AcID"] = new SelectList(ctx.Aircrafts, "AcID", "AcModel", flight.AcID);
            return View(flight);
        }

        [Route("vuelos/editar/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFlg(int id, [Bind("FlgID,AcID,FlgDeparture,FlgArrival,FlgFare,RouteID")] Flight flight)
        {
            if (id != flight.FlgID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ctx.Update(flight);
                    await ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ctx.Flights.Any(e => e.FlgID == flight.FlgID))
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
            ViewData["AcID"] = new SelectList(ctx.Aircrafts, "AcID", "AcModel", flight.AcID);
            return View(flight);
        }

        [Route("vuelos/eliminar/{id}")]
        [HttpGet]
        public async Task<IActionResult> DelFlg(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await ctx.Flights
                .Include(f => f.aircraft)
                .FirstOrDefaultAsync(m => m.FlgID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        [Route("vuelos/eliminar/{id}")]
        [HttpPost, ActionName("DelFlg")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SBCDelFlg(int id)
        {
            var flight = await ctx.Flights.FindAsync(id);
            ctx.Flights.Remove(flight);
            await ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
