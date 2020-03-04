using System;
using System.Collections.Generic;

namespace CustomStringInterpolation
{
    public interface IInterpolatedStringFormatter
    {
        string Format<TInstance>(string format, TInstance instance);
    }
}
