using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDown : MonoBehaviour
{
    public Collider parent;

    private void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(other, parent, true);
    }

    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other, parent, false);
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
