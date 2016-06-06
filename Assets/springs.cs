using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class springs : MonoBehaviour {
    public List<GameObject> balls;
    public List<float> natural_length;
    public float spring_constant;
    public float damping_constant;

    void Start() {
        for (int i = 0; i < balls.Count; ++i) {
            for (int j = i+1; j < balls.Count; ++j) {
                Rigidbody2D rb1 = balls[i].GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = balls[j].GetComponent<Rigidbody2D>();
                float d = (rb1.transform.position - rb2.transform.position).magnitude;
                natural_length.Add(d);
            }
        }
    }

    void FixedUpdate() {
        int nli = 0;
        for (int i = 0; i < balls.Count; ++i) {
            for (int j = i+1; j < balls.Count; ++j) {
                Rigidbody2D rb1 = balls[i].GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = balls[j].GetComponent<Rigidbody2D>();
                //float deltav = (rb1.velocity - rb2.velocity).magnitude;
                Vector3 delta = rb1.transform.position - rb2.transform.position;
                Vector3 dir = delta.normalized;
                float d = delta.magnitude;
                float f = (d - natural_length[nli++]) * spring_constant;
                rb1.AddForce(new Vector2(-f * dir.x, -f * dir.y));
                rb2.AddForce(new Vector2(f * dir.x, f * dir.y));
            }
        }
    }
}
