using Dapper;
using SY.Com.Medical.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Repository.Platform
{
    /// <summary>
    /// 用户数据库访问层
    /// </summary>
    public class UserRepository : BaseRepository<UserEntity>
    {

        /// <summary>
        /// 获取用户-通过账号密码
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public UserEntity Get(string Account,string Pwd)
        {
            string sql = " Select * From Users Where Account =@Account And Pwd=@Pwd ";
            return _db.QueryFirstOrDefault<UserEntity>(sql, new { Account = Account, Pwd = Pwd });
        }

        /// <summary>
        /// 获取用户-通过账号
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public UserEntity Get(string Account)
        {
            string sql = " Select * From Users Where Account = @Account ";
            return _db.QueryFirstOrDefault<UserEntity>(sql, new { Account = Account });
        }
        
        /// <summary>
        /// 获取用户-通过账号验证码
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="YZM"></param>
        /// <returns></returns>
        public UserEntity GetReSet(string Account,string YZM)
        {
            string sql = " Select * From Users Where Account = @Account And YZM = @YZM ";
            return _db.QueryFirstOrDefault<UserEntity>(sql, new { Account = Account, YZM= YZM });
        }

        public List<UserEntity> getUsers(List<int> UserIds)
        {
            string sql = " Select * From Users Where UserId in@ids ";
            return _db.Query<UserEntity>(sql, new { ids = UserIds })?.ToList();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool Change(int userid,string pwd)
        {
            string sql = " Update Users Set Pwd = @pwd Where UserId = @id ";
            return _db.Execute(sql, new { pwd = pwd, id = userid }) >= 0;
        }

    }
}
