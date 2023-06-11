using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SysMaTra.Models;

namespace SysMaTra.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly SysMaTraContext _context;
        HttpClientHandler _ClientHandler = new HttpClientHandler();
        Destination destination = new Destination();
        List<Destination> destinations = new List<Destination>();

        public DestinationsController(SysMaTraContext context,HttpClientHandler _ClientHandler)
        {
            _context = context;
            _ClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        }

        // GET: Destinations
        public async Task<IActionResult> Index()
        {
            destinations = new List<Destination>();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Destinations"))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Réponse de l'API : " + ApiResponse);
                    destinations = JsonConvert.DeserializeObject<List<Destination>>(ApiResponse);
                    Debug.WriteLine("Liste des Destinations : " + JsonConvert.SerializeObject(destinations));
                }
            }
            return View(destinations);

        }

        // GET: Destinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Destination == null)
            {
                return NotFound();
            }

            destination = new Destination();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Destinations/" + destination.Id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    destination = JsonConvert.DeserializeObject<Destination>(ApiResponse);
                }
            }
            return View(destination);
        }

        // GET: Destinations/Create
        public async Task<IActionResult> CreateAsync()
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
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,Logo,TrajetId")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient(_ClientHandler))
                {
                    StringContent Content = new StringContent(JsonConvert.SerializeObject(destination), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:5070/api/Destnations", Content))
                    {
                        string ApiResponse = await response.Content.ReadAsStringAsync();
                        destination = JsonConvert.DeserializeObject<Destination>(ApiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var trajetsController = new TrajetsController(_context, _ClientHandler);
            List<Trajet> trajet = await trajetsController.GetTrajets();
            ViewData["TrajetId"] = new SelectList(trajet, "Id", "Nom", destination.TrajetId);
            return View(destination);
        }

        // GET: Destinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Destination == null)
            {
                return NotFound();
            }

            destination = new Destination();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Destnations/" + destination.Id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    destination = JsonConvert.DeserializeObject<Destination>(ApiResponse);
                }
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Logo,TrajetId")] Destination destination)
        {
            if (id != destination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var httpClient = new HttpClient(_ClientHandler))
                    {
                        StringContent Content = new StringContent(JsonConvert.SerializeObject(destination), Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync("http://localhost:5070/api/Destinations/" + destination.Id, Content))
                        {
                            string ApiResponse = await response.Content.ReadAsStringAsync();
                            destination = JsonConvert.DeserializeObject<Destination>(ApiResponse);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinationExists(destination.Id))
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

            destination = new Destination();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Destinations/" + destination.Id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    destination = JsonConvert.DeserializeObject<Destination>(ApiResponse);
                }
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
            string message = " ";
            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5070/api/Destinations/" + destination.Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DestinationExists(int id)
        {
          return _context.Destination.Any(e => e.Id == id);
        }
    }
}
