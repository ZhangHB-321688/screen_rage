using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playAudio : MonoBehaviour
{
    public static bool start;
    AudioClip clip;
    private IEnumerator PlayAudio()
    {
        //获取.wav文件，并转成AudioClip
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + xuanguanTEXT.MusicPath, AudioType.MPEG);
        //等待转换完成
        yield return www.SendWebRequest();
        //获取AudioClip
        AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
        //设置当前AudioSource组件的AudioClip
        this.GetComponent<AudioSource>().clip = audioClip;
        //播放声音
        this.GetComponent<AudioSource>().Play();
    }
    // Start is called before the first frame update
    string path = xuanguanTEXT.tn;
    void Start()
    {
        offset = PlayerPrefs.GetFloat("Offset", 0);
        start = false;
        if (!xuanguanTEXT.isInput)
        {
            clip = Resources.Load<AudioClip>("Charts/" + path);
            this.GetComponent<AudioSource>().clip = clip;
        }
        f = true;
    }
    bool f;
    float offset;
    // Update is called once per frame
    void Update()
    {
        if (f) {
            if (Time.time > readChart2.startOffset + readChart2.inittime + offset) start = true;
            if (start == true)
            {
                StartCoroutine(PlayAudio());
                f = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) || readChart2.end)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
