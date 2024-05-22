using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Text.RegularExpressions;
using static LoadChart;

public class ReLoadChart : MonoBehaviour
{
    public bool isStart = false;

    public UnityEvent ChartLoadOver;

    public void ScriptStart()
    {
        
        for (int i = 0; i < chart.judgelineList.Count; i++)
        {
            if (chart.judgelineList[i].noteList.Count <= 0) continue;//判定判定线是否有note

            for (int m = 0; m < chart.judgelineList[i].noteList.Count; m++)
            {
                var note = chart.judgelineList[i].noteList[m];

                for (int k = 0; k < 4; k++)
                {
                    string part = note._color.Substring(2 * k, 2);
                    note.colorNum[k] = Function.GetNumber(part) / 255f;
                }
                note.color = new Color(note.colorNum[0], note.colorNum[1], note.colorNum[2], note.colorNum[3]);
                note.st = note.time / 1000f;
                note.longt = note.duration / 1000f;
                note.et = note.st + note.longt;
            }
        }

        string pattern = @"[-+]?\d+(\.\d+)?";
        MatchCollection matches;
        for (int i = 0; i < chart.judgelineList.Count; i++)
        {
            var line = chart.judgelineList[i];
            for (int k = 0; k < 4; k++)
            {
                string part = line._color.Substring(2 * k, 2);
                line.nowColor[k] = Function.GetNumber(part) / 255f;
            }
            line.color = new Color(line.nowColor[0], line.nowColor[1], line.nowColor[2], line.nowColor[3]);
            matches = Regex.Matches(line._pos, pattern);
            for (int k = 0; k < matches.Count; k++)
            {
                line.pos[k] = (k == 0 || k == 3) ? float.Parse(matches[k].Value) * sx :
                              (k == 1 || k == 4) ? float.Parse(matches[k].Value) * sy :
                               float.Parse(matches[k].Value);
            }

            //判定线移动事件预加载
            if (line.eventList.moveEvents.Count > 0)//判定判定线是否有note
            {
                for (int m = 0; m < line.eventList.moveEvents.Count; m++)
                {
                    var events = line.eventList.moveEvents[m];

                    matches = Regex.Matches(events._pos, pattern);
                    events.pos = new float[matches.Count];
                    for (int k = 0; k < matches.Count; k++)
                    {
                        events.pos[k] = (k == 0 || k == 3) ? float.Parse(matches[k].Value) * sx :
                                        (k == 1 || k == 4) ? float.Parse(matches[k].Value) * sy :
                                         float.Parse(matches[k].Value); ;
                    }

                    if (matches.Count == 6)
                    {
                        events.se[0] = (events.pos[3] - events.pos[0]);
                        events.se[1] = (events.pos[4] - events.pos[1]);
                        events.se[2] = events.pos[5] - events.pos[2];
                    }
                    else
                    {
                        List<float[]> pointList = new();
                        for (int k = 0; k < matches.Count / 3; k++)
                        {
                            float[] point = new float[3];
                            point[0] = float.Parse(matches[k * 3].Value) * sx;
                            point[1] = float.Parse(matches[k * 3 + 1].Value) * sy;
                            point[2] = float.Parse(matches[k * 3 + 2].Value);
                            pointList.Add(point);
                        }
                        events.poss = pointList;
                    }
                    events.st = events.startTime / 1000f;
                    events.et = events.endTime / 1000f;
                    events.set = events.et - events.st;   
                }
            }

            //判定线旋转事件预加载
            if (line.eventList.rotateEvents.Count > 0)
            {
                for (int m = 0; m < line.eventList.rotateEvents.Count; m++)
                {
                    var events = line.eventList.rotateEvents[m];

                    events.st = events.startTime / 1000f;
                    events.et = events.endTime / 1000f;
                    events.set = events.et - events.st;
                    events.se = events.endAngle - events.startAngle;
                }
            }

            //判定线颜色事件预加载
            if (chart.judgelineList[i].eventList.colorModifyEvents.Count > 0)
            {
                for (int m = 0; m < chart.judgelineList[i].eventList.colorModifyEvents.Count; m++)
                {
                    var events = chart.judgelineList[i].eventList.colorModifyEvents[m];

                    for (int k = 0; k < 4; k++)
                    {
                        string part = events._startcolor.Substring(2 * k, 2);
                        events.stColor[k] = Function.GetNumber(part) / 255f;
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        string part = events._endcolor.Substring(2 * k, 2);
                        events.edColor[k] = Function.GetNumber(part) / 255f;
                    }
                    events.st = events.startTime / 1000f;
                    events.et = events.endTime / 1000f;
                    events.set = events.et - events.st;
                    for (int k = 0; k < 4; k++)
                    {
                        events.se[k] = events.edColor[k] - events.stColor[k];
                    }
                    
                }
            }
        }


        for (int i = 0; i < chart.performImgList.Count; i++)
        {
            var pic = chart.performImgList[i];
            for (int k = 0; k < 4; k++)
            {
                string part = pic._color.Substring(2 * k, 2);
                pic.nowColor[k] = Function.GetNumber(part) / 255f;
            }
            pic.color = new Color(pic.nowColor[0], pic.nowColor[1], pic.nowColor[2], pic.nowColor[3]);
            matches = Regex.Matches(pic._pos, pattern);
            for (int k = 0; k < matches.Count; k++)
            {
                pic.pos[k] = (k == 0 || k == 3) ? float.Parse(matches[k].Value) * sx :
                             (k == 1 || k == 4) ? float.Parse(matches[k].Value) * sy :
                             float.Parse(matches[k].Value);
            }

            //贴图移动事件预加载
            if (pic.eventList.moveEvents.Count > 0)//判定判定线是否有note
            {
                for (int m = 0; m < pic.eventList.moveEvents.Count; m++)
                {
                    var events = pic.eventList.moveEvents[m];

                    matches = Regex.Matches(events._pos, pattern);
                    events.pos = new float[matches.Count];
                    for (int k = 0; k < matches.Count; k++)
                    {
                        events.pos[k] = (k == 0 || k == 3) ? float.Parse(matches[k].Value) * sx :
                                        (k == 1 || k == 4) ? float.Parse(matches[k].Value) * sy :
                                         float.Parse(matches[k].Value); ;
                    }

                    if (matches.Count == 6)
                    {
                        events.se[0] = (events.pos[3] - events.pos[0]);
                        events.se[1] = (events.pos[4] - events.pos[1]);
                        events.se[2] = events.pos[5] - events.pos[2];
                    }
                    else
                    {
                        List<float[]> pointList = new();
                        for (int k = 0; k < matches.Count / 3; k++)
                        {
                            float[] point = new float[3];
                            point[0] = float.Parse(matches[k * 3].Value) * sx;
                            point[1] = float.Parse(matches[k * 3 + 1].Value) * sy;
                            point[2] = float.Parse(matches[k * 3 + 2].Value);
                            pointList.Add(point);
                        }
                        events.poss = pointList;
                    }
                    events.st = events.startTime / 1000f;
                    events.et = events.endTime / 1000f;
                    events.set = events.et - events.st;
                }
            }

            //判定线旋转事件预加载
            if (pic.eventList.rotateEvents.Count > 0)
            {
                for (int m = 0; m < pic.eventList.rotateEvents.Count; m++)
                {
                    var events = pic.eventList.rotateEvents[m];

                    events.st = events.startTime / 1000f;
                    events.et = events.endTime / 1000f;
                    events.set = events.et - events.st;
                    events.se = events.endAngle - events.startAngle;
                }
            }

            //判定线颜色事件预加载
            if (pic.eventList.colorModifyEvents.Count > 0)
            {
                for (int m = 0; m < pic.eventList.colorModifyEvents.Count; m++)
                {
                    var events = pic.eventList.colorModifyEvents[m];

                    for (int k = 0; k < 4; k++)
                    {
                        string part = events._startcolor.Substring(2 * k, 2);
                        events.stColor[k] = Function.GetNumber(part) / 255f;
                    }
                    for (int k = 0; k < 4; k++)
                    {
                        string part = events._endcolor.Substring(2 * k, 2);
                        events.edColor[k] = Function.GetNumber(part) / 255f;
                    }
                    events.st = events.startTime / 1000f;
                    events.et = events.endTime / 1000f;
                    events.set = events.et - events.st;
                    for (int k = 0; k < 4; k++)
                    {
                        events.se[k] = events.edColor[k] - events.stColor[k];
                    }

                }
            }

            //判定线缩放事件预加载
            if (pic.eventList.scaleEvents.Count > 0)
            {
                for (int m = 0; m < pic.eventList.scaleEvents.Count; m++)
                {
                    var events = pic.eventList.scaleEvents[m];

                    events.st = events.startTime / 1000f;
                    events.et = events.endTime / 1000f;
                    events.set = events.et - events.st;
                    events.se = events.endScale - events.startScale;
                }
            }
        }

            ChartLoadOver.Invoke();

        isStart = !isStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;


    }
}
