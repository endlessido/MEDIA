using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace MEDIA.Infrastructure.Ioc
{
    /// <summary>
    /// Dependency Injection Context
    /// </summary>
    /// <remarks></remarks>
    public class IoCContext
    {
        /// <summary>
        /// Define UnityContainer
        /// </summary>
        private static IUnityContainer container;

        /// <summary>
        /// Get UnityContainer
        /// </summary>
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = new UnityContainer();
                }

                return container;
            }
        }

        /// <summary>
        /// Dispose UnityContainer
        /// </summary>
        /// <remarks></remarks>
        public static void CleanUp()
        {
            if (container != null)
            {
                container.Dispose();
            }
        }
    }
}
