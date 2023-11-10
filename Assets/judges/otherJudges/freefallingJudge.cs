using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freefallingJudge : MonoBehaviour
{
    public double time;
    public bool isOut;
    double JudgeTime;
    
    public string othrinfo;
    public double LastTime;
    double maxfalltime = 0;
    // Start is called before the first frame update
    void Start()
    {
        LastTime = System.Convert.ToDouble(othrinfo);
    }

    // Update is called once per frame
    void Update()
    {
        JudgeTime = Time.timeAsDouble - time - readChart2.fullOffset;
        if (JudgeTime >= -1)
        {
            this.gameObject.transform.localPosition = new Vector2(0, 0);
            if (isOut)
            {
                this.gameObject.transform.localScale = new Vector3(Mathf.Max((float)(10*(-JudgeTime)+2),2), Mathf.Max((float)(10 * (-JudgeTime) + 2),2), 0);
            }
            else
            {
                this.gameObject.transform.localScale = new Vector3(Mathf.Min((float)(2 * (JudgeTime + 1) / (LastTime + 1)),2), Mathf.Min((float)(2 * (JudgeTime + 1) / (LastTime + 1)), 2), 0);
            }

        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        maxfalltime = Mathf.Max((float)freefalling.falltime, (float)maxfalltime);
        if (JudgeTime > LastTime && maxfalltime > LastTime)
        {
            if (isOut)
            {
                combo.comboNum++;
                accuracy.d++;
                accuracy.n++;
            }
            
            Destroy(this.gameObject);
        }
        if (JudgeTime > LastTime+0.5 && !(maxfalltime > LastTime))
        {
            if (isOut)
            {
                accuracy.d++;
                combo.comboNum = 0;
            }
            Destroy(this.gameObject);
        }
    }
}