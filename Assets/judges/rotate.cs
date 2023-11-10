using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    float x, y, z;
    public static bool z_cw,z_ccw,y_l,y_r,x_f,x_b;
    public static float xrolling, zrolling,xrollTime,zrollTime;
    float lxr, lzr;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.gyro.rotationRate.x;
        y = Input.gyro.rotationRate.y;
        z = Input.gyro.rotationRate.z;
        z_cw = z < -8;
        z_ccw = z > 8;
        y_l = y > 10;
        y_r = y < -10;
        x_f = x > 12;
        x_b = x < -12;
        if (Mathf.Abs(lxr - x) < 1.0 && Mathf.Abs(x) > 6)
        {
            xrolling += x;
            xrollTime += Time.deltaTime;
        }
        else
        {
            xrolling = 0;
            xrollTime = 0;

        }
            
        if (Mathf.Abs(lzr - z) < 1.0 && Mathf.Abs(z) > 4)
        {
            zrolling += z;
            zrollTime += Time.deltaTime;
        }
        else
        {
            zrolling = 0;
            zrollTime = 0;

        }
        lxr = x;
        lzr = z;
    }
}
