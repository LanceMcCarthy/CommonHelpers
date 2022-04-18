using System;
using System.Collections.Generic;
using System.Text;

namespace CommonHelpers.Extensions
{
    public static class EasingExtensions
    {
        // BACK

        public static float BackEaseIn(this float p) => (float)(p * p * p - p * Math.Sin(p * Math.PI));

        public static float BackEaseOut(this float p) => (float)(1 - ((float)Math.Pow(1 - p, 3) - (1 - p) * Math.Sin((1 - p) * Math.PI)));

        public static float BackEaseInOut(this float p)
        {
            if (p < 0.5f)
            {
                return (float)(0.5f * ((float)Math.Pow(2 * p, 3) - 2 * p * Math.Sin(2 * p * Math.PI)));
            }

            return (float)(0.5f * (1 - ((float)Math.Pow(1 - (2 * p - 1), 3) - (1 - (2 * p - 1)) * Math.Sin((1 - (2 * p - 1)) * Math.PI))) + 0.5f);
        }


        // BOUNCE

        public static float BounceEaseIn(this float p) => 1 - BounceEaseOut(1 - p);

        public static float BounceEaseOut(this float p)
        {
            if (p < 4 / 11.0f)
            {
                return 121 * (float)Math.Pow(p, 2) / 16.0f;
            }

            if (p < 8 / 11.0f)
            {
                return 363 / 40.0f * (float)Math.Pow(p, 2) - 99 / 10.0f * p + 17 / 5.0f;
            }

            if (p < 9 / 10.0f)
            {
                return 4356 / 361.0f * (float)Math.Pow(p, 2) - 35442 / 1805.0f * p + 16061 / 1805.0f;
            }

            return 54 / 5.0f * (float)Math.Pow(p, 2) - 513 / 25.0f * p + 268 / 25.0f;
        }

        public static float BounceEaseInOut(this float p)
        {
            if (p < 0.5f)
            {
                return 0.5f * BounceEaseIn(p * 2);
            }

            return 0.5f * BounceEaseOut(p * 2 - 1) + 0.5f;
        }


        // CUBIC

        public static float CubicEaseIn(this float p) => (float)Math.Pow(p, 3);

        public static float CubicEaseOut(this float p) => (float)Math.Pow(p - 1, 3) + 1;

        public static float CubicEaseInOut(this float p)
        {
            if (p < 0.5f)
            {
                return 4 * (float)Math.Pow(p, 3);
            }

            return 0.5f * (float)Math.Pow(2 * p - 2, 3) + 1;
        }


        // CIRCULAR

        public static float CircularEaseIn(this float p) => (float)(1 - Math.Sqrt(1 - p * p));

        public static float CircularEaseOut(this float p) => (float)Math.Sqrt((2 - p) * p);

        public static float CircularEaseInOut(this float p)
        {
            if (p < 0.5f)
            {
                return (float)(0.5f * (1 - Math.Sqrt(1 - 4 * (p * p))));
            }

            return (float)(0.5f * (Math.Sqrt(-(2 * p - 3) * (2 * p - 1)) + 1));
        }


        // ELASTIC

        public static float ElasticEaseIn(this float p) => (float)(Math.Sin(13 * (Math.PI / 2) * p) * (float)Math.Pow(2, 10 * (p - 1)));

        public static float ElasticEaseOut(this float p) => (float)(Math.Sin(-13 * (Math.PI / 2) * (p + 1)) * (float)Math.Pow(2, -10 * p) + 1);

        public static float ElasticEaseInOut(this float p)
        {
            if (p < 0.5f)
            {
                return (float)(0.5f * Math.Sin(13 * (Math.PI / 2) * (2 * p)) * (float)Math.Pow(2, 10 * (2 * p - 1)));
            }

            return (float)(0.5f * (Math.Sin(-13 * (Math.PI / 2) * (2 * p - 1 + 1)) * (float)Math.Pow(2, -10 * (2 * p - 1)) + 2));
        }


        // EXPONENTIAL

        public static float ExponentialEaseIn(this float p) => p == 0.0f ? p : (float)Math.Pow(2, 10 * (p - 1));

        public static float ExponentialEaseOut(this float p) => Math.Abs(p - 1.0f) < 0.01 ? p : 1 - (float)Math.Pow(2, -10 * p);

        public static float ExponentialEaseInOut(this float p)
        {
            if (p == 0.0 || Math.Abs(p - 1.0) < 0.01) return p;

            if (p < 0.5f)
            {
                return (float)(0.5f * (float)Math.Pow(2, 20 * p - 10));
            }

            return (float)(-0.5f * (float)Math.Pow(2, -20 * p + 10) + 1);
        }


        // LINEAR

        public static float Linear(this float p) => p;


        // QUADRATIC

        public static float QuadraticEaseIn(this float p) => (float)Math.Pow(p, 2);

        public static float QuadraticEaseOut(this float p) => -(p * (p - 2));

        public static float QuadraticEaseInOut(this float p)
        {
            if (p < 0.5f)
            {
                return 2 * (float)Math.Pow(p, 2);
            }

            return -2 * (float)Math.Pow(p, 2) + 4 * p - 1;
        }


        // QUARTIC

        public static float QuarticEaseIn(this float p) => (float)Math.Pow(p, 4);

        public static float QuarticEaseOut(this float p) => (float)Math.Pow(p - 1, 3) * (1 - p) + 1;

        public static float QuarticEaseInOut(this float p)
        {
            if (p < 0.5f)
            {
                return 8 * (float)Math.Pow(p, 4);
            }

            return -8 * (float)Math.Pow(p - 1, 4) + 1;
        }


        // QUINTIC

        public static float QuinticEaseIn(this float p) => (float)Math.Pow(p, 5);

        public static float QuinticEaseOut(this float p) => (float)Math.Pow(p - 1, 5) + 1;

        public static float QuinticEaseInOut(this float p)
        {
            if (p < 0.5f)
            {
                return 16 * (float)Math.Pow(p, 5);
            }

            return 0.5f * (float)Math.Pow(2 * p - 2, 5) + 1;
        }


        // SINE

        public static float SineEaseIn(this float p) => (float)Math.Sin((p - 1) * (Math.PI / 2)) + 1;

        public static float SineEaseOut(this float p) => (float)Math.Sin(p * (Math.PI / 2));

        public static float SineEaseInOut(this float p) => (float)(0.5f * (1 - Math.Cos(p * Math.PI)));
    }
}
