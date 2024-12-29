using Microsoft.AspNetCore.Mvc;
using Ponude.Api.Models;
using Ponude.Web.Models;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace Ponude.Web.Controllers
{
    public class OfferEditorController : Controller
    {
        private readonly ILogger<OfferEditorController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public OfferEditorController(ILogger<OfferEditorController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Offer());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Offer offer)
        {
            if (!ModelState.IsValid) return View(offer);

            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.PostAsJsonAsync("api/offers", offer);

            if (!response.IsSuccessStatusCode) return View("Error");

            return RedirectToAction("Index", "OfferOverview");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync($"api/offers/{id}");

            if (!response.IsSuccessStatusCode) return View("Error");

            var offer = await response.Content.ReadFromJsonAsync<Offer>();
            return View(offer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Offer offer)
        {
            if (!ModelState.IsValid) return View(offer);

            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.PutAsJsonAsync($"api/offers/{offer.Id}", offer);

            if (!response.IsSuccessStatusCode) return View("Error");

            return RedirectToAction("Index", "OfferOverview");
        }

        [HttpGet]
        public async Task<IActionResult> EditItem(int offerId, int itemId)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync($"api/offers/{offerId}/items/{itemId}");

            if (!response.IsSuccessStatusCode) return View("Error");

            var offer = await response.Content.ReadFromJsonAsync<Offer>();
            return View(offer);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
