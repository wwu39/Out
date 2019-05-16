using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Running, Win, Lose, End }

public class GameManager : MonoBehaviour
{
    static public GameManager ins;
    public bool allowedInput;
    public GameState state = GameState.Running;

    public GameObject winsc;
    public GameObject losesc;
    GameObject canvas;
    bool restart = true;
    GameState WL = GameState.Lose;

    IEnumerator Wait(int sec)
    {
        yield return new WaitForSecondsRealtime(sec);
    }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (ins == null) ins = this;
        else if (ins != this) Debug.LogError("Error! Multiple GameManager");
        if (SceneManager.GetActiveScene().name == "Easy")
        {
            GlobalManager.ins.BGM_EasyPlayer.start();
            GlobalManager.ins.EasySceneLoadCount += 1;
        }
        if (SceneManager.GetActiveScene().name == "Start") GlobalManager.ins.EasySceneLoadCount += 0;
        if (SceneManager.GetActiveScene().name == "Hard") GlobalManager.ins.BGM_HardPlayer.start();
        allowedInput = true;
        canvas = GameObject.Find("Canvas");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GlobalManager.ins.BGM_EasyPlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            GlobalManager.ins.BGM_HardPlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                GlobalManager.ins.PlayerInitPos = PPos.Middle;
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }

        if (state == GameState.Win)
        {
            GlobalManager.ins.BGM_EasyPlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            GlobalManager.ins.BGM_HardPlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            GlobalManager.ins.BGM_WinPlayer.start();
            allowedInput = false;
            GameObject WIN = Instantiate(winsc, canvas.transform);
            state = GameState.End;
            WL = GameState.Win;
        }

        if (state == GameState.Lose)
        {
            GlobalManager.ins.BGM_EasyPlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            GlobalManager.ins.BGM_HardPlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            GlobalManager.ins.BGM_LosePlayer.start();
            allowedInput = false;
            restart = true;
            GameObject WIN = Instantiate(losesc, canvas.transform);
            state = GameState.End;
        }

        if (state == GameState.End)
        {
            if (restart)
            {
                StartCoroutine(Wait(5));
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    GlobalManager.ins.BGM_WinPlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    GlobalManager.ins.BGM_LosePlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    if (WL == GameState.Lose) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    else SceneManager.LoadScene(0);
                }
            }
        }
    }
}
