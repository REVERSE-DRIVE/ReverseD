using UnityEngine;

public class TimedBombObject : BombObject
{
    [Header("Timed Setting")] [SerializeField]
    private float _limitedTime = 5;
    public bool isBombTriggered;

    private float _currentTime = 0;

    protected override void Awake()
    {
    }

    private void OnEnable()
    {
        SetDefault();
        
        
    }
    
    

    private void Update()
    {
        if (TimeManager.TimeScale == 0) return;
        if (isBombTriggered)
        {
            _currentTime += Time.deltaTime * TimeManager.TimeScale;
            if (_currentTime >= _limitedTime)
            {
                Destroy();
            }
        }
    }

    public override void SetDefault()
    {
        _currentTime = 0;
        isBombTriggered = false;
    }

    public void OnTrigger()
    {
        isBombTriggered = true;
    }

}