using Microsoft.AspNetCore.Mvc;
using Ponude.Api.Models;
using System;

namespace Ponude.Web.Controllers
{
    public class OfferOverviewController : Controller
    {
        private readonly ILogger<OfferEditorController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public OfferOverviewController(ILogger<OfferEditorController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            // page size = 3
            var response = await client.GetAsync($"api/offers?page={page}&pageSize=3");

            if (!response.IsSuccessStatusCode) return View("Error");

            var offers = await response.Content.ReadFromJsonAsync<List<Offer>>();
            return View(offers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync($"api/offers/{id}");

            if (!response.IsSuccessStatusCode) return View("Error");

            var offer = await response.Content.ReadFromJsonAsync<Offer>();
            return View(offer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.DeleteAsync($"api/offers/{id}");

            if (!response.IsSuccessStatusCode) return View("Error");

            return RedirectToAction(nameof(Index));
        }
    }
}
