using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{

    public int videoIndex;

    private bool startTimer;
    private float timer;
    
    public static VideoController Instance { get; private set; }

    void Awake () {
        SceneManager.sceneUnloaded += OnExitScene;
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
    }
 
    
    
    void Start() {
        switch (videoIndex) {
            case 0:
                if(GameManager.Instance.gameState != GameState.OUTRO) GameManager.Instance.gameState = GameState.INTRO;
                GameManager.Instance.videoCanvas.SetActive(true);
                GameManager.Instance.videoDisplay.SetActive(true);
                GameManager.Instance.videoPlayer.clip = GameManager.Instance.clips[1];
                GameManager.Instance.videoPlayer.targetTexture = GameManager.Instance.clipsTextures[1];
                GameManager.Instance.videoDisplay.GetComponent<RawImage>().texture = GameManager.Instance.clipsTextures[1];
                GameManager.Instance.videoPlayer.Play();
                break;
        }
    }

    public void LaunchEndVideo()
    {
        if (!GameManager.Instance.videoPlayer.isPlaying) {
            Debug.Log("start");
            GameManager.Instance.videoDisplay.SetActive(true);
            GameManager.Instance.videoPlayer.clip = GameManager.Instance.clips[2];
            GameManager.Instance.videoPlayer.targetTexture = GameManager.Instance.clipsTextures[2];
            GameManager.Instance.videoDisplay.GetComponent<RawImage>().texture = GameManager.Instance.clipsTextures[2];
            GameManager.Instance.videoPlayer.Play();

            StartCoroutine(WaitToEnd());
        }
    }

    private IEnumerator WaitToEnd() {
        yield return new WaitForSeconds(GameManager.Instance.clipsDuration[1]);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Menu",LoadSceneMode.Additive);
    }
    
    private void OnExitScene<Scene> (Scene scene)
    {
        videoIndex++;
    }
}
