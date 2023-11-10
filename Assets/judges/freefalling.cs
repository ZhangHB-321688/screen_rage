using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freefalling : MonoBehaviour
{
    public static bool isfalling;
    public static double falltime;
    // Start is called before the first frame update
    void Start()
    {
        falltime = 0;
        isfalling = false;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        isfalling = Mathf.Abs(Input.gyro.gravity.x + Input.gyro.userAcceleration.x)+
            Mathf.Abs(Input.gyro.gravity.y + Input.gyro.userAcceleration.y)+
            Mathf.Abs(Input.gyro.gravity.z + Input.gyro.userAcceleration.z) < 0.3;
        if (isfalling)
        {
            falltime += Time.deltaTime;
        }
        else falltime = 0;
    }
}
