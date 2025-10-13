using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAsyncRepo<CLASS, ID, RET>
    {
        Task<List<CLASS>> GetAllAsync();
        Task<CLASS> GetByIdAsync(ID id);
        Task<CLASS> CreateAsync(CLASS entity);
        Task<CLASS> UpdateAsync(CLASS entity);
        Task<bool> DeleteAsync(ID id);
    }
}
