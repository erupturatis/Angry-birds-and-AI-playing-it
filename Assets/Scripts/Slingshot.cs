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

    Vector3 currentPostion;

    public float maxLength;

    public float BottomBoundary;

    public GameObject birdPrefab;

    public float birdPositionOffset;
    public float force;


    Collider2D birdCollider;
    Rigidbody2D bird;

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
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPostion = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPostion = center.position + Vector3.ClampMagnitude(currentPostion -center.position, maxLength);

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

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, BottomBoundary, 1000);
        return vector;
    }

    void CreateBird() {
        bird = Instantiate(birdPrefab).GetComponent<Rigidbody2D>();
        birdCollider = bird.GetComponent<Collider2D>();
        birdCollider.enabled = false;

        bird.isKinematic = true;

        ResetStrips();
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
        Debug.Log("shoot");
        bird.isKinematic = false;
        Vector3 birdForce = (currentPostion - center.position) * force * -1;
        bird.velocity = birdForce;

        bird = null;
        birdCollider = null;
        Invoke("CreateBird", 2);

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
        if (bird)
        {
            Vector3 dir = position - center.position;
            bird.transform.position = position + dir.normalized * birdPositionOffset;
            bird.transform.right = -dir.normalized;
        }
    }
}
