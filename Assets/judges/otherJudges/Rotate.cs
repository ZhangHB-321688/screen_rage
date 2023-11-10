using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public byte dir;
    public bool s;
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
            if (s)
            {
                if (dir == 0)
                {
                    this.gameObject.transform.localPosition = new Vector3(Mathf.Min((float)JudgeTime * 10, 0), 3.5f, 0);
                    this.gameObject.transform.localScale = new Vector3(0.5f, 2, 1);
                }
                else if (dir == 1)
                {
                    this.gameObject.transform.localPosition = new Vector3(Mathf.Min((float)JudgeTime * 10, 0), -3.5f, 0);
                    this.gameObject.transform.localScale = new Vector3(0.5f, 2, 1);
                }
                else if (dir == 2)
                {
                    this.gameObject.transform.localPosition = new Vector3(-3.5f, Mathf.Min((float)JudgeTime * 10, 0), 0);
                    this.gameObject.transform.localScale = new Vector3(2, 0.5f, 1);
                }
                else if (dir == 3)
                {
                    this.gameObject.transform.localPosition = new Vector3(3.5f, Mathf.Min((float)JudgeTime * 10, 0), 0);
                    this.gameObject.transform.localScale = new Vector3(2, 0.5f, 1);

                }
            }
            else
            {
                if (dir == 0)
                {
                    this.gameObject.transform.localPosition = new Vector3(-Mathf.Min((float)JudgeTime * 10, 0), 3.5f, 0);
                    this.gameObject.transform.localScale = new Vector3(0.5f, 2, 1);
                }
                else if (dir == 1)
                {
                    this.gameObject.transform.localPosition = new Vector3(-Mathf.Min((float)JudgeTime * 10, 0), -3.5f, 0);
                    this.gameObject.transform.localScale = new Vector3(0.5f, 2, 1);
                }
                else if (dir == 2)
                {
                    this.gameObject.transform.localPosition = new Vector3(-3.5f, -Mathf.Min((float)JudgeTime * 10, 0), 0);
                    this.gameObject.transform.localScale = new Vector3(2, 0.5f, 1);
                }
                else if (dir == 3)
                {
                    this.gameObject.transform.localPosition = new Vector3(3.5f, -Mathf.Min((float)JudgeTime * 10, 0), 0);
                    this.gameObject.transform.localScale = new Vector3(2, 0.5f, 1);

                }
            }
        }
        if (JudgeTime >= -0.2)
        {
            if ((Input.gyro.rotationRate.x < -5 && dir == 0)|| (Input.gyro.rotationRate.x > 5 && dir == 1)|| (Input.gyro.rotationRate.y < -5 && dir == 2)|| (Input.gyro.rotationRate.y > 5 && dir == 3))
            {
                judged = true;
            }
        }
        if (judged && JudgeTime >= 0 && s)
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
        if (JudgeTime >= 0 && !s) Destroy(this.gameObject);
    }
}