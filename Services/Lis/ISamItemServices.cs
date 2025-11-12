using System.Linq.Expressions;
using webapi.Models.LIS;

namespace webapi.Services
{
    public interface ISamItemServices
    {
        public List<SAM_ITEM> GetAll();
        public Task<List<SAM_ITEM>> GetListExpressionAsync(Expression<Func<SAM_ITEM, bool>> predicate);

    }
}