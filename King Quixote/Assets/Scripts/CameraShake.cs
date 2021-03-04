using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerable Shake (float duration, float magnitude)
    {
        Vector2 ogPosition = transform.localPosition;

        float timeElapsed = 0.0f;

        while(timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = ogPosition;
    }
}
