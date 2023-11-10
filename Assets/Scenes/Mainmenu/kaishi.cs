using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class kaishi : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 scr;
    public float needfalltime;
    float t, d,t3;
    void Start()
    {
        Input.gyro.enabled = true;
        scr = Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Screen.width, UnityEngine.Screen.height));
        d = 0;
    }
    // Update is called once per frame
    void Update()
    {
        needfalltime = (float)PlayerPrefs.GetInt("ThrowTime",30)/100;
        t = (float)freefalling.falltime / needfalltime;
        t3 = t * t * t;
        d = Mathf.Max((float)((d-t3)*0.95+t3-0.01), t3);
        this.gameObject.transform.localScale = new Vector3(d * scr.x * 2, 99, 1);
        if (freefalling.falltime > needfalltime)
        {
            SceneManager.LoadScene("xuanGuan");
        }
    }
}
