using FloAPI.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloAPI.DataAccess.Interfaces.Repositories
{
    public interface IRecordRepository
    {
        public Task<List<Record>> GetRecords();
        public Task<List<Record>> GetRecordsByMaterial(int materialId);
        public Task Add(Record record);
    }
}
