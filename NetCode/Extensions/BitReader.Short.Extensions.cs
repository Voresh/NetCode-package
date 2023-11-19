using System.Runtime.CompilerServices;
using NetCode.Limits;

namespace NetCode
{
    public static class BitReaderShortExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadShort(this BitReader reader, ShortLimit limit)
        {
            var value = (short)reader.ReadBits(limit.BitCount);
            return (short)(value + limit.Min);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadShort(this BitReader reader, short baseline)
        {
            var isChanged = reader.ReadBool();
            if (isChanged)
            {
                return reader.ReadShort();
            }

            return baseline;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadShort(this BitReader reader, short baseline, ShortLimit limit)
        {
            var isChanged = reader.ReadBool();
            if (isChanged)
            {
                return reader.ReadShort(limit);
            }

            return baseline;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadShort(this BitReader reader, short baseline, ShortLimit limit, ShortLimit diffLimit)
        {
            var isChanged = reader.ReadBool();
            if (isChanged)
            {
                var isDiff = reader.ReadBool();

                if (isDiff)
                {
                    var diff = reader.ReadShort(diffLimit);
                    return (short)(baseline + diff);
                }

                return reader.ReadShort(limit);
            }

            return baseline;
        }
    }
}