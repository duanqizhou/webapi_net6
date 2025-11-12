using System.Linq.Expressions;
using webapi.Models.LIS;

namespace webapi.Services
{
    public interface ISamTypeServices
    {
        public List<SAM_TYPE> GetAll();
        public Task<List<SAM_TYPE>> GetListExpressionAsync(List<string> types);
    }
}