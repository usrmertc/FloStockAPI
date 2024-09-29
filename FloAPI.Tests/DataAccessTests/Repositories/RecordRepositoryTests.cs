using FloAPI.DataAccess.DataContexts;
using FloAPI.DataAccess.Repositories;
using FloAPI.Tests.DataContexts;
using Microsoft.EntityFrameworkCore;


namespace FloAPI.Tests.DataAccessTests.Repositories
{
    public class RecordRepositoryTests
    {
        private readonly FloApiDataContext _floApiDataContext;

        public RecordRepositoryTests()
        {
            _floApiDataContext = ContextBuilder.Build();
        }

        [Fact]
        private async Task GetRecordsShouldReturnRecordsAsList()
        {
            RecordRepository recordRepository = new RecordRepository(_floApiDataContext);

            List<FloAPI.DataAccess.Entities.Record> records = await recordRepository.GetRecords();

            Assert.NotNull(records);
            Assert.Equal(11, records.Count);
            Assert.Contains(records, r => r.Id == 1);
            Assert.Contains(records, r => r.Id == 2);
            Assert.Contains(records, r => r.Id == 3);
            Assert.Contains(records, r => r.Id == 4);
            Assert.Contains(records, r => r.Id == 5);
            Assert.Contains(records, r => r.Id == 6);
            Assert.Contains(records, r => r.Id == 7);
            Assert.Contains(records, r => r.Id == 8);
            Assert.Contains(records, r => r.Id == 9);
            Assert.Contains(records, r => r.Id == 10);
            Assert.Contains(records, r => r.Id == 11);
        }

        [Fact]
        private async Task GetRecordByMaterialShouldReturnRecordListByMaterialId()
        {
            RecordRepository recordRepository = new RecordRepository(_floApiDataContext);

            List<FloAPI.DataAccess.Entities.Record> records = await recordRepository.GetRecordsByMaterial(1);

            Assert.NotNull(records);
            Assert.Equal(4, records.Count);
            Assert.Contains(records, r => r.Id == 1);
            Assert.Contains(records, r => r.Id == 2);
            Assert.Contains(records, r => r.Id == 3);
            Assert.Contains(records, r => r.Id == 4);
            Assert.DoesNotContain(records, r => r.Id == 5);
            Assert.DoesNotContain(records, r => r.Id == 6);
            Assert.DoesNotContain(records, r => r.Id == 7);
            Assert.DoesNotContain(records, r => r.Id == 8);
            Assert.DoesNotContain(records, r => r.Id == 9);
            Assert.DoesNotContain(records, r => r.Id == 10);
            Assert.DoesNotContain(records, r => r.Id == 11);
        }

        [Fact]
        private async Task GetRecordByMaterialWithOperationTypeShouldReturnRecordListByMaterialIdAndOperationType()
        {
            RecordRepository recordRepository = new RecordRepository(_floApiDataContext);

            List<FloAPI.DataAccess.Entities.Record> records = await recordRepository.GetRecordsByMaterial(1, true);

            Assert.NotNull(records);
            Assert.Equal(2, records.Count);
            Assert.Contains(records, r => r.Id == 1);
            Assert.DoesNotContain(records, r => r.Id == 2);
            Assert.DoesNotContain(records, r => r.Id == 3);
            Assert.Contains(records, r => r.Id == 4);
            Assert.DoesNotContain(records, r => r.Id == 5);
            Assert.DoesNotContain(records, r => r.Id == 6);
            Assert.DoesNotContain(records, r => r.Id == 7);
            Assert.DoesNotContain(records, r => r.Id == 8);
            Assert.DoesNotContain(records, r => r.Id == 9);
            Assert.DoesNotContain(records, r => r.Id == 10);
            Assert.DoesNotContain(records, r => r.Id == 11);
        }

        [Fact]
        private async Task AddMethodShouldAddRecord()
        {
            RecordRepository recordRepository = new RecordRepository(_floApiDataContext);

            FloAPI.DataAccess.Entities.Record record = new FloAPI.DataAccess.Entities.Record 
            { 
                Count = 20, 
                OperationType = true,
                MaterialId = 5
            };

            await recordRepository.Add(record);

            var result = await _floApiDataContext.Records
                .Where(r => r.Count == 20)
                .Where(r => r.OperationType == true)
                .Where(r => r.MaterialId == 5)
                .FirstOrDefaultAsync();
            Assert.NotNull(result);
            Assert.Equal(record.Count, result.Count);
            Assert.Equal(record.OperationType, result.OperationType);
            Assert.Equal(record.MaterialId, result.MaterialId);
        }


    }
}
