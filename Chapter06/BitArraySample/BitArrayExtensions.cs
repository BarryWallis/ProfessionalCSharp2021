using System.Collections;
using System.Text;

namespace BitArraySample;
public static class BitArrayExtensions
{
    public static string GetBitsFormat(this BitArray bitArray)
    {
        StringBuilder sb = new();
        for (int i = bitArray.Length - 1; i >= 0; i--)
        {
            _ = sb.Append(bitArray[i] ? 1 : 0);
            if (i != 0 && i % 4 == 0)
            {
                _ = sb.Append('_');
            }
        }

        return sb.ToString();
    }
}
