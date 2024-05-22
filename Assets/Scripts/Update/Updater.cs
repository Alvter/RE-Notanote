using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Updater : MonoBehaviour
{
    public static float realTime = -1f;
    public static float showTime = 0f;

    public float offset;

    public bool isStart = false;
    public bool isShow = false;

    public UnityEvent chartStart;
    public UnityEvent changeMusic;

    public GameObject realLineList;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isStart = !isStart;
            chartStart.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 0) // ���ʱ���Ѿ����ᣬ��ⶳ
            {
                Time.timeScale = 1;
                changeMusic.Invoke();
            }
            else // ���ʱ��û�ж��ᣬ�򶳽�
            {
                Time.timeScale = 0;
                changeMusic.Invoke();
            }
        }

        if (!isStart) return;

        realTime += Time.deltaTime;
        showTime = realTime + offset;

        if (realTime < 0.02f || isShow) return;
        realLineList.SetActive(true);
        isShow = true;
    }
}
