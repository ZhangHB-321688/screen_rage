using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zrotate : MonoBehaviour
{
    public double time;
    public bool clockwise;
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
            this.gameObject.transform.localPosition = new Vector2(0, 0);
            this.gameObject.transform.localScale = new Vector3(3, 3, 1);
            if(clockwise)
                this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, (float)Mathf.Max(-(float)JudgeTime * 120 - 180, -180)));
            else
                this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, (float)Mathf.Min((float)JudgeTime * 120 + 180, 180)));
        }
        else 
        {
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        if (JudgeTime >= -0.2)
        {
            if((Input.gyro.rotationRate.z < -6 && clockwise) ||(Input.gyro.rotationRate.z > 6 && !clockwise))
            {
                judged = true;
            }
        }
        if(judged && JudgeTime >= 0)
        {
            combo.comboNum++;
            accuracy.d++;
            accuracy.n++;
            Destroy(this.gameObject);
        }
        if (JudgeTime > 0.2)
        {
            accuracy.d++;
            combo.comboNum = 0;
            Destroy(this.gameObject);
        }
    }
}
