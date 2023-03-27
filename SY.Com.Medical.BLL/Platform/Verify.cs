using SY.Com.Medical.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL.Platform
{
    /// <summary>
    /// 生成存储和校验验证码
    /// </summary>
    public class Verify
    {
        public static ConcurrentDictionary<string, VerifyToken> systemVerifyCode = new ConcurrentDictionary<string, VerifyToken>();

        public VerifyCode Generator()
        {
            char[] chs = new char[6];
            for(int i = 0; i < 6; i++)
            {
                int index = new Random().Next(3);
                int rd = 48;
                switch (index)
                {
                    case 0: rd = new Random().Next(48, 57);break;
                    case 1: rd = new Random().Next(65, 90); break;
                    case 2: rd = new Random().Next(97, 122); break;
                    default:break;
                }
                chs[i] = (char)rd;
            }            
            string text = new string(chs);
            byte[] key, iv;
            
            VerifyToken vt = new VerifyToken();
            vt.CreateOn = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            vt.ID = Guid.NewGuid().ToString();
            vt.Code = text;
            if (!systemVerifyCode.ContainsKey(vt.ID))
            {
                systemVerifyCode.TryAdd(vt.ID, vt);
            }
            VerifyCode vc = new VerifyCode { Code = text, Token = vt.ID};
            return vc;
        }

        public bool AuthCode(VerifyCode vc)
        {
            if(systemVerifyCode.ContainsKey(vc.Token))
            {
                VerifyToken vt;
                systemVerifyCode.Remove(vc.Token,out vt);                
                if (vc.Code.ToLower() == vt.Code.ToLower())
                {
                    if( new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() - 120 < vt.CreateOn)
                    {
                        return true;
                    }
                    else
                    {
                        throw new MyException("验证码已过期");
                    }
                }
                else
                {
                    systemVerifyCode.TryAdd(vc.Token, vt);
                    return false;
                }                    
            }
            else
            {
                throw new MyException("验证码Token无效");
            }
        }

        public void Clear()
        {
            systemVerifyCode.Clear();
        }

    }
}
