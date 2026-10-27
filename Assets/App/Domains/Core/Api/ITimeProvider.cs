namespace App.Domains.Core.Api
{
    public interface ITimeProvider
    {
        float DeltaTime { get; }
        float Now { get; }
    }
}