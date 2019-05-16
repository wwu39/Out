using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject floorTile;

    // Start is called before the first frame update
    void Start()
    {
        for (float i = -18; i <= 18; i += 1.5f)
        {
            GameObject newTile = Instantiate(floorTile) as GameObject;
            newTile.transform.position = new Vector3(i, -10.5f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
