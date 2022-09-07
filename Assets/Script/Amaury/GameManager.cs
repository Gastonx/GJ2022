using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Video;

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
    
    
    void Start() {
        gameState = GameState.INTRO;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.R))
        {
            gameState = GameState.OUTRO;
        }
        
        
        switch (gameState) {
            case GameState.INTRO:
                if (videoPlayer.time >= clipsDuration[0] - 0.1f) {
                    videoDisplay.SetActive(false);
                    gameState = GameState.GAME;
                }

                break;
            
            case GameState.OUTRO:
                if (!videoPlayer.isPlaying) {
                    videoDisplay.SetActive(true);
                    videoPlayer.clip = clips[2];
                    videoPlayer.targetTexture = clipsTextures[2];
                    videoPlayer.Play();
                }
                break;
        }
    }
}
