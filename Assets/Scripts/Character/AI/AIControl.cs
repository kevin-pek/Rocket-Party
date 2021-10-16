using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIControl : CharacterControl
{
    [SerializeField] private Transform player;

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

    public bool FireWeaponAtTarget()
    {
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

    private void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateRate);
    }

    private void Update()
    {
        TickCooldownTimer();

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

        Vector2 direction = path.vectorPath[currentPathPointIndex] - transform.position;
        rigidBody.velocity = direction.normalized * characterSpeed;
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
        }
    }
}
