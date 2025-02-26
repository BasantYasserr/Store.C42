using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.C42.Core.Dtos.Products;
using Store.C42.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.C42.Core.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductDto>()     // from Product to ProductDto
                .ForMember(d => d.BrandName,options => options.MapFrom(s => s.Brand.Name))  //destination property => BrandName ,  source property => Brand.Name
                .ForMember(d => d.TypeName, options => options.MapFrom(s => s.Type.Name))
                //.ForMember(d => d.PictureUrl, options => options.MapFrom(s => $"{configuration["BASEURL"]}{s.PictureUrl}"))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(new PictureUrlResolver(configuration)))
                ;
            
            CreateMap<ProductBrand, TypeBrandDto>();
            CreateMap<ProductType, TypeBrandDto>();
        }
    }
}