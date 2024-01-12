using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float FlySpeed;
    [SerializeField] private bool _invertX;
    protected float Direction;

    protected Rigidbody2D Rigidbody;

    protected virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        var mod = _invertX ? -1 : 1;
        Direction = transform.lossyScale.x > 0 ? -1 : 1;
        Direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
    }
}
