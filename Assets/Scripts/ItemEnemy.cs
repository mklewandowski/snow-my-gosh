using UnityEngine;

public class ItemEnemy : Item
{
    void OnTriggerEnter(Collider collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        if (player != null && Globals.CurrentGameState == Globals.GameState.Playing && isActive)
        {
            isActive = false;

            audioManager.PlaySmashSound();

            int debrisMax = 15;
            int debrisAmount = Random.Range(10, debrisMax);
            debrisManager.StartDebris (debrisAmount, this.transform.position, debrisColor);

            Camera.main.GetComponent<CameraShake>().StartShake();

            if (!sceneManager.IsInvincible())
            {
                float newSpeed = Mathf.Max(Globals.minSpeed, Globals.ScrollSpeed.z - 1f);
                Globals.ScrollSpeed = new Vector3(0, 0, newSpeed);
            }
            Destroy(this.gameObject);
        }
    }

    public void BombEnemy()
    {
        isActive = false;
        int debrisAmount = 10;
        debrisManager.StartDebris (debrisAmount, this.transform.position, debrisColor);
        Destroy(this.gameObject);
    }
}
