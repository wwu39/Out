using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum dir { None, Up, Down }

public class FollowPlayer2 : MonoBehaviour
{
    public GameObject Player;
    dir moving = dir.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var camPos = transform.position;
        var pPosy = Player.transform.position.y;
        if (pPosy > 2 && camPos.y < 1.5 && moving == dir.None) moving = dir.Up;
        if (pPosy < -4.75 && camPos.y > -3 && moving == dir.None) moving = dir.Down;
        if (moving == dir.Up)
        {
            camPos.y += 0.15f;
            transform.position = camPos;
            if (camPos.y >= 1.5) moving = dir.None;
        }
        else if (moving == dir.Down)
        {
            camPos.y -= 0.15f;
            transform.position = camPos;
            if (camPos.y <= -3) moving = dir.None;
        }
    }
}
