
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BusMap.Mobile.Droid;
using BusMap.Mobile.Helpers;
using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.LayoutRenderers.Wrappers;
using NLog.Targets;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Environment = Android.OS.Environment;
using ILogger = BusMap.Mobile.Helpers;

[assembly: Dependency(typeof(NLogManager))]
namespace BusMap.Mobile.Droid
{
    public class NLogManager : ILogManager
    {
        public NLogManager()
        {
            var config = new LoggingConfiguration();
            var folderPath = Path.Combine(Environment.ExternalStorageDirectory.Path,
                "BusMap");

            Assembly assembly = Assembly.Load("BusMap.Mobile.Android");
            NLog.Config.ConfigurationItemFactory.Default.RegisterItemsFromAssembly(assembly);

            var consoleTarget = new ConsoleTarget("consoleTarget");
            var fileTarget = new FileTarget("fileTarget")
            {

                FileName = Path.Combine(folderPath, "Log.txt"),
                DeleteOldFileOnStartup = true,
                Header = "AndroidLog".PadLeft(20),
                Layout = "${longdate}|${level:uppercase=true}|${message}|${customlayout:${logger}}"
            };
            var fileCriticalTarget = new FileTarget("fileCriticalTarget")
            {
                FileName = Path.Combine(folderPath, "CriticalLog.txt"),
                DeleteOldFileOnStartup = true,
                Header = "AndroidCriticalLog".PadLeft(20),
                Layout = "${longdate}|${level:uppercase=true}|${message}|${customlayout:${logger}}" +
                         "|${exception:format=ToString,StackTrace}${newline}"
            };

            config.AddTarget(consoleTarget);
            config.AddTarget(fileTarget);
            config.AddTarget(fileCriticalTarget);

            config.AddRuleForAllLevels(consoleTarget);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, fileCriticalTarget));

            LogManager.Configuration = config;    
        }

        public ILogger.ILogger GetLog([System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "")
        {
            string fileName = callerFilePath;

            if (fileName.Contains("/"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("/", StringComparison.CurrentCultureIgnoreCase) + 1);
            }

            var logger = LogManager.GetLogger(fileName);
            return new NLogLogger(logger);
        }
    }

    [LayoutRenderer("customlayout")]
    [ThreadAgnostic]
    public sealed class LoggerPrefixRendererWrapper : WrapperLayoutRendererBase
    {
        protected override string Transform(string text)
        {
            var textArray = text.Split('\\');
            Array.Reverse(textArray);
            string result = "";

            int i = 0;
            while (!result.StartsWith(@"\BusMap.Mobile"))
            {
                result = "\\" + textArray[i] + result;
                i++;
            }

            result = result.TrimStart('\\');
            return result;
        }
    }
}