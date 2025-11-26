using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
              BossController h = collision.GetComponent<BossController>();

            if (h != null)
            {
                h.vida -= 1;
            }

            Destroy(gameObject);
        }
    }
}
