using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model
{
    /// <summary>
    /// 打印接口入参集合
    /// </summary>
    public class PrintRequest : BaseModel
    {
    }

    /// <summary>
    /// 添加打印文件
    /// </summary>
    public class PrintAddFileRequest : BaseModel
    {
        /// <summary>
        /// 打印类型Id
        /// </summary>
        public int TemplateId { get; set; }
    }

    /// <summary>
    /// 修改打印文件
    /// </summary>
    public class PrintUpdateFileRequest : BaseModel
    {
        /// <summary>
        /// 打印文件唯一Id
        /// </summary>
        public int FileId { get; set; }
        /// <summary>
        /// 打印文件二进制字节码
        /// </summary>
        public byte[] Bytes { get; set; }
    }
    /// <summary>
    /// 删除打印文件入参
    /// </summary>
    public class PrintDeleteFileRequest : BaseModel
    {
        /// <summary>
        /// 打印文件唯一Id
        /// </summary>
        public int FileId { get; set; }
    }

    /// <summary>
    /// 设置打印文件为使用状态
    /// </summary>
    public class PrintUseFileRequest : BaseModel
    {
        /// <summary>
        /// 打印文件唯一Id
        /// </summary>
        public int FileId { get; set; }
    }

}
