using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManagar : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip Kill, Die, CollectBook, CollectKey, UnlockedDoor, Victory;

    [SerializeField] private ParticleSystem DieParticle, CollectBookParticle, VictoryParticle;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void VictoryEffect()
    {
        audioSource.PlayOneShot(Victory);
        VictoryParticle.Play();
    }

    public void DieEffect()
    {
        audioSource.PlayOneShot(Die);
        DieParticle.Play();
    }

    public void KillEffect()
    {
        audioSource.PlayOneShot(Kill);
        DieParticle.Play();
    }

    public void CollectBookEffect()
    {
        audioSource.PlayOneShot(CollectBook);
        CollectBookParticle.Play();

    }

    public void CollectKeyEffect()
    {
        audioSource.PlayOneShot(CollectKey);
    }

    public void UnlockedDoorEffect()
    {
        audioSource.PlayOneShot(UnlockedDoor);
    }
}
