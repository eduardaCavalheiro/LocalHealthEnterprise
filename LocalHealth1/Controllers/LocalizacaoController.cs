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
    public class LocalizacaoController : Controller
    {
        private readonly LocalHealth1Context _context;

        public LocalizacaoController(LocalHealth1Context context)
        {
            _context = context;
        }

        // GET: Localizacao
        public async Task<IActionResult> Index(string searchString)
        {
            var local = from di in _context.Localizacao
                        select di;

            if (!string.IsNullOrEmpty(searchString))
            {
                local = local.Where(di => di.Cidade!.Contains(searchString));
            }

            return View(await local.ToListAsync());
        }

        // GET: Localizacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Localizacao == null)
            {
                return NotFound();
            }

            var localizacao = await _context.Localizacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localizacao == null)
            {
                return NotFound();
            }

            return View(localizacao);
        }

        // GET: Localizacao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Localizacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cep,Bairro,Cidade,Estado,Logradouro")] Localizacao localizacao)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(localizacao);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Local adicionado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(localizacao);
        }

        // GET: Localizacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Localizacao == null)
            {
                return NotFound();
            }

            var localizacao = await _context.Localizacao.FindAsync(id);
            if (localizacao == null)
            {
                return NotFound();
            }
            return View(localizacao);
        }

        // POST: Localizacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cep,Bairro,Cidade,Estado,Logradouro")] Localizacao localizacao)
        {
            if (id != localizacao.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(localizacao);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Registro editado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalizacaoExists(localizacao.Id))
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
            return View(localizacao);
        }

        // GET: Localizacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Localizacao == null)
            {
                return NotFound();
            }

            var localizacao = await _context.Localizacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localizacao == null)
            {
                return NotFound();
            }

            return View(localizacao);
        }

        // POST: Localizacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Localizacao == null)
            {
                return Problem("Entity set 'LocalHealth1Context.Localizacao'  is null.");
            }
            var localizacao = await _context.Localizacao.FindAsync(id);
            if (localizacao != null)
            {
                _context.Localizacao.Remove(localizacao);
            }
            
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Registro excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool LocalizacaoExists(int id)
        {
          return (_context.Localizacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
