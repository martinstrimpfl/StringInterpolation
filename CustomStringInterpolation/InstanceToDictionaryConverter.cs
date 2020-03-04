using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomStringInterpolation
{
    public class InstanceToDictionaryConverter : IInstanceToDictionaryConverter
    {
        public Dictionary<string, dynamic> Convert<TInstance>(TInstance instance)
        {
            var propertyNameValueDictionary = new Dictionary<string, dynamic>();
            GetAggregatedDomainObjectsTraversingRecursively(string.Empty, instance, propertyNameValueDictionary);
            return propertyNameValueDictionary;
        }

        private static void GetAggregatedDomainObjectsTraversingRecursively<TInstance>(
            string previousName,
            TInstance someInstance,
            Dictionary<string, dynamic> propertyNameValueDictionary)
        {
            if (someInstance == null)
            {
                return;
            }

            var propertyInfoList =
                someInstance
                .GetType()
                .GetProperties()
                .Where(propertyInfo => propertyInfo.DeclaringType == someInstance.GetType())
                .ToList();

            // Iterate through all the properties
            foreach (var property in propertyInfoList)
            {
                dynamic item = property.GetValue(someInstance);

                if (item != null)
                {
                    var key =
                        string.IsNullOrEmpty(previousName)
                            ? property.Name
                            : previousName + "." + property.Name;

                    propertyNameValueDictionary.Add(key, item);

                    if ((property.PropertyType.IsClass || property.PropertyType.IsInterface)
                        &&
                        !IsNotGenericList(property.PropertyType)
                        &&
                        property.PropertyType != typeof(string)
                        &&
                        !property.PropertyType.GetInterfaces().Any(ty => ty is IEnumerable))
                    {
                        // Recursive call to scan through and find child object properties
                        // ignore lists, dictionaries and similar types
                        GetAggregatedDomainObjectsTraversingRecursively(key, item, propertyNameValueDictionary);
                    }
                }
            }
        }

        private static bool IsNotGenericList(Type propertyType)
        {
            return
                propertyType.IsGenericType
                &&
                (propertyType.GetGenericTypeDefinition() == typeof(List<>)
                 ||
                 propertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>));
        }
    }
}
