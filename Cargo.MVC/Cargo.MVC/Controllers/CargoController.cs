using Cargo.MVC.Enums;
using Cargo.MVC.Extensions;
using Cargo.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Cargo.MVC.Controllers
{
    public class CargoController : Controller
    { 
        private readonly AppSettings _appSettings;
        public CargoController(AppSettings appSettings)
        {
                _appSettings = appSettings;
        }

        public async Task< IActionResult> Index(LoginUserViewModel loginUserViewModel)
        {
            var getAllUrl=$"{_appSettings.ApiBaseUrl}/cargos";
            using var client=new HttpClient();
            var response=await client.GetFromJsonAsync<List<CargoViewModel>>(getAllUrl);
            



            return View(response);
        }

        public IActionResult New()
        {
            var paymentEnumList = Enum.GetValues(typeof(PaymentEnum)) .Cast<PaymentEnum>() .Select(e => new SelectListItem
            {
              Text = e.ToString(),
              Value = ((int)e).ToString()
            }).ToList();
            var statusEnumList = Enum.GetValues(typeof(CargoStatusEnum)).Cast<CargoStatusEnum>().Select(e => new SelectListItem
            {
                Text = e.ToString(),
                Value = ((int)e).ToString()
            }).ToList();


            ViewBag.StatusOptions = new SelectList(statusEnumList, "Value", "Text");
            ViewBag.PaymentOptions = new SelectList(paymentEnumList, "Value", "Text");

            return View("Form");

        }

        public async Task <IActionResult> Edit(int id)
        {
            var getUrl =$"{_appSettings.ApiBaseUrl}/cargos/{id}";
            using var client=new HttpClient();
            var response = await client.GetFromJsonAsync<CargoViewModel>(getUrl);

            var paymentEnumList = Enum.GetValues(typeof(PaymentEnum)).Cast<PaymentEnum>().Select(e => new SelectListItem
            {
                Text = e.ToString(),
                Value = ((int)e).ToString()
            }).ToList();
            var statusEnumList = Enum.GetValues(typeof(CargoStatusEnum)).Cast<CargoStatusEnum>().Select(e => new SelectListItem
            {
                Text = e.ToString(),
                Value = ((int)e).ToString()
            }).ToList();


            ViewBag.StatusOptions = new SelectList(statusEnumList, "Value", "Text");
            ViewBag.PaymentOptions = new SelectList(paymentEnumList, "Value", "Text");

            return View("Form",response);



        }
        [HttpPost]
        public async Task<IActionResult> Add(CargoViewModel cargoViewModel) 
        {
            if(cargoViewModel.RecieverTelNo.Length!=10&& cargoViewModel.SenderTelNo.Length!=10&&cargoViewModel.RecieverTcNo.Length!=11) 
            {
                return View("Form", cargoViewModel);
            }

            if(cargoViewModel.Id== 0)
            { 
                var insertUrl=$"{_appSettings.ApiBaseUrl}/cargos";
            using var client=new HttpClient();
            var response=await client.PostAsJsonAsync(insertUrl,cargoViewModel);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Cargo");
            }
            else 
            {
                var paymentEnumList = Enum.GetValues(typeof(PaymentEnum)).Cast<PaymentEnum>().Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString()
                }).ToList();
                var statusEnumList = Enum.GetValues(typeof(CargoStatusEnum)).Cast<CargoStatusEnum>().Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString()
                }).ToList();


                ViewBag.StatusOptions = new SelectList(statusEnumList, "Value", "Text");
                ViewBag.PaymentOptions = new SelectList(paymentEnumList, "Value", "Text");


                return View("Form",cargoViewModel);
            }


           }

           else
            {
                var updateUrl = $"{_appSettings.ApiBaseUrl}/cargos/{cargoViewModel.Id}";
                using var client=new HttpClient();
                var response=await client.PutAsJsonAsync(updateUrl,cargoViewModel);
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Cargo");
                }
                else
                {
                    var paymentEnumList = Enum.GetValues(typeof(PaymentEnum)).Cast<PaymentEnum>().Select(e => new SelectListItem
                    {
                        Text = e.ToString(),
                        Value = ((int)e).ToString()
                    }).ToList();
                    var statusEnumList = Enum.GetValues(typeof(CargoStatusEnum)).Cast<CargoStatusEnum>().Select(e => new SelectListItem
                    {
                        Text = e.ToString(),
                        Value = ((int)e).ToString()
                    }).ToList();


                    ViewBag.StatusOptions = new SelectList(statusEnumList, "Value", "Text");
                    ViewBag.PaymentOptions = new SelectList(paymentEnumList, "Value", "Text");

                    return View("Form", cargoViewModel);
                }

            } 
        }


        public async Task<IActionResult> Delete(int id)
        {
            var deleteUrl=$"{_appSettings.ApiBaseUrl}/cargos/{id}";
            using var client=new HttpClient();
            var response = await client.DeleteAsync(deleteUrl);
            return RedirectToAction("Index", "Cargo");
        }
    }
}
