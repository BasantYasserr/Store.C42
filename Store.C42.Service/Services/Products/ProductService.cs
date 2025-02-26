using AutoMapper;
using Store.C42.Core;
using Store.C42.Core.Dtos.Products;
using Store.C42.Core.Entities;
using Store.C42.Core.Helper;
using Store.C42.Core.Services.Contract;
using Store.C42.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.C42.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 


        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpecParams)
        {
            var spec = new ProductSpecifications(productSpecParams);
            var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec));

            var count = await _unitOfWork.Repository<Product, int>().GetCountAsync(new ProductWithCountSpecifications(productSpecParams));

            return new PaginationResponse<ProductDto>(productSpecParams.PageSize, productSpecParams.PageIndex, count, mappedProducts);
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductSpecifications(id);

            return _mapper.Map<ProductDto>(await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spec));
            //var product = await _unitOfWork.Repository<Product, int>().GetAsync(id);
            //var mappedProduct = _mapper.Map<ProductDto>(product);
            //return mappedProduct;
        } 

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
            => _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductType, int>().GetAllAsync());
        
        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());
        }

    }
}
