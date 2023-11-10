using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class xuanguanDEMO : MonoBehaviour
{
    public GameObject g,linePos,downFallTap;
    public Text o,o1;
    float offset;
    // Start is called before the first frame update
    void Start()
    {
        o.text = PlayerPrefs.GetFloat("Offset", 0).ToString();
        linePos.GetComponent<Slider>().value = PlayerPrefs.GetFloat("LinePos", 1);
        downFallTap.GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("down",0) == 1;
    }
    public void click()
    {
        if (o1.text.Length > 0)
        {
            try
            {
                offset = (float)System.Convert.ToDouble(o1.text);
            }
            catch
            {
                offset = 0;
            }
        }
        else
        {
            offset = PlayerPrefs.GetFloat("Offset", 0);
        }
        readChart2.end = false;
        g.transform.parent = null;
        if(xuanguanTEXT.tn == "ceshi114514")
        {
            SceneManager.LoadScene("SampleScene");
            return;
        }
        PlayerPrefs.SetFloat("LinePos",linePos.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("Offset",offset);
        PlayerPrefs.SetInt("down",downFallTap.GetComponent<Toggle>().isOn?1:0);
#if UNITY_EDITOR
        xuanguanTEXT.ChartPath = Application.dataPath + "/Resources/Charts/" + xuanguanTEXT.tn + ".txt";
        xuanguanTEXT.MusicPath = Application.dataPath + "/Resources/Charts/" + xuanguanTEXT.tn + ".mp3";
#else
        xuanguanTEXT.ChartPath = Application.persistentDataPath + "/Charts/" + xuanguanTEXT.tn + ".txt";
        xuanguanTEXT.MusicPath = Application.persistentDataPath + "/Musics/" + xuanguanTEXT.tn + ".mp3";
#endif
        DontDestroyOnLoad(g);
        SceneManager.LoadScene("playingScene");
    }
    public void choose()
    {
        DontDestroyOnLoad(g);
        xuanguanTEXT.isInput = true;
        //xuanguanTEXT.ChartPath = EditorUtility.OpenFilePanel("选择谱面文件", "", "txt");
        //xuanguanTEXT.MusicPath = EditorUtility.OpenFilePanel("选择音频文件", "", "wav");
        xuanguanTEXT.ChartPath = xuanguanTEXT.tn + ".txt";
        xuanguanTEXT.MusicPath = xuanguanTEXT.tn + ".wav";
        SceneManager.LoadScene("playingScene");
        
    }
}
