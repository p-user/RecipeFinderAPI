﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Recipe.Queries.RecipeWithIngredientQuery
{
    public class GetRecipeWithIngredientQuery : BaseCommandQuery, IRequest<GetRecipeWithIngredientQueryResponse>
    {
    }
}
