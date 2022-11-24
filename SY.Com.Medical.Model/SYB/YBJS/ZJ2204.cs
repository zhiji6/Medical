using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZSI_Smart.Model.SYB
{
    /// <summary>
    /// 
    /// </summary>
    public class ZJ2204
    {
        /// <summary>
        /// 
        /// </summary>
        public class In2204
        {
            /// <summary>
            /// 
            /// </summary>
            public List<FeeDetail> feedetail { get; set; }        

        }
        /// <summary>
        /// 
        /// </summary>
        
        public class FeeDetail
        {
            /// <summary>
            /// 
            /// </summary>
            
            public string feedetl_sn { get; set; }// 费用明细流水号 字符型	30	　	Y 单次就诊内唯一
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_id { get; set; }// 就诊ID    字符型	30		Y
            /// <summary>
            /// 
            /// </summary>
            public string psn_no { get; set; }//  人员编号 字符型	30	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string chrg_bchno { get; set; }//  收费批次号 字符型	30		Y 同一收费批次号病种编号必须一致
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string dise_codg { get; set; }// 病种编码  按照标准编码填写：按病种结算病种目录代码(bydise_setl_list_code)、门诊慢特病病种目录代码(opsp_dise_cod)
            /// <summary>
            /// 
            /// </summary>
            public string rxno { get; set; }// 处方号 字符型	30	　		外购处方时，传入外购处方的处方号；非外购处方，传入医药机构处方号
            /// <summary>
            /// 
            /// </summary>
            public string rx_circ_flag { get; set; } //   外购处方标志 字符型	3	Y Y
            /// <summary>
            /// 
            /// </summary>
            public string fee_ocur_time { get; set; }// 费用发生时间  日期时间型 Y   yyyy-MM-dd HH:mm:ss
            /// <summary>
            /// 
            /// </summary>            
            public string med_list_codg { get; set; } //  医疗目录编码 字符型	50	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string medins_list_codg { get; set; }//    医药机构目录编码 字符型	150	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal det_item_fee_sumamt { get; set; }// 明细项目费用总额 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public double cnt { get; set; } //数量 数值型	16,4	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal pric { get; set; }// 单价 数值型	16,6	　	Y
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string sin_dos_dscr { get; set; }// 单次剂量描述 字符型	200		
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string used_frqu_dscr { get; set; }// 使用频次描述  字符型	200			
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int prd_days { get; set; }// 周期天数    数值型	4,2			
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string medc_way_dscr { get; set; }// 用药途径描述  字符型	200			
            /// <summary>
            /// 
            /// </summary>
            public string bilg_dept_codg { get; set; }// 开单科室编码  字符型	30	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string bilg_dept_name { get; set; } // 开单科室名称 字符型	100	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string bilg_dr_codg { get; set; }//    开单医生编码 字符型	30	　	Y 按照标准编码填写
            /// <summary>
            /// 
            /// </summary>
            public string bilg_dr_name { get; set; }// 开单医师姓名  字符型	50	　	Y
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string acord_dept_codg { get; set; }// 受单科室编码 字符型	30	　	　	
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string acord_dept_name { get; set; }// 受单科室名称  字符型	100	　	　	　
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string orders_dr_code { get; set; }// 受单医生编码  字符型	30	　	　	按照标准编码填写
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string orders_dr_name { get; set; }//  受单医生姓名 字符型	50	　	　	　
            /// <summary>
            /// 
            /// </summary>
            public string hosp_appr_flag { get; set; }// 医院审批标志  字符型	3	Y Y
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string tcmdrug_used_way { get; set; }// 中药使用方式  字符型	6	Y
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string etip_flag { get; set; }// 外检标志 字符型	3	Y
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string etip_hosp_code { get; set; }//  外检医院编码 字符型	30	　	　	按照标准编码填写
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string dscg_tkdrug_flag { get; set; }//    出院带药标志 字符型	3	Y
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string matn_fee_flag { get; set; }//  生育费用标志 字符型	6	Y
        }
        /// <summary>
        /// 
        /// </summary>
        public class Out2204
        {
            /// <summary>
            /// 
            /// </summary>
            public Result result { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        public class Result 
        {
            /// <summary>
            /// 
            /// </summary>
            public string feedetl_sn { get; set; }// 费用明细流水号 字符型	30	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal det_item_fee_sumamt { get; set; } //明细项目费用总额 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public double cnt { get; set; }//数量 数值型	16,4	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal pric { get; set; }//   单价 数值型	16,6	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal pric_uplmt_amt { get; set; }//  定价上限金额 数值型	16,6	　	Y
            /// <summary>
            /// 
            /// </summary>
            public double selfpay_prop { get; set; }//    自付比例 数值型	5,4	　	　
            /// <summary>
            /// 
            /// </summary>
            public decimal fulamt_ownpay_amt { get; set; }// 全自费金额   数值型	16,2	　	　	　
            /// <summary>
            /// 
            /// </summary>
            public decimal overlmt_amt { get; set; }// 超限价金额   数值型	16,2	　	　	　
            /// <summary>
            /// 
            /// </summary>
            public decimal preselfpay_amt { get; set; }// 先行自付金额  数值型	16,2	　	　	　
            /// <summary>
            /// 
            /// </summary>
            public decimal inscp_scp_amt { get; set; }// 符合政策范围金额    数值型	16,2	　	　	　
            /// <summary>
            /// 
            /// </summary>
            public string chrgitm_lv { get; set; } //收费项目等级  字符型	3	Y Y
            /// <summary>
            /// 
            /// </summary>
            public string med_chrgitm_type { get; set; }// 医疗收费项目类别    字符型	6	Y Y
            /// <summary>
            /// 
            /// </summary>
            public string bas_medn_flag { get; set; }// 基本药物标志  字符型	3	Y
            /// <summary>
            /// 
            /// </summary>
            public string hi_nego_drug_flag { get; set; }//   医保谈判药品标志 字符型	3	Y
            /// <summary>
            /// 
            /// </summary>
            public string chld_medc_flag { get; set; }//  儿童用药标志 字符型	3	Y
            /// <summary>
            /// 
            /// </summary>
            public string list_sp_item_flag { get; set; }//   目录特项标志 字符型	3	Y 特检特治项目或特殊药品
            /// <summary>
            /// 
            /// </summary>
            public string lmt_used_flag { get; set; }// 限制使用标志  字符型	3	Y Y
            /// <summary>
            /// 
            /// </summary>
            public string drt_reim_flag { get; set; }//直报标志    字符型	3	Y
            /// <summary>
            /// 
            /// </summary>
            public string memo { get; set; }//   备注 字符型	500	　	　	明细分割错误信息

        }
    }
}