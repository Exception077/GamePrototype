using UnityEngine;
using System.Collections;
using Pathfinding;

public class StateMachine : MonoBehaviour {
    public Transform Target;
    public float Speed;
    
    public float NextWaypointDistance = 3f;

    [SerializeField]
    Animator anim;
    [SerializeField]
    Rigidbody2D rbody2D;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    Seeker seeker;
    [SerializeField]
    Vector2 vector2;
    [SerializeField]
    Transform GFX;

    bool grounded;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    // Use this for initialization
    void Start() {
        InvokeRepeating("updatePath", 0f, 0.5f);
    }

    void updatePath() {
        if(seeker.IsDone())
            seeker.StartPath(rbody2D.position, Target.position, OnPathComplete);
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (path == null) {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            rbody2D.velocity = new Vector2(0, 0);
            rbody2D.gravityScale = 2.0f;
            return; 
        } else {
            reachedEndOfPath = false;
            rbody2D.gravityScale = 0;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rbody2D.position).normalized;
        Vector2 force = direction * Speed * Time.deltaTime;

        rbody2D.AddForce(force);
        //rbody2D.velocity = Speed * direction;
        //vector2 = rbody2D.velocity;
        animControl();

        if (rbody2D.velocity.x >= 0.01f) {
            GFX.localScale = new Vector3(1f, 1f, 1f);
        } else if(rbody2D.velocity.x <= -0.01f) {
            GFX.localScale = new Vector3(-1f, 1f, 1f);
        }

        float distance = Vector2.Distance(rbody2D.position, path.vectorPath[currentWaypoint]);
        if (distance < NextWaypointDistance) {
            currentWaypoint++;
        }

    }

    public void animControl() {
        anim.SetFloat("speed", Mathf.Abs(rbody2D.velocity.x));
        print("speed:" + rbody2D.velocity.x);
        anim.SetFloat("vspeed", rbody2D.velocity.y);
        grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        anim.SetBool("ground", grounded);
    }

}
