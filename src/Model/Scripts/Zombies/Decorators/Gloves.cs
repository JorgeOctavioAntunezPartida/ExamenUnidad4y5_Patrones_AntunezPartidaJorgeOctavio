public class Gloves : ZombieDecorator
{
    public Gloves(ZombieComponent zombie) : base(zombie) { }
    public override int Damage => _innerZombie.Damage + 4;
    public override float Speed => _innerZombie.Speed;
    public override int Life => _innerZombie.Life;
    public override int Shield => _innerZombie.Shield;
    public override float Size => _innerZombie.Size;
    public override void AddFunction() { }
}
