using System.Reflection;
using System.Linq;
using System;

namespace IcatuzinhoApp
{
    public static class Mapper
    {
        public static Object Setup(object source, object destination)
        {
            var propSource = source.GetType().GetRuntimeProperties();

            foreach (var s in propSource)
            {
                var prop = destination.GetType().GetRuntimeProperty(s.Name);

                if (prop != null)
                {
                    var sourceValue = s.GetValue(source);
                    destination.GetType().GetRuntimeProperty(s.Name).SetValue(destination, sourceValue);
                }
            }

            return destination;
        }
    }
}

