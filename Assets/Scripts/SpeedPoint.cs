using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPoint : MonoBehaviour
{
    private AudioManager audioManager;
    private SceneManager sceneManager;

    float destroyGameObjectZPos = -10f;

    void Awake()
    {
        audioManager = GameObject.Find("SceneManager").GetComponent<AudioManager>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
    }

    void Update()
    {
        if (Globals.CurrentGameState == Globals.GameState.Restart
            || Globals.CurrentGameState == Globals.GameState.Ready
            || this.transform.position.z < destroyGameObjectZPos)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (Globals.CurrentGameState == Globals.GameState.Ready
            || Globals.CurrentGameState == Globals.GameState.Playing
            || Globals.CurrentGameState == Globals.GameState.ShowScore)
        {
            Vector3 movement = new Vector3 (0, 0, Globals.ScrollSpeed.z * Globals.ScrollDirection.z);
            this.GetComponent<Rigidbody>().velocity = movement;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        if (player != null && Globals.CurrentGameState == Globals.GameState.Playing)
        {
            audioManager.PlaySpeedUpSound();
            audioManager.PlayEngineSound();
            sceneManager.SpeedUp();
        }
    }
}
