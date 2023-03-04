using Dapper;
using SY.Com.Medical.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Repository.Clinic
{


    public class SYBRepository : BaseRepository<SYBEntity>
    {
        /// <summary>
        /// 省医保签到
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="employeeId"></param>
        /// <param name="signno"></param>
        /// <returns></returns>
        public int SignIn(int tenantId,int employeeId,string signno)
        {
            string sql = @"  if exists(select id from  YBSign where TenantId=@TenantId and EmployeeId =@EmployeeId )
                            begin
	                            update YBSign
	                            Set SignNo=@SignNo,IsOpen=1,CreateTime = GETDATE()
	                            where TenantId=@TenantId and EmployeeId =@EmployeeId
                            end else begin
	                            Insert Into YBSign(TenantId,EmployeeId,IsOpen,SignNo)
	                            Values(@TenantId,@EmployeeId,1,@SignNo)
                            end ";
            return _db.Execute(sql, new { TenantId = tenantId, EmployeeId = employeeId, SignNo = signno });
        }

        /// <summary>
        /// 省医保签退
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public int SignOut(int tenantId, int employeeId)
        {
            string sql = @"  if exists(select id from  YBSign where TenantId=@TenantId and EmployeeId =@EmployeeId )
                            begin
	                            update YBSign
	                            Set SignNo=@SignNo,IsOpen=2,CreateTime = GETDATE()
	                            where TenantId=@TenantId and EmployeeId =@EmployeeId
                            end else begin
	                            Insert Into YBSign(TenantId,EmployeeId,IsOpen,SignNo)
	                            Values(@TenantId,@EmployeeId,2,@SignNo)
                            end ";
            return _db.Execute(sql, new { TenantId = tenantId, EmployeeId = employeeId, SignNo = "" });
        }

        /// <summary>
        /// 省医保获取签到
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public string GetSign(int tenantId, int employeeId)
        {
            string result = "sign_no";
            string sql = " Select * From YBSign Where TenantId=@TenantId and EmployeeId =@EmployeeId ";
            var mods = _db.Query<YBSign>(sql, new { TenantId = tenantId, EmployeeId = employeeId });
            if(mods != null && mods.Any())
            {
                var mod = mods.FirstOrDefault();
                if (!string.IsNullOrEmpty(mod.SignNo) && (DateTime.Now - mod.CreateTime.Value).TotalDays < 1)
                {
                    result = mod.SignNo;
                }
            }
            return result;
        }

        /// <summary>
        /// 保存卡信息
        /// </summary>
        /// <param name="area"></param>
        /// <param name="idcard"></param>
        /// <param name="ybcard"></param>
        /// <param name="ybcardsn"></param>
        /// <returns></returns>
        public int SetYBCardInfo(string area,string idcard,string ybcard,string ybcardsn)
        {
            string sql = @" if exists(select id from  YBCardInfo where IdCard=@idcard  )
                            begin
	                            update YBCardInfo
	                            Set Area=@area,YbCard=@ybcard,YbCardSn=@sn,UpdateTime = getdate()
	                            where IdCard=@idcard
                            end else begin
	                            Insert Into YBCardInfo(Area,IdCard,YbCard,YbCardSn,CreateTime,UpdateTime)
	                            Values(@area,@idcard,@ybcard,@sn,getdate(),getdate())
                            end ";
            return _db.Execute(sql, new { area = area, ybcard = ybcard, sn = ybcardsn, idcard= idcard });
        }

        public YBCardInfo GetYBCardInfo(string idcard)
        {
            string sql = " Select * From YBCardInfo where IdCard=@idcard  ";
            var mods = _db.Query<YBCardInfo>(sql, new { idcard = idcard });
            if(mods != null && mods.Any())
            {
                return mods.First();
            }
            return null;
        }

        public PatientEntity GetInsuplcAdmdvs(string psn_no,int tenantid)
        {
            string sql = " Select * From Patients Where TenantId =@tenantid And  psn_no = @psnno And insuplc_admdvs <> '' ";
            var mods = _db.Query<PatientEntity>(sql, new { psnno = psn_no,TenantId = tenantid });
            if (mods != null && mods.Any())
            {
                return mods.FirstOrDefault();                
            }
            return null;
        }



    }
}
