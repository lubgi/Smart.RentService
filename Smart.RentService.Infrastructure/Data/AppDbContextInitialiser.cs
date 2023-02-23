using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Smart.RentService.Core.Entities;

namespace Smart.RentService.Infrastructure.Data
{
    public class AppDbContextInitialiser
    {
        private readonly ILogger<AppDbContextInitialiser> _logger;
        private readonly AppDbContext _context;

        public AppDbContextInitialiser(ILogger<AppDbContextInitialiser> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_context.Premises.Any())
            {
                _context.Premises.AddRange(new List<Premise>()
                {
                    new(){Area = 10, Code = Guid.NewGuid(), Name = "FirstPremise"},
                    new(){Area = 5,  Code = Guid.NewGuid(), Name = "SecondPremise"},
                    new(){Area = 25, Code = Guid.NewGuid(), Name = "ThirdPremise"},
                    new(){Area = 40, Code = Guid.NewGuid(), Name = "FourthPremise"},
                    new(){Area = 34, Code = Guid.NewGuid(), Name = "FifthPremise"},
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.Equipments.Any())
            {
                _context.Equipments.AddRange(new List<Equipment>()
                {
                    new() { Area = 1,   Code = Guid.NewGuid(), Name = "FirstEquipment" },
                    new() { Area = 2,   Code = Guid.NewGuid(), Name = "SecondEquipment" },
                    new() { Area = 1.5, Code = Guid.NewGuid(), Name = "ThirdEquipment" },
                    new() { Area = 0.5, Code = Guid.NewGuid(), Name = "FourthEquipment" },
                    new() { Area = 0.3, Code = Guid.NewGuid(), Name = "FifthEquipment" },
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.Contracts.Any())
            {
                _context.Contracts.AddRange(new List<Contract>()
                {
                    new() { PremiseId = 1, EquipmentId = 1, EquipmentCount = 9 },
                    new() { PremiseId = 2, EquipmentId = 2, EquipmentCount = 2},
                    new() { PremiseId = 3, EquipmentId = 3, EquipmentCount = 16}
                });

                await _context.SaveChangesAsync();
            }
        }
    }

}
