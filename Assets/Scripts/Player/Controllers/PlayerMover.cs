using InputSystem.CameraControllers.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Collections;

namespace Player.Controllers
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent m_Agent;

        private ICharacterAnimationController m_CharacterAnimationController;
        private IScreenTapActionAdd m_ScreenTapActionAdd;
        private Camera m_CameraComponent;
        private UnityAction m_CollbackMove;
        private Coroutine m_MoveCoroutine;
        
        public void Init(Camera camera, IScreenTapActionAdd screenTapActionAdd,
            ICharacterAnimationController animationController)
        {
            m_CharacterAnimationController = animationController;
            m_CameraComponent = camera;
            m_ScreenTapActionAdd = screenTapActionAdd;
            m_ScreenTapActionAdd.OnScreenTapSubscribe(Move);
        }
        
        private void Move(Vector3 position)
        {
            if (m_Agent != null)
            {
                m_CollbackMove = null;
                if (m_MoveCoroutine != null)
                {
                    StopCoroutine(m_MoveCoroutine);
                }
                
                Ray ray = m_CameraComponent.ScreenPointToRay(position);
                
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponent(out IPlayerInteract interact))
                    {
                        m_CollbackMove += interact.Action;
                    }
                    
                    m_Agent.SetDestination(hit.point);
                    m_CharacterAnimationController.PlayWalkAnimation();
                    m_MoveCoroutine = StartCoroutine(WaitForDestinationReached());
                }
            }
        }
        
        private IEnumerator WaitForDestinationReached()
        {
            yield return new WaitUntil(() => m_Agent.pathPending == false);
            yield return new WaitUntil(() => !m_Agent.pathPending && 
                                            m_Agent.remainingDistance < 0.5f && 
                                            !m_Agent.hasPath);
            
            CollBackMove();
            m_MoveCoroutine = null;
        }

        private void CollBackMove()
        {
            m_CharacterAnimationController.PlayIdleAnimation();
            if(m_CollbackMove != null)
            {
                m_CharacterAnimationController.PlayInteractAnimation();
                m_CollbackMove.Invoke();
                m_CollbackMove = null;
            }
        }
    }
}