using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Services
{
    public class ApiLogService
    {
        private readonly BoyutCaseContext _db;

        public ApiLogService(BoyutCaseContext db)
        {
            _db = db;
        }

        public async Task Log(SysApiLogging apiLogItem)
        {
            try
            {
                apiLogItem.Id = Guid.NewGuid();
                _db.SysApiLogging.Add(apiLogItem);
                await _db.SaveChangesAsync();

            }
            catch (Exception)
            {

            }

        }
    }
}
