using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SysMaTra.Models;
using SysMaTra.Models.ApiServices;

namespace SysMaTra.Controllers
{
    public class BusesController : Controller
    {
        private readonly SysMaTraContext _context;
        VoyageApi _api = new VoyageApi();
        public BusesController(SysMaTraContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
            List<Bus> Buses = new List<Bus>();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Buses");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Buses = JsonConvert.DeserializeObject<List<Bus>>(results);
            }
            return View(Buses);
        }

        // GET: Buses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bus == null)
            {
                return NotFound();
            }

            Bus Bus = new Bus();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Buses" + Bus.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Bus = JsonConvert.DeserializeObject<Bus>(results);
            }

            return View(Bus);
        }

        // GET: Buses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Matricule,PlaceNum,Classe,Statut,Created")] Bus bus)
        {

            HttpClient Client = _api.Initial();
            string data = JsonConvert.SerializeObject(bus);

            StringContent content = new StringContent(data, Encoding.UTF8, "Application/JSon");

            HttpResponseMessage res = Client.PostAsync("/Buses", content).Result;

            if (res.IsSuccessStatusCode)

            {

                return RedirectToAction(nameof(Index));

            }
            return View(bus);
        }

        // GET: Buses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bus == null)
            {
                return NotFound();
            }

            Bus bus = new Bus();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Buses" + bus.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                bus = JsonConvert.DeserializeObject<Bus>(results);
            }
            return View(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matricule,PlaceNum,Classe,Statut,Created")] Bus bus)
        {
            if (id != bus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpClient Client = _api.Initial();
                string data = JsonConvert.SerializeObject(bus);

                StringContent content = new StringContent(data, Encoding.UTF8, "Application/JSon");

                HttpResponseMessage res = Client.PutAsync("/Buses" + bus.Id, content).Result;

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(bus);
        }

        // GET: Buses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bus == null)
            {
                return NotFound();
            }

            Bus bus = new Bus();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.GetAsync("/Buses" + bus.Id);
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                bus = JsonConvert.DeserializeObject<Bus>(results);
            }

            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bus == null)
            {
                return Problem("Entity set 'SysMaTraContext.Bus'  is null.");
            }
            Bus bus = new   Bus();
            HttpClient Client = _api.Initial();
            HttpResponseMessage res = await Client.DeleteAsync("/Buses" + bus.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool BusExists(int id)
        {
          return _context.Bus.Any(e => e.Id == id);
        }
    }
}
