using System.Collections.Generic;

namespace CustomStringInterpolation
{
    public interface ICompositeFormatStringDescription
    {
        string CompositeFormatString { get; }

        IList<string> OrderedPlaceholderNames { get; }
    }
}
