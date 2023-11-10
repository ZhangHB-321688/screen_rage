using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acc : MonoBehaviour
{
    public byte dir; //0^ 1v 2< 3>
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
            this.gameObject.transform.localScale = new Vector3(2, 2, 0);
            if (dir == 0)
            {
                this.gameObject.transform.localPosition = new Vector3(0, Mathf.Max(-(float)JudgeTime * 10, 0), 0);
                this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (dir == 1)
            {
                this.gameObject.transform.localPosition = new Vector3(0, Mathf.Min((float)JudgeTime * 10, 0), 0);
                this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
            else if (dir == 2)
            {
                this.gameObject.transform.localPosition = new Vector3(Mathf.Min((float)JudgeTime * 10, 0),0, 0);
                this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
            else if (dir == 3)
            {
                this.gameObject.transform.localPosition = new Vector3(Mathf.Max(-(float)JudgeTime * 10, 0), 0, 0);
                this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 270));
            }
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        if (JudgeTime >= -0.6)
        {
            if ((Input.gyro.userAcceleration.y < -1.2 && dir==0)|| (Input.gyro.userAcceleration.y > 1.2 && dir == 1)|| (Input.gyro.userAcceleration.x > 1.2 && dir == 2)|| (Input.gyro.userAcceleration.x < -1.2 && dir == 3))
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
            accuracy.d++;
            combo.comboNum = 0;
            Destroy(this.gameObject);
        }
    }
}