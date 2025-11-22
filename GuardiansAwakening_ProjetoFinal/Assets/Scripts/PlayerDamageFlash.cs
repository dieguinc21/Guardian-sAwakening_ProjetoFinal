using System.Collections;
using UnityEngine;

public class PlayerDamageFlash : MonoBehaviour
{
    public SpriteRenderer sr;

    public IEnumerator Flash()
    {
        for (int i = 0; i < 3; i++)
        {
            sr.color = new Color(1,1,1,0.3f);
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
