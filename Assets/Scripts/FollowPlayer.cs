using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    Vector3 left = new Vector3(-72, 0, -10);
    Vector3 middle = new Vector3(-36, 0, -10);
    Vector3 right = new Vector3(0, 0, -10);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var playerPos = Player.transform.position;
        if (playerPos.x <= -18 && playerPos.x > -54) transform.position = middle;
        //else if (playerPos.x <= 18 && playerPos.x > -18) transform.position = right;
        else if (playerPos.x <= -54 && playerPos.x > -90) transform.position = left;
    }
}
