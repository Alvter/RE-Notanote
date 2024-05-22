using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;
using static Updater;

public class EventProcess : MonoBehaviour
{
    public bool isStart = false;

    int[][] eventIndex;
    int[][] picEventIndex;

    public Transform realLineList;
    public Transform waitLineList;

    public Transform pictureList;

    public void ScriptStart()
    {
        eventIndex = new int[chart.judgelineList.Count][];
        for (int i = 0; i < chart.judgelineList.Count; i++)
        {
            eventIndex[i] = new int[4];
            for (int m = 0; m < 4; m++)
            {
                eventIndex[i][m] = 0;
            }
        }

        picEventIndex = new int[chart.performImgList.Count][];
        for (int i = 0; i < chart.performImgList.Count; i++)
        {
            picEventIndex[i] = new int[4];
            for (int m = 0; m < 4; m++)
            {
                picEventIndex[i][m] = 0;
            }
        }

        isStart = !isStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
        
        //判定线事件
        for (int i = 0; i < chart.judgelineList.Count; i++)
        {
            var line = chart.judgelineList[i];

            //移动事件
            if (chart.judgelineList[i].eventList.moveEvents.Count > 0)
            {
                int k = eventIndex[i][0];  
                var events = line.eventList.moveEvents[k];

                float runTime = realTime - events.st;//运行时间
                float resTime = events.et - realTime;//剩余时间

                if (events.pos.Length == 6)
                {
                    if (runTime > 0 && resTime > 0)
                    {
                        for (int m = 0; m < 3; m++)
                        {
                            line.nowPos[m] = events.pos[m] +
                                             EasingType.Ease(events.type, (float)runTime / events.set) *
                                             events.se[m];
                        }
                    }
                    else if (resTime < 0)
                    {
                        line.nowPos[0] = events.pos[3];
                        line.nowPos[1] = events.pos[4];
                        line.nowPos[2] = events.pos[5];
                        events.isProcess = true;
                        if (k < line.eventList.moveEvents.Count - 1) eventIndex[i][0]++;
                    }
                }
                else
                {
                    int count = events.poss.Count;
                    if (runTime > 0 && resTime > 0)
                    {
                        line.nowPos = Function.Apply(events.poss, (float)runTime / events.set, events.type);
                    }
                    else if (resTime < 0)
                    {
                        line.nowPos[0] = events.pos[count - 2];
                        line.nowPos[1] = events.pos[count - 1];
                        line.nowPos[2] = events.pos[count];
                        events.isProcess = true;
                        if (k < line.eventList.moveEvents.Count - 1) eventIndex[i][0]++;
                    }
                }
            }

            //旋转事件
            if (chart.judgelineList[i].eventList.rotateEvents.Count > 0)
            {
                int k = eventIndex[i][1];
                var events = line.eventList.rotateEvents[k];

                float runTime = realTime - events.st;//运行时间
                float resTime = events.et - realTime;//剩余时间

                if (runTime > 0 && resTime > 0)
                {
                    line.nowRotate = events.startAngle +
                                     EasingType.Ease(events.type, (float)runTime / events.set) *
                                     events.se;
                }
                else if (resTime < 0)
                {
                    line.nowRotate = events.endAngle;
                    events.isProcess = true;
                    if (k < line.eventList.rotateEvents.Count - 1) eventIndex[i][1]++;
                }
            }

            //颜色事件
            if (chart.judgelineList[i].eventList.colorModifyEvents.Count > 0)
            {
                int k = eventIndex[i][2];
                var events = line.eventList.colorModifyEvents[k];

                float runTime = realTime - events.st;//运行时间
                float resTime = events.et - realTime;//剩余时间
                if (runTime > 0 && resTime > 0)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        line.nowColor[m] = events.stColor[m] +
                                         EasingType.Ease(events.type, (float)runTime / events.set) *
                                         events.se[m];
                    }
                }
                else if (resTime < 0)
                {
                    line.nowColor[0] = events.edColor[0];
                    line.nowColor[1] = events.edColor[1];
                    line.nowColor[2] = events.edColor[2];
                    line.nowColor[3] = events.edColor[3];
                    events.isProcess = true;
                    if (k < line.eventList.colorModifyEvents.Count - 1) eventIndex[i][2]++;
                }
                realLineList.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(line.nowColor[0], line.nowColor[1], line.nowColor[2], line.nowColor[3]);
            }


            realLineList.GetChild(i).position = new Vector3(line.nowPos[0], line.nowPos[1], line.nowPos[2]);
            waitLineList.GetChild(i).position = new Vector3(line.nowPos[0], line.nowPos[1], line.nowPos[2]);

