using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    bool isMouseDown;
    bool canLaunch;

    Vector3 currentPostion;

    public float maxLength;

    public float BottomBoundary;

    public GameObject birdPrefab;

    public float birdPositionOffset;
    public float force;


    Collider2D birdCollider;
    Rigidbody2D bird;
    Rigidbody2D LaunchedBird;


    public Level L;

    public int launched = 0;
    public int launched2 = 0;

    public float x1 = 0;
    public float y1 = 0;

    public float worstcasescen=120f;

    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        CreateBird();
    }


    void Update()
    {
        worstcasescen -= Time.deltaTime;
        if(L.isMoving == false && launched == launched2 && L.pigNumber>0)
        {
            canLaunch = true;
        }
        if (canLaunch == true || worstcasescen<0f) {
            L.S.RequestDecision();
            canLaunch = false;
            launched2++;
            worstcasescen = 120f;
           // print(L.pigNumber);
        }

        if (0 == 1)
        {
            if (isMouseDown)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10;

                currentPostion = Camera.main.ScreenToWorldPoint(mousePosition);
                currentPostion = center.position + Vector3.ClampMagnitude(currentPostion - center.position, maxLength);

                currentPostion = ClampBoundary(currentPostion);

                SetStrips(currentPostion);

                if (birdCollider)
                {
                    birdCollider.enabled = true;
                }

            }
            else
            {
                ResetStrips();
            }
        }

        if(LaunchedBird)
        {
            L.resetTimer();
        }
    }

    

    public void AIShootsBird(float x,float y)
    {
        currentPostion = center.position + new Vector3(-x,y,0);
        currentPostion = center.position + Vector3.ClampMagnitude(currentPostion - center.position, maxLength);

        currentPostion = ClampBoundary(currentPostion);

        SetStrips(currentPostion);

        if (birdCollider)
        {
            birdCollider.enabled = true;
        }
        //print("ajung ai shoot bird");
        Invoke("Shoot", 1);
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, BottomBoundary, 1000);
        return vector;
    }

    void CreateBird() {
        if (launched < 3)
        {
            bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
            birdCollider = bird.GetComponent<Collider2D>();
            birdCollider.enabled = false;

            bird.isKinematic = true;

            ResetStrips();
        }
        else
        {
            Invoke("CreateBird", 1);
        }
    }
 
    private void OnMouseUp()
    {
        isMouseDown = false;
        if (Vector3.Distance(currentPostion, center.position) < 1)
        {
            Debug.Log("cancelled");
        }
        else
        {
            Shoot();
        }
    }
    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    void Shoot()
    {
        if (L.isMoving == false)
        {
            //Debug.Log("shoot");
            bird.isKinematic = false;
            Vector3 birdForce = (currentPostion - center.position) * force * -1;
            bird.velocity = birdForce;
            Bird b = bird.GetComponent<Bird>();
            b.launched = true;

            launched += 1;
            LaunchedBird = bird;

            bird = null;
            birdCollider = null;
            Invoke("CreateBird", 2);

            L.resetTimer();
        }

    }

    void ResetStrips()
    {
        currentPostion = idlePosition.position;
        SetStrips(currentPostion);
    }
    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);
        //print(position);
        if (bird)
        {
            //print("bird exists");
            Vector3 dir = position - center.position;
            bird.transform.position = position + dir.normalized * birdPositionOffset;
            bird.transform.right = -dir.normalized;
        }
    }
}
