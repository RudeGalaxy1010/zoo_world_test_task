using UnityEngine;

namespace App.Domains.Entity.Impl
{
    public class Entity : MonoBehaviour
    {
        public int Id { get; } = ++_id;

        private static int _id = -1;
    }
}