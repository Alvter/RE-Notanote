using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

public class LoadChart : MonoBehaviour
{
    [System.Serializable]
    public class Note
    {
        public int type;
        public float time;
        public float duration;
        public float speed;
        public float livingTime;
        public int lineId;
        public int lineSide;
        public bool fake;
        public string _color;

        public float[] colorNum = new float[4] {0f, 0f, 0f, 0f};
        public Color color;
        public float st;
        public float et;
        public float longt;
        public bool isPlayAudio = false;
        public bool isPlay = false;
        public bool isAdd = false;
    }

    [System.Serializable]
    public class EventList
    {
        public List<MoveEvents> moveEvents;
        public List<RotateEvents> rotateEvents;
        public List<ColorModifyEvents> colorModifyEvents;
        public List<ScaleEvents> scaleEvents;
    }

    [System.Serializable]
    public class MoveEvents
    {
        public int pathType;
        public float startTime;
        public float endTime;
        public int type;
        public string _pos;

        public float[] pos;
        public List<float[]> poss = new();
        public float st;
        public float et;
        public float set;
        public float[] se = new float[3];
        public bool isProcess = false;
        public List<float[]> GetPos()
        {
            return poss;
        }
    }

    [System.Serializable]
    public class RotateEvents
    {
        public float startAngle;
        public float endAngle;
        public float startTime;
        public float endTime;
        public int type;
   
        public float st;
        public float et;
        public float set;
        public float se;
        public bool isProcess = false;
    }

    [System.Serializable]
    public class ColorModifyEvents
    {
        public float startTime;
        public float endTime;
        public int type;
        public string _startcolor;
        public string _endcolor;

        public float st;
        public float et;
        public float set;
        public float[] stColor = new float[4] { 0f, 0f, 0f, 0f };
        public float[] edColor = new float[4] { 0f, 0f, 0f, 0f };
        public float[] se = new float[4];
        public bool isProcess = false;
    }

    [System.Serializable]
    public class ScaleEvents
    {
        public float startScale;
        public float endScale;
        public float startTime;
        public float endTime;
        public int type;

        public float st;
        public float et;
        public float set;
        public float se;
        public bool isProcess = false;
    }

    [System.Serializable]
    public class Judgeline
    {
        public List<Note> noteList;
        public EventList eventList;
        public float angle;
        public float scale;
        public string _color;
        public string _pos;

        public Color color;
        public float[] pos = new float[3];
        public float[] nowPos = new float[3] { 0f, 0f, 0f };
        public float nowRotate;
        public float[] nowColor = new float[4] { 0f, 0f, 0f, 0f };
    }

    [System.Serializable]
    public class PerformImg
    {
        public string path;
        public string name;
        public EventList eventList;
        public float angle;
        public float scale;
        public float startTime;
        public float endTime;
        public int sortingOrder;
        public string _color;
        public string _pos;

        public Color color;
        public float[] pos = new float[3];
        public float[] nowPos = new float[3] { 0f, 0f, 0f };
        public float nowRotate;
        public float[] nowColor = new float[4] { 0f, 0f, 0f, 0f };
        public float nowScale;
    }

    [System.Serializable]
    public class Chart
    {
        public int formatVersion;
        public string name;
        public string path;
        public string composer;
        public string charter;
        public string illustrator;
        public float difficulty;
        public float bpm;
        public List<float> bpmList;
        public float offset;
        public int noteNum;
        public List<Judgeline> judgelineList;
        public List<PerformImg> performImgList;
    }

    public static string chartName = "Power Overload";
    //public static string difficult = "sp";
    public static Chart chart;
    public TextAsset textAsset;
    public string json;
    public UnityEvent ChartLoadOver;
    public AudioSource music;
    public bool isOver = false;

    public static float sx = 320f;//x方向长度
    public static float sy = 180f;//y方向宽度
    public static float ns = 45f;//note速度
    public static float scale = 35.5f;//贴图倍数

    void Awake()
    {
        textAsset = Resources.Load<TextAsset>("Chart/" + chartName + "/" + chartName);
        json = textAsset.text;
        chart = JsonUtility.FromJson<Chart>(json);

        music.clip = Resources.Load<AudioClip>("Chart/" + chartName + "/" + chartName);

        Application.targetFrameRate = 250;
    }

    void Update()
    {
        if (chart != null && !isOver)
        {
            isOver = true;
            ChartLoadOver.Invoke();
        }
    }

}
