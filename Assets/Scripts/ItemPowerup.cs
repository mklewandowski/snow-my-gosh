using UnityEngine;

public class ItemPowerup : Item
{
    void OnTriggerEnter(Collider collider)
    {
        Player player = collider.gameObject.GetComponent<Player>();
        if (player != null && Globals.CurrentGameState == Globals.GameState.Playing && isActive)
        {
            isActive = false;

            int debrisAmount = Random.Range(10, 15);
            debrisManager.StartDebris (debrisAmount, this.transform.position, debrisColor);

            if (itemType == ItemType.Arrow)
            {
                audioManager.PlaySpeedUpSound();
                sceneManager.SpeedUp();
            }
            else if (itemType == ItemType.Star)
            {
                audioManager.PlayInvincibleSound();
                sceneManager.Invincible();
            }
            else if (itemType == ItemType.Bomb)
            {
                audioManager.PlayBombSound();
                sceneManager.Bomb();
            }

            Destroy(this.gameObject);
        }
    }
}
