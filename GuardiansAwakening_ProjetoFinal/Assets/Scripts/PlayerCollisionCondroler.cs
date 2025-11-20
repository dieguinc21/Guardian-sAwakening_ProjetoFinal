using UnityEngine;

public class Playerco : MonoBehaviour
{

    [SerializeField] private int moedaQtd = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private MoedaHudController mddController;

    private void Awake()
    {
        mddController = FindObjectOfType<MoedaHudController>();
        mddController.TextUpdate(moedaQtd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            moedaQtd = moedaQtd + 1;
            mddController.TextUpdate(moedaQtd);
            Destroy(collision.gameObject);
        }

    }
}
