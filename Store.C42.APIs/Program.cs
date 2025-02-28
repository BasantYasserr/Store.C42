
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store.C42.APIs.Errors;
using Store.C42.APIs.Helper;
using Store.C42.APIs.Middlewares;
using Store.C42.Core;
using Store.C42.Core.Mapping.Products;
using Store.C42.Core.Services.Contract;
using Store.C42.Repository;
using Store.C42.Repository.Data;
using Store.C42.Repository.Data.Contexts;
using Store.C42.Service.Services.Products;

namespace Store.C42.APIs
{
    public class Program
    {
        //Entry Point
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDI(builder.Configuration);
            

            var app = builder.Build();


            await app.ConfigureMiddlewareAsync();

            app.Run();
        }
    }
}
