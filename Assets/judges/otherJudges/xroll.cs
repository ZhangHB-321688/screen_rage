using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xroll : MonoBehaviour
{
    public double time;
    public bool frontback;
    double JudgeTime;
    public bool left;
    public string othrinfo;
    string[] otherinfos; // 0 幅度 1 时间
    double LastTime, spd;
    double initroll;

    // Start is called before the first frame update
    void Start()
    {
        otherinfos = othrinfo.Split(',');
        if (otherinfos.Length == 1)
        {
            spd = System.Convert.ToDouble(otherinfos[0]);
            LastTime = 1;
        }
        else if (otherinfos.Length == 2)
        {
            spd = System.Convert.ToDouble(otherinfos[0]);
            LastTime = System.Convert.ToDouble(otherinfos[1]);
        }
        else
        {
            spd = 100;
            LastTime = 1;
        }
    }
    bool f = true;
    // Update is called once per frame
    void Update()
    {
        JudgeTime = Time.timeAsDouble - time - readChart2.fullOffset;
        if (JudgeTime >= -1)
        {

            this.gameObject.transform.localPosition = new Vector2(0, 0);
            if (frontback)
            {
                this.gameObject.transform.localPosition = new Vector3(left ? -4 : 4, Mathf.Max(-(float)JudgeTime * 5,0) , 0);
                this.gameObject.transform.localScale = new Vector3((float)(spd / 120), 1, 1);
            }
            else
            {
                this.gameObject.transform.localPosition = new Vector3(left ? -4 : 4, Mathf.Min((float)JudgeTime * 5, 0), 0);
                this.gameObject.transform.localScale = new Vector3((float)(spd / 120), -1, 1);
            }
        }
        if (JudgeTime >= 0)
        {
            if (f)
            {
                f = false;
                initroll = rotate.xrolling;
            }
            if (rotate.xrolling == 0) f = true;
            if ((!frontback && rotate.xrolling - initroll < -spd) || (frontback && rotate.xrolling - initroll > spd))
            {
                if (left)
                {
                    combo.comboNum++;
                    accuracy.d++;
                    accuracy.n++;
                }
                Destroy(this.gameObject);
            }
        }
        if (JudgeTime > LastTime)
        {
            if (left)
            {
                accuracy.d++;
                combo.comboNum = 0;
            }
            Destroy(this.gameObject);
        }
    }
}
