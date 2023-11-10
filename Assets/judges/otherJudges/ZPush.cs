using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPush : MonoBehaviour
{
    public bool front;
    public double time;
    double JudgeTime;
    bool judged = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        JudgeTime = Time.timeAsDouble - time - readChart2.fullOffset;
        if (JudgeTime >= -1)
        {
            if(front)
                this.gameObject.transform.localScale = new Vector3(Mathf.Max(2 - (float)JudgeTime * 10,2), Mathf.Max(2 - (float)JudgeTime * 10, 2), 1);
            else
                this.gameObject.transform.localScale = new Vector3(Mathf.Min((float)JudgeTime * 2 +2,2), Mathf.Min((float)JudgeTime * 2 + 2, 2), 1);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        if (JudgeTime >= -0.6)
        {
            if((Input.gyro.userAcceleration.z > 2 && front)|| (Input.gyro.userAcceleration.z < -2 && !front))
            {
                judged = true;
            }
        }
        if (judged && JudgeTime >= 0)
        {
            combo.comboNum++;
            accuracy.d++;
            accuracy.n++;
            Destroy(this.gameObject);
        }
        if (JudgeTime > 0.2)
        {
            combo.comboNum = 0;
            accuracy.d++;
            Destroy(this.gameObject);
        }
        this.gameObject.transform.localPosition = new Vector2(0, 0);
    }
}
