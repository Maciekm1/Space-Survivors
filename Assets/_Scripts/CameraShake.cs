using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private void Start()
    {
        // Get the Cinemachine Virtual Camera component
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            // Get the CinemachineBasicMultiChannelPerlin component
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    public void ShakeCamera(float amp, float freq, float duration)
    {
        if (noise != null)
        {
            // Set the noise properties for camera shake
            noise.m_AmplitudeGain = amp;
            noise.m_FrequencyGain = freq;

            // Start the camera shake coroutine
            StartCoroutine(StopShaking(duration));
        }
    }

    private IEnumerator StopShaking(float duration)
    {
        // Wait for the specified shake duration
        yield return new WaitForSeconds(duration);

        // Reset the noise properties to stop the shake
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
    }
}




