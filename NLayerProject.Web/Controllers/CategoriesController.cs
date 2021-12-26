using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.Web.ApiServices;
using NLayerProject.Web.DTOs;
using NLayerProject.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryApiService _categoryApiService;

        public CategoriesController(CategoryApiService categoryApiService)
        {

            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryApiService.GetAllAsync();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            await _categoryApiService.AddAsync(categoryDto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryApiService.GetByIdAsync(id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(CategoryDto categoryDto)
        {
           await _categoryApiService.Update(categoryDto);

            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
       public async Task<IActionResult> DeleteAsync(int id)
        {
            await _categoryApiService.Remove(id);

            return RedirectToAction("Index");
        } 

        //[ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Products(int id)
        {
            var products = await _categoryApiService.GetWithProductsByIdAsync(id);

            return View(products);
        }
    }
}
