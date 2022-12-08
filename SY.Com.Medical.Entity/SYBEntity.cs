using SY.Com.Medical.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Entity
{
    public class SYBEntity : BaseEntity
    {
    }

    public class YBSign
    {
        [DB_Key("Id")]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int EmployeeId { get; set; }
        public int IsOpen { get; set; }
        public string SignNo { get; set; }
        public DateTime? CreateTime { get; set; }
    }

    public class YBCardInfo
    {
        [DB_Key("Id")]
        public int Id { get; set; }
        public string Area { get; set; }
        public string IdCard { get; set; }
        public string YbCard { get; set; }
        public string YbCardSn { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }

}
