using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using SY.Com.Medical.BLL.Clinic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SY.Com.Medical.Model;
using SY.Com.Medical.Entity;

namespace SY.Com.Medical.BLL.Platform
{
    public class GoodExcelFileUpload : FileUpload
    {
        private string[] filepathextent;
        private List<string> format = new List<string> { ".xls", ".xlsx"};
        Good bll = new Good();
        Dic dicbll = new Dic();
        public GoodExcelFileUpload(FileBus filebus, string extension, params string[] filepathparam) : base(filebus)
        {
            filepathextent = filepathparam;
            if (!format.Contains(extension))
            {
                throw new MyException("文件格式不匹配本业务");
            }
            _extension = extension;
        }
        public override string SaveFile(MemoryStream mstream)
        {
            try
            {
                if (mstream.Length > 1024 * 1024 * 10)//超过10M的文件不允许
                {
                    throw new MyException("文件不能超过10M");
                }

                string dir = System.Environment.CurrentDirectory + "/wwwroot/";
                string sortpath = fb.getPath(filepathextent);//获取路径
                string strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                string strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                string filename = strDateTime + strRan + _extension;
                string filepath = dir + sortpath;
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                #region 导入数据
                mstream.Position = 0;
                IWorkbook workbook;
                if (_extension == ".xlsx") { workbook = new XSSFWorkbook(mstream); } else if (_extension == ".xls") { workbook = new HSSFWorkbook(mstream); } else { workbook = null; }
                if (workbook != null)
                {
                    ISheet sheet = workbook.GetSheetAt(0);
                    //表头  
                    IRow header = sheet.GetRow(sheet.FirstRowNum);
                    List<int> columns = new List<int>();
                    for(int i =0;i< header.LastCellNum;i++)
                    {
                        columns.Add(i);
                    }
                    List<GoodEntity> datas = new List<GoodEntity>();
                    //数据  
                    for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                    {
                        GoodEntity data = new GoodEntity();
                        data.TenantId = int.Parse(filepathextent[0]);
                        bool hasValue = false;
                        foreach (int j in columns)
                        {
                            var cellvalue = GetValueType(sheet.GetRow(i).GetCell(j));
                            if (cellvalue == null)
                            {
                                continue;
                            }
                            if (cellvalue != null && cellvalue.ToString() != string.Empty)
                            {
                                hasValue = true;
                            }
                            switch (header.GetCell(j).StringCellValue)
                            {
                                case "类型":
                                    {
                                        switch (cellvalue)
                                        {
                                            case "西药": data.GoodType = Enum.GoodType.西药; break;
                                            case "中成药": data.GoodType = Enum.GoodType.中成药; break;
                                            case "中药": data.GoodType = Enum.GoodType.中药; break;
                                            case "项目": data.GoodType = Enum.GoodType.项目; break;
                                            case "材料": data.GoodType = Enum.GoodType.材料; break;
                                            default: data.GoodType = Enum.GoodType.西药; break;
                                        }
                                    }
                                    break;
                                case "名称": data.GoodName = cellvalue.ToString(); break;
                                case "规格": data.Norm = cellvalue.ToString(); break;
                                case "厂家": data.Factory = cellvalue.ToString(); break;
                                case "计价单位": data.SalesUnit = cellvalue.ToString(); break;
                                case "采购单位": data.StockUnit = cellvalue.ToString(); break;
                                case "比率": data.Ratio = int.Parse(cellvalue.ToString()); break;
                                case "物料分类": data.GoodSort = dicbll.getIdByValue(data.TenantId, cellvalue.ToString(), "GoodSort", data.GoodType.ToString()); break;
                                case "单次用量": data.Single = int.Parse(cellvalue.ToString()); break;
                                case "一天用量": data.EveryDay = cellvalue.ToString(); break;
                                case "用法": data.Usage = cellvalue.ToString(); break;
                                case "国药准字": data.GoodStandard = cellvalue.ToString(); break;
                                case "医保编码": data.InsuranceCode = cellvalue.ToString(); break;
                                case "机构编码": data.CustomerCode = cellvalue.ToString(); break;
                                case "条形码": data.BarCode = cellvalue.ToString(); break;
                                case "价格": data.Price = (long)(Math.Round(double.Parse(cellvalue.ToString()), 2) * 100); break;
                                case "部位": data.Place = cellvalue.ToString(); break;
                                default: break;
                            }
                            #region 注释
                            /*
                            switch (j)
                            {
                                case 0:
                                    {
                                        switch (cellvalue)
                                        {
                                            case "西药": data.GoodType = Enum.GoodType.西药; break;
                                            case "中成药": data.GoodType = Enum.GoodType.中成药; break;
                                            case "中药": data.GoodType = Enum.GoodType.中药; break;
                                            case "诊疗项目": data.GoodType = Enum.GoodType.项目; break;
                                            case "材料": data.GoodType = Enum.GoodType.材料; break;
                                            default: data.GoodType = Enum.GoodType.西药; break;
                                        }
                                    }
                                    break;
                                case 1: data.GoodName = cellvalue.ToString(); break;
                                case 2: data.Norm = cellvalue.ToString(); break;
                                case 3: data.Factory = cellvalue.ToString(); break;                                
                                case 4: data.SalesUnit = cellvalue.ToString(); break;
                                case 5: data.StockUnit = cellvalue.ToString(); break;
                                case 6: data.Ratio = int.Parse(cellvalue.ToString()); break;
                                case 7: data.GoodSort = dicbll.getIdByValue(data.TenantId, cellvalue.ToString(), "GoodSort", data.GoodType.ToString()); break;
                                case 8: data.Single = int.Parse(cellvalue.ToString()); break;
                                case 9: data.EveryDay = cellvalue.ToString(); break;
                                case 10: data.Usage = cellvalue.ToString(); break;
                                case 11: data.GoodStandard = cellvalue.ToString(); break;
                                case 12: data.InsuranceCode = cellvalue.ToString(); break;
                                case 13: data.CustomerCode = cellvalue.ToString(); break;
                                case 14: data.BarCode = cellvalue.ToString(); break;
                                case 15: data.Price = (long)(Math.Round(double.Parse(cellvalue.ToString()), 2) * 100); break;
                                case 16:data.Place = cellvalue.ToString();break;
                                default: break;
                            }
                            */
                            #endregion
                        }
                        if (hasValue)
                        {
                            datas.Add(data);
                        }
                    }
                    if (datas.Any())
                    {
                        bll.add(datas);
                    }
                }
                //
                #endregion
                FileStream fs = new FileStream(filepath + filename, FileMode.OpenOrCreate);
                try
                {                    
                    fs.Write(mstream.GetBuffer(), 0, mstream.GetBuffer().Length);
                }
                finally
                {
                    fs.Flush();
                    fs.Close();
                }

                

                return sortpath + filename;
            }catch(Exception ex)
            {
                throw new Exception("上传GoodExcel异常:" + ex.Message);
            }
            finally
            {
                mstream.Close();
            }

        }

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell">目标单元格</param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return null;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula:
                default:
                    return "=" + cell.CellFormula;
            }
        }
    }
}
