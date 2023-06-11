using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SysMaTra.Models;
using SysMaTra.Models.ApiServices;

namespace SysMaTra.Controllers
{
    public class CouleursController : Controller
    {
        private readonly SysMaTraContext _context;
        VoyageApi _api = new VoyageApi();

        public CouleursController(SysMaTraContext context)
        {
            _context = context;
        }

        // GET: Couleurs
        public async Task<IActionResult> Index()
        {
            List<Couleur> Couleurs = new List<Couleur>();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Couleurs");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Couleurs = JsonConvert.DeserializeObject<List<Couleur>>(results);
            }

            return View(Couleurs);
        }

        // GET: Couleurs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Couleur Couleur = new Couleur();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Couleurs" + Couleur.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Couleur = JsonConvert.DeserializeObject<Couleur>(results);
            }
            return View(Couleur);
           
        }

        // GET: Couleurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Couleurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nom,Code")] Couleur couleur)
        {
            HttpClient Client = _api.Initial();
            string data = JsonConvert.SerializeObject(couleur);

            StringContent content = new StringContent(data, Encoding.UTF8, "Application/JSon");

            HttpResponseMessage res = Client.PostAsync("/Couleurs", content).Result;
            if (res.IsSuccessStatusCode)

            {

                return RedirectToAction(nameof(Index));

            }
            return View(couleur);
        }

        // GET: Couleurs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Couleur Couleur = new Couleur();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Couleurs" + Couleur.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Couleur = JsonConvert.DeserializeObject<Couleur>(results);
            }
            return View(Couleur);
        }

        // POST: Couleurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Code")] Couleur couleur)
        {
            if (id != couleur.Id)
            {
                return NotFound();
            }

            HttpClient Client = _api.Initial();
            string data = JsonConvert.SerializeObject(couleur);

            StringContent content = new StringContent(data, Encoding.UTF8, "Application/JSon");

            HttpResponseMessage res = Client.PutAsync("/Couleurs" + couleur.Id, content).Result;

            if (res.IsSuccessStatusCode)

            {

                return RedirectToAction(nameof(Index));

            }
            return View(couleur);
        }

        // GET: Couleurs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Couleur Couleur = new Couleur();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Couleurs" + Couleur.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Couleur = JsonConvert.DeserializeObject<Couleur>(results);
            }
            return View(Couleur);
        }

        // POST: Couleurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Couleur Couleur = new Couleur();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.DeleteAsync("/Couleurs" + Couleur.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool CouleurExists(int id)
        {
          return _context.Couleur.Any(e => e.Id == id);
        }
    }
}
