using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeManager : MonoBehaviour
{
    [SerializeField]
    GameObject SmokeContainer;
    [SerializeField]
    GameObject SmokePrefab;
    GameObject[] smokePool = new GameObject[200];
    Smoke[] smokeScripts = new Smoke[200];

    float smokeTimer = 0f;
    float smokeTimerMax = .05f;
    float resumeTimer = 0f;

    float speedTimer = 0f;
    float speedTimerMax = 1f;

    bool inUse = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < smokePool.Length; x++)
        {
            smokePool[x] = (GameObject)Instantiate (SmokePrefab, new Vector3 (100f, 100f, 100f), Quaternion.identity, SmokeContainer.transform);
            smokeScripts[x] = smokePool[x].GetComponent<Smoke>();
        }
    }

    void Update()
    {
        if (resumeTimer > 0)
        {
            resumeTimer -= Time.deltaTime;
            if (resumeTimer < 0)
            {
                inUse = true;
            }
        }

        smokeTimer -= Time.deltaTime;
        if (smokeTimer <= 0)
        {
            StartSmoke(Random.Range(4, 6));
            smokeTimer = smokeTimerMax;
        }

        if (speedTimer > 0)
        {
            speedTimer -= Time.deltaTime;
        }
    }

    public void StartSmoke(int amount)
    {
        if (!inUse) return;
        int count = 0;
        for (int x = 0; x < smokePool.Length; x++)
        {
            Vector3 position = this.transform.position;
            position.x = position.x + Random.Range(0, -.3f);
            position.y = position.y + Random.Range(-.12f, .12f);
            position.z = position.z + Random.Range(-.12f, .12f);

            int randVal = Random.Range(0, 2);
            Color color = speedTimer <= 0
                ? Color.white
                : randVal == 1 ? Color.yellow : new Color(255f / 255f, 106f / 255f, 0);

            if (!smokeScripts[x].InUse)
            {
                count++;
                smokeScripts[x].Use(position, color);
            }
            if (count >= amount)
                break;
        }
    }

    public void SpeedUp()
    {
        speedTimer = speedTimerMax;
    }

    public void StopAll()
    {
        for (int x = 0; x < smokeScripts.Length; x++)
        {
            smokeScripts[x].StopUse();
        }
    }

    public void PauseSmoke()
    {
        inUse = false;
    }

    public void ResumeSmoke(float delay)
    {
        resumeTimer = delay;
    }
}
