using webapi.Dtos.Lis;
using webapi.Models.LIS;

namespace webapi.Services
{
    public interface ISamUrgentvaluepromptServices
    {
        public List<SAM_UrgentValuePrompt> GetAll();
        public bool InsertOrUpdate(SamUrgentvaluepromptDto samUrgentvaluepromptDto);

    }
}