using System;

namespace Helper.Shared
{
    public static class EnumExtention
    {
        public static int ToInt(this Enum e)
        {
            return System.Convert.ToInt32(e);
        }
    }
}