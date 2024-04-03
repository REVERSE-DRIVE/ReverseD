using System;
using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public event Action StageStartEvent; 
    [SerializeField] private Portal _portalPrefab;
    
    [SerializeField] private int currentChapter = 1;
    [SerializeField] private int currentStageNum = 0;
    [SerializeField] private int maxStageIndexInChaapter = 5;

    private void Start()
    {
        NextStage();
    }


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
    
    /**
     * <summary>
     * 다음 스테이지로 이동하는 함수
     * </summary>
     */
    public void NextStage()
    {
        currentStageNum++;
        StartCoroutine(MoveToNextStageCoroutine());
        
        GameManager.Instance._UIManager.ShowStageChangeEvent();
        GameManager.Instance._UIManager.ShowNewStageUI(currentChapter, currentStageNum);

        //StartCoroutine(ShowNewStageCoroutine());
    }
    
    //
    // private IEnumerator ShowNewStageCoroutine()
    // {}
    //
    
    private IEnumerator MoveToNextStageCoroutine()
    {
        
        GameManager.Instance._PlayerTransform.position = Vector3.zero;
        float term = GameManager.Instance._UIManager.LoadingDuration * 0.5f;
        yield return new WaitForSeconds(term);
        
        GameManager.Instance._RoomGenerator.ResetMap();
        if (currentStageNum < maxStageIndexInChaapter)
        {
            GeneratePortal();
            
        }
        else
        {
            // 챕터의 끝에 도달했으면 보스방을 생성
        }

    }

    private void GeneratePortal()
    {
        Vector2 generatePos = GameManager.Instance._RoomGenerator.LastRoom.transform.position;
        PoolManager.Get(_portalPrefab, generatePos, Quaternion.identity);
    }

    private void GenerateBossRoom()
    {
        Vector2 generatePos = GameManager.Instance._RoomGenerator.LastRoom.transform.position;
        
    }
}
