using System.Collections;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    public Material whiteFlashMaterial; // Assign the white flash material in the Inspector
    private Material originalMaterial;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public void StartFlash(float duration, int flashCount)
    {
        StartCoroutine(FlashCoroutine(duration, flashCount));
    }

    private IEnumerator FlashCoroutine(float duration, int flashCount)
    {
        Debug.Log("Flash");
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.material = whiteFlashMaterial;
            yield return new WaitForSeconds(duration / (flashCount * 2));

            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(duration / (flashCount * 2));
        }
    }
}
