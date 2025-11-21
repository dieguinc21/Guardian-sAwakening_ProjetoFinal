using UnityEngine;

public class Playerco : MonoBehaviour
{
    [SerializeField] private int moedaQtd = 0;

    [Header("Som da moeda")]
    public AudioClip somColeta;
    private AudioSource audioSource;

    private MoedaHudController mddController;

    [System.Obsolete]
    private void Awake()
    {
        mddController = FindObjectOfType<MoedaHudController>();
        mddController.TextUpdate(moedaQtd);

        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            moedaQtd++;
            mddController.TextUpdate(moedaQtd);

            if (somColeta != null && audioSource != null)
            {
                audioSource.PlayOneShot(somColeta);
            }

            Destroy(collision.gameObject);
        }
    }
}
