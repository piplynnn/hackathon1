using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Constants
    private float MOST_LEFT_LANE = -2;
     private float MOST_RIGHT_LANE = 2;
    [SerializeField] private Vector3 displacement = new Vector3(3, 0, 0);
    [SerializeField] private float speed = 10;
    public Rigidbody2D rb;
    public float rbVelocity = 2f;
    private Vector3 targetPosition;
    private Vector3 previousPosition;
    private float currentLane = 0;

    void Start() {
        previousPosition = rb.position;
    }
 
    void Update()
    {
        rb.velocity = Vector3.up * rbVelocity;

        if (Input.GetKeyDown(KeyCode.A) && currentLane > MOST_LEFT_LANE) {
            targetPosition = previousPosition - displacement;             
            previousPosition = targetPosition;
            currentLane--;
        } else if (Input.GetKeyDown(KeyCode.D) && currentLane < MOST_RIGHT_LANE) {
            targetPosition = previousPosition + displacement;
            previousPosition = targetPosition;
            currentLane++;
        }

        float rbSnapX = Mathf.Lerp(rb.position.x, targetPosition.x, speed * Time.deltaTime);
        rb.position = new Vector2(rbSnapX, rb.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Speed"))
        {
            rbVelocity += 2f;
            Debug.Log(rbVelocity);
        }

        if (other.CompareTag("Barrier")) {
            rbVelocity = 0f;
            Debug.Log(rbVelocity);
        }
    } 
}