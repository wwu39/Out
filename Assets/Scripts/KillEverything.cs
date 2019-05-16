using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEverything : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") GameManager.ins.state = GameState.Lose;
        else if (collision.gameObject.tag == "Crate") collision.gameObject.GetComponent<Reset>().resetTransform();
        else if (collision.gameObject.tag == "Cannon") { }
        else Destroy(collision.gameObject);
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
