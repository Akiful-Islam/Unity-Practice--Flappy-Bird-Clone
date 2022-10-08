using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;

    [SerializeField] private GameObject _fadeCanvas;
    [SerializeField] private Animator _fadeAnim;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void FadeIn(string levelName)
    {
        StartCoroutine(FadeInAnim(levelName));
    }

    private IEnumerator FadeInAnim(string levelName)
    {
        _fadeCanvas.SetActive(true);
        _fadeAnim.Play("Fade In");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
        FadeOut();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutAnim());
    }



    private IEnumerator FadeOutAnim()
    {
        _fadeAnim.Play("Fade Out");

        yield return new WaitForSeconds(1f);

        _fadeCanvas.SetActive(false);
    }
}
