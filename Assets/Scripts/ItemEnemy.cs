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

            if (sceneManager.IsInvincible())
            {
                int debrisMax = 15;
                int debrisAmount = Random.Range(10, debrisMax);
                debrisManager.StartDebris (debrisAmount, this.transform.position, debrisColor);
                Destroy(this.gameObject);
            }
            else
            {
                sceneManager.EndGame();
            }
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
