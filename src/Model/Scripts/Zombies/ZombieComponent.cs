public abstract class ZombieComponent
{
    public abstract int Damage { get; }
    public abstract float Speed { get; }
    public abstract int Life { get; }
    public abstract int Shield { get; }
    public abstract float Size { get; }
    public abstract void AddFunction();
}