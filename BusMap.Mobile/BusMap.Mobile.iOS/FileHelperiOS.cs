using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.iOS;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelperiOS))]
namespace BusMap.Mobile.iOS
{
    public class FileHelperiOS : IFileHelper
    {
        private readonly string _path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool CheckIfFileExist(string fileName)
            => Directory.Exists(Path.Combine(_path, fileName));

        public string GetLocalFilePath(string fileName)
            => Path.Combine(_path, fileName);

        public void CreateDirectory(string fileName)
            => Directory.CreateDirectory(Path.Combine(_path, fileName));
    }
}