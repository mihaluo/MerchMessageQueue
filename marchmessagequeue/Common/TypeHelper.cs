using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarchMessageQueue.Common
{
    public static class TypeHelper
    {
        public static Type[] GetTypesByBaseType(Type baseType)
        {
            if (baseType == null) throw new ArgumentNullException(nameof(baseType));

            IEnumerable<Type> types = baseType.Assembly.GetTypes()
                                                       .Where(t=>baseType.IsAssignableFrom(t) && t != baseType);
                    
            return types.ToArray();
        }

        public static Type[] GetTypesByInterfaceType(Type interfacType)
        {
            if (interfacType == null) throw new ArgumentNullException(nameof(interfacType));

            Assembly assembly = interfacType.Assembly;

            List<Type> types = new List<Type>();
            foreach (var type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
            {
                Type foundInterfaceType = type.GetInterface(interfacType.Name);
                if (foundInterfaceType != null)
                {
                    types.Add(type);
                }
            }

            return types.ToArray();
        }
    }
}