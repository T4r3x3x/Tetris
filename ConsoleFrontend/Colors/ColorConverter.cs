using System.Drawing;

namespace ConsoleFrontend.Colors
{
    public static class ColorConverter
    {
        public static ConsoleColor ClosestConsoleColor(Color color)
        {
            ConsoleColor ret = 0;

            double rr = color.R, gg = color.G, bb = color.B, delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                var c = Color.FromName(n == "DarkYellow" ? "Orange" : n);
                var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }
    }
}