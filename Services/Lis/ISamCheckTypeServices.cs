using System.Linq.Expressions;
using webapi.Models.LIS;

namespace webapi.Services
{
    public interface ISamCheckTypeServices
    {
        public List<SAM_CHECK_TYPE> GetAll();
        public Task<List<SAM_CHECK_TYPE>> GetListExpressionAsync(Expression<Func<SAM_CHECK_TYPE, bool>> predicate);
    }
}