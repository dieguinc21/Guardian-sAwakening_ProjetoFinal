using UnityEngine;

public class EnemyStomp : MonoBehaviour
{
    public EnemyRespawn enemyRespawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            enemyRespawn.KillEnemy(other);
        }
    }
}
