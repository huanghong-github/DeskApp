using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace DeskApp.Utils
{
    internal class ProcessUtil
    {
        public static string RunApp(string filename, string arguments = "", string workingDirectory = "", bool recordLog = false)
        {
            try
            {
                if (recordLog)
                {
                    Trace.WriteLine(filename + " " + arguments);
                }
                bool isNotCmdOrPowerShell = !(arguments == string.Empty && new string[] { "powershell.exe", "cmd.exe" }.Select(filename.EndsWith).Any());
                Process proc = new Process
                {
                    StartInfo =
                    {
                        FileName = filename,
                        CreateNoWindow = isNotCmdOrPowerShell,
                        Arguments = arguments,
                        RedirectStandardOutput = isNotCmdOrPowerShell,
                        UseShellExecute = false,
                        WorkingDirectory=workingDirectory
                    }
                };
                _ = proc.Start();

                if (isNotCmdOrPowerShell && recordLog)
                {
                    StreamReader sr = new StreamReader(proc.StandardOutput.BaseStream, Encoding.Default);

                    //上面标记的是原文，下面是我自己调试错误后自行修改的
                    Thread.Sleep(100); //貌似调用系统的nslookup还未返回数据或者数据未编码完成，程序就已经跳过直接执行
                    if (!proc.HasExited) //在无参数调用nslookup后，可以继续输入命令继续操作，如果进程未停止就直接执行
                    {
                        proc.Kill();
                    }

                    string txt = sr.ReadToEnd();
                    Trace.WriteLine(txt);
                    return txt;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取操作系统版本
        /// </summary>
        /// <returns></returns>
        public static string GetOsVersion()
        {
            return Environment.OSVersion.VersionString;
        }

        public static string RunCmd(string command)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException();
            }
            Process proc = new Process()
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
            try
            {
                _ = proc.Start();
                proc.StandardInput.WriteLine(command);
                proc.StandardInput.WriteLine("exit");
                proc.StandardInput.AutoFlush = true;
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                return output;
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
        }
        /// <summary>
        /// UseShellExecute = true, 类似cmd /c start filename，可能有潜在的安全风险
        /// </summary>
        /// <param name="filename"></param>
        public static void RunShell(string filename)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException();
            }
            try
            {
                Process proc = new Process
                {
                    StartInfo =
                    {
                        FileName = filename,
                        CreateNoWindow = true,
                        UseShellExecute = true
                    }
                };
                _ = proc.Start();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}
