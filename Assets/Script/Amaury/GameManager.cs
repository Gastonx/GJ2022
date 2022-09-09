using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState {
    PRE_INTRO,
    MENU,
    INTRO,
    GAME,
    OUTRO
}
public class GameManager : MonoBehaviour {
    // Start is called before the first frame update

    public GameState gameState;

    public VideoClip[] clips;
    public RenderTexture[] clipsTextures;
    
    public float[] clipsDuration;

    public VideoPlayer videoPlayer;
    public GameObject videoDisplay;

    public GameObject videoCanvas;
    public GameObject menuCanvas;

    private bool lastPlaying;
    
    public static GameManager Instance { get; private set; }

    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(videoCanvas);

        Instance = this;
    }

    private void Start() {
        if (gameState == GameState.PRE_INTRO) {
            menuCanvas.SetActive(false);
            videoPlayer.clip = clips[0];
            videoPlayer.targetTexture = clipsTextures[0];
            videoDisplay.GetComponent<RawImage>().texture = clipsTextures[0];
            videoPlayer.Play();
        }

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R))
            gameState = GameState.OUTRO;

        switch (gameState) {
            case GameState.PRE_INTRO:
                if (lastPlaying && !videoPlayer.isPlaying) {
                    gameState = GameState.MENU;
                    videoCanvas.SetActive(false);
                    menuCanvas.SetActive(true);
                }
                break;
            
            case GameState.INTRO:
                if (videoPlayer.time >= clipsDuration[0] - 0.1f) {
                    videoDisplay.SetActive(false);
                    gameState = GameState.GAME;
                    SceneManager.LoadScene("LD_Speed01");
                }

                break;
            
            case GameState.OUTRO:
                if (SceneManager.GetActiveScene().name != "VideoScene" && !videoPlayer.isPlaying) {
                    SceneManager.LoadScene("VideoScene");
                    VideoController.Instance.LaunchEndVideo();
                }
                break;
        }

        lastPlaying = videoPlayer.isPlaying;
    }


    public void OnLaunchGame() {
        SceneManager.LoadScene("VideoScene");
    }

    public void OnQuit() {
        Application.Quit();
    }

    
}
