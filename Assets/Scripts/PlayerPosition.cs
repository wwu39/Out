using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var Player = GameObject.FindGameObjectWithTag("Player");
        var PlayerPos = Player.transform.position;
        switch (GlobalManager.ins.PlayerInitPos)
        {
            case PPos.Middle:
                PlayerPos.x = 0;
                Player.transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));
                break;
            case PPos.Left:
                PlayerPos.x = -4.05f;
                Player.transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));
                break;
            case PPos.Right:
                PlayerPos.x = 4.05f;
                Player.transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0));
                break;
        }
        Player.transform.position = PlayerPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
