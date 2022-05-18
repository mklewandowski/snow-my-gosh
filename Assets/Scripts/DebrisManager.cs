using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisManager : MonoBehaviour
{
    [SerializeField]
    GameObject DebrisContainer;
    [SerializeField]
    GameObject DebrisPrefab;
    GameObject[] debrisPool = new GameObject[100];
    Debris[] debrisScripts = new Debris[100];

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < debrisPool.Length; x++)
        {
            debrisPool[x] = (GameObject)Instantiate (DebrisPrefab, new Vector3 (100f, 100f, 100f), Quaternion.identity, DebrisContainer.transform);
            debrisScripts[x] = debrisPool[x].GetComponent<Debris>();
        }
    }

    public void StartDebris(int amount, Vector3 position, Color color)
    {
        int count = 0;
        for (int x = 0; x < debrisPool.Length; x++)
        {
            if (!debrisScripts[x].InUse)
            {
                count++;
                debrisScripts[x].Use(position, color);
            }
            if (count >= amount)
                break;
        }
    }

    public void StopAll()
    {
        for (int x = 0; x < debrisScripts.Length; x++)
        {
            debrisScripts[x].StopUse();
        }
    }
}
