using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Helpers
{
    public interface IFileHelper
    {
        bool CheckIfFileExist(string fileName);
        string GetLocalFilePath(string fileName);
        void CreateDirectory(string fileName);
    }
}
