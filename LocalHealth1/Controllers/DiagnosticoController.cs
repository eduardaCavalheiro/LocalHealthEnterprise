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
    public class DiagnosticoController : Controller
    {
        private readonly LocalHealth1Context _context;

        public DiagnosticoController(LocalHealth1Context context)
        {
            _context = context;
        }

        // GET: Diagnostico
        public async Task<IActionResult> Index(string searchString)
        {
            var diag = from di in _context.Diagnostico
                     select di;

            if (!string.IsNullOrEmpty(searchString))
            {
                diag = diag.Where(di => di.SintomasPaciente!.Contains(searchString));
            }

            return View(await diag.ToListAsync());
        }

        // GET: Diagnostico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diagnostico == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico
                .Include(d => d.Localizacao)
                .Include(d => d.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // GET: Diagnostico/Create
        public IActionResult Create()
        {
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacao, "Id", "Id");
            ViewData["MedicoCrmId"] = new SelectList(_context.Medico, "CrmId", "CrmId");
            return View();
        }

        // POST: Diagnostico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SintomasPaciente,Doenca,MedicoCrmId,LocalizacaoId")] Diagnostico diagnostico)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(diagnostico);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Diagnóstico criado com sucesso!";
                return RedirectToAction(nameof(Index));
                
            }
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacao, "Id", "Id", diagnostico.LocalizacaoId);
            ViewData["MedicoCrmId"] = new SelectList(_context.Medico, "CrmId", "CrmId", diagnostico.MedicoCrmId);
            return View(diagnostico);
        }

        // GET: Diagnostico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diagnostico == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico.FindAsync(id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacao, "Id", "Id", diagnostico.LocalizacaoId);
            ViewData["MedicoCrmId"] = new SelectList(_context.Medico, "CrmId", "CrmId", diagnostico.MedicoCrmId);
            return View(diagnostico);
        }

        // POST: Diagnostico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SintomasPaciente,Doenca,MedicoCrmId,LocalizacaoId")] Diagnostico diagnostico)
        {
            if (id != diagnostico.Id)
            {
                return NotFound();

            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnostico);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Registro editado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticoExists(diagnostico.Id))
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
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacao, "Id", "Id", diagnostico.LocalizacaoId);
            ViewData["MedicoCrmId"] = new SelectList(_context.Medico, "CrmId", "CrmId", diagnostico.MedicoCrmId);
            return View(diagnostico);
        }

        // GET: Diagnostico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diagnostico == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico
                .Include(d => d.Localizacao)
                .Include(d => d.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // POST: Diagnostico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diagnostico == null)
            {
                return Problem("Entity set 'Diagnostico'  is null.");
            }
            var diagnostico = await _context.Diagnostico.FindAsync(id);
            if (diagnostico != null)
            {
                _context.Diagnostico.Remove(diagnostico);
            }
            
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Registro excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticoExists(int id)
        {
          return (_context.Diagnostico?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
