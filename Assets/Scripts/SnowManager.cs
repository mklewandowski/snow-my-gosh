using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManager : MonoBehaviour
{
    [SerializeField]
    GameObject SnowContainer;
    [SerializeField]
    GameObject SnowPrefab;
    GameObject[] snowPool = new GameObject[1000];
    Snow[] snowScripts = new Snow[1000];
    float startPosY;
    float snowTimer = 0;
    float snowTimerMax = .6f;

    void Awake()
    {
        startPosY = Screen.height + 100f;
        for (int x = 0; x < snowPool.Length; x++)
        {
            snowPool[x] = (GameObject)Instantiate (SnowPrefab, new Vector3 (100f, 100f, 100f), Quaternion.identity, SnowContainer.transform);
            snowScripts[x] = snowPool[x].GetComponent<Snow>();
        }
    }

    void Update()
    {
        snowTimer -= Time.deltaTime;
        if (snowTimer <= 0)
        {
            // make snow
            StartSnow(40);
            snowTimer = snowTimerMax;
        }
    }

    public void StartSnow(int amount)
    {
        int count = 0;
        for (int x = 0; x < snowPool.Length; x++)
        {
            if (!snowScripts[x].InUse)
            {
                count++;
                Vector3 position = new Vector3(Random.Range(0, Screen.width), Random.Range(startPosY, startPosY + 50f), 0);
                snowScripts[x].Use(position);
            }
            if (count >= amount)
                break;
        }
    }

}
