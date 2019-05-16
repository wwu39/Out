using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyBlock : Item
{
    bool enlarging = true;

    // Start is called before the first frame update
    new void Start()
    {
        transform.localScale = 0.05f * Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        if (enlarging)
        {
            if (transform.localScale.x >= 0.15)
            {
                transform.localScale = 0.15f * Vector3.one;
                enlarging = false;
            }
            else
            {
                transform.localScale += Vector3.one * Time.deltaTime * 0.05f;
            }
        }
        else if (!isDead)
        {
            GameObject newblock = Instantiate(RegeneratesTo);
            newblock.transform.position = transform.position;
            newblock.transform.rotation = transform.rotation;
            newblock.GetComponent<Item>().regenerateTime = regenerateTime;
            Destroy(gameObject);
            isDead = true;
        }
    }
}
