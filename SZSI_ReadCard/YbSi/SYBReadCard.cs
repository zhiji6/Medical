using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MkSi
{
    public class SYBReadCard
    {
        #region 初始化读卡链接库

        /// <returns> 0表示成功</returns>
        [DllImport(@"SSCard.dll", EntryPoint = "Init", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int Init(StringBuilder pUrl, StringBuilder pUser);

        //读医保卡
        /// <returns> 0表示成功</returns>
        [DllImport(@"SSCard.dll", EntryPoint = "ReadCardBas", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadCardBas(IntPtr pOutBuff, int nOutBuffLen, IntPtr pSignBuff, int nSignBuffLen);

        //读二维码
        /// <returns> 0表示成功</returns>
        [DllImport(@"SSCard.dll", EntryPoint = "GetQRBase", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetQRBase(int nTimeout, IntPtr pOutBuff, int nOutBuffLen, IntPtr pSignBuff, int nSignBuffLen);

        //读身份证
        /// <returns> 0表示成功</returns>
        [DllImport(@"SSCard.dll", EntryPoint = "ReadSFZ", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadSFZ(IntPtr pOutBuff, int nOutBuffLen, IntPtr pSignBuff, int nSignBuffLen);

        //检验PIN码
        /// <returns> 0表示成功</returns>
        [DllImport(@"SSCard.dll", EntryPoint = "VerifyPIN", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int VerifyPIN(IntPtr pOutBuff, int nOutBuffLen);

        //修改PIN码
        /// <returns> 0表示成功</returns>
        [DllImport(@"SSCard.dll", EntryPoint = "ChangePIN", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ChangePIN(IntPtr pOutBuff, int nOutBuffLen);


        #endregion

        //初始化
        public static int MyInit(string host,string areastr)
        {
            StringBuilder server = new StringBuilder(host);
            StringBuilder area = new StringBuilder(areastr);
            int retInit = Init(server, area);
            return retInit;
        }

        //读实体卡
        public static CardModel readCard()
        {
            IntPtr outInfo = Marshal.AllocHGlobal(1024);
            IntPtr SignInfo = Marshal.AllocHGlobal(1024);
            int ReadCard_ret = ReadCardBas(outInfo, 1024, SignInfo, 1024);
            string strOutInfo = Marshal.PtrToStringAnsi(outInfo);
            Marshal.FreeHGlobal(outInfo);
            string strSignInfo = Marshal.PtrToStringAnsi(SignInfo);
            Marshal.FreeHGlobal(SignInfo);
            CardModel mod = new CardModel
            {
                result = ReadCard_ret,
                method = "ReadCardBas",
                cardinfo = strOutInfo,
                sign = strSignInfo,
                err_msg = strOutInfo
            };
            //"440300|445222198709062439|BCH826720|440300D156000005006CFEED8AA783F6|陈力生|0081544C9286844403006CFEED|2.00|20180416|20280416|000000000000|00010601202211000461||
            if (mod.result == 0 && !string.IsNullOrEmpty(mod.cardinfo))
            {
                var infos = mod.cardinfo.Split('|');
                if (infos.Length > 1)
                    mod.cardinfo = infos[1];
            }
            return mod;
        }

        //读电子卡
        public static CardModel readQR()
        {
            int nTimeout = 60000;
            IntPtr pOutBuff = Marshal.AllocHGlobal(1024);
            IntPtr pSignBuff = Marshal.AllocHGlobal(1024);

            int ReadCard_ret = GetQRBase(nTimeout, pOutBuff, 1024, pSignBuff, 1024);

            string StrpOutBuff = Marshal.PtrToStringAnsi(pOutBuff);
            Marshal.FreeHGlobal(pOutBuff);

            string strpSignBuff = Marshal.PtrToStringAnsi(pSignBuff);
            Marshal.FreeHGlobal(pSignBuff);
            CardModel mod = new CardModel
            {
                result = ReadCard_ret,
                method = "GetQRBase",
                cardinfo = StrpOutBuff,
                sign = strpSignBuff,
                err_msg = StrpOutBuff
            };
            return mod;
        }

        //读身份证
        public static CardModel readID()
        {
            IntPtr pOutBuff = Marshal.AllocHGlobal(1024);
            IntPtr pSignBuff = Marshal.AllocHGlobal(1024);

            int ReadCard_ret = ReadSFZ(pOutBuff, 1024, pSignBuff, 1024);

            string StrpOutBuff = Marshal.PtrToStringAnsi(pOutBuff);
            Marshal.FreeHGlobal(pOutBuff);

            string strpSignBuff = Marshal.PtrToStringAnsi(pSignBuff);
            Marshal.FreeHGlobal(pSignBuff);
            CardModel mod = new CardModel
            {
                result = ReadCard_ret,
                method = "ReadSFZ",
                cardinfo = StrpOutBuff,
                sign = strpSignBuff,
                err_msg = StrpOutBuff
            };
            return mod;
        }

        //检验PIN码
        public static CardModel CheckPin()
        {
            IntPtr pOutBuff = Marshal.AllocHGlobal(1024);
            int ReadCard_ret = VerifyPIN(pOutBuff, 1024);
            string StrpOutBuff = Marshal.PtrToStringAnsi(pOutBuff);
            Marshal.FreeHGlobal(pOutBuff);
            CardModel mod = new CardModel
            {
                result = ReadCard_ret,
                method = "VerifyPIN",
                cardinfo = StrpOutBuff,
                sign = "",
                err_msg = StrpOutBuff
            };
            return mod;
        }

        //修改PIN码
        public static CardModel UpdatePin()
        {
            IntPtr pOutBuff = Marshal.AllocHGlobal(1024);
            int ReadCard_ret = ChangePIN(pOutBuff, 1024);
            string StrpOutBuff = Marshal.PtrToStringAnsi(pOutBuff);
            Marshal.FreeHGlobal(pOutBuff);
            CardModel mod = new CardModel
            {
                result = ReadCard_ret,
                method = "ChangePIN",
                cardinfo = StrpOutBuff,
                sign = "",
                err_msg = StrpOutBuff
            };
            return mod;
        }

    }
}
