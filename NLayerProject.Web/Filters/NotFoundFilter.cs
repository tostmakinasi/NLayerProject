using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerProject.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLayerProject.Web.ApiServices;

namespace NLayerProject.Web.Filters
{
    public class NotFoundFilter: ActionFilterAttribute
    {
        private readonly CategoryApiService _categoryService;

        public NotFoundFilter(CategoryApiService categoryService)
        {
            _categoryService = categoryService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var category = await _categoryService.GetByIdAsync(id);

            if (category != null)
                await next();
            else
            {
                ErrorDto errorDto = new ErrorDto();

                errorDto.Status = 404;
                errorDto.Errors.Add($"id'si {id} olan kategori veritabanında bulunamadı.");

                context.Result = new RedirectToActionResult("Error","Home",errorDto);
            }
        }
    }
}
