using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace MEDIA.Infrastructure.Utilities
{
  
    /// <summary>
    /// Unity Clasee
    /// </summary>
    public static class UnityUtil
    {
        /// <summary>
        /// get UnityConfigurationSection 
        /// </summary>
        /// <param name="configFilePath">configFile Path</param>
        /// <returns>UnityConfigurationSection entity</returns>
        public static UnityConfigurationSection LoadUnityConfiguration(string configFilePath)
        {
            return LoadUnityConfiguration(configFilePath, UnityConfigurationSection.SectionName);
        }

        /// <summary>
        /// get UnityConfigurationSection 
        /// </summary>
        /// <param name="configFilePath">configFile Path</param>
        /// <param name="configSectionName">section name</param>
        /// <returns>UnityConfigurationSection entity</returns>
        public static UnityConfigurationSection LoadUnityConfiguration(string configFilePath, string configSectionName)
        {
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = configFilePath;

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            ConfigurationSection section = config.GetSection(configSectionName);

            return section as UnityConfigurationSection;
        }

        /// <summary>
        /// Load Unity into Container
        /// </summary>
        /// <param name="configFileName">configFile output path</param>
        /// <param name="container">IUnityContainer</param>
        public static void LoadUnityConfigurationSectionToContainer(string configFileName, IUnityContainer container)
        {
            string path = PathUtil.GetLocalPathByConfigName(configFileName);
            UnityConfigurationSection section = UnityUtil.LoadUnityConfiguration(path);
            section.Configure(container);
        }
    }
}
