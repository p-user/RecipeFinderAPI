﻿using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Commands.UpdateIngredientCommand
{
    public class UpdateIngredientResponse : BaseResponse
    {
        public Guid Id { get; set; }
    }
}
