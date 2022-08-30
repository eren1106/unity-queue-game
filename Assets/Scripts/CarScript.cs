using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    [SerializeField]
    string carName;
    [SerializeField]
    float speed;
    [SerializeField]
    int priority;
    Transform target;

    void FixedUpdate()
    {
        move();
        checkDestroy();
    }

    public void setTarget(Transform _target)
    {
        target = _target;
    }

    void move()
    {
        if (transform.position.x > target.position.x + speed)
        {
            Vector3 temp = transform.position;
            temp.x -= speed;
            transform.position = temp;
        }
        else if (transform.position.x < target.position.x - speed)
        {
            Vector3 temp = transform.position;
            temp.x += speed;
            transform.position = temp;
        }
    }

    public string getCarName()
    {
        return carName;
    }

    void checkDestroy()
    { //destroy this object when its position exceed certain boundaries
        if (transform.position.x < -12f || transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    public int getPriority()
    {
        return priority;
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.collider.tag == "Car")
    //     {
    //         Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
    //     }
    // }
}
