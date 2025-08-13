using UnityEngine;

namespace Player.Controllers
{
    public class PlayerAnimationController : MonoBehaviour, ICharacterAnimationController
    {
        [SerializeField] private Animator m_Animator;
        
        private const string KEY_ANIMATION_IDLE = "idle";
        private const string KEY_ANIMATION_WALK = "walk";
        private const string KEY_ANIMATION_INTERACT = "interact";
        
        public void PlayIdleAnimation()
        {
            m_Animator.Play(KEY_ANIMATION_IDLE);
        }

        public void PlayWalkAnimation()
        {
            m_Animator.Play(KEY_ANIMATION_WALK);
        }

        public void PlayInteractAnimation()
        {
            m_Animator.Play(KEY_ANIMATION_INTERACT);
        }
    }
}