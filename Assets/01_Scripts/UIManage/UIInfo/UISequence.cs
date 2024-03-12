using UnityEngine;
using UnityEngine.Events;

namespace UIManage
{
    [System.Serializable]
    public class UISequence
    {
        [Tooltip("실행전 시간차")]
        public float beforeDelay = 0;
        [Tooltip("해당 순서에 호출할 이벤트")]
        public UnityEvent indexedEvent;
        
    }
}