using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
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
    public class AdressesController : Controller
    {
        private readonly SysMaTraContext _context;

        HttpClientHandler _ClientHandler = new HttpClientHandler();
        Adresse adresse = new Adresse();
        List<Adresse> adresses = new List<Adresse>();

        public AdressesController(SysMaTraContext context)
        {
            _context = context;
            _ClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        }

        // GET: Adresses
        public async Task<IActionResult> Index()
        {
            adresses = new List<Adresse>();

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Adresses"))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    adresses = JsonConvert.DeserializeObject<List<Adresse>>(ApiResponse);
                }
            }
            return View(adresses);
        }

        // GET: Adresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Adresses/" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    adresse = JsonConvert.DeserializeObject<Adresse>(ApiResponse);
                }
            }
            return View(adresse);
        }

        // GET: Adresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Email,Telephone,Domicile")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {

                using (var httpClient = new HttpClient(_ClientHandler))
                {
                    StringContent Content = new StringContent(JsonConvert.SerializeObject(adresse), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:5070/api/Adresses", Content))
                    {
                        string ApiResponse = await response.Content.ReadAsStringAsync();
                        adresse = JsonConvert.DeserializeObject<Adresse>(ApiResponse);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adresse);
        }

        // GET: Adresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Adresses/" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    adresse = JsonConvert.DeserializeObject<Adresse>(ApiResponse);
                }
            }
            return View(adresse);
        }

        // POST: Adresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Email,Telephone,Domicile")] Adresse adresse)
        {

            if (adresse.id != 0)
            {
                if (ModelState.IsValid)
                {

                    using (var httpClient = new HttpClient(_ClientHandler))
                    {
                        StringContent Content = new StringContent(JsonConvert.SerializeObject(adresse), Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PutAsync("http://localhost:5070/api/Adresses/" +id, Content))
                        {
                            string ApiResponse = await response.Content.ReadAsStringAsync();
                            adresse = JsonConvert.DeserializeObject<Adresse>(ApiResponse);
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(adresse);
        }

            // GET: Adresses/Delete/5
            public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient(_ClientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5070/api/Adresses/" + id))
                {
                    string ApiResponse = await response.Content.ReadAsStringAsync();
                    adresse = JsonConvert.DeserializeObject<Adresse>(ApiResponse);
                }
            }
            return View(adresse);
        }

        // POST: Adresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Adresse == null)
            {
                return Problem("Entity set 'SysMaTraContext.Adresse'  is null.");
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

        private bool AdresseExists(int id)
        {
          return _context.Adresse.Any(e => e.id == id);
        }
    }
}
