using App.Domains.Entity.Impl;

namespace App.Domains.Resource.Api
{
    public interface IPrefabStorage
    {
        Level Level { get; }
        Animal Frog { get; }
        Animal Snake { get; }
    }
}
