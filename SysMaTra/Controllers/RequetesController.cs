using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SysMaTra.Models;
using SysMaTra.Models.ApiServices;

namespace SysMaTra.Controllers
{
    public class RequetesController : Controller
    {
        private readonly SysMaTraContext _context;
        HttpClientHandler _ClientHandler = new HttpClientHandler();
        Requete requete = new Requete();
        List<Requete> requetes = new List<Requete>();
        private readonly IWebHostEnvironment _hostingEnvironment;

        public RequetesController(SysMaTraContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _ClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        }

        // GET: Requetes
        public async Task<IActionResult> Index()
        {
            requetes = new List<Requete>();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Requetes"))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    requetes = JsonConvert.DeserializeObject<List<Requete>>(ApiResponse);
                }
            }

            return View(requetes);
        }

        // GET: Requetes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            requete = new Requete();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Requetes" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    requete = JsonConvert.DeserializeObject<Requete>(ApiResponse);
                }
            }

            return View(requete);
        }

        // GET: Requetes/Create
        public IActionResult Create()
        {
            ViewData["AdresseId"] = new SelectList(_context.Adresse, "id", "Telephone");
            return View();
        }

        // POST: Requetes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Objet,Contenu,Destinataire,Statut,Date,Created")] Requete requete, IFormFile Piece)
        {
            if (ModelState.IsValid)
            {
                if (Piece is null || Piece.Length == 0)
                {
                    requete.Piece = "60111.jpg";
                    return BadRequest("fichier introuvable");
                }

                requete.Piece = await SaveFileAsync(Piece);
                using (var httpClient = new HttpClient(_ClientHandler))
                {
                    StringContent Content = new StringContent(JsonConvert.SerializeObject(requete), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:5070/api/Requetes", Content))
                    {
                        string ApiResponse = await response.Content.ReadAsStringAsync();
                        requete = JsonConvert.DeserializeObject<Requete>(ApiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(requete);
        }

        // GET: Requetes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            requete = new Requete();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Requetes" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    requete = JsonConvert.DeserializeObject<Requete>(ApiResponse);
                }
            }

            return View(requete);
        }

        // POST: Requetes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Objet,Contenu,Destinataire,Statut,Date,Created")] Requete requete, IFormFile Piece)
        {
            if (id != requete.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid) 
            { 
                requete.Piece = await SaveFileAsync(Piece);
                using (var httpClient = new HttpClient(_ClientHandler))
                {
                    StringContent Content = new StringContent(JsonConvert.SerializeObject(requete), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:5070/api/Requetes", Content))
                    {
                        string ApiResponse = await response.Content.ReadAsStringAsync();
                        requete = JsonConvert.DeserializeObject<Requete>(ApiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(requete);
        }

        // GET: Requetes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            requete = new Requete();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Requetes" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    requete = JsonConvert.DeserializeObject<Requete>(ApiResponse);
                }
            }

            return View(requete);
        }

        // POST: Requetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Requete == null)
            {
                return Problem("Entity set 'SysMaTraContext.Adresse'  is null.");
            }

            string message = " ";
            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5070/api/Requetes/" + id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
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
        private bool RequeteExists(int id)
        {
          return _context.Requete.Any(e => e.Id == id);
        }
    }
}
