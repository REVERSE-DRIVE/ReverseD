using UnityEngine;

namespace SaveDatas
{
    [System.Serializable]
    public class PlayData
    {
        public int playTime;
        public bool isComplete;

        public PlayData(int playTime, bool isComplete)
        {
            this.playTime = playTime;
            this.isComplete = isComplete;
        }
    }
}