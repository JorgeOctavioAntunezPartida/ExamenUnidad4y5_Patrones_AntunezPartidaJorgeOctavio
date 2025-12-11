using UnityEngine;
using UnityEngine.Rendering;

public class ZombieManagerSounds : MonoBehaviour
{
    private AudioSource audioImpact;

    private void Awake()
    {
        audioImpact = GetComponent<AudioSource>();
    }

    void PlaySoundEffect(GameObject zombie)
    {
        Debug.Log("Playing zombie death sound effect.");
        audioImpact.Play();
    }

    private void OnEnable()
    {
        Zombie.OnZombieDead += PlaySoundEffect;
    }

    private void OnDisable()
    {
        Zombie.OnZombieDead -= PlaySoundEffect;
    }
}