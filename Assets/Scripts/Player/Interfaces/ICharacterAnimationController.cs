namespace Player
{
    public interface ICharacterAnimationController
    {
        void PlayIdleAnimation();
        void PlayWalkAnimation();
        void PlayInteractAnimation();
    }
}