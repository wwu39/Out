using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string EnterSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FMODUnity.RuntimeManager.PlayOneShot(EnterSound);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
