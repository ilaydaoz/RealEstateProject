using DTO.DTOs.AdvertDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace RealEstateProject.Controllers
{
    public class AdvertController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdvertController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateAdvertDTO createAdvertDTO)
        {
            var username= HttpContext.Session.GetString("Username");
            var client = _httpClientFactory.CreateClient();
            createAdvertDTO.Date=DateTime.Now;
            createAdvertDTO.UserName = username;
            createAdvertDTO.ImageUrl2 = "bos";
            var JsonData = JsonConvert.SerializeObject(createAdvertDTO);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7132/api/Advert", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }    

        public async Task<IActionResult> DeleteAdvert(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7132/api/Advert/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Default");
            }
            return View();
        }
    }
}
