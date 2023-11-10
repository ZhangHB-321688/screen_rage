using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gyro : MonoBehaviour
{
    float x, y, z;
    public Text t,txyz;
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.gyro.attitude.x;
        y = Input.gyro.attitude.y;
        z = Input.gyro.attitude.z;
        //t.text = Mathf.Sqrt(Input.acceleration.z* Input.acceleration.z+
        //    Input.acceleration.y* Input.acceleration.y+
        //    Input.acceleration.x* Input.acceleration.x).ToString();
        //txyz.text = x.ToString() + " " + y.ToString() + " " + z.ToString();
        txyz.text = rotate.xrolling.ToString()+" "+rotate.zrolling.ToString();
        t.text = Input.gyro.rotationRate.z.ToString();
    }
}
