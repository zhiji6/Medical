﻿using SY.Com.Medical.Entity;
using SY.Com.Medical.Infrastructure;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using SY.Com.Medical.Extension;
using System.Text;
using System.Threading.Tasks;
using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.Enum;
using NPOI.HSSF.Record.Aggregates;

namespace SY.Com.Medical.BLL.Platform
{
    /// <summary>
    /// 租户
    /// </summary>
    public class Tenant
    {
        private TenantRepository db;

        public Tenant(string connection)
        {
            db = new TenantRepository(connection);
        }

        public Tenant()
        {
            db = new TenantRepository();
        }

        /// <summary>
        /// 返回用户关联的租户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<UserTenantResponse> GetTenants(BaseModel request)
        {
            var join = db.getTenants(request.UserId, Enum.IsBoss.不是);
            var boss = db.getTenants(request.UserId, Enum.IsBoss.是);
            List<UserTenantResponse> responsesjoin = new List<UserTenantResponse>();
            List<UserTenantResponse> responsesboss = new List<UserTenantResponse>();
            if (join.Any()) join.ToList().ForEach(entity => responsesjoin.Add(entity.EntityToDto<UserTenantResponse>()));
            if (boss.Any()) boss.ToList().ForEach(entity => responsesboss.Add(entity.EntityToDto<UserTenantResponse>()));
            if (responsesjoin.Any()) responsesjoin.ForEach(response => response.IsBoss = (int)Enum.IsBoss.不是);
            if (responsesboss.Any()) responsesboss.ForEach(response => response.IsBoss = (int)Enum.IsBoss.是);
            var resp = responsesjoin.Concat(responsesboss);
            foreach(var tenant in resp)
            {
                tenant.IsExpire = tenant.TenantServiceEnd < DateTime.Now;
            }
            return resp;
        }

        /// <summary>
        /// 返回用户关联的特定租户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserTenantResponse GetTenant(BaseModel request)
        {
            var join = db.getTenants(request.UserId, Enum.IsBoss.不是);
            var boss = db.getTenants(request.UserId, Enum.IsBoss.是);
            List<UserTenantResponse> responsesjoin = new List<UserTenantResponse>();
            List<UserTenantResponse> responsesboss = new List<UserTenantResponse>();
            if (join.Any()) join.ToList().ForEach(entity => responsesjoin.Add(entity.EntityToDto<UserTenantResponse>()));
            if (boss.Any()) boss.ToList().ForEach(entity => responsesboss.Add(entity.EntityToDto<UserTenantResponse>()));
            if (responsesjoin.Any()) responsesjoin.ForEach(response => response.IsBoss = (int)Enum.IsBoss.不是);
            if (responsesboss.Any()) responsesboss.ForEach(response => response.IsBoss = (int)Enum.IsBoss.是);
            return responsesjoin.Concat(responsesboss).ToList().Find(x => x.TenantId == request.TenantId);
        }

        /// <summary>
        /// 后台创建租户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserTenantResponse CreateTenantAdmin(TenentCreateAdminRequest request)
        {
            //创建用户
            User ubll = new User();
            UserEntity ue;
            if(ubll.ExistsAccount(request.Account))
            {
                ue = ubll.getByAccount(request.Account);
            }
            else
            {
                ubll.Register(new RegisterRequest { Account = request.Account, Pwd = string.IsNullOrEmpty(request.PassWord) ? "123456" : request.PassWord, YZM = "123456" });
                ue = ubll.getByAccount(request.Account);
            }
            TenentCreateRequest tentity = new TenentCreateRequest();
            tentity.TenantName = request.TenantName;
            tentity.TenantType = request.TenantType;
            tentity.UserId = ue.UserId;            
            return CreateTenant(tentity,request.UserName);
        }

