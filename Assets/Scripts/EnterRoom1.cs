﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoom1 : MonoBehaviour
{
    public GameObject setActiveObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            setActiveObject.SetActive(true);
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
