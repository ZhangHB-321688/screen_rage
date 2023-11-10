using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kaishiTXT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int throwtimeasInt = Mathf.Min(PlayerPrefs.GetInt("ThrowTime", 29) + 1, 120);
        float throwtime = (float)throwtimeasInt / 100;
        PlayerPrefs.SetInt("ThrowTime", throwtimeasInt);
        this.gameObject.GetComponent<Text>().text = "抛出手机" + throwtime.ToString() + "秒（" + (Mathf.Round((float)(throwtime*throwtime*1226))/1000).ToString() + "m以上）以开始";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
