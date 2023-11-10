using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xuanguanTEXT : MonoBehaviour
{
    public Text t;
    public static string tn,MusicPath,ChartPath;
    public static bool isInput;
    float inittime;
    // Start is called before the first frame update
    void Start()
    {
        isInput = false;
        inittime = 999999999999999;
    }

    // Update is called once per frame
    void Update()
    {
        
        tn = t.text;
        if(t == null)
        {
            if(inittime > Time.time)
                inittime = Time.time;
            if(inittime - Time.time < -5)
                Destroy(this.gameObject);
        }
    }
}
