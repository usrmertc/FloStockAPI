using FloAPI.DataAccess.DataContexts;
using FloAPI.DataAccess.Entities;
using FloAPI.DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FloAPI.DataAccess.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly FloApiDataContext _floApiDataContext;
        public RecordRepository(FloApiDataContext floApiDataContext)
        {
            _floApiDataContext = floApiDataContext;
        }

        public async Task Add(Record record)
        {
            await _floApiDataContext.Records.AddAsync(record);
            await _floApiDataContext.SaveChangesAsync();
        }

        public async Task<List<Record>> GetRecords()
        {
            return await _floApiDataContext.Records.ToListAsync();
        }

        public async Task<List<Record>> GetRecordsByMaterial(int materialId)
        {
            return await _floApiDataContext.Records.Where(r => r.MaterialId == materialId).ToListAsync();
        }

        public async Task<List<Record>> GetRecordsByMaterial(int materialId, bool operationType)
        {
            return await _floApiDataContext.Records
                .Where(r => r.MaterialId == materialId)
                .Where(r => r.OperationType == operationType)
                .ToListAsync();
        }
    }
}
