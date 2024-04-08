using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private Image _gauge;
    [SerializeField] private Image _backGauge;

    [SerializeField] private float termToDecrease = 0.2f;
    
    public void DecreaseGauge(int beforeValue, int afterValue, int max)
    {
        float percent = beforeValue / (float)max;
        
        
        
    }

    public void DecreaseGauge(float beforePercent, float afterPercent)
    {
    }
    private IEnumerator DecreaseGaugeRoutine()
    { // _backGauge 채우고 _gauge 채우기

        yield return new WaitForSeconds(termToDecrease);
        
    }
    
    public void IncreaseGauge(int beforeValue, int afterValue, int max)
    {
        
    }

    private IEnumerator IncreaseGaugeRoutine()
    { // _gauge 채우고 _backGauge 채우기

        yield return new WaitForSeconds(termToDecrease);
        
    }

}
