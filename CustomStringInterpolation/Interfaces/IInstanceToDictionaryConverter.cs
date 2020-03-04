using System.Collections.Generic;

namespace CustomStringInterpolation
{
    public interface IInstanceToDictionaryConverter
    {
        Dictionary<string, dynamic> Convert<TInstance>(TInstance instance);
    }
}
