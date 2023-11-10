using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePos : MonoBehaviour
{
    public bool right;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("down", 0) == 0)
        {
            float edgex = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, 0, 0)).x * PlayerPrefs.GetFloat("LinePos", 1);
            this.gameObject.transform.localPosition = new Vector3((float)(right ? edgex : -edgex), 0, 0);
        }
        else
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            this.gameObject.transform.localPosition = new Vector3(0, (float)(Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, 0, 0)).y * PlayerPrefs.GetFloat("LinePos", 1)), 0);
        }

        Destroy(this);
    }
}
