using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showLinePos : MonoBehaviour
{
    double edgex,edgey;
    public bool right;
    public GameObject ipf,toggle;
    // Start is called before the first frame update
    void Start()
    {
        edgex = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, 0, 0)).x;
        edgey = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, 0, 0)).y;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle.GetComponent<Toggle>().isOn)
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            this.gameObject.transform.localPosition = new Vector3(0, (float)(edgey * ipf.GetComponent<Slider>().value), 0);
        }
        else
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            this.gameObject.transform.localPosition = new Vector3((float)(right ? edgex * ipf.GetComponent<Slider>().value : -edgex * ipf.GetComponent<Slider>().value), 0, 0);
        }
    }
}
