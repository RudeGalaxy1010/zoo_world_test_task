namespace App.Domains.Entity.Api
{
    public interface IMoveService
    {
        void Register(IMovable movable);
        void Unregister(IMovable movable);
    }
}