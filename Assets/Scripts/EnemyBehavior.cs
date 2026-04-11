using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;
using Unity.VisualScripting;
public class EnemyBehavior : MonoBehaviour


{
    private int _lives = 3;
    public Transform Player;
    public Transform PatrolRoute;
    public List<Transform> Locations;
    private int _locationIndex=0;
    private NavMeshAgent _agent;
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player dectected - attack!");
            _agent.destination = Player.position;
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("player out of range - resume patorl");
        }
    }
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

         Player = GameObject.Find("Player").transform;

        InitializePatrolRoute();
        MoveToNextPatrolLocation();
        

    }
    void InitializePatrolRoute()
    {
       foreach(Transform child in PatrolRoute)
        {
            Locations.Add(child);
        }
    }
   void MoveToNextPatrolLocation()
    {
     _agent.destination = Locations[_locationIndex].position;
      if (Locations.Count == 0)
        return;
        
        _agent.destination = Locations[_locationIndex].position;
        _locationIndex = (_locationIndex + 1) % Locations.Count;
    }
 
    // Update is called once per frame
    void Update()
    {
        if(_agent.remainingDistance < 0.2f && !_agent.pathPending)
        {
            
            MoveToNextPatrolLocation();
        }
    }
    public int EnemyLives
    {
        get { return _lives; }
        set
        {
            _lives = value;
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down!");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
           EnemyLives -= 1;
           Debug.Log("critical hit!");
        }
    }
}
