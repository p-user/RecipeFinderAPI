using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MeasurementUnitsRepository : BaseRepository<MeasurementUnit>, IMeasurementUnitsRepository
    {
        public MeasurementUnitsRepository(ApiDbContext apiDbContext) : base(apiDbContext)
        {
        }

        public async Task<MeasurementUnit> FindBySymbol(string name) => await _entities.FirstOrDefaultAsync(s => s.Symbol == name);

    }
}
