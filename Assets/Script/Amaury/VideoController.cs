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
                
                GameManager.Instance.videoPlayerOutro.gameObject.SetActive(false);
                GameManager.Instance.videoDisplayOutro.SetActive(false);
                GameManager.Instance.videoPlayer.clip = GameManager.Instance.clips[1];
                GameManager.Instance.videoPlayer.targetTexture = GameManager.Instance.clipsTextures[1];
                GameManager.Instance.videoDisplay.GetComponent<RawImage>().texture = GameManager.Instance.clipsTextures[1];
                GameManager.Instance.videoPlayer.Play();
                break;
        }
    }

    public void LaunchEndVideo()
    {
        if (!GameManager.Instance.videoPlayerOutro.isPlaying) {
            Debug.Log("enter outro " + GameManager.Instance.clips[2].name);
            GameManager.Instance.videoPlayerOutro.clip = GameManager.Instance.clips[2];
            GameManager.Instance.videoPlayerOutro.targetTexture = GameManager.Instance.clipsTextures[2];
            GameManager.Instance.videoDisplayOutro.GetComponent<RawImage>().texture = GameManager.Instance.clipsTextures[2];
            Debug.Log("clip Name : " + GameManager.Instance.videoPlayer.clip.name);
            GameManager.Instance.videoPlayerOutro.gameObject.SetActive(true);
            GameManager.Instance.videoDisplayOutro.SetActive(true);
            
            GameManager.Instance.videoCanvas.SetActive(false);
            GameManager.Instance.videoDisplay.SetActive(false);
            GameManager.Instance.videoPlayerOutro.Play();

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
