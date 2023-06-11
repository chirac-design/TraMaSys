using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SysMaTra.Models;
using SysMaTra.Models.ApiServices;

namespace SysMaTra.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly SysMaTraContext _context;
        VoyageApi _api = new VoyageApi();
        HttpClientHandler _ClientHandler = new HttpClientHandler();
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DestinationsController(SysMaTraContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _ClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        }


        // GET: Destinations
        public async Task<IActionResult> Index()
        {
            List<Destination> Destinations = new List<Destination>();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Destinations");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Destinations = JsonConvert.DeserializeObject<List<Destination>>(results);
            }

            return View(Destinations);
        }

        // GET: Destinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Destination destination = new Destination();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Destination" + destination.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                destination = JsonConvert.DeserializeObject<Destination>(results);
            }
            return View(destination);
        }

        // GET: Destinations/Create
        public async Task<IActionResult> Create()
        {
            var trajetsController = new TrajetsController(_context, _ClientHandler);
            List<Trajet> trajet = await trajetsController.GetTrajets();
            ViewData["TrajetId"] = new SelectList(trajet, "Id", "Nom");
            return View();
        }

        // POST: Destinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,TrajetId")] Destination destination, IFormFile Logo)
        {
            if (Logo is null || Logo.Length == 0)
            {
                destination.Logo = "60111.jpg";
                return BadRequest("fichier introuvable");
            }

            destination.Logo = await SaveFileAsync(Logo);
            HttpClient Client = _api.Initial();
            string data = JsonConvert.SerializeObject(destination);

            StringContent content = new StringContent(data, Encoding.UTF8, "Application/JSon");

            HttpResponseMessage res = Client.PostAsync("/Destinations", content).Result;

            if (res.IsSuccessStatusCode)

            {

                return RedirectToAction(nameof(Index));

            }

            // Récupération des données depuis l'action GetTrajets du TrajetsController
            var trajetsController = new TrajetsController(_context, _ClientHandler);
            List<Trajet> trajet = await trajetsController.GetTrajets();

            // Peuplement de la SelectList
            ViewData["TrajetId"] = new SelectList(trajet, "Id", "Nom", destination.TrajetId);

            return View(destination);

        }

        // GET: Destinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Destination destination = new Destination();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Destinations" + destination.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                destination = JsonConvert.DeserializeObject<Destination>(results);
            }
            var trajetsController = new TrajetsController(_context, _ClientHandler);
            List<Trajet> trajet = await trajetsController.GetTrajets();
            ViewData["TrajetId"] = new SelectList(trajet, "Id", "Nom", destination.TrajetId);
            return View(destination);
        }

        // POST: Destinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,TrajetId")] Destination destination, IFormFile Logo)
        {
            if (id != destination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (Logo is null || Logo.Length == 0)
                {
                    destination.Logo = "60111.jpg";
                    return BadRequest("fichier introuvable");
                }

                destination.Logo = await SaveFileAsync(Logo);
                HttpClient Client = _api.Initial();
                string data = JsonConvert.SerializeObject(destination);

                StringContent content = new StringContent(data, Encoding.UTF8, "Application/JSon");

                HttpResponseMessage res = Client.PutAsync("/Destinations" + destination.Id, content).Result;

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            var trajetsController = new TrajetsController(_context, _ClientHandler);
            List<Trajet> trajet = await trajetsController.GetTrajets();
            ViewData["TrajetId"] = new SelectList(trajet, "Id", "Nom", destination.TrajetId);
            return View(destination);
        }

        // GET: Destinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Destination == null)
            {
                return NotFound();
            }

            Destination destination = new Destination();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Destinations" + destination.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                destination = JsonConvert.DeserializeObject<Destination>(results);
            }

            return View(destination);
        }

        // POST: Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Destination == null)
            {
                return Problem("Entity set 'SysMaTraContext.Destination'  is null.");
            }
            Destination destination = new Destination();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.DeleteAsync("/Destinations" + destination.Id);

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
        private bool DestinationExists(int id)
        {
          return _context.Destination.Any(e => e.Id == id);
        }
    }
}
