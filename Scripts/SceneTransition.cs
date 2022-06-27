using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    [SerializeField] AudioSource componentAudioSource;
    Animator componentAnimator;
    AsyncOperation loadingSceneOperation;
    static SceneTransition instance;
    static bool shouldPlayOpeningAnimation = false;


    void Awake()
    {
        instance = this;
        componentAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        if (shouldPlayOpeningAnimation)
        {
            Time.timeScale = 1;
            componentAnimator.SetTrigger("sceneOpening");
            StartCoroutine("AudioVolumeUp");
            shouldPlayOpeningAnimation = false;
        }
    }

    public async static void SwitchToScene(string sceneName)
    {
        Time.timeScale = 0;
        instance.componentAnimator.SetTrigger("sceneClosing");
        instance.StartCoroutine("AudioVolumeDown");
        await Task.Delay(500);
        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.loadingSceneOperation.allowSceneActivation = false;
    }

    public void OnAnimationOver()
    {
        loadingSceneOperation.allowSceneActivation = true;
        shouldPlayOpeningAnimation = true;
    }

    IEnumerator AudioVolumeUp()
    {
        for (int i = 0; i < 10; i++)
        {
            componentAudioSource.volume += 0.1f;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    IEnumerator AudioVolumeDown()
    {
        for (int i = 0; i < 10; i++)
        {
            componentAudioSource.volume -= 0.1f;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}