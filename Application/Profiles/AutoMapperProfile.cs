using Application.Dtos;
using Application.Features.Recipe.Commands.CreateRecipeCommand;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RecipeDto, Recipe>()
                .ReverseMap();
            CreateMap<IngredientDto, Ingredient>()
                .ReverseMap();

            CreateMap<IngredientDto, RecipeIngredient>()
               .ReverseMap();

            CreateMap<RecipeIngredientBaseDto, RecipeIngredient>()
              .ReverseMap();

            CreateMap<RecipeWithIngredientsDto, Recipe>()
                .ForMember(s=>s.Ingredients, s=>s.MapFrom(s=>s.Ingredients))
                .ReverseMap();

        }
    }
}
