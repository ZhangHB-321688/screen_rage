using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for every tap note

public class tap : MonoBehaviour
{
    public bool Right;
    public double time;
    float edgex,edgey,linepos;
    bool down;
    public double JudgeTime;
    // Camera.main.ScreenToWorldPoint
    // Start is called before the first frame update
    void Start()
    {
        down = PlayerPrefs.GetInt("down", 0) == 1;
        linepos = PlayerPrefs.GetFloat("LinePos", 1);
        edgex = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, 0, 0)).x;
        edgey = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, 0, 0)).y;
        if(down)
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            this.gameObject.transform.localScale = new Vector3(0.3f, edgex, 1);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        JudgeTime = Time.timeAsDouble - time - readChart2.fullOffset;
        if (JudgeTime >= -1) {
            if (Right)
            {
                if (!down)
                    this.gameObject.transform.localPosition = new Vector2((float)(JudgeTime + 1) * edgex*linepos, 0);
                else
                {
                    this.gameObject.transform.localPosition = new Vector2(edgex/2, (float)(JudgeTime + 0+linepos/2) *2 * edgey);
                }
            }
            else
            {
                if (!down)
                    this.gameObject.transform.localPosition = new Vector2(-(float)(JudgeTime + 1) * edgex*linepos, 0);
                else
                {
                    this.gameObject.transform.localPosition = new Vector2(-edgex/2, (float)(JudgeTime + 0+linepos/2) *2* edgey);
                }
            }
            
        }
        else
        {
            this.gameObject.transform.localPosition = new Vector2(99, 0);
        }
    }
}
