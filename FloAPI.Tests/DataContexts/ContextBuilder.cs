using FloAPI.DataAccess.DataContexts;
using FloAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FloAPI.Tests.DataContexts
{
    public static class ContextBuilder
    {
        public static FloApiDataContext Build()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FloApiDataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging();

            var context = new FloApiDataContext(optionsBuilder.Options);

            context.Materials.AddRange(
                new Material { Id = 1, Name = "Leather", Count = 85, ThresholdLimit = 10 },
                new Material { Id = 2, Name = "Rubber", Count = 40, ThresholdLimit = 10 },
                new Material { Id = 3, Name = "Textiles", Count = 30, ThresholdLimit = 10 },
                new Material { Id = 4, Name = "Synthetics", Count = 35, ThresholdLimit = 10 },
                new Material { Id = 5, Name = "Foam", Count = 35, ThresholdLimit = 10 }
                );

            context.Barcodes.AddRange(
                new Barcode { Id = 1, Value = 1_000_000_000_000_000, NumberOfDecrease = 1, MaterialId = 1 },
                new Barcode { Id = 2, Value = 1_000_000_000_000_001, NumberOfDecrease = 1, MaterialId = 2 },
                new Barcode { Id = 3, Value = 1_000_000_000_000_002, NumberOfDecrease = 1, MaterialId = 3 },
                new Barcode { Id = 4, Value = 1_000_000_000_000_003, NumberOfDecrease = 1, MaterialId = 4 },
                new Barcode { Id = 5, Value = 1_000_000_000_000_004, NumberOfDecrease = 1, MaterialId = 5 },
                new Barcode { Id = 6, Value = 1_000_000_000_000_005, NumberOfDecrease = 5, MaterialId = 5 }
                );

            context.Records.AddRange(
                new DataAccess.Entities.Record { Id = 1, Count = 120, OperationType = true, MaterialId = 1 },
                new DataAccess.Entities.Record { Id = 2, Count = 30, OperationType = false, MaterialId = 1 },
                new DataAccess.Entities.Record { Id = 3, Count = 10, OperationType = false, MaterialId = 1 },
                new DataAccess.Entities.Record { Id = 4, Count = 5, OperationType = true, MaterialId = 1 },

                new DataAccess.Entities.Record { Id = 5, Count = 40, OperationType = true, MaterialId = 2 },

                new DataAccess.Entities.Record { Id = 6, Count = 55, OperationType = true, MaterialId = 3 },
                new DataAccess.Entities.Record { Id = 7, Count = 25, OperationType = false, MaterialId = 3 },

                new DataAccess.Entities.Record { Id = 8, Count = 55, OperationType = true, MaterialId = 4 },
                new DataAccess.Entities.Record { Id = 9, Count = 15, OperationType = true, MaterialId = 4 },
                new DataAccess.Entities.Record { Id = 10, Count = 25, OperationType = false, MaterialId = 4 },

                new DataAccess.Entities.Record { Id = 11, Count = 35, OperationType = true, MaterialId = 5 }
                );

            context.SaveChanges();

            return context;
        }

    }
}
