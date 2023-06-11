using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SysMaTra.Models;

namespace SysMaTra.Controllers
{
    public class PersonnelsController : Controller
    {
        private readonly SysMaTraContext _context;

        public PersonnelsController(SysMaTraContext context)
        {
            _context = context;
        }

        // GET: Personnels
        public async Task<IActionResult> Index()
        {
            var sysMaTraContext = _context.Personnel.Include(p => p.Adresse);
            return View(await sysMaTraContext.ToListAsync());
        }

        // GET: Personnels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personnel == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel
                .Include(p => p.Adresse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // GET: Personnels/Create
        public IActionResult Create()
        {
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "id", "Telephone");
            return View();
        }

        // POST: Personnels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Fonction,AdresseId,Statut,Date,Created")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personnel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "id", "Telephone", personnel.AdresseId);
            return View(personnel);
        }

        // GET: Personnels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personnel == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel.FindAsync(id);
            if (personnel == null)
            {
                return NotFound();
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "id", "Telephone", personnel.AdresseId);
            return View(personnel);
        }

        // POST: Personnels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Fonction,AdresseId,Statut,Date,Created")] Personnel personnel)
        {
            if (id != personnel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personnel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonnelExists(personnel.Id))
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
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "id", "Telephone", personnel.AdresseId);
            return View(personnel);
        }

        // GET: Personnels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personnel == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel
                .Include(p => p.Adresse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // POST: Personnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personnel == null)
            {
                return Problem("Entity set 'SysMaTraContext.Personnel'  is null.");
            }
            var personnel = await _context.Personnel.FindAsync(id);
            if (personnel != null)
            {
                _context.Personnel.Remove(personnel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonnelExists(int id)
        {
          return _context.Personnel.Any(e => e.Id == id);
        }
    }
}
