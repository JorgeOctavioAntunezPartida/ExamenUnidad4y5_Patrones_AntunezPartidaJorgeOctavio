public class GiantPotion : ZombieDecorator
{
    public GiantPotion(ZombieComponent zombie) : base(zombie) { }
    public override int Damage => _innerZombie.Damage;
    public override float Speed => _innerZombie.Speed - 0.75f;
    public override int Life => _innerZombie.Life + 20;
    public override int Shield => _innerZombie.Shield;
    public override float Size => _innerZombie.Size + 0.4f;
    public override void AddFunction() { }
}
