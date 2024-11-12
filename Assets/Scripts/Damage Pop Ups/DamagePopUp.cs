using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Color textColor; // Use a Color bc you cannot change textMesh.color.a value
    private float lifeTime;

    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    // Create a damage text pop up object
    public static void Create(Vector3 position, int damageAmount)
    {
        DamagePopUp damagePopUp = ObjectPools.DequeueObject<DamagePopUp>("DamagePopUp");
        damagePopUp.gameObject.transform.position = position;
        damagePopUp.gameObject.SetActive(true);
        
        damagePopUp.textMesh.SetText(damageAmount.ToString());
        damagePopUp.lifeTime = 1f;
        damagePopUp.textColor = damagePopUp.textMesh.color;

        damagePopUp.StartCoroutine(damagePopUp.Animate());
    }

    IEnumerator Animate()
    {
        float moveYSpeed = 0.2f;
        while (true) 
        {
            transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

            lifeTime -= Time.deltaTime;

            if (lifeTime > 0.7f) // Text animation
            {
                transform.localScale += 1.5f * Time.deltaTime * Vector3.one; // Inflate the text
            } else
            {
                transform.localScale -= 1f * Time.deltaTime * Vector3.one; // Deflate the text
            }
            
            if (lifeTime <= 0) // if life time ends then start disappearing
            {
                float disappearSpeed = 5f;
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                
                if (textColor.a <= 0)
                {
                    ObjectPools.EnqueueObject(this, "DamagePopUp");
                    yield break;
                }
            }
            yield return null; // wait until next frame and continue execution
        }
    }
}
