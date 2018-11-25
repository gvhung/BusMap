using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BusMap.Mobile.Droid;
using BusMap.Mobile.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelperDroid))]
namespace BusMap.Mobile.Droid
{
    public class FileHelperDroid : IFileHelper
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