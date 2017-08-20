using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace UpdaterApp
{
    class Program
    {
        private static string[] Scopes = { DriveService.Scope.Drive };
        private static string ApplicationName = "PrintLableApi";
        private static string _filePath = @"D:\TEST_UPDATER\";
        private static string _idFile = "0B5A9dfwwm9dIXzhJZ091V0tOQVU";
        private static string _nameSetupFile = "FileSetup.txt";
        private static char _separate = '@';
        private static string path { get { return _filePath + _nameSetupFile; } }
        static void Main(string[] args)
        {
            Console.Title = "Update Application";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Create credential...");
            UserCredential credetial = GetUserCredential();

            Console.WriteLine("Get service...");
            DriveService service = GetDriveService(credetial);

            Console.WriteLine("Download files...");
            if (DownloadFileFromDrive(service, _idFile, _filePath))
            {
                string[] ids = ReadUpdatedFilesId();
                if (ids != null)
                {
                    foreach (string id in ids)
                    {
                        DownloadFileFromDrive(service, id, _filePath);
                    }
                }
            }
            Console.WriteLine("End...");
            Console.ReadKey();
        }
        private static UserCredential GetUserCredential()
        {
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string createPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                createPath = Path.Combine(createPath, "driveApiCredentials", "drive-credentials.json");
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "User",
                    CancellationToken.None,
                    new FileDataStore(createPath, true)).Result;
            }
        }
        private static DriveService GetDriveService(UserCredential credential)
        {
            return new DriveService(
                new BaseClientService.Initializer() { HttpClientInitializer = credential, ApplicationName = ApplicationName }
                );
        }
        private static bool DownloadFileFromDrive(DriveService service, string fileId, string filePath)
        {
            fileId = fileId.Replace("\0", "");
            string PATH = path;
            if (fileId.Contains(_separate))
            {
                var parts = fileId.Split(_separate);
                fileId = parts[0];
                PATH = _filePath + parts[1].Trim();
            }
            fileId = fileId.Trim();
            bool result = false;
            var request = service.Files.Get(fileId);

            using (var memoryStream = new MemoryStream())
            {
                request.MediaDownloader.ProgressChanged += (IDownloadProgress progress) =>
                  {
                      switch (progress.Status)
                      {
                          case DownloadStatus.Downloading:
                              Console.WriteLine(progress.BytesDownloaded);
                              break;
                          case DownloadStatus.Completed:
                              Console.WriteLine(PATH);
                              result = true;
                              break;
                          case DownloadStatus.Failed:
                              Console.WriteLine("Downloaded failed!!!");
                              result = false;
                              break;
                      }
                  };
                request.Download(memoryStream);
                using (FileStream fileStream = new FileStream(PATH, FileMode.Create, FileAccess.Write))
                {
                    fileStream.Write(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
                }
            }
            return result;
        }
        private static string[] ReadUpdatedFilesId()
        {
            string[] filesId = null;
            if (System.IO.File.Exists(path))
            {
                filesId = System.IO.File.ReadAllLines(path);
            }
            return filesId;
        }
    }
}
