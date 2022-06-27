using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField] VideoPlayer componentVideoPlayer;
    [SerializeField] GameObject skipIntroButton;

    int s_SkipIntroButton = 0;


    void Awake()
    {
        s_SkipIntroButton = PlayerPrefs.GetInt("SkipIntroButton");
    }
    void Start()
    {
        componentVideoPlayer.loopPointReached += EndIntro;
        componentVideoPlayer.loopPointReached += SkipIntroButtonOn;
        if (s_SkipIntroButton == 1)
        {
            skipIntroButton.SetActive(true);
        }
    }
    
    void EndIntro(VideoPlayer vp)
    {
        SceneTransition.SwitchToScene("MenuScene");
    }

    public void SkipIntro()
    {
        SceneTransition.SwitchToScene("MenuScene");
    }

    public void SkipIntroButtonOn(VideoPlayer vp)
    {
        s_SkipIntroButton = 1;
        PlayerPrefs.SetInt("SkipIntroButton", s_SkipIntroButton);
    }

}
