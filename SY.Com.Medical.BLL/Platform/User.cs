using SY.Com.Medical.Entity;
using SY.Com.Medical.Extension;
using SY.Com.Medical.Infrastructure;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL.Platform
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        private UserRepository db;
        public User() 
        {
            db = new UserRepository();
            
        }

        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public UserModel getUser(int UserId)
        {
            UserModel result = new UserModel();
            return CloneClass.Clone<UserEntity, UserModel>(db.Get(UserId), result);            
        }

        /// <summary>
        /// 根据UserId批量获取用户账号信息
        /// </summary>
        /// <param name="UserIds"></param>
        /// <returns></returns>
        public List<UserModel> getUsers(List<int> UserIds)
        {
            if (UserIds == null) return null;
            var entitys = db.getUsers(UserIds);
            if(entitys != null)
            {
                return entitys.EntityToDto<UserModel>();
            }
            return null;

        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UserModel Login(LoginRequest request)
        {
            UserEntity entity = db.Get(request.Account);
            if(entity == null)
            {
                throw new DataNotExists("账号不存在");
            }
            if(entity.Pwd != request.Pwd)
            {
                throw new DataLogicFails("密码错误");
            }
            if(entity.IsDelete == Enum.Delete.删除 || entity.IsEnable == Enum.Enable.禁用)
            {
                throw new DataStateException("账户状态异常,请联系管理员");
            }
            UserModel response = new UserModel();
            response.Account = entity.Account;
            response.UserId = entity.UserId;
            response.LogoImg = entity.LogoImg;
            response.UserId = entity.UserId;
            return response;
        }        

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RegisterResponse Register(RegisterRequest request)
        {
            
            UserEntity entitytemp = request.DtoToEntity<UserEntity>();
            entitytemp.IsEnable = Enum.Enable.启用;
            entitytemp.IsDelete = Enum.Delete.正常;
            int userid = db.Create(entitytemp);
            var entity = db.Get(userid);
            RegisterResponse response = new RegisterResponse();
            response.UserId = entity.UserId;
            return response;
        }

        /// <summary>
        /// 判断账号是否存在
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public bool ExistsAccount(string Account)
        {
            var entity = db.Get(Account);
            if(entity != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据账号获取信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public UserEntity getByAccount(string Account)
        {
            var entity = db.Get(Account);
            return entity;
        }

        /// <summary>
        /// 重置密码并返回
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResetResponse Reset(ResetRequest request)
        {
            var entity = db.Get(request.Account);
            if(entity == null)
            {
                throw new DataNotExists("账号不存在");
            }
            if(entity.YZM != request.YZM)
            {
                throw new DataLogicFails("验证码不符合");
            }
            entity.Pwd = request.Pwd;
            db.Update(entity);
            ResetResponse response = new ResetResponse();
            response.UserId = entity.UserId;
            return response;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public bool Change(string pwd,int userid)
        {
            return db.Change(userid, pwd);
        }


    }
}
