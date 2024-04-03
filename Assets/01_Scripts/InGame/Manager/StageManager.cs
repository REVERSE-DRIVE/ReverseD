using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    /**
    * <summary>
    * 디바이스의 감염정도
    * </summary>
    */
    private int infectedLevel = 0;
    public int InfectedLevel => infectedLevel;

    public void AddInfect(int amount)
    {
        infectedLevel += amount;
    }
    
    public void MoveToNextStage()
    {
        GameManager.Instance._PlayerTransform.position = Vector3.zero;
        GameManager.Instance._RoomGenerator.ResetMap();
        
    }
}
