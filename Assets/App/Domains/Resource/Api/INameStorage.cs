namespace App.Domains.Resource.Api
{
    public interface INameStorage
    {
        string[] PredatorNames { get; }
        string[] PreyNames { get; }
    }
}