        /// <summary>
        /// 前台创建租户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserTenantResponse CreateTenant(TenentCreateRequest request,string employeeName = "")
        {

            var entity = request.DtoToEntity<TenantEntity>();
            entity.Boss = request.UserId;
            entity.TenantServiceStart = DateTime.Now;
            entity.TenantServiceEnd = DateTime.Now.AddDays(double.Parse(ReadConfig.GetConfigSection("TenantTryDay")));
            int TenantID = db.Create(entity);
            if (TenantID > 0)
            {
                db.CreateUserTenant(request.UserId, TenantID, Enum.IsBoss.是);
                //创建员工
                User ubll = new User();
                Employee embll = new Employee();
                EmployeeModel emmod = new EmployeeModel();
                UserModel ummod = ubll.getUser(request.UserId);
                var mod = CloneClass.Clone<UserModel, EmployeeModel>(ummod, emmod);
                mod.TenantId = TenantID;
                mod.Roles = "1,2";
                mod.EmployeeName = employeeName;                    
                embll.createEmployee(mod);
            }
            UserTenantResponse response = new UserTenantResponse();
            response.TenantId = TenantID;
            response.TenantName = request.TenantName;
            response.TenantType = request.TenantType;
            response.TenantServiceStart = entity.TenantServiceStart;
            response.TenantServiceEnd = entity.TenantServiceEnd;
            //
            DepartmentRepository dbdepart = new DepartmentRepository();
            dbdepart.CopyToTenant(TenantID);
            if(request.TenantType == (int)Enum.TenantType.口腔诊所 || request.TenantType == (int)Enum.TenantType.口腔门诊部)
            {
                //复制口腔项目药品
                Good goodbll = new Good();                
                goodbll.CopyTo(0, TenantID);
            }

            return response;
        }

        /// <summary>
        /// 创建用户和诊所的关系
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="TenantId"></param>
        /// <param name="isboss"></param>
        /// <returns></returns>
        public int CreateUserTenant(int UserId, int TenantId, IsBoss isboss = IsBoss.不是)
        {
            return db.CreateUserTenant(UserId, TenantId, isboss);
        }

        /// <summary>
        /// 修改租户信息
        /// </summary>
        /// <param name="request"></param>
        public void UpdateTenant(TenantRequest request)
        {
            var entity = request.DtoToEntity<TenantEntity>();
            db.Update(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="TenantId"></param>
        public void DeleteTenant(int TenantId,int UserId)
        {
            db.DeleteTenant(TenantId, UserId);            
        }

        public void DisableTenant(int TenantId,int enable)
        {
            var e = (Enum.Enable)enable;
            db.DisableTenant(TenantId, e);
        }

        /// <summary>
        /// 点击进入租户
        /// 根据用户信息获取员工信息
        /// 根据员工角色信息获取菜单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<MenuTreeResponse> GetMenu(EmployeeGetModel request)
        {
            Employee em = new Employee();
            var emmodel = em.getEmployee(request);            
            List<RoleEntity> listrole = new List<RoleEntity>();
            foreach(var item in emmodel.Roles.Split(',').ToList())
            {
                listrole.Add(new RoleEntity() { RoleId = int.Parse(item) });
            }
            Role rolebll = new Role();            
            RoleRepository dbrole = new RoleRepository();
            var menus = dbrole.getMenus(listrole);
            Menu menubll = new Menu();
            var retmenus = menubll.GetSystemTree(menus);
            retmenus.ForEach(f => f.SubMenu = f.SubMenu.OrderBy(o => o.Sort).ToList());
            var sortretmenus = retmenus.OrderBy(x => x.Sort).ToList();
            return sortretmenus;
            //return dbrole.getMenus(listrole).EntityToDto<MenuResponse>();
        }

        public UserTenantResponse getById(int tenantId)
        {
            var entity = db.getById(tenantId);
            if (entity == null)
            {
                return null;
            }
            else {
                return entity.EntityToDto<UserTenantResponse>();
            }
        }

        public Tuple<List<TenantAllSearchResponse>,int> getAllPlatform(TenantAllSearchRequest request)
        {
            var tuple = db.getAllPaltform(request.TenantName, request.TenantIds, request.BossName
                , request.TenantServiceEndStart, request.TenantServiceEndEnd
                , request.CreateTimeStart, request.CreateTimeEnd,request.PageSize,request.PageIndex,request.Account);
            return new Tuple<List<TenantAllSearchResponse>, int>(tuple.Item1.ToList(), tuple.Item2);
        }

        public bool BuyServiceTime(TenantBuyRequest request)
        {
            int result = db.BuyServiceTime(request);
            return result > 0;
        }

        public EmployeeModel IntoTenant(int tenantid,int userid)
        {
            Employee em = new Employee();
            var emmodel = em.getEmployee(new EmployeeGetModel { TenantId = tenantid, UserId = userid });
            return emmodel;
        }

    }
}
