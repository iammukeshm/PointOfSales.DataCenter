using CoreLibrary.Helpers;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Features.Account.Commands;
using PointOfSales.DataCenter.Application.Features.Account.ViewModels;
using PointOfSales.DataCenter.Application.Features.ProductFeatures.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PointOfSales.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Stopwatch a = new Stopwatch();
            a.Start();
            var data = new LoginUserCommand { Email = "iammukeshm@gmail.com", Password = "Pa$$w0rd." };
            ApiHelper api = new ApiHelper("https://localhost:44348/");
            var result = await api.PostAsync<Result<LoginUserViewModel>>("api/Account/login", data);

            var accessToken = result.Data.Token;

            api.AddJwtAuthorization(accessToken);
            //var allProducts = await apiAuth.GetAsync<Result<IEnumerable<ProductViewModel>>>("api/v1/Product");
            var productOne = await api.GetAsync<Result<ProductViewModel>>("api/v1/Product/1");

            var productTwo = await api.GetAsync<Result<ProductViewModel>>("api/v1/Product/2");
            api.RemoveJwtAuthorization();
            var productThree = await api.GetAsync<Result<ProductViewModel>>("api/v1/Product/3");

            await api.GetAsync<Result<ProductViewModel>>("api/v1/Product/3");

            a.Stop();
            Console.WriteLine(a.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}
