using System;
using System.Linq;
using System.Reflection;

namespace StrimmBL.Downloadable
{
    public static class DownloadableFactory
    {
        public static IDownloadableObject Create(string type)
        {
            var instType = Assembly.GetAssembly(typeof(IDownloadableObject)).GetTypes()
                .FirstOrDefault(x => x.IsClass && typeof(IDownloadableObject).IsAssignableFrom(x) && x.Name.ToLower().Contains(type.ToLower()));

            if (instType == null) return null;

            var instance = Activator.CreateInstance(instType) as IDownloadableObject;

            return instance;
        }
    }
}
