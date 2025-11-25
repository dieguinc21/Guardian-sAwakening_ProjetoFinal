using UnityEngine;

public class PlayerTiro : MonoBehaviour
{
    public GameObject prefabTiro;
    public Transform spawnTiro;
    public float velocidade = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject t = Instantiate(prefabTiro, spawnTiro.position, spawnTiro.rotation);
            t.GetComponent<Rigidbody2D>().velocity = transform.right * velocidade;
        }
    }
}



