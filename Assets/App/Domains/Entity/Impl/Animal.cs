using System;
using System.Collections;
using App.Domains.Entity.Api;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace App.Domains.Entity.Impl
{
    public class Animal : Entity, IMovable
    {
        [SerializeField, Min(0)] private int _predationPriority;
        [SerializeField] private MovementStrategy _movementStrategy = null!;
        [SerializeField] private MeshRenderer _renderer = null!;
        [SerializeField] private TMP_Text _nameLabel = null!;
        [SerializeField] private ParticleSystem _deadEffect = null!;
        [SerializeField] private ParticleSystem _despawnEffect = null!;

        public event Action<Animal, Animal> Collided = delegate { };
        public event Action<Animal> Removed = delegate { };

        public int PredationPriority => _predationPriority;
        public bool IsDead { get; private set; }
        public bool DestinationReached => _movementStrategy.DestinationReached;

        private void Update()
        {
            if (!IsDead)
            {
                _movementStrategy.Tick(Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Animal animal))
            {
                Collided.Invoke(this, animal);
            }
        }

        private void OnDisable()
        {
            if (!IsDead)
            {
                Removed.Invoke(this);
            }
        }

        public void SetColor(Color color) =>
            _renderer.material.color = color;

        public void SetDestination(Vector3 destination) =>
            _movementStrategy.SetDestination(destination);

        public void Stop() =>
            _movementStrategy.Stop();

        public void SetName(string value) =>
            _nameLabel.text = value;

        public void Die() =>
            Remove(_deadEffect);

        public void Despawn() =>
            Remove(_despawnEffect);

        private void Remove(ParticleSystem effect)
        {
            if (IsDead)
            {
                return;
            }

            IsDead = true;
            Stop();
            GetComponent<Collider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            _renderer.enabled = false;
            _nameLabel.GetComponentInParent<Canvas>().gameObject.SetActive(false);
            Removed.Invoke(this);
            StartCoroutine(FinishDeath(effect));
        }

        private IEnumerator FinishDeath(ParticleSystem effect)
        {
            if (effect != null)
            {
                ParticleSystem.MainModule main = effect.main;
                main.loop = false;
                effect.Play();

                while (effect.IsAlive(true))
                {
                    yield return null;
                }
            }

            Destroy(gameObject);
        }
    }
}
