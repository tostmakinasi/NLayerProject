﻿using AutoMapper;
using NLayerProject.API.DTOs;
using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Mapping
{
    public class MapProfile:Profile
    {

        public MapProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Category, CategoryWithProductsDto>();
            CreateMap<CategoryWithProductsDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<ProductWithCategoryDto, Product>();

            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();
        }
    }
}
