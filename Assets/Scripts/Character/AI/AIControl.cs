using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIControl : CharacterControl
{
    public float speed;
    public float nextPathPointDistance;
    public float pathUpdateRate = 0.5f;

    public Transform spawnPos;

    public GameObject rocket;

    public Transform player;

    private new Rigidbody2D rigidbody;
    private Seeker seeker;

    private bool isMoving = false;
    private Vector2 target;
    private Path path;
    private int currentPathPointIndex = 0;
    private bool reachedTarget = false;

    private float rocketCoolDownTime = 0.5f;
    private float rocketCoolDownTimer;

    /// <summary>
    /// Goes to the given position
    /// </summary>
    /// <returns>Reached target or not</returns>
    public bool GoTo(Vector2 target, float stopDistance)
    {
        if ((rigidbody.position - target).sqrMagnitude < stopDistance * stopDistance)
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

    public bool FireRocket(Vector3 angle)
    {
        if (rocketCoolDownTimer > 0) 
        { 
            return false; 
        }

        Instantiate(rocket, transform.position, Quaternion.Euler(angle));
        rocketCoolDownTimer = rocketCoolDownTime;
        return true;
    }

    public override void TakeDamage()
    {
        // todo
        transform.position = spawnPos.position;
    }

    public Vector2 GetTargetPos()
    {
        return player.position;
    }

    public Vector2 GetPosition()
    {
        return rigidbody.position;
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateRate);
    }

    private void Update()
    {
        if (rocketCoolDownTimer > 0)
        {
            rocketCoolDownTimer -= Time.deltaTime;
        }

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

        Vector2 direction = (Vector2)path.vectorPath[currentPathPointIndex] - rigidbody.position;
        rigidbody.AddForce(direction.normalized * speed * Time.deltaTime);
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

        seeker.StartPath(rigidbody.position, target, OnPathComplete);
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
        }
    }

    public override float TGetFireAngle()
    {
        return 0.0f;
    }
}
