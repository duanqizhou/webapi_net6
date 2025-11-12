using Mapster;

namespace webapi.Dtos.Lis
{
    public class SamUrgentvaluepromptDto
    {
        public int ID { get; set; }
        public string finstr_id { get; set; }
        public string fitem_code { get; set; }
        public string fname { get; set; }
        public string UrgentValueName { get; set; }
        public string UrgentValueRef { get; set; }
    }
}
