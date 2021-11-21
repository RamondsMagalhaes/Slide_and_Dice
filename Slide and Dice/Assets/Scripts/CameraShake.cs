using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapse = 0;
        while (elapse < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3( originalPosition.x + x, originalPosition.y +y, originalPosition.z );
            elapse += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }


}
