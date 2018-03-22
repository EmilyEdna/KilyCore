using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.Extension.ValidateExtension
{
    /// <summary>
    /// 验证码管理
    /// </summary>
    public class ValidateCode
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public static string CreateValidateCode()
        {
            char[] CharArray ={
                '1','2','3','4','5','6','7','8','9',
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            };
            string randomNum = "";
            int flag = -1;//记录上次随机数的数值，尽量避免产生几个相同的随机数 
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                if (flag != -1)
                {
                    rand = new Random(i * flag * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(60);
                if (flag == t)
                {
                    return CreateValidateCode();
                }
                flag = t;
                randomNum += CharArray[t];
            }
            return randomNum;
        }
    }
}
