using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class readChart : MonoBehaviour
{
    int isNum(char a)
    {
        for(int i = 0; i <= 9; i += 1)
        {
            if (a == i.ToString()[0])
            {
                return i;
            }
        }
        return -1;
    }
    public string path;
    public GameObject notetap;
    string rawChart;
    struct t
    {
        public double beat;
        public double nume;//分子
        public double deno;//分母
    };
    struct note
    {
        public string type;
        public double time;
        public double otherInfo;
    };
    Queue notes = new Queue();
    // Start is called before the first frame update
    void Start()
    {
        t timer;
        rawChart = File.ReadAllText(Application.dataPath + path);
        bool IsZhushi = false, IsInFangkuohao = false;
        note thisnote;
        timer.beat = 0;
        timer.nume = 0;
        timer.deno = 0;
        thisnote.otherInfo = 0;
        int i = 0;
        while(true)
        {
            if (i >= rawChart.Length) break;

            if (rawChart[i] == '[')
            {
                IsInFangkuohao = true;
                IsZhushi = false;
            }
            if (rawChart[i] == ']' && !IsZhushi)
            {
                IsInFangkuohao = false;
            }
            if (rawChart[i] == '*')
            {
                IsZhushi = !IsZhushi;
            }
            if (IsInFangkuohao && !IsZhushi)
            {
                int state = 0;
                bool doRead = true;
                while (true)
                {
                    if (isNum(rawChart[i]) == -1)
                    {
                        i++;
                    }
                    else if(rawChart[i] == ']')
                    {
                        doRead = false;
                        IsInFangkuohao = false;
                        i++;
                        break;

                    }
                    else if (rawChart[i] == '*')
                    {
                        doRead = false;
                        IsZhushi = true;
                        i++;
                        break;

                    }
                    else break;
                }
                if (doRead)
                {
                    while (true)  // 读一个note,先读time
                    {
                        if (isNum(rawChart[i]) != -1)
                        {
                            if (state == 0)
                            {
                                timer.beat = timer.beat * 10;
                                timer.beat += isNum(rawChart[i]);
                            }
                            if (state == 1)
                            {
                                timer.nume = timer.nume * 10;
                                timer.nume += isNum(rawChart[i]);
                            }
                            if (state == 2)
                            {
                                timer.deno = timer.deno * 10;
                                timer.deno += isNum(rawChart[i]);
                            }
                            i += 1;
                        }
                        else if (rawChart[i] == ':')
                        {
                            state = 1;
                            i += 1;
                        }
                        else if (rawChart[i] == '/')
                        {
                            state = 2;
                            i += 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (state == 0)
                    {
                        timer.nume = 0;
                    }
                    if (state < 2)
                    {
                        timer.deno = 16;
                    }
                    state = 0;
                    thisnote.type = rawChart[i].ToString() + rawChart[i + 1].ToString(); //然后读type
                    i = i + 2;
                    string otherInfo = "";
                    bool isoi = false;
                    while (isNum(rawChart[i]) != -1 || rawChart[i]=='.') //最后读转动幅度
                    {
                        isoi = true;
                        otherInfo = otherInfo + rawChart[i].ToString();
                        i = i + 1;
                    }
                    if(isoi) thisnote.otherInfo = System.Convert.ToDouble(otherInfo);
                    isoi = false;
                    thisnote.time = timer.beat + (timer.nume / timer.deno);
                    notes.Enqueue(thisnote);
                    Debug.Log(thisnote.type.ToString() + " " + thisnote.otherInfo.ToString() + " " +thisnote.time.ToString());
                    timer.beat = 0;
                    timer.nume = 0;
                    timer.deno = 0;
                    thisnote.otherInfo = 0;
                    i = i + 1;
                }
            }
            else
            {
                i = i + 1;
            }
            
        }
    }

    // Update is called once per frame
    double thetime;
    void Update()
    {
        if (Time.timeAsDouble % 3 < thetime)
        {
            Instantiate(notetap);
        }
        thetime = Time.timeAsDouble%3;
    }
}