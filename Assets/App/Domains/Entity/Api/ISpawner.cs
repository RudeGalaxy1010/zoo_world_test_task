namespace App.Domains.Entity.Api
{
    public interface ISpawner
    {
        void Start(float period);
        void Stop();
    }
}