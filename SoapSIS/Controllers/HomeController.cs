using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoapSIS.Models;
using SIS;
using System.Diagnostics;

namespace SoapSIS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                Service1Soap soapServiceChannel = new Service1SoapClient(Service1SoapClient.EndpointConfiguration.Service1Soap);
                var respuesta = await soapServiceChannel.GetSessionAsync(new GetSessionRequest()
                {
                    Body = new GetSessionRequestBody()
                    {
                        strUsuario = "00003543",
                        strClave = "123456"
                    }
                });

                var viewModel = new IndexViewModel
                {
                    GetSessionResult = respuesta.Body.GetSessionResult
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
