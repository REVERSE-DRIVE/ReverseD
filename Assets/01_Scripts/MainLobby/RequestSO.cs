using UnityEngine;

[CreateAssetMenu(menuName = "SO/RequestSO")]
public class RequestSO : ScriptableObject
{
    public int id;
    
    public string requestName;
    public RequestType requestType;
    public int progress;
    public int goalProgress;
    public string description;
}