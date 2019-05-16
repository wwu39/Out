using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float travelDistance;
    public bool movingDown = true;
    public float movingSpeed = 0.2f;

    Vector3 startPos;
    Vector3 curPos;

    Material mat;
    float i = 0;
    bool reverse;
    Color c;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        c = new Color();
        startPos = transform.position;
        if (!movingDown)
        {
            startPos.y -= travelDistance;
            transform.position = startPos;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // changing color
        if (i >= 200) reverse = true;
        if (i <= 0) reverse = false;
        if (reverse) i -= 10f; else i += 10f;
        c.r = i / 255f;
        c.g = i / 255f;
        mat.color = c;

        // moving
        if (travelDistance > 0)
        {
            curPos = transform.position;
            if (curPos.y <= startPos.y) movingDown = true;
            if (curPos.y >= startPos.y + travelDistance) movingDown = false;
            if (movingDown) curPos.y += movingSpeed;
            else curPos.y -= movingSpeed;
            transform.position = curPos;
        }
    }
}
