using System;

namespace GalaxyLogic
{
    public class GalaxyEngine
    {
        private Random _rnd = new Random();
        public double GetFunctional(double d, double R, double power)
        {
            if (d > R) return 0;
            return Math.Pow(1.0 - d / R, power);
        }

        public bool ShouldPlaceStar(double functionalValue)
        {
            return _rnd.NextDouble() <= functionalValue;
        }

        public bool ShouldPlaceBackgroundStar(double density = 0.001)
        {
            return _rnd.NextDouble() <= density;
        }

        public bool CalculateSpiral(double x, double y, double cx, double cy, double maxR)
        {
            double dx = x - cx;
            double dy = y - cy;
            double r = Math.Sqrt(dx * dx + dy * dy);

            if (r > maxR) return false;
            double coreF = GetFunctional(r, maxR * 0.25, 0.8);
            if (ShouldPlaceStar(coreF * 0.7)) return true;
            double twist = 3.5;
            double angle = (r / maxR) * Math.PI * twist;
            double nx = dx * Math.Cos(angle) + dy * Math.Sin(angle);
            double ny = -dx * Math.Sin(angle) + dy * Math.Cos(angle);


            double armThickness = maxR / 2.5;

            double fX = GetFunctional(r, maxR, 0.4);
            double fY = GetFunctional(Math.Abs(ny), armThickness, 0.9);
            double diskGlow = GetFunctional(r, maxR, 1.5) * 0.15;

            return ShouldPlaceStar((fX * fY) + diskGlow);
        }

        public bool CalculateElliptical(double x, double y, double cx, double cy, double radius, double power)
        {
            double d = Math.Sqrt(Math.Pow(x - cx, 2) + Math.Pow(y - cy, 2));
            return ShouldPlaceStar(GetFunctional(d, radius, power));
        }

        public bool CalculateAlmond(double x, double y, double cx, double cy, double a, double b, double power)
        {
            double dx = x - cx;
            double dy = y - cy;
            double value = (dx * dx) / (a * a) + (dy * dy) / (b * b);
            if (value > 1.0) return false;
            return ShouldPlaceStar(Math.Pow(1.0 - value, power));
        }
    }
}