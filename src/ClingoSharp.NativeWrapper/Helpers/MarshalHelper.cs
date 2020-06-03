using System;

namespace ClingoSharp.NativeWrapper.Helpers
{
    public static class MarshalHelper
    {
        public static void Copy(IntPtr source, ushort[] destination, int startIndex, int length)
        {
            if (source == IntPtr.Zero)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (startIndex < 0 || startIndex > destination.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length < 0 || length > (destination.Length + startIndex))
                throw new ArgumentOutOfRangeException(nameof(length));

            unsafe
            {
                ushort* sourcePtr = (ushort*)source;
                for (int i = startIndex; i < startIndex + length; ++i)
                {
                    destination[i] = *sourcePtr++;
                }
            }
        }

        public static void Copy(IntPtr source, uint[] destination, int startIndex, int length)
        {
            if (source == IntPtr.Zero)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (startIndex < 0 || startIndex > destination.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length < 0 || length > (destination.Length + startIndex))
                throw new ArgumentOutOfRangeException(nameof(length));

            unsafe
            {
                uint* sourcePtr = (uint*)source;
                for (int i = startIndex; i < startIndex + length; ++i)
                {
                    destination[i] = *sourcePtr++;
                }
            }
        }

        public static void Copy(IntPtr source, ulong[] destination, int startIndex, int length)
        {
            if (source == IntPtr.Zero)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (startIndex < 0 || startIndex > destination.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (length < 0 || length > (destination.Length + startIndex))
                throw new ArgumentOutOfRangeException(nameof(length));

            unsafe
            {
                ulong* sourcePtr = (ulong*)source;
                for (int i = startIndex; i < startIndex + length; ++i)
                {
                    destination[i] = *sourcePtr++;
                }
            }
        }
    }
}
