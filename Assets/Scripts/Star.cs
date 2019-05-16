using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject FX;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SetActive(false);
            var newFX = Instantiate(FX);
            var FXPos = collision.gameObject.transform.position;
            FXPos.y += 0.75f;
            newFX.transform.position = FXPos;
            newFX.transform.localScale *= 10;
            GameManager.ins.state = GameState.Win;
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
