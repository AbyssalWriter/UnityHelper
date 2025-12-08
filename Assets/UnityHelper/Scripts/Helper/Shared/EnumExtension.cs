namespace Helper.Shared
{
    public static class EnumExtention
    {
        public static int ToInt(this System.Enum e)
        {
            return System.Convert.ToInt32(e);
        }
    }
}