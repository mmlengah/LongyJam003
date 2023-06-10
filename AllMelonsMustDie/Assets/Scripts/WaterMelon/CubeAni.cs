using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAni : MonoBehaviour
{
    public float jumpHeight = 1f;
    public float squashFactor = 0.5f;
    public float jumpDuration = 0.5f;
    public float squashDuration = 0.1f;

    void Start()
    {
        StartCoroutine(JumpAndSquash());
    }

    IEnumerator JumpAndSquash()
    {
        Vector3 originalScale = transform.localScale;

        while (true)
        {
            // Squash
            yield return ScaleOverTime(new Vector3(originalScale.x, originalScale.y * squashFactor, originalScale.z), squashDuration);

            // Jump
            Vector3 targetPosition = transform.position + new Vector3(0, jumpHeight, 0);
            yield return MoveOverTime(targetPosition, jumpDuration);

            // Unsquash
            yield return ScaleOverTime(originalScale, squashDuration);

            // Fall
            yield return MoveOverTime(transform.position - new Vector3(0, jumpHeight, 0), jumpDuration);
        }
    }

    IEnumerator MoveOverTime(Vector3 target, float duration)
    {
        Vector3 startPosition = transform.position;
        float time = 0;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, target, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
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
