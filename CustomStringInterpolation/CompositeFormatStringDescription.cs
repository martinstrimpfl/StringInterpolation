using System.Collections.Generic;

namespace CustomStringInterpolation
{
    public class CompositeFormatStringDescription : ICompositeFormatStringDescription
    {
        public CompositeFormatStringDescription(string template, List<string> placeholders)
        {
            CompositeFormatString = template;
            OrderedPlaceholderNames = placeholders.AsReadOnly();
        }

        public string CompositeFormatString { get; }

        public IList<string> OrderedPlaceholderNames { get; }
    }
}
