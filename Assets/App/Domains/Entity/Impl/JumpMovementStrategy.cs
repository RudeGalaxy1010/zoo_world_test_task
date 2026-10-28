using UnityEngine;

namespace App.Domains.Entity.Impl
{
    public class JumpMovementStrategy : NavMeshMovementStrategy
    {
        [SerializeField] private Transform _visual = null!;
        [SerializeField, Min(0f)] private float _jumpHeight = 0.6f;
        [SerializeField, Min(0.01f)] private float _jumpDuration = 0.35f;
        [SerializeField, Min(0f)] private float _pauseDuration = 0.2f;

        private float _elapsed;
        private bool _isJumping;

        public override bool DestinationReached => !_isJumping && base.DestinationReached;

        public override void Tick(float deltaTime)
        {
            if (!IsAgentReady)
            {
                Stop();
                return;
            }

            if (!_isJumping)
            {
                base.Tick(deltaTime);

                if (DestinationReached || Agent.pathPending)
                {
                    return;
                }

                _elapsed += deltaTime;

                if (_elapsed >= _pauseDuration)
                {
                    StartJump();
                }

                return;
            }

            _elapsed += deltaTime;
            float progress = Mathf.Clamp01(_elapsed / Mathf.Max(0.01f, _jumpDuration));
            _visual.localPosition = Vector3.up * CalculateJumpHeight(progress);

            if (progress >= 1f)
            {
                Land();
            }
        }

        public override void SetDestination(Vector3 destination)
        {
            base.SetDestination(destination);

            if (IsAgentReady)
            {
                Agent.isStopped = true;
            }
        }

        public override void Stop()
        {
            base.Stop();
            Land();
        }

        private void StartJump()
        {
            _elapsed = 0f;
            _isJumping = true;
            Agent.isStopped = false;
        }

        private float CalculateJumpHeight(float progress) =>
            4f * _jumpHeight * progress * (1f - progress);

        private void Land()
        {
            _isJumping = false;
            _elapsed = 0f;

            if (_visual != null)
            {
                _visual.localPosition = Vector3.zero;
            }

            if (IsAgentReady)
            {
                Agent.isStopped = true;
            }
        }
    }
}
