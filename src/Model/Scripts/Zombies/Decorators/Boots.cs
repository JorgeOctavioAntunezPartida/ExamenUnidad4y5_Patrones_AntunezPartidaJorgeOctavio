public class Boots : ZombieDecorator
{
    public Boots(ZombieComponent zombie) : base(zombie) { }
    public override int Damage => _innerZombie.Damage;
    public override float Speed => _innerZombie.Speed + 1;
    public override int Life => _innerZombie.Life;
    public override int Shield => _innerZombie.Shield;
    public override float Size => _innerZombie.Size;
    public override void AddFunction() { }
}
