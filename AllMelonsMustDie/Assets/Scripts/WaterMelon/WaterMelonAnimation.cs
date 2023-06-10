using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterMelonAnimation : MonoBehaviour
{
    public UnityEvent onShouldSquashAndUnsquash = new UnityEvent();

    public float squashFactor = 0.5f;
    public float squashDuration = 0.1f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void SquashAndUnsquash()
    {
        StartCoroutine(SquashAndUnsquashCoroutine());
    }

    IEnumerator SquashAndUnsquashCoroutine()
    {
        // Squash
        yield return ScaleOverTime(new Vector3(originalScale.x, originalScale.y * squashFactor, originalScale.z), squashDuration);

        // Invoke the event to signal that the squash is complete
        onShouldSquashAndUnsquash.Invoke();

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
