using UnityEngine;
using TMPro;
public class MoedaHudController : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI moedaText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void TextUpdate(int value)
    {
        moedaText.text = value.ToString();
        
    }

}
