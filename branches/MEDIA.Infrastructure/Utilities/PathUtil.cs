using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Reflection;
using MEDIA.Infrastructure.Properties;


namespace MEDIA.Infrastructure.Utilities
{
    /// <summary>
    /// Path Util Class
    /// </summary>
    public static class PathUtil
    {
        /// <summary>
        /// Get the current excute assembly the full physical path
        /// </summary>
        /// <param name="configName">config file's name</param>
        /// <returns>the full physical path</returns>
        public static string GetLocalPathByConfigName(string configName)
        {
            if (string.IsNullOrEmpty(configName))
            {
                throw new ArgumentException(Resources.Exception_ArgumentException_NullOrEmpty);
            }

            Uri u = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            string path = string.Format("{0}{1}{2}", Path.GetDirectoryName(u.LocalPath), Path.DirectorySeparatorChar, configName);

            if (!File.Exists(path))
            {
                throw new ArgumentException(string.Format(Resources.Exception_FileNotFound, path));
            }

            return path;
        }

        /// <summary>
        /// Get the Configuration obj
        /// </summary>
        /// <param name="configFilePath">The config file path.</param>
        /// <returns>Configuration</returns>
        public static Configuration GetConfigurationByPath(string configFilePath)
        {
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = configFilePath;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            return config;
        }

        /// <summary>
        /// Get config value by key
        /// </summary>
        /// <param name="configFilePath">The config file path.</param>
        /// <param name="key">The key.</param>
        /// <returns>value</returns>
        public static string GetConfigurationAppSettingValue(string configFilePath, string key)
        {
            Configuration config = GetConfigurationByPath(configFilePath);
            return config.AppSettings.Settings[key].Value;
        }

        /// <summary>
        /// Roots the file name and ensure target folder exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static string RootFileNameAndEnsureTargetFolderExists(string fileName)
        {
            string rootedFileName = fileName;
            if (!Path.IsPathRooted(rootedFileName))
            {
                rootedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rootedFileName);
            }

            string directory = Path.GetDirectoryName(rootedFileName);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return rootedFileName;
        }
    }
}
