using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService _advertService;

        public AdvertController(IAdvertService advert)
        {
            _advertService = advert;
        }

        [HttpGet]
        public IActionResult AdvertList()
        {
            var values = _advertService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddAdvert(Advert advert)
        {
            _advertService.TInsert(advert);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdvert(string id)
        {
            var value = _advertService.TGetByID(id);
            _advertService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetAdvert(string id)
        {
            var value = _advertService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateAdvert(Advert advert)
        {
            _advertService.TUpdate(advert);
            return Ok();
        }
    }
}
