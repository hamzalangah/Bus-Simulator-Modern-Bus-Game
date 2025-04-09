using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;
    public float dampingSpeed = 0.25f;
    private Vector3 initialPosition;
    private bool isShaking = false;
    private void Start()
    {
        instance = this;
        initialPosition = transform.localPosition;
    }
    public void StartShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        if (!isShaking) 
        {
            StartCoroutine(Shake());
        }
    }
    private IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = initialPosition + new Vector3(randomOffset.x, randomOffset.y, 0);
            elapsed += Time.deltaTime;
            yield return null; 
        }
        transform.localPosition = initialPosition;
        isShaking = false;
    }
}