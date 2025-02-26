using Store.C42.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.C42.Core.Specifications.Products
{
    public class ProductWithCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithCountSpecifications(ProductSpecParams productSpecParams) : base(P =>
                (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search))
                &&
                (!productSpecParams.BrandId.HasValue || productSpecParams.BrandId == P.BrandId)
                &&
                (!productSpecParams.TypeId.HasValue || productSpecParams.TypeId == P.TypeId) ) 
        { 
        }


    }
}
