namespace TetrisEngine.Extensions
{
    public static class RandomExtensions
    {
        public static TEnum NextEnumValue<TEnum>(this Random random) where TEnum : struct, Enum
        {
            var enumValues = Enum.GetValues<TEnum>();
            return enumValues[random.Next(enumValues.Length)];
        }
    }
}
