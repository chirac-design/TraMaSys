using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SysMaTra.Models;
using SysMaTra.Models.ApiServices;

namespace SysMaTra.Controllers
{
    public class ColisController : Controller
    {
        private readonly SysMaTraContext _context;
        VoyageApi _api = new VoyageApi();
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ColisController(SysMaTraContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Colis
        public async Task<IActionResult> Index()
        {
            List<Colis> Colis = new List<Colis>();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Colis");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Colis = JsonConvert.DeserializeObject<List<Colis>>(results);
            }
            return View(Colis);
        }

        // GET: Colis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Colis == null)
            {
                return NotFound();
            }
            Colis colis = new Colis();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Colis" + colis.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                colis = JsonConvert.DeserializeObject<Colis>(results);
            }

            return View(colis);
        }

        // GET: Colis/Create
        public IActionResult Create()
        {
            ViewData["DestinationId"] = new SelectList(_context.Set<Destination>(), "Id", "Nom");
            return View();
        }

        // POST: Colis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Valeur,Description,DestinationId,Destinateur,DestinateurPhone,Destinatire,DestinatairePhone,IsPaye,Statut,Date,Created")] Colis colis, IFormFile Image)
        {
            if (Image is null || Image.Length == 0)
            {
                colis.Image = "60111.jpg";
                return BadRequest("fichier introuvable");
            }

            colis.Image = await SaveFileAsync(Image);
            HttpClient Client = _api.Initial();
            string data = JsonConvert.SerializeObject(colis);

            StringContent content = new StringContent(data, Encoding.UTF8, "Application/JSon");

            HttpResponseMessage res = Client.PostAsync("/Colis", content).Result;

            if (res.IsSuccessStatusCode)

            {

                return RedirectToAction(nameof(Index));

            }
            ViewData["DestinationId"] = new SelectList(_context.Set<Destination>(), "Id", "Nom", colis.DestinationId);
            return View(colis);
        }

        // GET: Colis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Colis == null)
            {
                return NotFound();
            }

            Colis colis = new Colis();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Colis" + colis.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                colis = JsonConvert.DeserializeObject<Colis>(results);
            }
            ViewData["DestinationId"] = new SelectList(_context.Set<Destination>(), "Id", "Nom", colis.DestinationId);
            return View(colis);
        }

        // POST: Colis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valeur,Description,DestinationId,Destinateur,DestinateurPhone,Destinatire,DestinatairePhone,IsPaye,Statut,Date,Created")] Colis colis, IFormFile Image)
        {
            if (id != colis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (Image is null || Image.Length == 0)
                {
                    colis.Image = "60111.jpg";
                    return BadRequest("fichier introuvable");
                }

                colis.Image = await SaveFileAsync(Image);
                HttpClient Client = _api.Initial();
                string data = JsonConvert.SerializeObject(colis);

                StringContent content = new StringContent(data, Encoding.UTF8, "Application/JSon");

                HttpResponseMessage res = Client.PutAsync("/Colis" + colis.Id, content).Result;

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DestinationId"] = new SelectList(_context.Set<Destination>(), "Id", "Nom", colis.DestinationId);
            return View(colis);
        }

        // GET: Colis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Colis == null)
            {
                return NotFound();
            }

            Colis colis = new Colis();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Colis" + colis.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                colis = JsonConvert.DeserializeObject<Colis>(results);
            }

            return View(colis);
        }

        // POST: Colis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Colis == null)
            {
                return Problem("Entity set 'SysMaTraContext.Colis'  is null.");
            }
            Colis colis = new Colis();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.DeleteAsync("/Colis" + colis.Id);

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
        private bool ColisExists(int id)
        {
          return _context.Colis.Any(e => e.Id == id);
        }
    }
}
