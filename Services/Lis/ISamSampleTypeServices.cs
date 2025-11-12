using System.Linq.Expressions;
using webapi.Models.LIS;

namespace webapi.Services
{
    public interface ISamSampleTypeServices
    {
        public List<SAM_SAMPLE_TYPE> GetAll();
        public Task<List<SAM_SAMPLE_TYPE>> GetListExpressionAsync(Expression<Func<SAM_SAMPLE_TYPE, bool>> predicate);
    }
}