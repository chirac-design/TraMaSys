using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SysMaTra.Models;
using System.Diagnostics;
using System.Text;

namespace SysMaTra.Controllers
{
    public class TrajetsController : Controller
    {
        private readonly SysMaTraContext _context;
        HttpClientHandler _ClientHandler = new HttpClientHandler();
        Trajet trajet = new Trajet();
        List<Trajet> trajets = new List<Trajet>();
        public TrajetsController(SysMaTraContext context, HttpClientHandler _ClientHandler)
        {
            _context = context;
            _ClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        }

        // GET: Trajets
        public async Task<IActionResult> Index()
        {
            trajets = new List<Trajet>();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Trajets"))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Réponse de l'API : " + ApiResponse);
                    trajets = JsonConvert.DeserializeObject<List<Trajet>>(ApiResponse);
                    Debug.WriteLine("Liste des trajets : " + JsonConvert.SerializeObject(trajets));
                }
            }

            //trajets = trajets.Where(t => t.Nom == "Douala-Yaounde").ToList();
            //_context.Trajet.AddRange(trajets);
            //await _context.SaveChangesAsync();
            return View(trajets);
        }

        public async Task<List<Trajet>> GetTrajets()
        {
            List<Trajet> trajets = new List<Trajet>();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Trajets"))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Réponse de l'API : " + ApiResponse);
                    trajets = JsonConvert.DeserializeObject<List<Trajet>>(ApiResponse);
                    Debug.WriteLine("Liste des trajets : " + JsonConvert.SerializeObject(trajets));
                }
            }

            return trajets;
        }



        // GET: Trajets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trajet == null)
            {
                return NotFound();
            }

            trajet = new Trajet();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Trajets!" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    trajet = JsonConvert.DeserializeObject<Trajet>(ApiResponse);
                }
            }
            return View(trajet);
        }

        // GET: Trajets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trajets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,Tarif")] Trajet trajet)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient(_ClientHandler))
                {
                    StringContent Content = new StringContent(JsonConvert.SerializeObject(trajet), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:5070/api/Trajets", Content))
                    {
                        string ApiResponse = await response.Content.ReadAsStringAsync();
                        trajet = JsonConvert.DeserializeObject<Trajet>(ApiResponse);
                    }
                }

                // New trajet

                //Trajet Ctrajet = new Trajet
                //{
                //    Nom = trajet.Nom,
                //    Description = trajet.Description,
                //    Tarif = trajet.Tarif
                //};
                //_context.Trajet.Add(Ctrajet);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trajet);
        }

        // GET: Trajets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trajet == null)
            {
                return NotFound();
            }

            trajet = new Trajet();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Trajets!" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    trajet = JsonConvert.DeserializeObject<Trajet>(ApiResponse);
                }
            }
            return View(trajet);
        }

        // POST: Trajets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Tarif")] Trajet trajet)
        {
            if (id != trajet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient(_ClientHandler))
                {
                    StringContent Content = new StringContent(JsonConvert.SerializeObject(trajet), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:5070/api/Trajets/" + id, Content))
                    {
                        string ApiResponse = await response.Content.ReadAsStringAsync();
                        trajet = JsonConvert.DeserializeObject<Trajet>(ApiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trajet);
        }

        // GET: Trajets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trajet == null)
            {
                return NotFound();
            }

            trajet = new Trajet();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Trajets!" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    trajet = JsonConvert.DeserializeObject<Trajet>(ApiResponse);
                }
            }
            return View(trajet);
        }

        // POST: Trajets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trajet == null)
            {
                return Problem("Entity set 'SysMaTraContext.Trajet'  is null.");
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

        private bool TrajetExists(int id)
        {
          return _context.Trajet.Any(e => e.Id == id);
        }
    }
}
