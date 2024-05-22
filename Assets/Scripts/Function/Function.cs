using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function : MonoBehaviour
{
    // 将两位十六进制颜色转换为十进制数字
    public static float GetNumber(string hex)
    {
        if (hex.Length != 2)
        {
            return 0;
        }

        char char1 = hex[0];
        char char2 = hex[1];

        // 将字符转换为对应的十进制值
        int value1 = HexCharToDecimal(char1);
        int value2 = HexCharToDecimal(char2);

        int decimalColor = value1 * 16 + value2;

        return decimalColor;
    }

    // 将单个十六进制字符转换为对应的十进制值
    private static int HexCharToDecimal(char hexChar)
    {
        if (hexChar >= '0' && hexChar <= '9')
        {
            return hexChar - '0';
        }
        else if (hexChar >= 'A' && hexChar <= 'F')
        {
            return hexChar - 'A' + 10;
        }
        else if (hexChar >= 'a' && hexChar <= 'f')
        {
            return hexChar - 'a' + 10;
        }
        else
        {
            return 0;
        }
    }


    public static float[] Apply(List<float[]> points, float t, int type)
    {
        int n = points.Count;
        t = EasingType.Ease(type, t);
        float[][] tempPoints = new float[n][];
        for (int i = 0; i < n; i++)
        {
            tempPoints[i] = new float[] { points[i][0], points[i][1] , points[i][2] };
        }

        for (int k = 1; k < n; k++)
        {
            for (int i = 0; i < n - k; i++)
            {
                tempPoints[i][0] = (1 - t) * tempPoints[i][0] + t * tempPoints[i + 1][0];
                tempPoints[i][1] = (1 - t) * tempPoints[i][1] + t * tempPoints[i + 1][1];
                tempPoints[i][2] = (1 - t) * tempPoints[i][2] + t * tempPoints[i + 1][2];
            }
        }

        return tempPoints[0];
    }

    private static float[] getBezierPosition(List<float[]> points, float t)
    {
        int n = points.Count;
 
        for (int k = 1; k < n; k++)
        {
            for (int i = 0; i < n - k; i++)
            {
                points[i][0] = (1 - t) * points[i][0] + t * points[i + 1][0];
                points[i][1] = (1 - t) * points[i][1] + t * points[i + 1][1];
                //points[i][2] = (1 - t) * points[i][2] + t * points[i + 1][2];
            }
        }

        return points[0];
    }
}
