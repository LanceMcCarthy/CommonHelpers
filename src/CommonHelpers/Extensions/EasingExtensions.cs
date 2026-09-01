using System;

namespace CommonHelpers.Extensions;

public static class EasingExtensions
{
    // BACK

    extension(float p)
    {
        public float BackEaseIn() => (float)(p * p * p - p * Math.Sin(p * Math.PI));
        public float BackEaseOut() => (float)(1 - ((float)Math.Pow(1 - p, 3) - (1 - p) * Math.Sin((1 - p) * Math.PI)));

        public float BackEaseInOut()
        {
            if (p < 0.5f)
            {
                return (float)(0.5f * ((float)Math.Pow(2 * p, 3) - 2 * p * Math.Sin(2 * p * Math.PI)));
            }

            return (float)(0.5f * (1 - ((float)Math.Pow(1 - (2 * p - 1), 3) - (1 - (2 * p - 1)) * Math.Sin((1 - (2 * p - 1)) * Math.PI))) + 0.5f);
        }

        public float BounceEaseIn() => 1 - BounceEaseOut(1 - p);

        public float BounceEaseOut()
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

        public float BounceEaseInOut()
        {
            if (p < 0.5f)
            {
                return 0.5f * BounceEaseIn(p * 2);
            }

            return 0.5f * BounceEaseOut(p * 2 - 1) + 0.5f;
        }

        public float CubicEaseIn() => (float)Math.Pow(p, 3);
        public float CubicEaseOut() => (float)Math.Pow(p - 1, 3) + 1;

        public float CubicEaseInOut()
        {
            if (p < 0.5f)
            {
                return 4 * (float)Math.Pow(p, 3);
            }

            return 0.5f * (float)Math.Pow(2 * p - 2, 3) + 1;
        }

        public float CircularEaseIn() => (float)(1 - Math.Sqrt(1 - p * p));
        public float CircularEaseOut() => (float)Math.Sqrt((2 - p) * p);

        public float CircularEaseInOut()
        {
            if (p < 0.5f)
            {
                return (float)(0.5f * (1 - Math.Sqrt(1 - 4 * (p * p))));
            }

            return (float)(0.5f * (Math.Sqrt(-(2 * p - 3) * (2 * p - 1)) + 1));
        }

        public float ElasticEaseIn() => (float)(Math.Sin(13 * (Math.PI / 2) * p) * (float)Math.Pow(2, 10 * (p - 1)));
        public float ElasticEaseOut() => (float)(Math.Sin(-13 * (Math.PI / 2) * (p + 1)) * (float)Math.Pow(2, -10 * p) + 1);

        public float ElasticEaseInOut()
        {
            if (p < 0.5f)
            {
                return (float)(0.5f * Math.Sin(13 * (Math.PI / 2) * (2 * p)) * (float)Math.Pow(2, 10 * (2 * p - 1)));
            }

            return (float)(0.5f * (Math.Sin(-13 * (Math.PI / 2) * (2 * p - 1 + 1)) * (float)Math.Pow(2, -10 * (2 * p - 1)) + 2));
        }

        public float ExponentialEaseIn() => p == 0.0f ? p : (float)Math.Pow(2, 10 * (p - 1));
        public float ExponentialEaseOut() => Math.Abs(p - 1.0f) < 0.01 ? p : 1 - (float)Math.Pow(2, -10 * p);

        public float ExponentialEaseInOut()
        {
            if (p == 0.0 || Math.Abs(p - 1.0) < 0.01) return p;

            if (p < 0.5f)
            {
                return (float)(0.5f * (float)Math.Pow(2, 20 * p - 10));
            }

            return (float)(-0.5f * (float)Math.Pow(2, -20 * p + 10) + 1);
        }

        public float Linear() => p;
        public float QuadraticEaseIn() => (float)Math.Pow(p, 2);
        public float QuadraticEaseOut() => -(p * (p - 2));

        public float QuadraticEaseInOut()
        {
            if (p < 0.5f)
            {
                return 2 * (float)Math.Pow(p, 2);
            }

            return -2 * (float)Math.Pow(p, 2) + 4 * p - 1;
        }

        public float QuarticEaseIn() => (float)Math.Pow(p, 4);
        public float QuarticEaseOut() => (float)Math.Pow(p - 1, 3) * (1 - p) + 1;

        public float QuarticEaseInOut()
        {
            if (p < 0.5f)
            {
                return 8 * (float)Math.Pow(p, 4);
            }

            return -8 * (float)Math.Pow(p - 1, 4) + 1;
        }

        public float QuinticEaseIn() => (float)Math.Pow(p, 5);
        public float QuinticEaseOut() => (float)Math.Pow(p - 1, 5) + 1;

        public float QuinticEaseInOut()
        {
            if (p < 0.5f)
            {
                return 16 * (float)Math.Pow(p, 5);
            }

            return 0.5f * (float)Math.Pow(2 * p - 2, 5) + 1;
        }

        public float SineEaseIn() => (float)Math.Sin((p - 1) * (Math.PI / 2)) + 1;
        public float SineEaseOut() => (float)Math.Sin(p * (Math.PI / 2));
        public float SineEaseInOut() => (float)(0.5f * (1 - Math.Cos(p * Math.PI)));
    }


    // BOUNCE


    // CUBIC


    // CIRCULAR


    // ELASTIC


    // EXPONENTIAL


    // LINEAR


    // QUADRATIC


    // QUARTIC


    // QUINTIC


    // SINE
}