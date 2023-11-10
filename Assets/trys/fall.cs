using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(freefalling.isfalling == true)
        {
            this.gameObject.transform.localScale = new Vector3(20, 20, 1);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
