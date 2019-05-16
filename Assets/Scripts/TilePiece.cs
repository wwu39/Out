using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePiece : MonoBehaviour
{
    public bool shrinking;

    // Start is called before the first frame update
    void Start()
    {
        shrinking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shrinking)
        {
            if (transform.localScale.x > 0) transform.localScale -= Vector3.one * Time.deltaTime * 0.1f;
            else if (transform.localScale.x < 0) transform.localScale += Vector3.one * Time.deltaTime * 0.1f;
            if (transform.localScale.magnitude < 0.25f)
            {
                var deadbody = GetComponentInParent<PlayerDeadbody>();
                if (deadbody) deadbody.fragcount--;
                Destroy(gameObject);
            }
        }
    }
}
