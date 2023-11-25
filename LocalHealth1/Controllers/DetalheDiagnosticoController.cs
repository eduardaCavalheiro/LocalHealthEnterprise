using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocalHealth1.Data;
using LocalHealth1.Models;

namespace LocalHealth1.Controllers
{
    public class DetalheDiagnosticoController : Controller
    {
        private readonly LocalHealth1Context _context;

        public DetalheDiagnosticoController(LocalHealth1Context context)
        {
            _context = context;
        }

        // GET: DetalheDiagnostico
        public async Task<IActionResult> Index(string searchString)
        {
            ViewBag.Page = "Index";

            var dd = from di in _context.DetalheDiagnostico
                     select di;

            if (!string.IsNullOrEmpty(searchString))
            {
                dd = dd.Where(di => di.ExamesSolicitados!.Contains(searchString));
            }

            return View(await dd.ToListAsync());
        }

        // GET: DetalheDiagnostico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetalheDiagnostico == null)
            {
                return NotFound();
            }

            var detalheDiagnostico = await _context.DetalheDiagnostico
                .Include(d => d.Diagnostico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalheDiagnostico == null)
            {
                return NotFound();
            }

            return View(detalheDiagnostico);
        }

        // GET: DetalheDiagnostico/Create
        public IActionResult Create()
        {
            ViewData["DiagnosticoId"] = new SelectList(_context.Diagnostico, "Id", "Id");
            return View();
        }

        // POST: DetalheDiagnostico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,ExamesSolicitados,NomePaciente,DiagnosticoId")] DetalheDiagnostico detalheDiagnostico)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(detalheDiagnostico);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Informações do Diagnóstico acrescentadas com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiagnosticoId"] = new SelectList(_context.Diagnostico, "Id", "Id", detalheDiagnostico.DiagnosticoId);
            return View(detalheDiagnostico);
        }

        // GET: DetalheDiagnostico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetalheDiagnostico == null)
            {
                return NotFound();
            }

            var detalheDiagnostico = await _context.DetalheDiagnostico.FindAsync(id);
            if (detalheDiagnostico == null)
            {
                return NotFound();
            }
            ViewData["DiagnosticoId"] = new SelectList(_context.Diagnostico, "Id", "Id", detalheDiagnostico.DiagnosticoId);
            return View(detalheDiagnostico);
        }

        // POST: DetalheDiagnostico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,ExamesSolicitados,NomePaciente,DiagnosticoId")] DetalheDiagnostico detalheDiagnostico)
        {
            if (id != detalheDiagnostico.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalheDiagnostico);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Registro editado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalheDiagnosticoExists(detalheDiagnostico.Id))
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
            ViewData["DiagnosticoId"] = new SelectList(_context.Diagnostico, "Id", "Id", detalheDiagnostico.DiagnosticoId);
            return View(detalheDiagnostico);
        }

        // GET: DetalheDiagnostico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetalheDiagnostico == null)
            {
                return NotFound();
            }

            var detalheDiagnostico = await _context.DetalheDiagnostico
                .Include(d => d.Diagnostico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalheDiagnostico == null)
            {
                return NotFound();
            }

            return View(detalheDiagnostico);
        }

        // POST: DetalheDiagnostico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetalheDiagnostico == null)
            {
                return Problem("Entity set 'LocalHealth1Context.DetalheDiagnostico'  is null.");
            }
            var detalheDiagnostico = await _context.DetalheDiagnostico.FindAsync(id);
            if (detalheDiagnostico != null)
            {
                _context.DetalheDiagnostico.Remove(detalheDiagnostico);
            }
            
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Registro excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool DetalheDiagnosticoExists(int id)
        {
          return (_context.DetalheDiagnostico?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
