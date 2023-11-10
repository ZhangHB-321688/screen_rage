using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zroll : MonoBehaviour
{
    public double time;
    public bool clockwise;
    double JudgeTime;
    public string othrinfo;
    string[] otherinfos; // 0 幅度 1 时间
    double LastTime,spd;
    double initroll;

    // Start is called before the first frame update
    void Start()
    {
        otherinfos = othrinfo.Split(',');
        if(otherinfos.Length == 1)
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
            spd = 120;
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
            if (clockwise)
            {
                this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -(float)JudgeTime * (float)spd * 3 - 180));
                this.gameObject.transform.localScale = new Vector3(Mathf.Max(2 - (float)JudgeTime * 10, 2), Mathf.Max(2 - (float)JudgeTime * 10, 2), 1);
            }
            else
            {
                this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, (float)JudgeTime * (float)spd * 3 + 180));
                this.gameObject.transform.localScale = new Vector3(Mathf.Max(2 - (float)JudgeTime * 10, 2), Mathf.Max(2 - (float)JudgeTime * 10, 2), 1);
            }
        }
        if (JudgeTime >= 0)
        {
            if (f)
            {
                f = false;
                initroll = rotate.zrolling;
            }
            if (rotate.zrolling == 0) f = true;
            if ((clockwise&& rotate.zrolling - initroll < -spd) || (!clockwise && rotate.zrolling - initroll > spd))
            {
                combo.comboNum++;
                accuracy.d++;
                accuracy.n++;
                Destroy(this.gameObject);
            }      
        }
        if (JudgeTime > LastTime)
        {
            accuracy.d++;
            combo.comboNum = 0;
            Destroy(this.gameObject);
        }
    }
}
