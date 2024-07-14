using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(HealthHolder))]
public class DestroySoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private HealthHolder healthHolder;

    void Start()
    {
        healthHolder.DestroyedAction += OnDestroyAction;
    }

    private void OnDestroyAction(HealthHolder healthHolder)
    {
        audioSource.pitch = UnityEngine.Random.Range(-1f, 3f);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
