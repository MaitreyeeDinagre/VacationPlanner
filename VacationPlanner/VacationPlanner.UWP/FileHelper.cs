using System.IO;
using Xamarin.Forms;
using Windows.Storage;
using VacationPlanner.UWP;

[assembly: Dependency(typeof(FileHelper))]

namespace VacationPlanner.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}