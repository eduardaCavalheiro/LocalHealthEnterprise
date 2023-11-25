using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocalHealth1.Data;
using LocalHealth1.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Globalization;
using System.Text;

using CsvHelper;
using System.IO;
using CsvHelper.Configuration;


namespace LocalHealth1.Controllers
{

    public class MedicoCsvMap : ClassMap<Medico>
    {
        public MedicoCsvMap()
        {
            Map(m => m.Nome).Name("Nome");
            Map(m => m.Especialidade).Name("Especialidade");
            Map(m => m.CrmId).Name("CrmId");
            // Adicione mapeamentos para outros campos, se necessário
        }
    }
    public class MedicoController : Controller
    {
        private readonly LocalHealth1Context _context;

        public MedicoController(LocalHealth1Context context)
        {
            _context = context;
        }

        // GET: Medico
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Medico == null)
            {
                return Problem("Entity set 'Medico'  is null.");
            }

            var med = from m in _context.Medico
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                med = med.Where(s => s.Nome!.Contains(searchString));
            }

            return View(await med.ToListAsync());
        }

        // GET: Medico/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Medico == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
                .FirstOrDefaultAsync(m => m.CrmId == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,CrmId,Especialidade,Senha")] Medico medico)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Médico adicionado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medico/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Medico == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nome,CrmId,Especialidade,Senha")] Medico medico)
        {
            if (id != medico.CrmId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Registro editado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.CrmId))
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
            return View(medico);
        }

        // GET: Medico/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Medico == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
                .FirstOrDefaultAsync(m => m.CrmId == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Medico == null)
            {
                return Problem("Entity set 'Medico'  is null.");
            }
            var medico = await _context.Medico.FindAsync(id);
            if (medico != null)
            {
                _context.Medico.Remove(medico);
            }
            
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Registro excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(string id)
        {
          return (_context.Medico?.Any(e => e.CrmId == id)).GetValueOrDefault();
        }

        
        public IActionResult ExportToCSV()
        {
            var data = _context.Medico.ToList(); // Obtém os dados da entidade Medico

            var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, 1024, true))
            {
                using (var csvWriter = new CsvHelper.CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(data);
                }
            }

            memoryStream.Position = 0;
            return File(memoryStream, "text/csv", "medico.csv");
        }

    }
}
