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
    public class DoencaController : Controller
    {
        private readonly LocalHealth1Context _context;

        public DoencaController(LocalHealth1Context context)
        {
            _context = context;
        }

        // GET: Doenca
        public async Task<IActionResult> Index(string searchString)
        {
            var dd = from di in _context.Doenca
                        select di;

            if (!string.IsNullOrEmpty(searchString))
            {
                dd = dd.Where(di => di.Nome!.Contains(searchString));
            }

            return View(await dd.ToListAsync());
        }

        // GET: Doenca/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Doenca == null)
            {
                return NotFound();
            }

            var doenca = await _context.Doenca
                .FirstOrDefaultAsync(m => m.NrCid == id);
            if (doenca == null)
            {
                return NotFound();
            }

            return View(doenca);
        }

        // GET: Doenca/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doenca/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NrCid,Nome,Sintomas")] Doenca doenca)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(doenca);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Doença adicionada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(doenca);
        }

        // GET: Doenca/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Doenca == null)
            {
                return NotFound();
            }

            var doenca = await _context.Doenca.FindAsync(id);
            if (doenca == null)
            {
                return NotFound();
            }
            return View(doenca);
        }

        // POST: Doenca/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NrCid,Nome,Sintomas")] Doenca doenca)
        {
            if (id != doenca.NrCid)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(doenca);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Registro editado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoencaExists(doenca.NrCid))
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
            return View(doenca);
        }

        // GET: Doenca/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Doenca == null)
            {
                return NotFound();
            }

            var doenca = await _context.Doenca
                .FirstOrDefaultAsync(m => m.NrCid == id);
            if (doenca == null)
            {
                return NotFound();
            }

            return View(doenca);
        }

        // POST: Doenca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Doenca == null)
            {
                return Problem("Entity set 'LocalHealth1Context.Doenca'  is null.");
            }
            var doenca = await _context.Doenca.FindAsync(id);
            if (doenca != null)
            {
                _context.Doenca.Remove(doenca);
            }
            
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Registro excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool DoencaExists(string id)
        {
          return (_context.Doenca?.Any(e => e.NrCid == id)).GetValueOrDefault();
        }
    }
}
