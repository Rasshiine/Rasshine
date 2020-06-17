using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MovingCameraScript : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;
    public float smoothTime = 0.5f;

    public float minZoom = 200f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;
    private Camera cam;

    public PlayerScript playerScript1;
    public PlayerScript playerScript2;

    public bool isGameSet1;
    public bool isGameSet2;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
         isGameSet1 = playerScript1.isGameSet;
         isGameSet2 = playerScript2.isGameSet;
    }

    void LateUpdate()
    {
        if (targets.Count == 0)
            return;
        
        Move();
        Zoom();
    }

   void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }
   void Move()
    {

        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
            {
                bounds.Encapsulate(targets[i].position);

            }
        }
        return bounds.size.magnitude;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        if (isGameSet1 == true)
        {
            return targets[1].position;
        }
        else if (isGameSet2 == true)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
            {
                bounds.Encapsulate(targets[i].position);
            }
           
        }

        return bounds.center;
    }

}
