using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Player>(out Player player);

        if (player)
        {
            player.AddCoin(1);
            Destroy(gameObject);
        }

    }

}
