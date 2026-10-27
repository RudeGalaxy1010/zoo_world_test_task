namespace App.Domains.Resource.Api
{
    public interface IPrefabStorage
    {
        bool TryGet<T>(out T? prefab);
    }
}