using DTO.DTOs.AdvertDTOs;
using DTO.DTOs.CommentDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RealEstateProject.Controllers
{

    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string status,string adress,int price)
        {
            ViewBag.username= HttpContext.Session.GetString("Username");
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/Comment");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDTO>>(jsonData);
                ViewBag.Comments = values.ToList();
            }          
            var responseMessage2 = await client.GetAsync("https://localhost:7132/api/Advert");
            if (responseMessage2.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage2.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAdvertDTO>>(jsonData);
                if (!string.IsNullOrEmpty(adress) && !string.IsNullOrEmpty(status))
                {
                    values=values.Where(x => x.Status == status && status != "" && x.Adress.Contains(adress)).ToList();
                }
                else if (!string.IsNullOrEmpty(status))
                {
                   values= values.Where(x => x.Status == status).ToList();
                }
                if (!string.IsNullOrEmpty(adress))
                {
                    values=values.Where(x => x.Adress.Contains(adress)).ToList();
                }
                if (price > 0)
                {
                    values=values.Where(x => x.Price == price).ToList();
                }
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> AdvertList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/Advert");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAdvertListDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> AdvertDetails(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/Advert");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAdvertListDTO>>(jsonData);
                var result = values.FirstOrDefault(x=>x.Id==id);              
                if (result != null)
                {                  
                    return View(result);
                }
            }
            return View();

        }

        public async void CommentList()
        {
            
        }

       
    }
}
