using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class readChart2 : MonoBehaviour
{
    bool isLetter(char a)
    {
        if ((97 <= a && a < 97 + 26) || (65 <= a && a < 65 + 26)) return true;
        return false;
    }
    bool isNumber(char a)
    {
        if ((48 <= a && a < 48 + 10)||a=='.') return true;
        return false;
    }
    struct note
    {
        public string type;
        public double time;
        public double timeAsSec;
        public string otherInfo;
    };

    string path;
    note thisnote;
    Queue notes = new Queue();
    AudioClip clip;
    public static double inittime;
    public static double startOffset; // 从进入游玩界面到开始谱面和音乐的延迟秒数
    double bpm;
    note n;

    public static double beat;
    public static double offset;
    public static double fullOffset;

    void Start()
    {
        bool firstbpm = false, firstOffset = false;
        double LastTimeChangeBPM = 0, LastBeatChangeBPM = 0, currentBPM = 120;
        string rawChart = "";
        path = xuanguanTEXT.tn;
        try
        {
            rawChart = File.ReadAllText(xuanguanTEXT.ChartPath) + " ";
            //clip = Resources.Load<AudioClip>("Charts/" + path);
            //this.GetComponent<AudioSource>().clip = clip;
        }
        catch
        {

            SceneManager.LoadScene("xuanGuan");
            return;
        }

        int i = 0;
        bool zhushi = false;
        double beat,fm,fz;
        while (i< rawChart.Length)
        {
            beat = 0;
            fm = 16;
            fz = 0;
            thisnote.otherInfo = "";
            thisnote.type = "";
            if (rawChart[i] == '*')
            {
                zhushi = !zhushi;
            }
            if(!zhushi && isNumber(rawChart[i]))
            {
                while (isNumber(rawChart[i]))
                {
                    beat = beat * 10;
                    beat += rawChart[i] - 48;
                    i++;
                }
                if (!isLetter(rawChart[i]))
                {
                    i++;
                    while (isNumber(rawChart[i]))
                    {
                        fz = fz * 10;
                        fz += rawChart[i] - 48;
                        i++;
                    }
                }
                if (!isLetter(rawChart[i]))
                {
                    fm = 0;
                    i++;
                    while (isNumber(rawChart[i]))
                    {
                        fm = fm * 10;
                        fm += rawChart[i] - 48;
                        i++;
                    }
                }
                thisnote.time = beat + fz / fm;
                while (isLetter(rawChart[i]))
                {
                    thisnote.type = thisnote.type + rawChart[i];
                    i++;
                }
                if (i < rawChart.Length)
                {
                    if(rawChart[i] == '|')
                    {
                        i++;
                        while((isNumber(rawChart[i]) || rawChart[i] == ',' || rawChart[i] == '-') && i < rawChart.Length)
                        {
                            thisnote.otherInfo = thisnote.otherInfo + rawChart[i];
                            i++;
                        }
                    }
                }
                thisnote.timeAsSec = (thisnote.time - LastBeatChangeBPM) /currentBPM*60 + LastTimeChangeBPM;

                if (thisnote.type != "bpm")
                {
                    if (thisnote.type == "tl" || thisnote.type == "tr")
                    {
                        AllTapsJudge.tapTypeBeforeJudge.Enqueue(thisnote.type);
                        AllTapsJudge.tapTimeBeforeJudge.Enqueue(thisnote.timeAsSec);
                    }
                    else notes.Enqueue(thisnote);
                }
                else
                {
                    double BeatAfterLastChangeBPM = beat - LastBeatChangeBPM;
                    LastTimeChangeBPM += BeatAfterLastChangeBPM / currentBPM * 60;
                    LastBeatChangeBPM = beat;
                    currentBPM = System.Convert.ToDouble(thisnote.otherInfo);
                }
                if(thisnote.type == "bpm" && firstbpm == false)
                {
                    bpm = System.Convert.ToDouble(thisnote.otherInfo);
                    firstbpm = true;
                }
                if (thisnote.type == "offset" && firstOffset == false)
                {
                    offset = System.Convert.ToDouble(thisnote.otherInfo);
                    firstOffset = true;
                }
                //Debug.Log(thisnote.time.ToString()+" "+ thisnote.type+" "+ thisnote.otherInfo);
            }

            i = i + 1;
        }
        inittime = Time.timeAsDouble; //[!]
        startOffset = 2;

        n = (note)notes.Dequeue();
        flag = true;
    }


    public static bool end = false;
    //playerOffset
    public GameObject tap,zAcc,Acc,Zccw,Zcw,FrontUDLR,Zcwroll,Zccwroll,Xroll,freefall;
    GameObject j,k;
    bool flag;
    void Update()
    {
        fullOffset = inittime + startOffset + offset;
        beat = (Time.time - inittime) / 60 * bpm;
        while (flag)
        {
            if (n.timeAsSec <= (Time.time - fullOffset)+1)
            {
                //召唤note
                //Debug.Log(n.timeAsSec);
                if (n.type == "fa" || n.type == "ba")
                {
                    j = Instantiate(zAcc);
                    j.GetComponent<ZPush>().time = n.timeAsSec;
                    j.GetComponent<ZPush>().front = n.type == "fa";
                }

                if (n.type == "la" || n.type == "ra" || n.type == "ua" || n.type == "da")
                {
                    j = Instantiate(Acc);
                    j.GetComponent<Acc>().time = n.timeAsSec;
                    if (n.type == "ua")
                        j.GetComponent<Acc>().dir = 0;
                    else if (n.type == "da")
                        j.GetComponent<Acc>().dir = 1;
                    else if (n.type == "la")
                        j.GetComponent<Acc>().dir = 2;
                    else
                        j.GetComponent<Acc>().dir = 3;
                }

                if (n.type == "fu" || n.type == "fd" || n.type == "fl" || n.type == "fr")
                {
                    j = Instantiate(FrontUDLR);
                    k = Instantiate(FrontUDLR);
                    j.GetComponent<Rotate>().time = n.timeAsSec;
                    k.GetComponent<Rotate>().time = n.timeAsSec;
                    k.GetComponent<Rotate>().s = true;
                    if (n.type == "fu")
                    {
                        j.GetComponent<Rotate>().dir = 0;
                        k.GetComponent<Rotate>().dir = 0;
                    }
                    else if (n.type == "fd")
                    {
                        j.GetComponent<Rotate>().dir = 1;
                        k.GetComponent<Rotate>().dir = 1;
                    }
                    else if (n.type == "fl")
                    {
                        j.GetComponent<Rotate>().dir = 2;
                        k.GetComponent<Rotate>().dir = 2;
                    }
                    else
                    {
                        j.GetComponent<Rotate>().dir = 3;
                        k.GetComponent<Rotate>().dir = 3;
                    }
                }

                if (n.type == "cc")
                {
                    j = Instantiate(Zccw);
                    j.GetComponent<Zrotate>().time = n.timeAsSec;
                    j.GetComponent<Zrotate>().clockwise = false;
                }
                if (n.type == "cw")
                {
                    j = Instantiate(Zcw);
                    j.GetComponent<Zrotate>().time = n.timeAsSec;
                    j.GetComponent<Zrotate>().clockwise = true;
                }

                if (n.type == "ff")
                {
                    j = Instantiate(freefall);
                    k = Instantiate(freefall);
                    j.GetComponent<freefallingJudge>().time = n.timeAsSec;
                    k.GetComponent<freefallingJudge>().time = n.timeAsSec;
                    j.GetComponent<freefallingJudge>().othrinfo = n.otherInfo;
                    k.GetComponent<freefallingJudge>().othrinfo = n.otherInfo;
                    j.GetComponent<freefallingJudge>().isOut = true;
                    k.GetComponent<freefallingJudge>().isOut = false;
                }

                if(n.type == "cz"){
                    j = Instantiate(Zccwroll);
                    j.GetComponent<Zroll>().othrinfo = n.otherInfo;
                    j.GetComponent<Zroll>().time = n.timeAsSec;
                    j.GetComponent<Zroll>().clockwise = false;
                }
                if(n.type == "zr")
                {
                    j = Instantiate(Zcwroll);
                    j.GetComponent<Zroll>().othrinfo = n.otherInfo;
                    j.GetComponent<Zroll>().time = n.timeAsSec;
                    j.GetComponent<Zroll>().clockwise = true;
                }
                if (n.type == "xr" || n.type == "cx")
                {
                    j = Instantiate(Xroll);
                    k = Instantiate(Xroll);
                    j.GetComponent<xroll>().othrinfo = n.otherInfo;
                    j.GetComponent<xroll>().time = n.timeAsSec;
                    j.GetComponent<xroll>().frontback = n.type == "xr";
                    k.GetComponent<xroll>().othrinfo = n.otherInfo;
                    k.GetComponent<xroll>().time = n.timeAsSec;
                    k.GetComponent<xroll>().frontback = n.type == "xr";
                    k.GetComponent<xroll>().left = true; ;

                }

                if (n.type == "end")
                {
                    end = true;
                }

                if (!(notes.Count == 0))
                    n = (note)notes.Dequeue();
                else flag = false;
            }
            else break;
        }
        if (Input.GetKeyDown(KeyCode.Escape) || end)
        {
            notes.Clear();
            SceneManager.LoadScene("xuanGuan");
            return;
        }
    }
}
