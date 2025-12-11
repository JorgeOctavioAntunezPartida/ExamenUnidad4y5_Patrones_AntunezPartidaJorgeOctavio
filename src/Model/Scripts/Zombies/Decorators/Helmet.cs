public class Helmet : ZombieDecorator
{
    public Helmet(ZombieComponent zombie) : base(zombie) { }
    public override int Damage => _innerZombie.Damage;
    public override float Speed => _innerZombie.Speed;
    public override int Life => _innerZombie.Life;
    public override int Shield => _innerZombie.Shield + 20;
    public override float Size => _innerZombie.Size;
    public override void AddFunction() { }
}