using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerTitle : MonoBehaviour
{
    [SerializeField] Text bestResultText;

    [SerializeField] GameObject imageShadow;
    [SerializeField] GameObject windowGameDevTeam;
    [SerializeField] GameObject windowLeaderboard;
    [SerializeField] GameObject windowNews;
    [SerializeField] GameObject windowNewsSH;
    [SerializeField] GameObject windowNewsSHHandle;
    [SerializeField] GameObject windowNewsSV;
    [SerializeField] GameObject windowNewsSVHHandle;

    [SerializeField] ButtonAndWindow bawImageShadow;
    [SerializeField] ButtonAndWindow bawWindowGameDevTeam;
    [SerializeField] ButtonAndWindow bawWindowLeaderboard;
    [SerializeField] ButtonAndWindow bawBestResultText;
    [SerializeField] ButtonAndWindow bawWindowNews;
    [SerializeField] ButtonAndWindow bawNewsText;
    [SerializeField] ButtonAndWindow bawWindowNewsSH;
    [SerializeField] ButtonAndWindow bawWindowNewsSHHandle;
    [SerializeField] ButtonAndWindow bawWindowNewsSV;
    [SerializeField] ButtonAndWindow bawWindowNewsSVHandle;
    // [SerializeField] ButtonAndWindow bawButtonWorldResults;
    [SerializeField] AudioSource componentAudioSource;
    [SerializeField] AudioClip[] audioGuiFx;

    DataHolder dataHolder;


    void Awake()
    {
        imageShadow.SetActive(false);
        windowGameDevTeam.SetActive(false);
        windowLeaderboard.SetActive(false);
        windowNews.SetActive(false);
        
        bestResultText.text = PlayerPrefs.GetInt("Result").ToString();
    }

    // Buttons Main menu
    public void Run()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[0]);
        SceneTransition.SwitchToScene("MainRunScene");
    }

    public async void Leaderboard()
    {
        imageShadow.SetActive(true);
        windowLeaderboard.SetActive(true);
        await Task.Delay(1);
        componentAudioSource.PlayOneShot(audioGuiFx[1]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseOpen");
        bawWindowLeaderboard.StartCoroutine("ColorAlphaObjectOpen");
        // bawButtonWorldResults.StartCoroutine("ColorAlphaObjectOpen");
        bawBestResultText.StartCoroutine("ColorAlphaTextOpen");
    }

    public void WorldResults()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[5]);
    }

    public void LeaderboardExit()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[4]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseClose");
        bawWindowLeaderboard.StartCoroutine("ColorAlphaObjectClose");
        // bawButtonWorldResults.StartCoroutine("ColorAlphaObjectClose");
        bawBestResultText.StartCoroutine("ColorAlphaTextClose");
    }

    public async void GameDevTeam()
    {
        imageShadow.SetActive(true);
        windowGameDevTeam.SetActive(true);
        await Task.Delay(1);
        componentAudioSource.PlayOneShot(audioGuiFx[2]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseOpen");
        bawWindowGameDevTeam.StartCoroutine("ColorAlphaObjectOpen");
    }

    public void GameDevTeamExit()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[4]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseClose");
        bawWindowGameDevTeam.StartCoroutine("ColorAlphaObjectClose");
    }

    public void Quit()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[3]);
        Application.Quit();
    }

    // Buttons Optional menu
    public void LicenceAgreement(string url)
    {
        componentAudioSource.PlayOneShot(audioGuiFx[2]);
        Application.OpenURL(url);
        
    }

    public void News()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[2]);
        imageShadow.SetActive(true);
        windowNews.SetActive(true);
        bawImageShadow.StartCoroutine("ColorAlphaPauseOpen");
        bawWindowNews.StartCoroutine("ColorAlphaObjectOpen");
        bawNewsText.StartCoroutine("ColorAlphaTextOpen");
        // bawWindowNewsSH.StartCoroutine("ColorAlphaObjectOpen");
        // bawWindowNewsSHHandle.StartCoroutine("ColorAlphaObjectOpen");
        bawWindowNewsSV.StartCoroutine("ColorAlphaObjectOpen");
        bawWindowNewsSVHandle.StartCoroutine("ColorAlphaObjectOpen");
    }

    public void NewsExit()
    {
        componentAudioSource.PlayOneShot(audioGuiFx[4]);
        bawImageShadow.StartCoroutine("ColorAlphaPauseClose");
        bawWindowNews.StartCoroutine("ColorAlphaObjectClose");
        bawNewsText.StartCoroutine("ColorAlphaTextClose");
        // bawWindowNewsSH.StartCoroutine("ColorAlphaObjectClose");
        // bawWindowNewsSHHandle.StartCoroutine("ColorAlphaObjectClose");
        bawWindowNewsSV.StartCoroutine("ColorAlphaObjectClose");
        bawWindowNewsSVHandle.StartCoroutine("ColorAlphaObjectClose");
    }

    // Optional Functions
    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
}
