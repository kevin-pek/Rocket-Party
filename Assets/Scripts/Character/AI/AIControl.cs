using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIControl : CharacterControl
{
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private Transform player;
    [SerializeField] private RoutePoint routePoint;

    public float nextPathPointDistance;
    public float pathUpdateRate = 0.5f;

    private Seeker seeker;
    private int currentPathPointIndex = 0;
    private bool isMoving = false;
    private bool reachedTarget = false;
    private Vector2 target;
    private Path path;

    /// <summary>
    /// Goes to the given position
    /// </summary>
    /// <returns>Reached target or not</returns>
    public bool GoTo(Vector2 target, float stopDistance)
    {
        if ((rigidBody.position - target).sqrMagnitude < stopDistance * stopDistance)
        {
            return true;
        }

        isMoving = true;
        this.target = target;
        return false;
    }

    public void Idle()
    {
        isMoving = false;
    }

    public void Patrol()
    {
        if (GoTo(routePoint.transform.position, 0.3f))
        {
            routePoint = routePoint.GetNextPoint();
            print("go next " + routePoint.gameObject.name);
        }
    }

    public bool CanShootRocket()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            player.position - transform.position,
            Vector2.Distance(transform.position, player.position),
            obstacleLayerMask);
        return hit.collider == null;
    }

    public bool FireWeaponAtTarget()
    {
        // do a raycast to player
        if (!CanShootRocket())
        {
            return false;
        }
        return FireWeapon(player.position);
    }

    public Vector2 GetTargetPos()
    {
        return player.position;
    } 

    public float GetTargetDistance()
    {
        return Vector2.Distance(player.position, transform.position);
    }

    private void LookAtPlayer()
    {
        Vector3 diff = player.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    protected override void Start()
    {
        base.Start(); 
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateRate);
    }

    private void Update()
    {
        TickCooldownTimer();
        LookAtPlayer();

        if (!isMoving)
        {
            return;
        }

        if (path == null)
        {
            return;
        }

        reachedTarget = currentPathPointIndex >= path.vectorPath.Count;

        if (reachedTarget)
        {
            return;
        }

        Vector2 direction = (Vector2)path.vectorPath[currentPathPointIndex] - rigidBody.position;
        rigidBody.position += direction.normalized * characterSpeed * Time.deltaTime;

        if (direction.sqrMagnitude < nextPathPointDistance * nextPathPointDistance)
        {
            currentPathPointIndex++;
        }
    }

    private void UpdatePath()
    {
        if (!isMoving)
        {
            return;
        }
        
        seeker.StartPath(rigidBody.position, target, OnPathComplete);
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentPathPointIndex = 0;
        }
    }
}
