using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SysMaTra.Models;
using SysMaTra.Models.ApiServices;
using System.Net.Http.Headers;
using System.Text;

namespace SysMaTra.ModuleVoyageControllers
{
    public class AgencesController : Controller
    {
        private readonly SysMaTraContext _context;

        HttpClientHandler _ClientHandler = new HttpClientHandler();
        Agence agence = new Agence();
        List<Agence> agences = new List<Agence>();

        public AgencesController(SysMaTraContext context)
        {
            _context = context;
            _ClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        }

        // GET: Agences
        public async Task<IActionResult> Index()
        {
            agences = new List<Agence>();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Agences"))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    agences = JsonConvert.DeserializeObject<List<Agence>>(ApiResponse);
                }
            }
            return View(agences);
        }

        // GET: Agences/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            agence = new Agence();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Agences/" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    agence = JsonConvert.DeserializeObject<Agence>(ApiResponse);
                }
            }
            return View(agence);
        }

        // GET: Agences/Create
        public IActionResult Create()
        {
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "id", "Telephone");
            ViewData["DestinationId"] = new SelectList(_context.Set<Destination>(), "Id", "Nom");
            return View();
        }

        // POST: Agences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,AdresseId,DestinationId,Created")] Agence agence)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient(_ClientHandler))
                {
                    StringContent Content = new StringContent(JsonConvert.SerializeObject(agence), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:5070/api/Agences", Content))
                    {
                        string ApiResponse = await response.Content.ReadAsStringAsync();
                        agence = JsonConvert.DeserializeObject<Agence>(ApiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "id", "Telephone", agence.AdresseId);
            ViewData["DestinationId"] = new SelectList(_context.Set<Destination>(), "Id", "Nom", agence.DestinationId);
            return View(agence);
        }

        // GET: Agences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            agence = new Agence();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Agences/" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    agence = JsonConvert.DeserializeObject<Agence>(ApiResponse);
                }
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "id", "Telephone", agence.AdresseId);
            ViewData["DestinationId"] = new SelectList(_context.Set<Destination>(), "Id", "Nom", agence.DestinationId);
            return View(agence);
        }

        // POST: Agences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,AdresseId,DestinationId,Created")] Agence agence)
        {
            if (id != agence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                using (var httpClient = new HttpClient(_ClientHandler))
                {
                    StringContent Content = new StringContent(JsonConvert.SerializeObject(agence), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:5070/api/Agences", Content))
                    {
                        string ApiResponse = await response.Content.ReadAsStringAsync();
                        agence = JsonConvert.DeserializeObject<Agence>(ApiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "id", "Telephone", agence.AdresseId);
            ViewData["DestinationId"] = new SelectList(_context.Set<Destination>(), "Id", "Nom", agence.DestinationId);
            return View(agence);
        }

        // GET: Agences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agence == null)
            {
                return NotFound();
            }

            agence = new Agence();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Agences/" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    agence = JsonConvert.DeserializeObject<Agence>(ApiResponse);
                }
            }
            return View(agence);
        }

        // POST: Agences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agence == null)
            {
                return Problem("Entity set 'SysMaTraContext.Agence'  is null.");
            }
            string message = " ";
            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5070/api/Adresses/" + id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AgenceExists(int id)
        {
          return _context.Agence.Any(e => e.Id == id);
        }
    }
}
