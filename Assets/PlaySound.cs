using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Sound;

    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Sound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
