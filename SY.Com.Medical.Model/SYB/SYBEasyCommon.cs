using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static SY.Com.Medical.Model.SYBCommon;

namespace SY.Com.Medical.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SYBEasyCommon<T> : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public long? zhid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string opter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string opter_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fixmedins_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fixmedins_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sign_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T input { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>    
    public class SYBCommonModel : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeId { get; set; }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SYBCommonModel<T> : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeId { get; set; }        
        /// <summary>
        /// 
        /// </summary>
        public T input { get; set; }
    }

    /// <summary>
    /// 解析报文-医保返回报文
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SYBCommonParseModel<T> : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 具体报文
        /// </summary>
        public OutCommon<T> Message { get; set; }
    }

    /// <summary>
    /// 解析报文-医保返回报文-无报文体的时候使用
    /// </summary>    
    public class SYBCommonParseModel : BaseModel
    {
        /// <summary>
        /// 操作人员ID
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OutCommon Message { get; set; }
    }


    /// <summary>
    /// 门诊就诊信息上传
    /// </summary>
    public class SYBMZUploadModel : BaseModel
    {
        /// <summary>
        /// 操作者Id
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 医生ID
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// 挂号ID
        /// </summary>
        public int RegisterId { get; set; }
        /// <summary>
        /// 诊断信息
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// 疾病名称,多个用逗号(,)隔开
        /// </summary>
        public string DiseaseName { get; set; }
        /// <summary>
        /// 疾病编码,多个用逗号(,)隔开
        /// </summary>
        public string DiseaseCode { get; set; }      

    }

    /// <summary>
    /// 门诊费用明细上传
    /// </summary>
    public class SYBMZ2204Model : BaseModel
    {
        /// <summary>
        /// 操作者Id
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 门诊Id
        /// </summary>
        public int OutpatientId { get; set; }
    }

    /// <summary>
    /// 门诊费用明细撤销
    /// </summary>
    public class SYBMZ2205Model:BaseModel
    {
        /// <summary>
        /// 操作者Id
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 门诊Id
        /// </summary>
        public int OutpatientId { get; set; }

    }


    /// <summary>
    /// 解析报文-医保返回报文
    /// </summary>    
    public class SYB2205ParseModel : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int OutpatientId { get; set; }
        /// <summary>
        /// 具体报文
        /// </summary>
        public OutCommon Message { get; set; }
    }

    /// <summary>
    /// 预结算获取报文入参
    /// </summary>
    public class SYJS2206Model:BaseModel
    {
        /// <summary>
        /// 操作员工Id
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 门诊id
        /// </summary>
        public int OutpatientId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Mdtrt_cert_type { get; set; }// 就诊凭证类型 字符型	3	Y Y
        /// <summary>
        /// 
        /// </summary>
        public string Mdtrt_cert_no { get; set; }// 就诊凭证编号  字符型	50			就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
    }

    /// <summary>
    /// 预结算出参
    /// </summary>
    public class SYJS2206OutModel
    {
        /// <summary>
        /// 实收
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balc { get; set; }
        /// <summary>
        /// 医保实收
        /// </summary>
        public decimal YBCost { get; set; }
        /// <summary>
        /// 自费实收
        /// </summary>
        public decimal CashCost { get; set; }
        /// <summary>
        /// 全自费金额
        /// </summary>
        public decimal fulamt_ownpay_amt { get; set; }
        /// <summary>
        /// 超限价自费费用
        /// </summary>
        public decimal overlmt_selfpay { get; set; }
        /// <summary>
        /// 先行自付金额
        /// </summary>
        public decimal preselfpay_amt { get; set; }
        /// <summary>
        /// 符合政策范围金额
        /// </summary>
        public decimal inscp_scp_amt { get; set; }
        /// <summary>
        /// 基本医疗保险统筹基金支出
        /// </summary>
        public decimal hifp_pay { get; set; }

    }

    /// <summary>
    /// 结算获取报文入参
    /// </summary>
    public class SYJS2207Model : BaseModel
    {
        /// <summary>
        /// 操作员工Id
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 门诊id
        /// </summary>
        public int OutpatientId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Mdtrt_cert_type { get; set; }// 就诊凭证类型 字符型	3	Y Y
        /// <summary>
        /// 
        /// </summary>
        public string Mdtrt_cert_no { get; set; }// 就诊凭证编号  字符型	50			就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
        /// <summary>
        /// 全自费金额
        /// </summary>
        public decimal fulamt_ownpay_amt { get; set; }
        /// <summary>
        /// 超限价金额
        /// </summary>
        public decimal overlmt_selfpay { get; set; }
        /// <summary>
        /// 先行自付金额
        /// </summary>
        public decimal preselfpay_amt { get; set; }
        /// <summary>
        /// 符合政策范围金额
        /// </summary>
        public decimal inscp_scp_amt { get; set; }

    }

    /// <summary>
    /// 预结算出参
    /// </summary>
    public class SYJS2207OutModel
    {
        /// <summary>
        /// 结算ID
        /// </summary>
        public string setl_id { get; set; }
        /// <summary>
        /// 实收
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balc { get; set; }
        /// <summary>
        /// 医保实收
        /// </summary>
        public decimal YBCost { get; set; }
        /// <summary>
        /// 自费实收
        /// </summary>
        public decimal CashCost { get; set; }
        /// <summary>
        /// 基本医疗保险统筹基金支出
        /// </summary>
        public decimal hifp_pay { get; set; }



    }

    /// <summary>
    /// 
    /// </summary>    
    public class SYB2208Model : BaseModel
    {
        /// <summary>
        /// 操作人员Id
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 门诊ID
        /// </summary>
        public int OutpatientId { get; set; }
    }

    /// <summary>
    /// MZ4203自费病人就诊以及费用明细上传完成
    /// </summary>
    public class SYB4203Model : BaseModel
    {
        /// <summary>
        /// 操作员工Id
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 门诊id
        /// </summary>
        public int OutpatientId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Cplt_flag { get; set; }
    }

    /// <summary>
    /// MZ4025自费病人门诊就医信息上传
    /// </summary>
    public class SYB4025Model : BaseModel
    {
        /// <summary>
        /// 操作者Id
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 医生ID
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// 挂号ID
        /// </summary>
        public int RegisterId { get; set; }
        /// <summary>
        /// 诊断信息
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// 疾病名称,多个用逗号(,)隔开
        /// </summary>
        public string DiseaseName { get; set; }
        /// <summary>
        /// 疾病编码,多个用逗号(,)隔开
        /// </summary>
        public string DiseaseCode { get; set; }
        /// <summary>
        /// 门诊Id
        /// </summary>
        public int OutpatientId { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string Invoice_no { get; set; }
    }

    


}
