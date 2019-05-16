using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] missiles;
    public float[] rebornTimes;
    public GameObject counterPrefab;
    Vector3[] rebornPoints;
    float[] rebornStartTimes;
    bool[] reborning;
    public float rebornTime = 3;
    GameObject[] counters;

    // Start is called before the first frame update
    void Start()
    {
        rebornPoints = new Vector3[missiles.Length];
        rebornStartTimes = new float[missiles.Length];
        reborning = new bool[missiles.Length];
        counters = new GameObject[missiles.Length];
        for (int i = 0; i < missiles.Length; ++i)
        {
            rebornPoints[i] = missiles[i].transform.position;
            counters[i] = Instantiate(counterPrefab);
            counters[i].transform.position = rebornPoints[i];
            counters[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < missiles.Length; ++i)
        {
            if (!missiles[i].activeSelf && !reborning[i])
            {
                reborning[i] = true;
                rebornStartTimes[i] = Time.time;
            }
            if (reborning[i])
            {
                if (Time.time - rebornStartTimes[i] > rebornTimes[i])
                {
                    missiles[i].SetActive(true);
                    missiles[i].transform.position = rebornPoints[i];
                    counters[i].SetActive(false);
                    reborning[i] = false;
                }
                else
                {
                    counters[i].SetActive(true);
                    counters[i].GetComponentInChildren<TextMesh>().text = (rebornTimes[i] - Mathf.RoundToInt(Time.time - rebornStartTimes[i])).ToString();
                }
            }
        }
    }
}
