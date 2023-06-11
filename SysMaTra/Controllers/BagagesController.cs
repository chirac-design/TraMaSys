using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SysMaTra.Models;

namespace SysMaTra.Controllers
{
    public class BagagesController : Controller
    {
        private readonly SysMaTraContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BagagesController(SysMaTraContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Bagages
        public async Task<IActionResult> Index()
        {
            var sysMaTraContext = _context.Bagage.Include(b => b.Passager);
            return View(await sysMaTraContext.ToListAsync());
        }

        // GET: Bagages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bagage == null)
            {
                return NotFound();
            }

            var bagage = await _context.Bagage
                .Include(b => b.Passager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bagage == null)
            {
                return NotFound();
            }

            return View(bagage);
        }

        // GET: Bagages/Create
        public IActionResult Create()
        {
            ViewData["PassagerId"] = new SelectList(_context.Set<Passager>(), "Id", "Nom");
            return View();
        }

        // POST: Bagages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Poids,Description,PassagerId")] Bagage bagage, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image is null || Image.Length == 0)
                {
                    bagage.Image = "60111.jpg";
                    return BadRequest("fichier introuvable");
                }

                bagage.Image = await SaveFileAsync(Image);
                _context.Add(bagage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PassagerId"] = new SelectList(_context.Set<Passager>(), "Id", "Nom", bagage.PassagerId);
            return View(bagage);
        }

        // GET: Bagages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bagage == null)
            {
                return NotFound();
            }

            var bagage = await _context.Bagage.FindAsync(id);
            if (bagage == null)
            {
                return NotFound();
            }
            ViewData["PassagerId"] = new SelectList(_context.Set<Passager>(), "Id", "Nom", bagage.PassagerId);
            return View(bagage);
        }

        // POST: Bagages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Poids,Description,PassagerId")] Bagage bagage, IFormFile Image)
        {
            if (id != bagage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is null || Image.Length == 0)
                    {
                        bagage.Image = "60111.jpg";
                        return BadRequest("fichier introuvable");
                    }

                    bagage.Image = await SaveFileAsync(Image);
                    _context.Update(bagage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BagageExists(bagage.Id))
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
            ViewData["PassagerId"] = new SelectList(_context.Set<Passager>(), "Id", "Nom", bagage.PassagerId);
            return View(bagage);
        }

        // GET: Bagages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bagage == null)
            {
                return NotFound();
            }

            var bagage = await _context.Bagage
                .Include(b => b.Passager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bagage == null)
            {
                return NotFound();
            }

            return View(bagage);
        }

        // POST: Bagages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bagage == null)
            {
                return Problem("Entity set 'SysMaTraContext.Bagage'  is null.");
            }
            var bagage = await _context.Bagage.FindAsync(id);
            if (bagage != null)
            {
                _context.Bagage.Remove(bagage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/Images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"/uploads/Images/{fileName}";
        }

        private bool BagageExists(int id)
        {
          return (_context.Bagage?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
