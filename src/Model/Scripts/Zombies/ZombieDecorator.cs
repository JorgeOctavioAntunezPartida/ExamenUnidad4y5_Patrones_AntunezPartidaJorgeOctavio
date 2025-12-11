public abstract class ZombieDecorator : ZombieComponent
{
    protected ZombieComponent _innerZombie;

    public ZombieDecorator(ZombieComponent zombie)
    {
        _innerZombie = zombie;
    }
    public ZombieComponent Inner => _innerZombie;

}