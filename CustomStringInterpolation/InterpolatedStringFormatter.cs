using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomStringInterpolation
{
    public class InterpolatedStringFormatter : IInterpolatedStringFormatter
    {
        private readonly IGetCompositeFormatStringDescriptionAction getCompositeFormatStringDescriptionAction;
        private readonly IInstanceToDictionaryConverter instanceConverter;

        public InterpolatedStringFormatter(
            IGetCompositeFormatStringDescriptionAction getCompositeFormatStringDescriptionAction,
            IInstanceToDictionaryConverter instanceConverter)
        {            
            this.getCompositeFormatStringDescriptionAction = getCompositeFormatStringDescriptionAction;
            this.instanceConverter = instanceConverter;
        }

        public string Format<TInstance>(string format, TInstance instance)
        {
            var templateDescription = getCompositeFormatStringDescriptionAction.Execute(format);
            var values = GetValuesInArray(instance, templateDescription.OrderedPlaceholderNames);

            return string.Format(templateDescription.CompositeFormatString, values);
        }

        private dynamic[] GetValuesInArray<TInstance>(
            TInstance instance,
            IList<string> orderedPlaceholderNames)
        {
            var propertyValues = instanceConverter.Convert(instance);

            return
                propertyValues
                    .Where(k => orderedPlaceholderNames.Contains(k.Key, StringComparer.OrdinalIgnoreCase))
                    .OrderBy(k => orderedPlaceholderNames.IndexOf(k.Key))
                    .Select(k => k.Value)
                    .ToArray();
        }
    }
}
