using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PPos { Middle, Left, Right }

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager ins;

    public PPos PlayerInitPos = PPos.Middle;
    public int EasySceneLoadCount = 0;

    [FMODUnity.EventRef]
    public string Ticking;
    public FMOD.Studio.EventInstance TickingEvent;



    [FMODUnity.EventRef]
    public string BGM_Easy;
    [FMODUnity.EventRef]
    public string BGM_Hard;
    [FMODUnity.EventRef]
    public string BGM_Win;
    [FMODUnity.EventRef]
    public string BGM_Lose;
    public FMOD.Studio.EventInstance BGM_EasyPlayer;
    public FMOD.Studio.EventInstance BGM_HardPlayer;
    public FMOD.Studio.EventInstance BGM_WinPlayer;
    public FMOD.Studio.EventInstance BGM_LosePlayer;


    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        TickingEvent = FMODUnity.RuntimeManager.CreateInstance(Ticking);
        BGM_EasyPlayer = FMODUnity.RuntimeManager.CreateInstance(BGM_Easy);
        BGM_HardPlayer = FMODUnity.RuntimeManager.CreateInstance(BGM_Hard);
        BGM_WinPlayer = FMODUnity.RuntimeManager.CreateInstance(BGM_Win);
        BGM_LosePlayer = FMODUnity.RuntimeManager.CreateInstance(BGM_Lose);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
