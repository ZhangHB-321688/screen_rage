using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localScale = new Vector3(Mathf.Abs(Input.gyro.userAcceleration.x) , 1, 1);
        this.gameObject.transform.localPosition = new Vector2(Input.gyro.userAcceleration.x / 2 , 1);
    }
}