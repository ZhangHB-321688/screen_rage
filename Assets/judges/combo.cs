using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class combo : MonoBehaviour
{
    public static int comboNum;
    // Start is called before the first frame update
    void Start()
    {
        comboNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (comboNum > 0)
            this.gameObject.GetComponent<Text>().text = comboNum.ToString();
        else this.gameObject.GetComponent<Text>().text = "";
    }
}
