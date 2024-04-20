using UnityEngine;

namespace InGameSaveData
{
    public struct RoadData
    {
        /**
         * <summary>
         * true이면 가로, false이면 세로
         * </summary>
         */
        public bool isRoadHorizontal;
        public Vector2 roadPosition;
    }
}