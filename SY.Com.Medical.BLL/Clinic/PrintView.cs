using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Extension;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SY.Com.Medical.BLL.Clinic
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public class PrintView 
	{
		private PrintViewRepository db;
		public PrintView()
		{
			db = new PrintViewRepository();
		}

		///<summary> 
		///获取详情记录
		///</summary> 
		///<param name="id"></param>
		/// <returns></returns>
		public PrintViewModel get(int id)
		{
			return db.Get(id).EntityToDto<PrintViewModel>();
		}

		public List<PrintStyleModel> getStyles()
        {
			//1:挂号打印，2:退号打印,3:中药处方打印,4:西药处方打印,5:项目处方打印,6:收费打印,7:收费打印,8:退费打印,9:治疗单打印,10:病历打印
			List<PrintStyleModel> styles = new List<PrintStyleModel>() { 
				new PrintStyleModel{ Id = 1,Name = "挂号打印", TenantId=0, UserId=0},
				new PrintStyleModel{ Id = 2,Name = "退号打印", TenantId=0, UserId=0},
				new PrintStyleModel{ Id = 3,Name = "中药处方打印", TenantId=0, UserId=0},
				new PrintStyleModel{ Id = 4,Name = "西药处方打印", TenantId=0, UserId=0},
				new PrintStyleModel{ Id = 5,Name = "项目处方打印", TenantId=0, UserId=0},
				new PrintStyleModel{ Id = 6,Name = "收费打印", TenantId=0, UserId=0},
				new PrintStyleModel{ Id = 7,Name = "收费打印", TenantId=0, UserId=0},
				new PrintStyleModel{ Id = 8,Name = "退费打印", TenantId=0, UserId=0},
				new PrintStyleModel{ Id = 9,Name = "治疗单打印", TenantId=0, UserId=0},
				new PrintStyleModel{ Id = 10,Name = "病历打印", TenantId=0, UserId=0},
				new PrintStyleModel{ Id=100,Name="测试保存打印",TenantId=0,UserId=0 }
			};
			return styles;

		}

		///<summary> 
		///获取列表
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public List<PrintViewModel> gets(PrintViewRequest request)
		{
			var datas  = db.getViews(request.Style,request.TenantId);			
			if(datas == null )
            {
				return null;
            }
			return datas.EntityToDto<PrintViewModel>().ToList();
		}

		///<summary> 
		///新增
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public int add(PrintViewAdd request)
		{
			PrintViewEntity view = new PrintViewEntity();
			var smod = getStyles().Find(x => x.Id == request.Style);
			if(smod == null)
            {				
				throw new MyException($"打印视图Id不对,输入Id:{request.Style}");
			}
			view.PrintViewName = smod.Name;
			view.TenantId = request.TenantId;
			view.Style = request.Style;
			return db.Add(view);
		}
		
		

	}
} 