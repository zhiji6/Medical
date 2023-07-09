using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model.SYB
{
    public class MZ4205
    {
        public class In4025Data
        {
            /// <summary>
            /// 
            /// </summary>
            public MdtrtInfo2 mdtrtinfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Diseinfo2> diseinfo { get; set; }
            public List<FeeDetail2> feedetail { get; set; }

        }
        /// <summary>
        /// 
        /// </summary>
        public class MdtrtInfo2
        {
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_id { get; set; }//  就诊ID 字符型	30	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string psn_no { get; set; }//  人员编号 字符型	30	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string med_type { get; set; }//   医疗类别 字符型	6	Y Y
            /// <summary>
            /// 
            /// </summary>
            public string begntime { get; set; }// 开始时间    日期时间型 Y   就诊时间
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string main_cond_dscr { get; set; } // 主要病情描述 字符型	1000	　	　	
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string dise_codg { get; set; }// 病种编码    字符型	30	　	　
            /// <summary>
            /// 
            /// </summary>
            //按照标准编码填写：按病种结算病种目录代码(bydise_setl_list_code)、门诊慢特病病种目录代码(opsp_dise_cod)、
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string dise_name { get; set; }// 病种名称   字符型 500
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string birctrl_type { get; set; }// 计划生育手术类别    字符型	6	Y 生育门诊按需录入
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string birctrl_matn_date { get; set; }// 计划生育手术或生育日期 日期型 生育门诊按需录入，yyyy-MM-dd
        }
        /// <summary>
        /// 
        /// </summary>
        public class Diseinfo2
        {
            /// <summary>
            /// 
            /// </summary>
            public string diag_type { get; set; }//诊断类别    字符型	3	　Y Y
            /// <summary>
            /// 
            /// </summary>
            public string diag_srt_no { get; set; }// 诊断排序号   数值型	2	　	　Y
            /// <summary>
            /// 
            /// </summary>
            public string diag_code { get; set; }//   诊断代码 字符型	20	　	　Y
            /// <summary>
            /// 
            /// </summary>
            public string diag_name { get; set; }//   诊断名称 字符型	100	　	　Y
            /// <summary>
            /// 
            /// </summary>            
            public string diag_dept { get; set; }//   诊断科室 字符型	50	　	　Y
            /// <summary>
            /// 
            /// </summary>
            public string dise_dor_no { get; set; }// 诊断医生编码 字符型	30	　	　Y
            /// <summary>
            /// 
            /// </summary>
            public string dise_dor_name { get; set; }// 诊断医生姓名 字符型	50	　	　Y
            /// <summary>
            /// 
            /// </summary>
            public string diag_time { get; set; }//   诊断时间 日期时间型   Y yyyy-MM-dd HH:mm:ss
            /// <summary>
            /// 
            /// </summary>
            public string vali_flag { get; set; } //  有效标志 字符型	3	Y Y

        }
        public class FeeDetail2
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
    }
}