            realLineList.GetChild(i).eulerAngles = new Vector3(0, 0, line.nowRotate);
            waitLineList.GetChild(i).eulerAngles = new Vector3(0, 0, line.nowRotate);
        }

        //贴图事件
        for (int i = 0; i < chart.performImgList.Count; i++)
        {
            var pic = chart.performImgList[i];

            //移动事件
            if (pic.eventList.moveEvents.Count > 0)
            {
                int k = picEventIndex[i][0];
                var events = pic.eventList.moveEvents[k];

                float runTime = realTime - events.st;//运行时间
                float resTime = events.et - realTime;//剩余时间

                if (events.pos.Length == 6)
                {
                    if (runTime > 0 && resTime > 0)
                    {
                        for (int m = 0; m < 3; m++)
                        {
                            pic.nowPos[m] = events.pos[m] +
                                            EasingType.Ease(events.type, (float)runTime / events.set) *
                                            events.se[m];
                        }
                    }
                    else if (resTime < 0)
                    {
                        pic.nowPos[0] = events.pos[3];
                        pic.nowPos[1] = events.pos[4];
                        pic.nowPos[2] = events.pos[5];
                        events.isProcess = true;
                        if (k < pic.eventList.moveEvents.Count - 1) picEventIndex[i][0]++;
                    }
                }
                else
                {
                    int count = events.poss.Count;
                    if (runTime > 0 && resTime > 0)
                    {
                        pic.nowPos = Function.Apply(events.poss, (float)runTime / events.set, events.type);
                    }
                    else if (resTime < 0)
                    {
                        pic.nowPos[0] = events.pos[count - 2];
                        pic.nowPos[1] = events.pos[count - 1];
                        pic.nowPos[2] = events.pos[count];
                        events.isProcess = true;
                        if (k < pic.eventList.moveEvents.Count - 1) picEventIndex[i][0]++;
                    }
                }
            }

            //旋转事件
            if (pic.eventList.rotateEvents.Count > 0)
            {
                int k = picEventIndex[i][1];
                var events = pic.eventList.rotateEvents[k];

                float runTime = realTime - events.st;//运行时间
                float resTime = events.et - realTime;//剩余时间

                if (runTime > 0 && resTime > 0)
                {
                    pic.nowRotate = events.startAngle +
                                     EasingType.Ease(events.type, (float)runTime / events.set) *
                                     events.se;
                }
                else if (resTime < 0)
                {
                    pic.nowRotate = events.endAngle;
                    events.isProcess = true;
                    if (k < pic.eventList.rotateEvents.Count - 1) picEventIndex[i][1]++;
                }
                pictureList.GetChild(i).eulerAngles = new Vector3(0, 0, pic.nowRotate);
            }

            //颜色事件
            if (pic.eventList.colorModifyEvents.Count > 0)
            {
                int k = picEventIndex[i][2];
                var events = pic.eventList.colorModifyEvents[k];

                float runTime = realTime - events.st;//运行时间
                float resTime = events.et - realTime;//剩余时间
                if (runTime > 0 && resTime > 0)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        pic.nowColor[m] = events.stColor[m] +
                                         EasingType.Ease(events.type, (float)runTime / events.set) *
                                         events.se[m];
                    }
                }
                else if (resTime < 0)
                {
                    pic.nowColor[0] = events.edColor[0];
                    pic.nowColor[1] = events.edColor[1];
                    pic.nowColor[2] = events.edColor[2];
                    pic.nowColor[3] = events.edColor[3];
                    events.isProcess = true;
                    if (k < pic.eventList.colorModifyEvents.Count - 1) picEventIndex[i][2]++;
                }
                pictureList.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(pic.nowColor[0], pic.nowColor[1], pic.nowColor[2], pic.nowColor[3]);
            }

            //缩放事件
            if (pic.eventList.scaleEvents.Count > 0)
            {
                int k = picEventIndex[i][3];
                var events = pic.eventList.scaleEvents[k];

                float runTime = realTime - events.st;//运行时间
                float resTime = events.et - realTime;//剩余时间

                if (runTime > 0 && resTime > 0)
                {
                    pic.nowScale = events.startScale +
                                   EasingType.Ease(events.type, (float)runTime / events.set) *
                                   events.se;
                }
                else if (resTime < 0)
                {
                    pic.nowScale = events.endScale;
                    events.isProcess = true;
                    if (k < pic.eventList.scaleEvents.Count - 1) picEventIndex[i][3]++;
                }
                pictureList.GetChild(i).localScale = new Vector2(scale * pic.nowScale, scale * pic.nowScale);
            }


            pictureList.GetChild(i).position = new Vector3(pic.nowPos[0], pic.nowPos[1], pic.nowPos[2]);
        }
    }
}
