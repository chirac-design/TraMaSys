using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SysMaTra.Models;
using SysMaTra.Models.ApiServices;

namespace SysMaTra.Controllers
{
    public class PassagersController : Controller
    {
        private readonly SysMaTraContext _context;
        VoyageApi _api = new VoyageApi();
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PassagersController(SysMaTraContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Passagers
        public async Task<IActionResult> Index()
        {
            List<Passager> Passagers = new List<Passager>();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Passagers");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Passagers = JsonConvert.DeserializeObject<List<Passager>>(results);
            }

            return View(Passagers);
        }

        // GET: Passagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Passager Passager = new Passager();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Passagers" + Passager.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Passager = JsonConvert.DeserializeObject<Passager>(results);
            }
            return View(Passager);
        }

        // GET: Passagers/Create
        public IActionResult Create()
        {
            ViewData["DestinationId"] = new SelectList(_context.Destination, "Id", "Nom");
            return View();
        }

        // POST: Passagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,DestinationId,Classe,IsPaye,AgenceId,Date,Created")] Passager passager, IFormFile ImageCNI)
        {
            if (ModelState.IsValid)
            {
                if (ImageCNI is null || ImageCNI.Length == 0)
                {
                    passager.ImageCNI = "60111.jpg";
                    return BadRequest("fichier introuvable");
                }

                passager.ImageCNI = await SaveFileAsync(ImageCNI);
                HttpClient Client = _api.Initial();
                string data = JsonConvert.SerializeObject(passager);

                StringContent content = new StringContent(data, Encoding.UTF8, "Application/JSon");

                HttpResponseMessage res = Client.PostAsync("/Passagers", content).Result;

                if (res.IsSuccessStatusCode)

                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DestinationId"] = new SelectList(_context.Destination, "Id", "Nom", passager.DestinationId);
            return View(passager);
        }

        // GET: Passagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Passager passager = new Passager();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Passagers" + passager.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                passager = JsonConvert.DeserializeObject<Passager>(results);
            }
            ViewData["DestinationId"] = new SelectList(_context.Destination, "Id", "Nom", passager.DestinationId);
            return View(passager);
        }

        // POST: Passagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,DestinationId,Classe,IsPaye,AgenceId,Date,Created")] Passager passager,
            IFormFile ImageCNI)
        {
            if (id != passager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    passager.ImageCNI = await SaveFileAsync(ImageCNI);
                    HttpClient Client = _api.Initial();
                    string data = JsonConvert.SerializeObject(passager);

                    StringContent content = new StringContent(data, Encoding.UTF8, "Application/JSon");

                    HttpResponseMessage res = Client.PutAsync("/Passagers" + passager.Id, content).Result;

                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassagerExists(passager.Id))
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
            ViewData["DestinationId"] = new SelectList(_context.Destination, "Id", "Nom", passager.DestinationId);
            return View(passager);
        }

        // GET: Passagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Passager passager = new Passager();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Passagers" + passager.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                passager = JsonConvert.DeserializeObject<Passager>(results);
            }

            return View(passager);
        }

        // POST: Passagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Passager == null)
            {
                return Problem("Entity set 'SysMaTraContext.Passager'  is null.");
            }
            Passager passager = new Passager();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.DeleteAsync("/Passagers" + passager.Id);
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
        private bool PassagerExists(int id)
        {
          return _context.Passager.Any(e => e.Id == id);
        }
    }
}
