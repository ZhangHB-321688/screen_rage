using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guodu : MonoBehaviour
{
    Vector3 scr;
    float t;
    void Start()
    {
        Input.gyro.enabled = true;
        scr = Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Screen.width, UnityEngine.Screen.height));
        this.gameObject.transform.localScale = new Vector3(scr.x * 2, scr.y*2, 1);
        t = 0;
    }

    // Update is called once per frame

    void Update()
    {
        t = t + Time.deltaTime;
        if (t > 1) Destroy(this.gameObject);
        this.gameObject.transform.localPosition = new Vector3(-t*t*t*scr.x*2,0,-1);
    }
}
