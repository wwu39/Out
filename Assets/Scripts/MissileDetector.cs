using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDetector : MonoBehaviour
{
    Missile thisMisl;

    // Start is called before the first frame update
    void Start()
    {
        thisMisl = GetComponentInParent<Missile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!thisMisl.Target)
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Bomber")
            {
                thisMisl.Target = other.gameObject;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == thisMisl.Target)
        {
            thisMisl.Target = null;
        }
    }
}
