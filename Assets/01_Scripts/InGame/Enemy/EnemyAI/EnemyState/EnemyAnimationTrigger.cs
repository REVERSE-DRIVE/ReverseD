using UnityEngine;
using UnityEngine.Events;


namespace EnemyManage
{
    
    public class EnemyAnimationTrigger : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;

        public UnityEvent OnAnimationEvent;
    
        private void AnimationEnd()
        {
            _enemy.AnimationEndTrigger();
        }

        private void AnimationEvent()
        {
            OnAnimationEvent?.Invoke();
        }
    
    
    }

}