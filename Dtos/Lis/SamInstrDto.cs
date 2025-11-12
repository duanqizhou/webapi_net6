using Mapster;

namespace webapi.Dtos.Lis
{
    public class SamInstrDto
    {
        [AdaptMember("finstr_id")]
        public string value { get; set; }
        [AdaptMember("fname")]
        public string label { get; set; }
        public bool disabled { get; set; } = false;
    }
}
