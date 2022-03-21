using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaPlant : MonoBehaviour
{
    public float speed;
    float _timeSinceReach = 0.0f;
    public float _timeToMove = 5f;

    public int startingPoint;

    private int i;

    public Transform[] points;



    void Start()
    {
        transform.position = points[startingPoint].position;
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            _timeSinceReach += Time.deltaTime;
            if (_timeSinceReach >= _timeToMove)
            {
                _timeSinceReach = 0.0f;
                i++;
            }
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }



}
