using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class accuracy : MonoBehaviour
{
    public static double d, n;
    // Start is called before the first frame update
    void Start()
    {
        d = 0;
        n = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (d == 0)
        {
            this.gameObject.GetComponent<Text>().text = "100.0%";
        }
        else
        {
            this.gameObject.GetComponent<Text>().text = (Mathf.Round((float)(n/d*1000))/10).ToString() + "%";
        }
    }
}
