//-----------------------------------------------------------------------
// <copyright file="ReflectionHelper.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Beto.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using Microsoft.Extensions.DependencyModel;

    /// <summary>
    /// Reflection Helper
    /// </summary>
    public class ReflectionHelper
    {
        /// <summary>
        /// Gets the types on project. <![CDATA[Retorna los tipos existentes en la solución que implementen determinada interfaz]]>
        /// </summary>
        /// <param name="typeSearched">The type searched.</param>
        /// <param name="projectalias">searches in DLLs that contain this word</param>
        /// <returns>the value</returns>
        public static IEnumerable<Type> GetTypesOnProject(Type typeSearched, string projectalias)
        {
            var deps = DependencyContext.Default;

            var assemblies = new List<Assembly>();

            foreach (var library in deps.RuntimeLibraries.Where(c => c.Name.ToLower().Contains(projectalias.ToLower())))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }

            var allTypes = assemblies.SelectMany(c => c.ExportedTypes);

            var foundTypes = new List<Type>();

            var typeSearchedInfo = typeSearched.GetTypeInfo();

            foreach (var t in allTypes)
            {
                var name = t.Name;
                var typeInfo = t.GetTypeInfo();

                if (t.GetTypeInfo().IsInterface)
                {
                    continue;
                }

                if (typeSearched.IsAssignableFrom(t) || (typeSearchedInfo.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(typeInfo, typeSearched)))
                {
                    foundTypes.Add(t);
                }
            }

            return foundTypes;
        }

        public static byte[] ObjectToByteArray(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Validates if does the type implement open generic.
        /// </summary>
        /// <param name="typeinfo">The type.</param>
        /// <param name="openGeneric">The open generic.</param>
        /// <returns>the value</returns>
        private static bool DoesTypeImplementOpenGeneric(TypeInfo typeinfo, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();

                foreach (var implementedInterface in typeinfo.FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.GetTypeInfo().IsGenericType)
                    {
                        continue;
                    }

                    var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                    return isMatch;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}