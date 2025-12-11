using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using System.Collections;
using URandom = UnityEngine.Random;

public class Zombie : MonoBehaviour
{
    #region Attributes
    [SerializeField] public int _id;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private int _life;
    [SerializeField] private int _shield;
    [SerializeField] private float _size;

    private ZombieComponent zombieComponent;
    public ZombieComponent ZombieComponent => zombieComponent;

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public int Life
    {
        get => _life;
        set => _life = value;
    }
    public int Shield
    {
        get => _shield;
        set => _shield = value;
    }
    public int Damage => _damage;
    public float Speed => _speed;
    public float Size => _size;
    #endregion

    #region Variables
    public Vector2 direction = Vector2.right;

    [SerializeField] private ZombieSpawner probabilityDecorator;
    #endregion

    #region Eventos Propios
    public static event Action<GameObject> OnZombieDead;
    public static event Action<GameObject> OnZombieIsDamage;
    public static event Action OnZombieSummon;
    #endregion

    void ApplyStatsFromComponent()
    {
        _damage = zombieComponent.Damage;
        _speed = zombieComponent.Speed;
        _life = zombieComponent.Life;
        _shield = zombieComponent.Shield;
        _size = zombieComponent.Size;
        transform.localScale = Vector3.one * _size;
    }

    void Awake()
    {
        zombieComponent = new ZombieConcrete();
        probabilityDecorator = FindFirstObjectByType<ZombieSpawner>();
    }

    private void OnEnable()
    {
        zombieComponent = StripAllDecorators(zombieComponent);
        ApplyStatsFromComponent();
    }

    public void DecorateZombie()
    {
        float addHelmetProbability = URandom.value;
        if (addHelmetProbability <= probabilityDecorator.helmet) AddHelmet();

        float addBootsProbability = URandom.value;
        if (addBootsProbability <= probabilityDecorator.boots) AddBoots();

        float addGiantPotionProbability = URandom.value;
        if (addGiantPotionProbability <= probabilityDecorator.giantPotion) AddGiantPotion();

        float addGlovesProbability = URandom.value;
        if (addGlovesProbability <= probabilityDecorator.gloves) AddGloves();
    }

    void AddHelmet()
    {
        zombieComponent = new Helmet(zombieComponent);
        ApplyStatsFromComponent();
        OnZombieSummon?.Invoke();
    }

    void AddBoots()
    {
        zombieComponent = new Boots(zombieComponent);
        ApplyStatsFromComponent();
        OnZombieSummon?.Invoke();
    }

    void AddGiantPotion()
    {
        zombieComponent = new GiantPotion(zombieComponent);
        ApplyStatsFromComponent();
        OnZombieSummon?.Invoke();
    }

    void AddGloves()
    {
        zombieComponent = new Gloves(zombieComponent);
        ApplyStatsFromComponent();
        OnZombieSummon?.Invoke();
    }

    ZombieComponent StripAllDecorators(ZombieComponent current)
    {
        while (current is ZombieDecorator decorator)
        {
            current = decorator.Inner;
        }

        return current;
    }

    private void Update()
    {
        transform.Translate(-direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie"))
            return;

        if (other.CompareTag("Bullet"))
        {
            Bullet bulletReceived = other.GetComponent<Bullet>();

            if (_shield > 0)
            {
                _shield -= bulletReceived.Damage;
                OnZombieIsDamage?.Invoke(this.gameObject);
                if (_shield < 0)
                {
                    _life += _shield;
                    _shield = 0;
                }
                return;
            }
            _life -= bulletReceived.Damage;
            OnZombieIsDamage?.Invoke(this.gameObject);
        }

        if (_life <= 0)
        {
            OnZombieDead?.Invoke(this.gameObject);
            return;
        }
    }
}