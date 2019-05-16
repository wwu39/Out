using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterMenu : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "Hard")
            {
                GlobalManager.ins.PlayerInitPos = PPos.Left;
            }
            if (SceneManager.GetActiveScene().name == "Easy")
            {
                GlobalManager.ins.PlayerInitPos = PPos.Right;
            }
            GlobalManager.ins.BGM_EasyPlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            GlobalManager.ins.BGM_HardPlayer.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            SceneManager.LoadScene("Start");
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
