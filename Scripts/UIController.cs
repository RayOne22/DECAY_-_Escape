using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Text distanceText;
    [SerializeField] Text finalDistanceText;

    [SerializeField] GameObject imageShadow;
    [SerializeField] GameObject windowResult;
    [SerializeField] GameObject windowPause;
    [SerializeField] GameObject windowBackToMenu;
    [SerializeField] GameObject windowInstruction;

    [SerializeField] ButtonAndWindow bawImageShadow;
    [SerializeField] ButtonAndWindow bawWindowRules;
    [SerializeField] ButtonAndWindow bawRulesText;
    [SerializeField] ButtonAndWindow bawButtonRulesExit;
    [SerializeField] ButtonAndWindow bawWindowPause;
    [SerializeField] ButtonAndWindow bawButtonBackToMenu;
    [SerializeField] ButtonAndWindow bawButtonContinue;
    [SerializeField] ButtonAndWindow bawWindowBackToMenu;
    [SerializeField] ButtonAndWindow bawButtonYes;
    [SerializeField] ButtonAndWindow bawButtonNo;
    [SerializeField] ButtonAndWindow bawWindowResult;
    [SerializeField] ButtonAndWindow bawButtonMenu;
    [SerializeField] ButtonAndWindow bawButtonRetry;
    [SerializeField] ButtonAndWindow bawFinalDistanceText;
    [SerializeField] AudioSource componentAudioSource;
    [SerializeField] AudioClip[] audioGuiFx;
    [SerializeField] AudioClip music;

    DataHolder dataHolder;

    bool s_Result = true;
    public int s_WindowRules = 0;


    void Awake()
    {
        s_WindowRules = PlayerPrefs.GetInt("Rules");

        imageShadow.SetActive(false);
        windowInstruction.SetActive(false);
        windowResult.SetActive(false);
        windowPause.SetActive(false);
        windowBackToMenu.SetActive(false);
    }

    void Start()
    {
        // componentAudioSource.PlayOneShot(music);
        if (s_WindowRules == 0)
        {
            WindowRulesStart();
        }
    }

    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = $"RUN: {distance}";

        if (player.isKnockOut & s_Result)
        {
            s_Result = false;
            Result();
            finalDistanceText.text = $"{distance}";
            int highResult = distance;

            if (PlayerPrefs.GetInt("Result") < highResult)
            {
                componentAudioSource.PlayOneShot(audioGuiFx[6]);
                PlayerPrefs.SetInt("Result", highResult);
            }
            else
            {
                componentAudioSource.PlayOneShot(audioGuiFx[7]);
            }
        }
    }

    public async void Result()
    {
        imageShadow.SetActive(true);
        windowResult.SetActive(true);
        await Task.Delay(1);
        bawImageShadow.StartCoroutine("ColorAlphaPauseOpen");
        bawWindowResult.StartCoroutine("ColorAlphaObjectOpen");
        bawFinalDistanceText.StartCoroutine("ColorAlphaTextOpen");
        bawButtonMenu.StartCoroutine("ColorAlphaObjectOpen");
        bawButtonRetry.StartCoroutine("ColorAlphaObjectOpen");
    }

    public void Menu()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[5]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseClose");
        bawWindowResult.StartCoroutine("ColorAlphaObjectClose");
        bawFinalDistanceText.StartCoroutine("ColorAlphaTextClose");
        bawButtonMenu.StartCoroutine("ColorAlphaObjectClose");
        bawButtonRetry.StartCoroutine("ColorAlphaObjectClose");
        SceneTransition.SwitchToScene("MenuScene");
    }

    public void Retry()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[4]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseClose");
        bawWindowResult.StartCoroutine("ColorAlphaObjectClose");
        bawFinalDistanceText.StartCoroutine("ColorAlphaTextClose");
        bawButtonMenu.StartCoroutine("ColorAlphaObjectClose");
        bawButtonRetry.StartCoroutine("ColorAlphaObjectClose");
        SceneTransition.SwitchToScene("MainRunScene");
    }

    public async void BackToMenu()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[8]);
        windowBackToMenu.SetActive(true);
        await Task.Delay(1);
        bawWindowBackToMenu.StartCoroutine("ColorAlphaObjectOpen");
        bawButtonNo.StartCoroutine("ColorAlphaObjectOpen");
        bawButtonYes.StartCoroutine("ColorAlphaObjectOpen");
    }

    public void BackToMenuYes()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[5]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseClose");
        bawWindowPause.StartCoroutine("ColorAlphaObjectClose");
        bawButtonBackToMenu.StartCoroutine("ColorAlphaObjectClose");
        bawButtonContinue.StartCoroutine("ColorAlphaObjectClose");
        bawWindowBackToMenu.StartCoroutine("ColorAlphaObjectClose");
        bawButtonNo.StartCoroutine("ColorAlphaObjectClose");
        bawButtonYes.StartCoroutine("ColorAlphaObjectClose");
        Time.timeScale = 1;
        SceneTransition.SwitchToScene("MenuScene");
    }

    public void BackToMenuNo()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[8]);
        bawWindowBackToMenu.StartCoroutine("ColorAlphaObjectClose");
        bawButtonNo.StartCoroutine("ColorAlphaObjectClose");
        bawButtonYes.StartCoroutine("ColorAlphaObjectClose");
    }

    public async void Continue()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[3]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseClose");
        bawWindowPause.StartCoroutine("ColorAlphaObjectClose");
        bawButtonBackToMenu.StartCoroutine("ColorAlphaObjectClose");
        bawButtonContinue.StartCoroutine("ColorAlphaObjectClose");
        await Task.Delay(280);
        Time.timeScale = 1;
    }

    public async void Pause()
    {
        Time.timeScale = 0;
        imageShadow.SetActive(true);
        windowPause.SetActive(true);
        await Task.Delay(1);
        componentAudioSource.PlayOneShot(audioGuiFx[2]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseOpen");
        bawWindowPause.StartCoroutine("ColorAlphaObjectOpen");
        bawButtonBackToMenu.StartCoroutine("ColorAlphaObjectOpen");
        bawButtonContinue.StartCoroutine("ColorAlphaObjectOpen");
    }

    public async void WindowRulesStart()
    {
        await Task.Delay(2000);
        Time.timeScale = 0;
        imageShadow.SetActive(true);
        windowInstruction.SetActive(true);
        await Task.Delay(1);
        componentAudioSource.PlayOneShot(audioGuiFx[0]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseOpen");
        bawWindowRules.StartCoroutine("ColorAlphaObjectOpen");
        bawRulesText.StartCoroutine("ColorAlphaTextOpen");
        bawButtonRulesExit.StartCoroutine("ColorAlphaObjectOpen");
    }

    public async void WindowRulesClose()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[1]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseClose");
        bawWindowRules.StartCoroutine("ColorAlphaObjectClose");
        bawRulesText.StartCoroutine("ColorAlphaTextClose");
        bawButtonRulesExit.StartCoroutine("ColorAlphaObjectClose");
        s_WindowRules = 1;
        PlayerPrefs.SetInt("Rules", s_WindowRules);
        await Task.Delay(280);
        Time.timeScale = 1;
    }
}
