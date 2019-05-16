using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star2 : MonoBehaviour
{
    public GameObject FX;
    public GameObject Hint;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Crate")
        {
            collision.gameObject.SetActive(false);
            var newFX = Instantiate(FX);
            var FXPos = collision.gameObject.transform.position;
            FXPos.y += 0.75f;
            newFX.transform.position = FXPos;
            newFX.transform.localScale *= 10;
            GameManager.ins.state = GameState.Win;
        }
        if (collision.gameObject.tag == "Player")
        {
            Hint.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Hint.SetActive(false);
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
