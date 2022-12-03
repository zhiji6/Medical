using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL.Clinic
{
    public class SYBKS
    {

        public static Dictionary<string, string> departmentdic = new Dictionary<string, string>(new List<KeyValuePair<string, string>> {
           new KeyValuePair<string, string>("预防保健科","0100"),
            new KeyValuePair<string, string>("全科医疗科","0200"),
            new KeyValuePair<string, string>("内科","0300"),
            new KeyValuePair<string, string>("呼吸内科专业","0301"),
            new KeyValuePair<string, string>("消化内科专业","0302"),
            new KeyValuePair<string, string>("神经内科专业","0303"),
            new KeyValuePair<string, string>("心血管内科专业","0304"),
            new KeyValuePair<string, string>("血液内科专业","0305"),
            new KeyValuePair<string, string>("肾病学专业","0306"),
            new KeyValuePair<string, string>("内分泌专业","0307"),
            new KeyValuePair<string, string>("免疫学专业","0308"),
            new KeyValuePair<string, string>("变态反应专业","0309"),
            new KeyValuePair<string, string>("老年病专业","0310"),            
            new KeyValuePair<string, string>("外科","0400"),
            new KeyValuePair<string, string>("普通外科专业","0401"),
            new KeyValuePair<string, string>("神经外科专业","0402"),
            new KeyValuePair<string, string>("骨科专业","0403"),
            new KeyValuePair<string, string>("泌尿外科专业","0404"),
            new KeyValuePair<string, string>("胸外科专业","0405"),
            new KeyValuePair<string, string>("心脏大血管外科专业","0406"),
            new KeyValuePair<string, string>("烧伤科专业","0407"),
            new KeyValuePair<string, string>("整形外科专业","0408"),            
            new KeyValuePair<string, string>("妇产科","0500"),
            new KeyValuePair<string, string>("妇科专业","0501"),
            new KeyValuePair<string, string>("产科专业","0502"),
            new KeyValuePair<string, string>("计划生育专业","0503"),
            new KeyValuePair<string, string>("优生学专业","0504"),
            new KeyValuePair<string, string>("生殖健康与不孕症专业","0505"),            
            new KeyValuePair<string, string>("妇女保健科","0600"),
            new KeyValuePair<string, string>("青春期保健专业","0601"),
            new KeyValuePair<string, string>("围产期保健专业","0602"),
            new KeyValuePair<string, string>("更年期保健专业","0603"),
            new KeyValuePair<string, string>("妇女心理卫生专业","0604"),
            new KeyValuePair<string, string>("妇女营养专业","0605"),            
            new KeyValuePair<string, string>("儿科","0700"),
            new KeyValuePair<string, string>("新生儿专业","0701"),
            new KeyValuePair<string, string>("小儿传染病专业","0702"),
            new KeyValuePair<string, string>("小儿消化专业","0703"),
            new KeyValuePair<string, string>("小儿呼吸专业","0704"),
            new KeyValuePair<string, string>("小儿心脏病专业","0705"),
            new KeyValuePair<string, string>("小儿肾病专业","0706"),
            new KeyValuePair<string, string>("小儿血液病专业","0707"),
            new KeyValuePair<string, string>("小儿神经病学专业","0708"),
            new KeyValuePair<string, string>("小儿内分泌专业","0709"),
            new KeyValuePair<string, string>("小儿遗传病专业","0710"),
            new KeyValuePair<string, string>("小儿免疫专业","0711"),            
            new KeyValuePair<string, string>("小儿外科","0800"),
            new KeyValuePair<string, string>("小儿普通外科专业","0801"),
            new KeyValuePair<string, string>("小儿骨科专业","0802"),
            new KeyValuePair<string, string>("小儿泌尿外科专业","0803"),
            new KeyValuePair<string, string>("小儿胸心外科专业","0804"),
            new KeyValuePair<string, string>("小儿神经外科专业","0805"),            
            new KeyValuePair<string, string>("儿童保健科","0900"),
            new KeyValuePair<string, string>("儿童生长发育专业","0901"),
            new KeyValuePair<string, string>("儿童营养专业","0902"),
            new KeyValuePair<string, string>("儿童心理卫生专业","0903"),
            new KeyValuePair<string, string>("儿童五官保健专业","0904"),
            new KeyValuePair<string, string>("儿童康复专业","0905"),            
            new KeyValuePair<string, string>("眼科","1000"),
            new KeyValuePair<string, string>("耳鼻咽喉科","1100"),
            new KeyValuePair<string, string>("耳科专业","1101"),
            new KeyValuePair<string, string>("鼻科专业","1102"),
            new KeyValuePair<string, string>("咽喉科专业","1103"),            
            new KeyValuePair<string, string>("口腔科","1200"),
            new KeyValuePair<string, string>("口腔内科专业","1201"),
            new KeyValuePair<string, string>("口腔假面外科专业","1202"),
            new KeyValuePair<string, string>("正畸专业","1203"),
            new KeyValuePair<string, string>("口腔修复专业","1204"),
            new KeyValuePair<string, string>("口腔预防保健专业","1205"),            
            new KeyValuePair<string, string>("皮肤科","1300"),
            new KeyValuePair<string, string>("皮肤病专业","1301"),
            new KeyValuePair<string, string>("性传播疾病专业","1302"),            
            new KeyValuePair<string, string>("医疗美容科","1400"),
            new KeyValuePair<string, string>("精神科","1500"),
            new KeyValuePair<string, string>("精神病专业","1501"),
            new KeyValuePair<string, string>("精神卫生专业","1502"),
            new KeyValuePair<string, string>("药物依赖专业","1503"),
            new KeyValuePair<string, string>("精神康复专业","1504"),
            new KeyValuePair<string, string>("社区防治专业","1505"),
            new KeyValuePair<string, string>("临床心理专业","1506"),
            new KeyValuePair<string, string>("司法精神专业","1507"),            
            new KeyValuePair<string, string>("传染科","1600"),
            new KeyValuePair<string, string>("肠道传染病专业","1601"),
            new KeyValuePair<string, string>("呼吸道传染病专业","1602"),
            new KeyValuePair<string, string>("肝炎专业","1603"),
            new KeyValuePair<string, string>("虫媒传染病专业","1604"),
            new KeyValuePair<string, string>("动物源性传染病专业","1605"),
            new KeyValuePair<string, string>("蠕虫病专业","1606"),            
            new KeyValuePair<string, string>("结核病科","1700"),
            new KeyValuePair<string, string>("地方病科","1800"),
            new KeyValuePair<string, string>("肿瘤科","1900"),
            new KeyValuePair<string, string>("急诊医学科","2000"),
            new KeyValuePair<string, string>("康复医学科","2100"),
            new KeyValuePair<string, string>("运动医学科","2200"),
            new KeyValuePair<string, string>("职业病科","2300"),
            new KeyValuePair<string, string>("职业中毒专业","2301"),
            new KeyValuePair<string, string>("尘肺专业","2302"),
            new KeyValuePair<string, string>("放射病专业","2303"),
            new KeyValuePair<string, string>("物理因素损伤专业","2304"),
            new KeyValuePair<string, string>("职业健康监护专业","2305"),            
            new KeyValuePair<string, string>("临终关怀科","2400"),
            new KeyValuePair<string, string>("特种医学与军事医学科","2500"),
            new KeyValuePair<string, string>("麻醉科","2600"),
            new KeyValuePair<string, string>("疼痛科","2700"),
            new KeyValuePair<string, string>("重症医学科","2800"),
            new KeyValuePair<string, string>("医学检验科","3000"),
            new KeyValuePair<string, string>("临床体液、血液专业","3001"),
            new KeyValuePair<string, string>("临床微生物学专业","3002"),
            new KeyValuePair<string, string>("临床化学检验专业","3003"),
            new KeyValuePair<string, string>("临床免疫、血清学专业","3004"),
            new KeyValuePair<string, string>("临床细胞分子遗传学专业","3005"),            
            new KeyValuePair<string, string>("病理科","3100"),
            new KeyValuePair<string, string>("医学影像科","3200"),
            new KeyValuePair<string, string>("X线诊断专业","3201"),
            new KeyValuePair<string, string>("CT诊断专业","3202"),
            new KeyValuePair<string, string>("磁共振成像诊断专业","3203"),
            new KeyValuePair<string, string>("核医学专业","3204"),
            new KeyValuePair<string, string>("超声诊断专业","3205"),
            new KeyValuePair<string, string>("心电诊断专业","3206"),
            new KeyValuePair<string, string>("脑电及脑血流图诊断专业","3207"),
            new KeyValuePair<string, string>("神经肌肉电图专业","3208"),
            new KeyValuePair<string, string>("介入放射学专业","3209"),
            new KeyValuePair<string, string>("放射治疗专业","3210"),            
            new KeyValuePair<string, string>("中医科","5000"),
            new KeyValuePair<string, string>("内科专业","5001"),
            new KeyValuePair<string, string>("外科专业","5002"),
            new KeyValuePair<string, string>("妇产科专业","5003"),
            new KeyValuePair<string, string>("儿科专业","5004"),
            new KeyValuePair<string, string>("皮肤科专业","5005"),
            new KeyValuePair<string, string>("H/l科专","5006"),
            new KeyValuePair<string, string>("耳鼻咽喉科专业","5007"),
            new KeyValuePair<string, string>("口腔科专业","5008"),
            new KeyValuePair<string, string>("肿瘤科专业","5009"),
            new KeyValuePair<string, string>("骨伤科专业","5010"),
            new KeyValuePair<string, string>("虹肠科专业","5011"),
            new KeyValuePair<string, string>("老年病科专业","5012"),
            new KeyValuePair<string, string>("针灸病科专业","5013"),
            new KeyValuePair<string, string>("推拿病科专业","5014"),
            new KeyValuePair<string, string>("康复学专业","5015"),
            new KeyValuePair<string, string>("急诊科专业","5016"),
            new KeyValuePair<string, string>("预防保健科专业","5017"),            
            new KeyValuePair<string, string>("民族医学科","5100"),
            new KeyValuePair<string, string>("维吾尔医学","5101"),
            new KeyValuePair<string, string>("藏医学","5102"),
            new KeyValuePair<string, string>("蒙医学","5103"),
            new KeyValuePair<string, string>("彝族学","5104"),
            new KeyValuePair<string, string>("傣族学","5105"),            
            new KeyValuePair<string, string>("中西医结合科","5200"),


        });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string getYBDepartCode(string name)
        {
            string code = "";
            if (SYBKS.departmentdic.ContainsKey(name))
            {                
                code = SYBKS.departmentdic[name];
            }
            else if (name.IndexOf("口腔") != -1)
            {
                code = "1200";
            }
            else
            {
                code = "5000";
            }
            return code;
        }

    }
}
