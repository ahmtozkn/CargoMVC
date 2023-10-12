using Cargo.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppSettings _appSettings;
        public HomeController(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public IActionResult Index(CargoViewModel? cargoViewModel)
        {
            ViewBag.CargoViewModel = cargoViewModel;

            return View();
        }

        public async Task<IActionResult> Search(CargoSearchViewModel cargoSearchViewModel)
        {
            var getAllUrl=$"{_appSettings.ApiBaseUrl}/cargos";
            using var client=new HttpClient();
            var response=await client.GetFromJsonAsync<List<CargoViewModel>>(getAllUrl);
           
            var searchResponse=response.FirstOrDefault(x=>x.CargoNo==cargoSearchViewModel.CargoNo);


            return  RedirectToAction("Index","Home",searchResponse);
        }
    }
}
