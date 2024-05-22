using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuadraticBezierCurve : MonoBehaviour
{
    public GameObject startPoint;    // 贝塞尔曲线起始点
    public Transform controlPoint;  // 控制点
    public Transform endPoint;      // 终点
    public int numberOfPoints = 1000; // 曲线上的点数量
    public float time = 0f;

    private void OnDrawGizmos()
    {
        if (startPoint == null || controlPoint == null || endPoint == null)
            return;
        float[][] points = new float[][]
        {
            new float[] { 0, -108 ,0},
            new float[] { -81, -108 ,0},
            new float[] { -135, -54 ,0}
        };
        // 绘制贝塞尔曲线
        Gizmos.color = Color.white;
        for (int i = 0; i <= numberOfPoints; i++)
        {
            float t = i / (float)numberOfPoints;
            //float[] point1 = Apply(points,t);
            //Vector3 point = new Vector3 (point1[0], point1[1], point1[2]);
            //Gizmos.DrawSphere(point, 1f);
        }


    }
    float[][] points = new float[][]
        {
            new float[] { 0, -108 ,0},
            new float[] { -81, -108 ,0},
            new float[] { -135, -54 ,0}
        };
    public void Update()
    {
        time += Time.deltaTime;
        if (time > 1f) time -= 1f;
       // Debug.Log(time);
        float[] point2 = Apply(points, time);
      //  Debug.Log(point2[0] + "," + point2[1] + "," + point2[2]);
        startPoint.transform.position = new Vector2(point2[0], point2[1]);
    }
    // 计算二阶贝塞尔曲线上的点
    private float getY(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        float y = uu * p0.y + 2 * u * t * p1.y + tt * p2.y;
        return y;
    }

    private Vector3 getBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        // 计算曲线上点的坐标
        float x = uu * p0.x + 2 * u * t * p1.x + tt * p2.x;
        float z = uu * p0.z + 2 * u * t * p1.z + tt * p2.z;

        u = 1 - Ease(t);
        tt = Ease(t) * Ease(t);
        uu = u * u;

        float y = uu * p0.y + 2 * u * Ease(t) * p1.y + tt * p2.y;

        return new Vector3(x, y, z);
    }

    public enum EasingType
    {
        Linear,
        InQuart,
        OutQuart
    }

    public static EasingType easingType = EasingType.Linear;

    // 应用缓动曲线函数
    private static float Ease(float t)
    {
        switch (easingType)
        {
            case EasingType.Linear:
                return 1 - (float)MathF.Pow(1 - t, 3);
            case EasingType.InQuart:
                return EaseInQuart(t);
            case EasingType.OutQuart:
                return EaseOutQuart(t);
            default:
                return t;
        }
    }

    // InQuart 缓动曲线函数
    private static float EaseInQuart(float t)
    {
        return t * t * t * t;
    }

    // OutQuart 缓动曲线函数
    private static float EaseOutQuart(float t)
    {
        t -= 1;
        return 1 - (t * t * t * t);
    }

    // De_Casteljau算法
    public static float[] getBezierPosition(float[][] points, float t)
    {
        int n = points.Length;
        float[][] tempPoints = new float[n][];
        for (int i = 0; i < n; i++)
        {
            tempPoints[i] = points[i];
        }

        for (int k = 1; k < n; k++)
        {
            for (int i = 0; i < n - k; i++)
            {
                tempPoints[i][0] = (1 - t) * tempPoints[i][0] + t * tempPoints[i + 1][0];
                tempPoints[i][1] = (1 - t) * tempPoints[i][1] + t * tempPoints[i + 1][1];
            }
        }

        return tempPoints[0];
    }

    public static float[] Apply(float[][] points, float t)
    {
        int n = points.Length;
        float[][] tempPoints = new float[n][];
        for (int i = 0; i < n; i++)
        {
            tempPoints[i] = new float[] { points[i][0], points[i][1] };
        }

        for (int k = 1; k < n; k++)
        {
            for (int i = 0; i < n - k; i++)
            {
                tempPoints[i][0] = (1 - t) * tempPoints[i][0] + t * tempPoints[i + 1][0];
                tempPoints[i][1] = (1 - t) * tempPoints[i][1] + t * tempPoints[i + 1][1];
            }
        }

        return tempPoints[0];
    }

    public static float Apply2(List<Vector3> points, float t)
    {
        int n = points.Count;
        List<Vector3> tempPoints = new(points);
        for (int k = 1; k < n; k++)
        {
            for (int i = 0; i < n - k; i++)
            {
                tempPoints[i] = (1 - t) * tempPoints[i] + t * tempPoints[i + 1];
            }
        }
        return tempPoints[0].y;
    }

}