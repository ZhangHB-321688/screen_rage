using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTapsJudge : MonoBehaviour
{
    public static Queue tapTypeBeforeJudge = new Queue(), tapTimeBeforeJudge = new Queue();
    Queue RtapToJudge = new Queue(),LtapToJudge = new Queue();
    public GameObject tap;

    bool flag = true, flag1 = true;
    
    void Start()
    {
    }
    double timeassec;
    string type;
    int ltaps = 0, rtaps = 0;
    bool islfront = false, isrfront = false, isNoteInFront = false , prevINIF = false;
    GameObject Lfront, Rfront;
    void Update()
    {
        //Debug.Log(tapTypeBeforeJudge.Count);
        if (flag)
        {
            if (tapTypeBeforeJudge.Count == 0)
            {
                flag1 = false;
            }
            else
            {
                type = (string)tapTypeBeforeJudge.Dequeue();
                timeassec = (double)tapTimeBeforeJudge.Dequeue();
                isNoteInFront = true;
                prevINIF = true;
            }
            
            flag = false;
        }
        while (flag1)
        {
            double time = Time.time - readChart2.fullOffset;
            if(!prevINIF)
            {
                flag1 = false;
            }
            if (prevINIF && timeassec <= time + 1)
            {
                GameObject x = Instantiate(tap);
                x.GetComponent<tap>().Right = type == "tr";
                x.GetComponent<tap>().time = timeassec;
                x.GetComponent<tap>().JudgeTime = -999;
                if (type == "tl")
                {
                    if (LtapToJudge.Count == 0 && !islfront)
                    {
                        islfront = true;
                        Lfront = x;
                    }
                    else
                        LtapToJudge.Enqueue(x);
                }
                else
                {
                    if (RtapToJudge.Count == 0 && !isrfront)
                    {
                        isrfront = true;
                        Rfront = x;
                    }
                    else
                        RtapToJudge.Enqueue(x);
                }
                if (tapTypeBeforeJudge.Count != 0)
                {
                    type = (string)tapTypeBeforeJudge.Dequeue();
                    timeassec = (double)tapTimeBeforeJudge.Dequeue();
                }
                prevINIF = isNoteInFront;
                isNoteInFront = tapTypeBeforeJudge.Count != 0;
            }
            else
            {
                break;
            }
        }
        rtaps = 0;
        ltaps = 0;
        for(int i = 0; i < Input.touchCount; ++i)
        {
            if(Input.touches[i].phase == TouchPhase.Began)
            {
                if (Input.touches[i].position.x > Screen.width / 2) rtaps++;
                else ltaps++;
            }
        }

        while((islfront && ltaps!=0))
        {
            if (Lfront.GetComponent<tap>().JudgeTime >= -0.15)
            {
                ltaps--;
                if (Lfront.GetComponent<tap>().JudgeTime >= -0.12)
                {
                    combo.comboNum++;
                    accuracy.n++;
                    accuracy.d++;
                }
                else
                {
                    combo.comboNum = 0;
                    accuracy.d++;
                }
                Destroy(Lfront);
                if (LtapToJudge.Count != 0) Lfront = (GameObject)LtapToJudge.Dequeue();
                else islfront = false;
            }
            else
            {
                break;
            }
        }
        while ((isrfront && rtaps != 0))
        {
            if (Rfront.GetComponent<tap>().JudgeTime >= -0.15)
            {
                rtaps--;
                if (Rfront.GetComponent<tap>().JudgeTime >= -0.12)
                {
                    combo.comboNum++;
                    accuracy.n++;
                    accuracy.d++;
                }
                else
                {
                    combo.comboNum = 0;
                    accuracy.d++;
                }
                Destroy(Rfront);
                if (RtapToJudge.Count != 0) Rfront = (GameObject)RtapToJudge.Dequeue();
                else isrfront = false;
            }
            else
            {
                break;
            }
        }
        if (islfront)
        {
            while (Lfront.GetComponent<tap>().JudgeTime > 0.12 && islfront)
            {
#if UNITY_EDITOR
                combo.comboNum++;
                accuracy.n++;
                accuracy.d++;
#else
                accuracy.d++;
                combo.comboNum = 0;
#endif
                Destroy(Lfront);
                if (LtapToJudge.Count != 0) Lfront = (GameObject)LtapToJudge.Dequeue();
                else islfront = false;
            }
        }
        if (isrfront)
        {
            while (Rfront.GetComponent<tap>().JudgeTime > 0.12 && isrfront) {
#if UNITY_EDITOR
                accuracy.n++;
                accuracy.d++;
                combo.comboNum++;
#else
                accuracy.d++;
                combo.comboNum = 0;
#endif
                Destroy(Rfront);
                if (RtapToJudge.Count != 0) Rfront = (GameObject)RtapToJudge.Dequeue();
                else isrfront = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) || readChart2.end)
        {
            tapTimeBeforeJudge.Clear();
            tapTypeBeforeJudge.Clear();
            LtapToJudge.Clear();
            RtapToJudge.Clear();
            Destroy(this);
            return;
        }
    }
}
