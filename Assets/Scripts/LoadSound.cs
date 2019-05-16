using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string LoadLevelSound;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            FMODUnity.RuntimeManager.PlayOneShot(LoadLevelSound);
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
