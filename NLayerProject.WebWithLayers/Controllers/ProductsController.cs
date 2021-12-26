using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayerProject.Core.Models;
using NLayerProject.Core.Sevices;
using NLayerProject.Web.DTOs;
using NLayerProject.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;

            _mapper = mapper;

            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllWithCategoryAsync();

            return View(_mapper.Map<IEnumerable<ProductWithCategoryDto>>(products));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();

            ViewBag.Categories = new SelectList(_mapper.Map<IEnumerable<CategoryDto>>(categories), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            await _productService.AddAsync(_mapper.Map<Product>(productDto));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(_mapper.Map<IEnumerable<CategoryDto>>(categories), "Id", "Name");

            return View(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public IActionResult Update(ProductDto productDto)
        {
            _productService.Update(_mapper.Map<Product>(productDto));

            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;

            _productService.Remove(product);

            return RedirectToAction("Index");
        }
    }
}
