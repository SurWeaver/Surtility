namespace Surtility.Tweening;


/// <summary>
/// Функции плавности. X - значение прогресса анимации от 0 до 1.
/// </summary>
public static class EasingFunctions
{
    public delegate float EaseFunction(float x);

    public static EaseFunction GetEaseFunction(EasingType easingType)
    {
        return easingType switch
        {
            EasingType.Linear => Linear,
            EasingType.SineIn => SineIn,
            EasingType.SineOut => SineOut,
            EasingType.SineInOut => SineInOut,
            EasingType.QuadIn => QuadIn,
            EasingType.QuadOut => QuadOut,
            EasingType.QuadInOut => QuadInOut,
            EasingType.CubicIn => CubicIn,
            EasingType.CubicOut => CubicOut,
            EasingType.CubicInOut => CubicInOut,
            EasingType.QuartIn => QuartIn,
            EasingType.QuartOut => QuartOut,
            EasingType.QuartInOut => QuartInOut,
            EasingType.QuintIn => QuintIn,
            EasingType.QuintOut => QuintOut,
            EasingType.QuintInOut => QuintInOut,
            EasingType.ExpoIn => ExpoIn,
            EasingType.ExpoOut => ExpoOut,
            EasingType.ExpoInOut => ExpoInOut,
            EasingType.CircIn => CircIn,
            EasingType.CircOut => CircOut,
            EasingType.CircInOut => CircInOut,
            EasingType.BackIn => BackIn,
            EasingType.BackOut => BackOut,
            EasingType.BackInOut => BackInOut,
            EasingType.ElasticIn => ElasticIn,
            EasingType.ElasticOut => ElasticOut,
            EasingType.ElasticInOut => ElasticInOut,
            EasingType.BounceIn => BounceIn,
            EasingType.BounceOut => BounceOut,
            EasingType.BounceInOut => BounceInOut,
            _ => Linear,
        };
    }

    public static float Linear(float x) => x;

    public static float SineIn(float x) =>
        1.0f - MathF.Cos(x * MathF.PI / 2.0f);

    public static float SineOut(float x) =>
        MathF.Sin(x * MathF.PI / 2);

    public static float SineInOut(float x) =>
        -(MathF.Cos(MathF.PI * x) - 1) / 2;

    public static float QuadIn(float x) =>
        x * x;

    public static float QuadOut(float x) =>
        1 - (1 - x) * (1 - x);

    public static float QuadInOut(float x) =>
        x < 0.5
        ? 2 * x * x
        : 1 - MathF.Pow(-2 * x + 2, 2) / 2;

    public static float CubicIn(float x) =>
        x * x * x;

    public static float CubicOut(float x) =>
        1 - MathF.Pow(1 - x, 3);

    public static float CubicInOut(float x) =>
        x < 0.5
        ? 4 * x * x * x
        : 1 - MathF.Pow(-2 * x + 2, 3) / 2;

    public static float QuartIn(float x) =>
        x * x * x * x;

    public static float QuartOut(float x) =>
        1 - MathF.Pow(1 - x, 4);

    public static float QuartInOut(float x) =>
        x < 0.5 ? 8 * x * x * x * x : 1 - MathF.Pow(-2 * x + 2, 4) / 2;

    public static float QuintIn(float x) =>
        x * x * x * x * x;

    public static float QuintOut(float x) =>
        1 - MathF.Pow(1 - x, 5);

    public static float QuintInOut(float x) =>
        x < 0.5 ? 16 * x * x * x * x * x : 1 - MathF.Pow(-2 * x + 2, 5) / 2;

    public static float ExpoIn(float x) =>
        x == 0 ? 0 : MathF.Pow(2, 10 * x - 10);

    public static float ExpoOut(float x) =>
        x == 1 ? 1 : 1 - MathF.Pow(2, -10 * x);

    public static float ExpoInOut(float x) =>
        x == 0
        ? 0
        : x == 1
        ? 1
        : x < 0.5
        ? MathF.Pow(2, 20 * x - 10) / 2
        : (2 - MathF.Pow(2, -20 * x + 10)) / 2;

    public static float CircIn(float x) =>
        1 - MathF.Sqrt(1 - MathF.Pow(x, 2));

    public static float CircOut(float x) =>
        MathF.Sqrt(1 - MathF.Pow(x - 1, 2));

    public static float CircInOut(float x) =>
        x < 0.5
        ? (1 - MathF.Sqrt(1 - MathF.Pow(2 * x, 2))) / 2
        : (MathF.Sqrt(1 - MathF.Pow(-2 * x + 2, 2)) + 1) / 2;

    public static float BackIn(float x)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1;

        return c3 * x * x * x - c1 * x * x;
    }

    public static float BackOut(float x)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1;

        return 1 + c3 * MathF.Pow(x - 1, 3) + c1 * MathF.Pow(x - 1, 2);
    }

    public static float BackInOut(float x)
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;

        return x < 0.5
            ? MathF.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2) / 2
            : (MathF.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
    }

    public static float ElasticIn(float x)
    {
        const float c4 = 2 * MathF.PI / 3;

        return x == 0
            ? 0
            : x == 1
            ? 1
            : -MathF.Pow(2, 10 * x - 10) * MathF.Sin((x * 10 - 10.75f) * c4);
    }


    public static float ElasticOut(float x)
    {
        const float c4 = 2 * MathF.PI / 3;

        return x == 0
            ? 0
            : x == 1
            ? 1
            : MathF.Pow(2, -10 * x) * MathF.Sin((x * 10 - 0.75f) * c4) + 1;
    }


    public static float ElasticInOut(float x)
    {
        const float c5 = 2 * MathF.PI / 4.5f;

        return x == 0
            ? 0
            : x == 1
            ? 1
            : x < 0.5
            ? -(MathF.Pow(2, 20 * x - 10) * MathF.Sin((20 * x - 11.125f) * c5)) / 2
            : MathF.Pow(2, -20 * x + 10) * MathF.Sin((20 * x - 11.125f) * c5) / 2 + 1;
    }

    public static float BounceIn(float x) =>
        1 - BounceOut(1 - x);

    public static float BounceOut(float x)
    {
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2 / d1)
        {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        }
        else if (x < 2.5 / d1)
        {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
    }

    public static float BounceInOut(float x) =>
        x < 0.5
        ? (1 - BounceOut(1 - 2 * x)) / 2
        : (1 + BounceOut(2 * x - 1)) / 2;
}

public enum EasingType
{
    Linear,
    SineIn,
    SineOut,
    SineInOut,
    QuadIn,
    QuadOut,
    QuadInOut,
    CubicIn,
    CubicOut,
    CubicInOut,
    QuartIn,
    QuartOut,
    QuartInOut,
    QuintIn,
    QuintOut,
    QuintInOut,
    ExpoIn,
    ExpoOut,
    ExpoInOut,
    CircIn,
    CircOut,
    CircInOut,
    BackIn,
    BackOut,
    BackInOut,
    ElasticIn,
    ElasticOut,
    ElasticInOut,
    BounceIn,
    BounceOut,
    BounceInOut,
}
