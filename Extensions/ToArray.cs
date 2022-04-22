using System.Collections.Generic;

namespace Extensions
{
    public static class Extensions
    {
        public static byte[] ToArray(this ulong number)
        {
            var values = new Stack<byte>(16);

            while (number != 0)
            {
                values.Push((byte)(number % 10));
                number /= 10;
            }
            return values.ToArray();
        }
    }
}
