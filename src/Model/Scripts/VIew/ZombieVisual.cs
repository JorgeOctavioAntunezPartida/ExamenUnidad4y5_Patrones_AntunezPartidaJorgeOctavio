using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVisual : MonoBehaviour
{
    [SerializeField] private Zombie zombie;

    [SerializeField] private SpriteRenderer helmet;
    [SerializeField] private SpriteRenderer boot1;
    [SerializeField] private SpriteRenderer boot2;
    [SerializeField] private SpriteRenderer glove1;
    [SerializeField] private SpriteRenderer glove2;

    [SerializeField] private AudioSource audioImpact;

    private void Awake()
    {
        audioImpact = GetComponent<AudioSource>();
    }

    List<Type> DecoratorsActives(ZombieComponent zombie)
    {
        List<Type> decoradores = new List<Type>();
        ZombieComponent current = zombie;

        while (current is ZombieDecorator decorator)
        {
            decoradores.Add(current.GetType());
            current = decorator.Inner;
        }

        return decoradores;
    }

    private void Update()
    {
        if (helmet.enabled == true && zombie.Shield <= 0)
        {
            helmet.enabled = false;
        }
    }

    private void Equip()
    {
        var lista = DecoratorsActives(zombie.ZombieComponent);
        foreach (var t in lista)
        {
            if (t == typeof(Helmet) && zombie.Shield > 0)
            {
                helmet.enabled = true;
            }

            if (t == typeof(Boots))
            {
                boot1.enabled = true;
                boot2.enabled = true;
            }

            if (t == typeof(Gloves))
            {
                glove1.enabled = true;
                glove2.enabled = true;
            }
        }
    }

    private void OnEnable()
    {
        Zombie.OnZombieSummon += Equip;
        Zombie.OnZombieIsDamage += AplyDamageEffect;
    }

    private void OnDisable()
    {
        Zombie.OnZombieSummon -= Equip;
        Zombie.OnZombieIsDamage -= AplyDamageEffect;

        ResetZombie();
    }

    private void ResetZombie()
    {
        helmet.enabled = false;
        boot1.enabled = false;
        boot2.enabled = false;
        glove1.enabled = false;
        glove2.enabled = false;

        var sprites = GetComponentsInChildren<SpriteRenderer>(includeInactive: true);
        foreach (var s in sprites)
            s.color = Color.white;
    }

    void AplyDamageEffect(GameObject target)
    {
        if (target != gameObject) return;
        audioImpact.Play();
        StartCoroutine(FlashDamage(target, Color.red, 0.1f));
    }

    public IEnumerator FlashDamage(GameObject target, Color flashColor, float duration)
    {
        SpriteRenderer[] sprites = target.GetComponentsInChildren<SpriteRenderer>(includeInactive: true);

        Color[] original = new Color[sprites.Length];

        for (int i = 0; i < sprites.Length; i++)
            original[i] = sprites[i].color;

        for (int i = 0; i < sprites.Length; i++)
            sprites[i].color = flashColor;

        yield return new WaitForSeconds(duration);

        for (int i = 0; i < sprites.Length; i++)
            sprites[i].color = original[i];
    }
}