using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public VideoPlayer video;
    public Button skip;
    // Start is called before the first frame update
    void Start()
    {
        video.loopPointReached += EndReached;
        skip.onClick.AddListener(SkipIntro);
    }

    private void EndReached(VideoPlayer video)
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void SkipIntro()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
