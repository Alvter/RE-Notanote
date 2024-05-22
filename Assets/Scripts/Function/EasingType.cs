using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class EasingType : MonoBehaviour
{
    public UnityEvent myevent;
    public static float Ease(int id, float x)
    {
        return id switch
        {
            0 => LineX(x),
            1 => InSine(x),
            2 => OutSine(x),
            3 => InOutSine(x),
            4 => InQuad(x),
            5 => OutQuad(x),
            6 => InOutQuad(x),
            7 => InCubic(x),
            8 => OutCubic(x),
            9 => InOutCubic(x),
            10 => InQuart(x),
            11 => OutQuart(x),
            12 => InOutQuart(x),
            13 => InQuint(x),
            14 => OutQuint(x),
            15 => InOutQuint(x),
            16 => InExpo(x),
            17 => OutExpo(x),
            18 => InOutExpo(x),
            19 => InCirc(x),
            20 => OutCirc(x),
            21 => InOutCirc(x),
            22 => InBack(x),
            23 => OutBack(x),
            24 => InOutBack(x),
            25 => InElastic(x),
            26 => OutElastic(x),
            27 => InOutElastic(x),
            28 => InBounce(x),
            29 => OutBounce(x),
            30 => InOutBounce(x),
            _ => x,// 默认情况下返回 x
        };
    }

    public static float LineX(float x)
    {
        return x;
    }

    public static float OutSine(float x)
    {
        return (float)MathF.Sin((x * MathF.PI) / 2);
    }

    public static float InSine(float x)
    {
        return 1 - (float)MathF.Cos((x * MathF.PI) / 2);
    }

    public static float OutQuad(float x)
    {
        return 1 - (1 - x) * (1 - x);
    }

    public static float InQuad(float x)
    {
        return x * x;
    }

    public static float InOutSine(float x)
    {
        return -(float)(MathF.Cos(MathF.PI * x) - 1) / 2;
    }

    public static float InOutQuad(float x)
    {
        if (x < 0.5f)
        {
            return 2 * x * x;
        }
        else
        {
            return 1 - (float)MathF.Pow(-2 * x + 2, 2) / 2;
        }
    }

    public static float OutCubic(float x)
    {
        return 1 - (float)MathF.Pow(1 - x, 3);
    }

    public static float InCubic(float x)
    {
        return (float)MathF.Pow(x, 3);
    }

    public static float OutQuart(float x)
    {
        return 1 - (float)MathF.Pow(1 - x, 4);
    }

    public static float InQuart(float x)
    {
        return (float)MathF.Pow(x, 4);
    }

    public static float InOutCubic(float x)
    {
        if (x < 0.5f)
        {
            return 4 * (float)MathF.Pow(x, 3);
        }
        else
        {
            return 1 - (float)MathF.Pow(-2 * x + 2, 3) / 2;
        }
    }

    public static float InOutQuart(float x)
    {
        if (x < 0.5f)
        {
            return 8 * (float)MathF.Pow(x, 4);
        }
        else
        {
            return 1 - (float)MathF.Pow(-2 * x + 2, 4) / 2;
        }
    }

    public static float OutQuint(float x)
    {
        return 1 - (float)MathF.Pow(1 - x, 5);
    }

    public static float InOutQuint(float x)
    {
        return (x < 0.5f) ? 16 * (float)MathF.Pow(x, 5) : 1 - (float)MathF.Pow(-2 * x + 2, 5) / 2f;
    }

    public static float InQuint(float x)
    {
        return (float)MathF.Pow(x, 5);
    }

    public static float InOutExpo(float x)
    {
        if (x == 1 || x == 0)
        {
            return x;
        }
        else
        {
            return (x < 0.5f) ? MathF.Pow(2, 20 * x -10) / 2f: (2 - MathF.Pow(2, -20 * x + 10) / 2f);
        }
    }

    public static float OutExpo(float x)
    {
        if (x == 1)
        {
            return 1;
        }
        else
        {
            return 1 - (float)MathF.Pow(2, -10 * x);
        }
    }

    public static float InExpo(float x)
    {
        if (x == 0)
        {
            return 0;
        }
        else
        {
            return (float)MathF.Pow(2, 10 * x - 10);
        }
    }

    public static float OutCirc(float x)
    {
        return (float)MathF.Sqrt(1 - MathF.Pow(x - 1, 2));
    }

    public static float InCirc(float x)
    {
        return 1 - (float)MathF.Sqrt(1 - MathF.Pow(x, 2));
    }

    public static float OutBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return 1 + c3 * (float)MathF.Pow(x - 1, 3) + c1 * (float)MathF.Pow(x - 1, 2);
    }

    public static float InBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return c3 * x * x * x - c1 * x * x;
    }

    public static float InOutCirc(float x)
    {
        if (x < 0.5f)
        {
            return (1 - (float)MathF.Sqrt(1 - MathF.Pow(2 * x, 2))) / 2;
        }
        else
        {
            return ((float)MathF.Sqrt(1 - MathF.Pow(-2 * x + 2, 2)) + 1) / 2;
        }
    }

    public static float InOutBack(float x)
    {
        float c1 = 1.70158f;
        float c2 = c1 * 1.525f;

        if (x < 0.5f)
        {
            return (float)(MathF.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2;
        }
        else
        {
            return (float)(MathF.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
        }
    }

    public static float OutElastic(float x)
    {
        float c4 = (2 * (float)MathF.PI) / 3;

        if (x == 0)
        {
            return 0;
        }
        else if (x == 1)
        {
            return 1;
        }
        else
        {
            return (float)(MathF.Pow(2, -10 * x) * MathF.Sin((x * 10f - 0.75f) * c4) + 1);
        }
    }

    public static float InElastic(float x)
    {
        float c4 = (2 * (float)MathF.PI) / 3;

        if (x == 0)
        {
            return 0;
        }
        else if (x == 1)
        {
            return 1;
        }
        else
        {
            return (float)(-MathF.Pow(2, 10 * x - 10) * MathF.Sin((x * 10f - 10.75f) * c4));
        }
    }

    public static float OutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

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

    public static float InBounce(float x)
    {
        return 1 - OutBounce(1 - x);
    }

    public static float InOutBounce(float x)
    {
        if (x < 0.5f)
        {
            return (1 - OutBounce(1 - 2 * x)) / 2;
        }
        else
        {
            return (1 + OutBounce(2 * x - 1)) / 2;
        }
    }

    public static float InOutElastic(float x)
    {
        float c5 = (2 * MathF.PI) / 4.5f;

        if (x == 0)
        {
            return 0;
        }
        else if (x == 1)
        {
            return 1;
        }
        else if (x < 0.5f)
        {
            return -(float)(MathF.Pow(2, 20 * x - 10) * MathF.Sin((20f * x - 11.125f) * c5)) / 2;
        }
        else
        {
            return (float)(MathF.Pow(2, -20 * x + 10) * MathF.Sin((20f * x - 11.125f) * c5)) / 2 + 1;
        }
    }
}
