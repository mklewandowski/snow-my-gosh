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

            if (itemType == ItemType.Heart)
            {
                sceneManager.GetHeart();
            }
            else if (itemType == ItemType.Coin)
            {
                audioManager.PlayCoinSound();
                sceneManager.GetCoin();
            }

            Destroy(this.gameObject);
        }
    }
}
