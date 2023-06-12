using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterMelonAnimation : MonoBehaviour
{
    private WaterMelonMovement waterMelonMovement;

    public float squashFactor = 0.5f;
    public float squashDuration = 0.25f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;

        waterMelonMovement = GetComponent<WaterMelonMovement>();
        waterMelonMovement.squash.AddListener(StartSquash);
        waterMelonMovement.unsquash.AddListener(StartUnsquash);
    }

    private void StartSquash()
    {
        StartCoroutine(Squash());
    }

    private void StartUnsquash()
    {
        StartCoroutine(Unsquash());
    }

    private IEnumerator Squash()
    {
        // Squash
        yield return ScaleOverTime(new Vector3(originalScale.x, originalScale.y * squashFactor, originalScale.z), squashDuration);
    }

    private IEnumerator Unsquash()
    {
        yield return new WaitForSeconds(squashDuration);

        // Unsquash
        yield return ScaleOverTime(originalScale, squashDuration);
    }

    IEnumerator ScaleOverTime(Vector3 target, float duration)
    {
        Vector3 startScale = transform.localScale;
        float time = 0;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, target, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = target;
    }
}
