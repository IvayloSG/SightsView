namespace SightsView.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SightsView.Web.ViewModels;

    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var image = await System.IO.File.ReadAllBytesAsync(@"C:\Users\Ivaylo\OneDrive\Desktop\Test\DSCN2113.jpg");
            string imageBase64Data = Convert.ToBase64String(image);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

            this.ViewData["byte"] = imageDataURL;
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
