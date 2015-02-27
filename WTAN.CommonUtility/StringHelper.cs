using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Web;

namespace WTAN.CommonUtility
{
    public static class StringHelper
    {

        public static String UrlEncode(this String str)
        {
            return HttpUtility.UrlEncode(str, System.Text.Encoding.UTF8);
        }

        public static String ToChinaTxt(this String str)
        {
            switch (str.ToLower())
            {
                case "all": return "在整站内容";
                case "province": return "在省內旅游";
                case "domestic": return "在国内旅游";
                case "overseas": return "在港澳台及境外旅遊";
                case "guide": return "在目的地指南";
                case "question": return "在签证常见问题";
                case "raiders": return "在旅游攻略";
                case "visa": return "在签证";
                case "ticket": return "在票务预订";
                default: return "在其它";
            }
        }

        public static HtmlString ToHighlightStr(this String str, String keyword)
        {
            foreach (var item in keyword.SplitKeyWord())
            {
                str = str.Replace(item, "<span class=\"emfont\">" + item + "</span>");
            }
            return new HtmlString(str);
        }

        public static String ToLink(this string str)
        {
            if (!str.IsNullOrEmpty())
            {
                if (str.StartsWith("/"))
                    return str;
                return "http://" + str.RemoveStartWith("http://");
            }
            return String.Empty;
        }

        public static String[] SplitKeyWord(this String str)
        {
            Regex r = new Regex("\\s+");
            str = r.Replace(str, "Ψ");
            return str.Split('Ψ');
        }

        public static String[] Split(this String str, String split)
        {
            return str.Replace(split, "Ψ").Split('Ψ');
        }

        /// <summary>
        /// 将字符串数组合并为字符串
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="spit"></param>
        /// <returns></returns>
        public static String Merge(this String[] strs, String spit)
        {
            StringBuilder rs = new StringBuilder();
            string laststr = strs.LastOrDefault();
            foreach (string s in strs)
            {
                rs.Append(s);
                if (!s.Equals(laststr))
                    rs.Append(spit);
            }
            return rs.ToString();
        }

        /// <summary>
        /// 根据邮箱获得邮箱登陆地址
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static String GetMailStmp(this String email)
        {
            string mainAddress = email.Substring(email.LastIndexOf("@") + 1);

            string DomainAddress = mainAddress.Substring(0, mainAddress.LastIndexOf("."));
            String mailloginurl = String.Empty;
            switch (DomainAddress)
            {
                case "163":   //网易163
                    mailloginurl = "http://mail.163.com/";
                    break;
                case "126":   //  网易126
                    mailloginurl = "http://mail.126.com/";
                    break;
                case "sina":  //  sina
                    mailloginurl = "http://mail.sina.com.cn/";
                    break;
                case "yahoo": //雅虎
                    mailloginurl = "http://mail.cn.yahoo.com/";
                    break;
                case "sohu":  //  搜狐
                    mailloginurl = "http://mail.sohu.com/";
                    break;
                case "yeah":  //网易yeah.net
                    mailloginurl = "http://www.yeah.net/";
                    break;
                case "gmail": //Gmail
                    mailloginurl = "http://gmail.google.com/";
                    break;
                case "hotmail":   //Hotmail
                    mailloginurl = "http://www.hotmail.com/";
                    break;
                case "live":      //Hotmail
                    mailloginurl = "http://www.hotmail.com/";
                    break;
                case "qq":        //QQ
                    mailloginurl = "https://mail.qq.com/";
                    break;
                case "139":       //139
                    mailloginurl = "http://mail.10086.cn/";
                    break;
                default:
                    mailloginurl = String.Format("mail.{0}", mainAddress);
                    break;
            }
            return mailloginurl;
        }

        /// <summary>
        /// 将邮箱加密显示
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static String ToMailEncrypt(this String email)
        {
            return String.Format("{0}*******{1}", email.Substring(0, 2), email.Substring(email.IndexOf('@')));
        }

        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string email)
        {
            bool flag = false;
            Regex reg = new Regex("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
            if (reg.IsMatch(email))
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 顯示特定長度的字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxlen"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static String DisplayByMaxLength(this String str, int maxlen, String sub = "...")
        {
            return str.IsNullOrEmpty() ? "" : str.Length > maxlen ? str.Substring(0, maxlen) + sub : str;
        }


        #region 處理字符串的加密解密 及 MD5加密
        public static string ToPasswordEncrypt(this string originalString)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(originalString));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 转换MD5密码
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string ToMD5(this string pass)
        {
            byte[] result = Encoding.Default.GetBytes(pass);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }

        private static byte[] Keys = { 0xF4, 0xCD, 0xBA, 0x24, 0x36, 0xA8, 0xE9, 0x12 };

        private static string EncryptKey = "Xk3q0d9d";

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位  可随意定制 必须与解密的encryptKey相同</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(this string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }


        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(this string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位  可随意定制 必须与解密的encryptKey相同</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(this string encryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(EncryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }


        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(this string decryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(EncryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        #endregion

        /// <summary>
        /// 移除字符串尾部不需要的內容
        /// </summary>
        /// <param name="str"></param>
        /// <param name="removeStr"></param>
        /// <returns></returns>
        public static String RemoveEndsWith(this String str, String removeStr)
        {
            if (str != null && str.Length > 0)
                return str.EndsWith(removeStr) ? str.Substring(0, str.Length - removeStr.Length) : str;
            else
                return String.Empty;
        }

        /// <summary>
        /// 移除字符串頭部不需要內容 不区分大小写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="removeStr"></param>
        /// <returns></returns>
        public static String RemoveStartWith(this String str, String removeStr)
        {
            if (str != null && str.Length > 0)
                return str.StartsWith(removeStr,StringComparison.OrdinalIgnoreCase) ? str.Substring(removeStr.Length) : str;
            else
                return String.Empty;
        }


        /// <summary>
        ///去空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToEmptyTrimString(this object s)
        {
            if (s == null)
            {
                return string.Empty;
            }
            return s.ToString().Trim();
        }

        /// <summary>
        /// 字符串轉型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static Int32 ToInt32WithDefault(this string s, Int32 defaultvalue)
        {
            Int32 d = Int32.MinValue;
            if (Int32.TryParse(s, out d))
            {
                return d;
            }
            else
            {
                return defaultvalue;
            }
        }

        public static String MaxDisplayLen(this String str, int maxlen, String addstr = "...")
        {
            str = str.ToEmptyTrimString();
            if (str.Length > maxlen)
            {
                return str.Substring(0, maxlen) + addstr;
            }
            return str;
        }

        /// <summary>
        /// 字符串是否為Null 或 empty
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 判斷字符串是否為數字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumber(this string s)
        {
            return (s.Where(c => c.IsNumber() || s.Equals('.')).Count()) == s.Length;
        }

        /// <summary>
        /// 字符是否為數字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsNumber(this char c)
        {
            return char.IsNumber(c);
        }

        public static String ToPrice(this Decimal price)
        {
            return String.Format("{0:F}", price);
        }
    }
}
