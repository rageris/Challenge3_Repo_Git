﻿using System.Collections;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    public Boundary boundary;

    public float dodge;
    public float tilt;
    public float smoothing;

    private float currentSpeed;
    private float targetManeuver;

    private Rigidbody rb;
    

    void Start ()
    {
        rb = GetComponent<Rigidbody>();

        currentSpeed = rb.velocity.z;

        StartCoroutine(Evade());
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) *-Mathf.Sign (transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));

            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

	// Update is called once per frame
	void FixedUpdate ()
    {

        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);

        rb.velocity = new Vector3(newManeuver, 0f, currentSpeed);

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
        rb.rotation = Quaternion.Euler(0.0f, 180f, rb.velocity.x * -tilt);
    }
}
