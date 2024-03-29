using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using API.Dtos;
using AutoMapper;
using API.Helpers;
using API.Errors;

namespace API.Controllers
{
    
    public class ProductsController :BaseApiController
    {
    //    private readonly IProductRepository _repo;
        
    //    public ProductsController(IProductRepository repo) 
    //    {
    //         _repo = repo;
        
    //    }
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
         private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,IGenericRepository<ProductBrand> productBrandRepo
    ,IGenericRepository<ProductType> productTypeRepo,IMapper mapper)
    {
            _productTypeRepo = productTypeRepo;
         
            _productBrandRepo = productBrandRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        
    }
        
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>>  GetProducts([FromQuery]ProductSpecParams productParams)
        {
            // var products = await _repo.GetProductsAsync();
           // var products = await _productRepo.ListAllAsync();

            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var totalItem = await _productRepo.CountAsync(countSpec);
            var products = await _productRepo.ListAsync(spec);
            // return Ok(products);
            var data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize,totalItem,data));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            // return await _repo.GetProductByIdAsync(id);

            
            // return await _productRepo.GetByIdAsync(id);
             var spec = new ProductsWithTypesAndBrandsSpecification(id);
            //   return await _productRepo.GetEntityWithSpec(spec);


             var product= await _productRepo.GetEntityWithSpec(spec);
             


            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            

              return _mapper.Map<Product, ProductToReturnDto>(product);

        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>>  GetProductBrands()
        {
            // return Ok( await _repo.GetProductBrandsAsync());
            return Ok( await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>>  GetProductTypes()
        {
            // return Ok( await _repo.GetProductTypesAsync());
            return Ok( await _productTypeRepo.ListAllAsync());
        }
        
    }
}