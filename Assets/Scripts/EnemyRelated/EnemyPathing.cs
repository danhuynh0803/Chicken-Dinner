using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    public EnemyPathNode Path { get; set; }
    public float initialSpeed;
    public float initialAccelerationNorm;
    public float speedFactor;
    public int currentPathIndex;
    private Enemy enemy;
    [SerializeField]
    protected Vector3 targetPoint;
    [SerializeField]
    protected Vector3 velocity;
    [SerializeField]
    protected Vector3 targetDirection;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        targetPoint = Path.GetPoint(currentPathIndex+1);
        targetDirection = (targetPoint - transform.position).normalized;
        velocity = (initialSpeed + initialAccelerationNorm * Time.deltaTime) * targetDirection;
        //transform.rotation = Quaternion.LookRotation(velocity);
        if (enemy.isByAutarca)
        {
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.localEulerAngles = new Vector3(0f, -1 * angle + 90f, 0f);
        }
    }

    private void Update()
    {
        if (Path == null)
        {
            return;
        }
        if (TargetReached())
        {
            if (!SetNextTarget())
            {                
                enemy.DamagePlayer();
                Destroy(gameObject);
                return;
            }
        }
        transform.position += (velocity * Time.deltaTime);
        targetDirection = (targetPoint - transform.position).normalized;
        velocity += initialAccelerationNorm * targetDirection * Time.deltaTime;
    }

    private bool TargetReached()
    {
        return (Vector3.Distance(transform.position, targetPoint) < Path.radius);
    }

    private bool SetNextTarget()
    {
        bool success = false;
        if (currentPathIndex < Path.Length - 1)
        {
            currentPathIndex++;
            success = true;
        }
        else
        {
            success = false;
        }

        targetPoint = Path.GetPoint(currentPathIndex);
        targetDirection = (targetPoint - transform.position).normalized;
        velocity = velocity.magnitude * targetDirection;

        if (enemy.isByAutarca)
        {
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.localEulerAngles = new Vector3(0f, -1 * angle + 90f, 0f);
        }

        return success;
    }
}
