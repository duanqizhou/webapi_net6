using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace webapi.Models.LIS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("SAM_JY")]
    public partial class SAM_JY
    {
           public SAM_JY(){


           }
           /// <summary>
           /// Desc:检验_id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string fjy_id {get;set;}

           /// <summary>
           /// Desc:检验日期
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string fjy_date {get;set;}

           /// <summary>
           /// Desc:仪器id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string fjy_instr {get;set;}

           /// <summary>
           /// Desc:检验样本条码号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string fjy_yb_code {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fjy_yb_tm {get;set;}

           /// <summary>
           /// Desc:检验样本类型id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fjy_yb_type {get;set;}

           /// <summary>
           /// Desc:检验费别id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fjy_sf_type {get;set;}

           /// <summary>
           /// Desc:检验费
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public double? fjy_f {get;set;}

           /// <summary>
           /// Desc:检验收费否
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fjy_f_ok {get;set;}

           /// <summary>
           /// Desc:检验状态
           /// Default:未审核
           /// Nullable:True
           /// </summary>           
           public string fjy_zt {get;set;}

           /// <summary>
           /// Desc:检验目的
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fjy_md {get;set;}

           /// <summary>
           /// Desc:检验临床诊断
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fjy_lczd {get;set;}

           /// <summary>
           /// Desc:患者id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_id {get;set;}

           /// <summary>
           /// Desc:患者类型_id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_type_id {get;set;}

           /// <summary>
           /// Desc:患者住院号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_zyh {get;set;}

           /// <summary>
           /// Desc:患者姓名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_name {get;set;}

           /// <summary>
           /// Desc:患者性别
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_sex {get;set;}

           /// <summary>
           /// Desc:患者年龄
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_age {get;set;}

           /// <summary>
           /// Desc:患者年龄单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_age_unit {get;set;}

           /// <summary>
           /// Desc:患者生日
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_age_date {get;set;}

           /// <summary>
           /// Desc:患者病区
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_bq {get;set;}

           /// <summary>
           /// Desc:患者科室
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_dept {get;set;}

           /// <summary>
           /// Desc:患者房间号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_room {get;set;}

           /// <summary>
           /// Desc:患者床号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_bed {get;set;}

           /// <summary>
           /// Desc:患者民族
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_volk {get;set;}

           /// <summary>
           /// Desc:患者婚姻
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_hy {get;set;}

           /// <summary>
           /// Desc:患者职业
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_job {get;set;}

           /// <summary>
           /// Desc:患者过敏史
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_gms {get;set;}

           /// <summary>
           /// Desc:患者住址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_add {get;set;}

           /// <summary>
           /// Desc:患者电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fhz_tel {get;set;}

           /// <summary>
           /// Desc:申请单id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fapply_id {get;set;}

           /// <summary>
           /// Desc:申请人/送检医师
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fapply_user_id {get;set;}

           /// <summary>
           /// Desc:申请科室_id/病人科室
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fapply_dept_id {get;set;}

           /// <summary>
           /// Desc:申请时间/送检时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fapply_time {get;set;}

           /// <summary>
           /// Desc:采样人_id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fsampling_user_id {get;set;}

           /// <summary>
           /// Desc:采样时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fsampling_time {get;set;}

           /// <summary>
           /// Desc:检验人_id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fjy_user_id {get;set;}

           /// <summary>
           /// Desc:审核医师_id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fchenk_user_id {get;set;}

           /// <summary>
           /// Desc:审核时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fcheck_time {get;set;}

           /// <summary>
           /// Desc:报告时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string freport_time {get;set;}

           /// <summary>
           /// Desc:打印时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fprint_time {get;set;}

           /// <summary>
           /// Desc:打印状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fprint_zt {get;set;}

           /// <summary>
           /// Desc:打印次数
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public int? fprint_count {get;set;}

           /// <summary>
           /// Desc:系统用户
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fsys_user {get;set;}

           /// <summary>
           /// Desc:系统时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fsys_time {get;set;}

           /// <summary>
           /// Desc:系统部门
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fsys_dept {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string fremark {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string f1 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string f2 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string f3 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string f4 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string f5 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string f6 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string f7 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string f8 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string f9 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string f10 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string checkid {get;set;}

    }
}
