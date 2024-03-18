using UnityEngine;

public class Beam : MonoBehaviour
{
        
    [SerializeField] private int reflection = 0;
    [SerializeField] private LineRenderer _lineRenderer;

    private float time;

    [SerializeField]
    private Vector2 targetPos;
    
    public void Reflect()
    {
        Vector2 dir = targetPos - new Vector2(transform.position.x, transform.position.y);
        Physics2D.RaycastAll(transform.position, dir);

    } 
}