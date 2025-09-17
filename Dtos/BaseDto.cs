using System;

namespace webapi.Models
{
    #region 三大库
    #region 药品
    public class DictLibListDto
    {
        public int CurrentPage { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? cname { get; set; }
        public string? ccode { get; set; }
        public string? ClassifyCode { get; set; }

    }

    public class DictLibDto
    {
        public string CODE { get; set; } = null!;
        public string CNAME { get; set; } = null!;
        public string? SPEC { get; set; }
        public string? BIGUNIT { get; set; }
        public string? SMALLUNIT { get; set; }

        public int? PACKRULE { get; set; }
        public decimal? PRICE { get; set; }
        public string? GuoJiaBianMa { get; set; }
        public int? SIGNTYPE { get; set; }
        public string? ClassifyCode { get; set; }

        public int? FACTORY { get; set; }
        public string? FILENO { get; set; }
        public string? CustomCode { get; set; }
        public DateTime? RequireDate { get; set; }
        public decimal? Dosage { get; set; }
        public string? DosageUnit { get; set; }

    }
    #endregion

    #region 医疗材料
    public class MatLibDto
    {
        public int CurrentPage { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? name { get; set; }
        public string? code { get; set; }
    }
    #endregion

    #region 医疗服务
    public class CureLibDto
    {
        public int CurrentPage { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? name { get; set; }
        public string? code { get; set; }
    }
    #endregion

    #endregion


}
