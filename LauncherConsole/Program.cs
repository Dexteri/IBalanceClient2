using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LauncherConsole
{
    class Program
    {
        private static FtpClient ftpClient;
        private static string pathToSource = "/www/giriki.autobanner.biz/orderManager/";
        private static string locationApp = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("LauncherConsole.exe","");
        public static void Init()
        {
            ftpClient = new FtpClient();
            ftpClient.Host = "s15.thehost.com.ua";
            ftpClient.UserName = "vitek";
            ftpClient.Password = "OJgIr8va";
            ftpClient.UseSSL = false;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Console.ForegroundColor = ConsoleColor.Red;
        }
        static void Main(string[] args)
        {
            Init();
            DownLoad(locationApp, pathToSource);
            Console.WriteLine("Complete.....");
            Console.ReadKey();
        }
        private static void DownLoad(string localPath, string ftpPath)
        {
            FileStruct[] directory = ftpClient.ListDirectory(ftpPath);
            foreach (FileStruct file in directory)
            {
                Console.WriteLine("Start copy {0} to {1}", file.Name, localPath);
                string newPath = localPath + file.Name;
                if (file.IsDirectory)
                {
                    if (!Directory.Exists(newPath))
                    {
                        Console.WriteLine("Created directory: {0}....", file.Name);
                        Directory.CreateDirectory(newPath);
                    }
                    DownLoad(newPath, ftpPath + file.Name + "/");
                }
                else
                {
                    string time = GetCreateDate(file.Name).ToString("MMM  d hh:mm");
                    if (!FileExist(file.Name) || !time.Equals(file.CreateTime))
                    {
                        ftpClient.DownloadFile(ftpPath, file.Name, localPath);
                    }
                }
                Console.WriteLine("End copy {0} to {1}", file.Name, localPath);
            }
        }
        public static bool FileExist(string nameFile)
        {
            return File.Exists(locationApp + nameFile);
        }
        public static DateTime GetCreateDate(string nameFile)
        {
            return File.GetLastWriteTime(locationApp + nameFile);
        }
    }
}
