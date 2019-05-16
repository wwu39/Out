using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : Item
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        CenterPos = transform.position;
        if (RegeneratesTo.tag == "Tile" || RegeneratesTo.tag == "TileLike")
        {
            CenterPos.y += 0.75f;
            CenterPos.z += 1.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        GlobalManager.ins.TickingEvent.getPlaybackState(out state);
        if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING) GlobalManager.ins.TickingEvent.start();
        Update_regen(true);
    }
}
