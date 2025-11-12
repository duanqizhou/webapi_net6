using System;

namespace webapi.Dtos
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
    public class MatLibListDto
    {
        public int CurrentPage { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? name { get; set; }
        public string? code { get; set; }
    }
    public class MatLibDto
    {
        public int ID { get; set; }
        public string SystemCode { get; set; }
        public int? BigType { get; set; }
        public string? SmallType { get; set; }
        public string Name { get; set; } = null!;
        public string? Spec { get; set; }
        public string? Unit { get; set; }
        public decimal? Price { get; set; }
        public string? DosageType { get; set; }
        public string? IncomeType { get; set; }
        public string? Requestor { get; set; }
        public DateTime? RequireDate { get; set; }
        public string? ApproveNotes { get; set; }
        public string? SFBM { get; set; }
        public string? shangchanhcangjia { get; set; }
        public string? ZFLX { get; set; }
        public string? Beizhu { get; set; }
    }
    #endregion

    #region 医疗服务
    public class CureLibListDto
    {
        public int CurrentPage { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? name { get; set; }
        public string? code { get; set; }
    }
    public class CureLibDto
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string? Spec { get; set; }
        public string? Unit { get; set; }
        public decimal? Price { get; set; }
        public string? PriceCode { get; set; }
        public string? CustomCode { get; set; }
        public string? ClassifyCode { get; set; }
        public string? Requestor { get; set; }
        public DateTime? RequireDate { get; set; }
        public string? Beizhu { get; set; }
        public string? Code { get; set; }
        public string? GuoJiaBianMa { get; set; }
        public string? ExecDept { get; set; }
    }

    #endregion

    #endregion


}
