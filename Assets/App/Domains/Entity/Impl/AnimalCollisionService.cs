using UnityEngine;

namespace App.Domains.Entity.Impl
{
    public class AnimalCollisionService
    {
        public void Resolve(Animal first, Animal second)
        {
            if (first.Id >= second.Id || first.IsDead || second.IsDead ||
                !first.isActiveAndEnabled || !second.isActiveAndEnabled)
            {
                return;
            }

            if (first.PredationPriority == second.PredationPriority)
            {
                Animal victim = Random.value < 0.5f ? first : second;
                victim.Die();
            }
            else
            {
                Animal victim = first.PredationPriority < second.PredationPriority ? first : second;
                victim.Despawn();
            }
        }
    }
}
