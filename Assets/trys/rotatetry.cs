using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatetry : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (index == 1)
        {
            this.gameObject.transform.localScale = new Vector3(Mathf.Abs(Input.gyro.rotationRate.x), 1, 1);
        }
        if (index == 2)
        {
            this.gameObject.transform.localScale = new Vector3(1, Mathf.Abs(Input.gyro.rotationRate.y), 1);
        }
        if (index == 3)
        {
            this.gameObject.transform.localScale = new Vector3(Mathf.Abs(Input.gyro.rotationRate.z), Mathf.Abs(Input.gyro.rotationRate.z), 1);
        }
    }
}
