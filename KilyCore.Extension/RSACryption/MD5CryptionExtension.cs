using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using KilyCore.Configure;

/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Extension.RSACryption
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class MD5CryptionExtension
    {
        public static string MD5HASH(string source)
        {
            MD5 md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x2"));
            }
            return sb.ToString().ToUpper();
        }
        public static string Command() {
           Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;    
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            proc.StandardInput.WriteLine("netstat -ano&exit");
            proc.StandardInput.AutoFlush = true;
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();//等待程序执行完退出进程
            proc.Close();
            return output;
        }
        public static string GetCommand() {
            return Configer.ConnentionString;
        }
    }
}