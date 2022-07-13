using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL;
internal static class StringExtensions
{
    public static ReadOnlySpan<char> AsSpan(this string str, Range range)
    {
        var (offset, length) = range.GetOffsetAndLength(str.Length);

        return str.AsSpan(offset, length);
    }
}
