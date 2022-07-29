using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    [SerializeField]
    string carName;
    [SerializeField]
    float speed = 0.05f;
    int priority;
    Transform target;
    void Start()
    {
        switch (carName)
        {
            case "Motorcycle":
                priority = 4;
                break;
            case "Police Car":
                priority = 3;
                break;
            case "Ambulance":
                priority = 2;
                break;
            default:
                priority = 1;
                break;
        }
    }

    void Update()
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

    public void setTarget(Transform _target)
    {
        target = _target;
    }

    void move()
    {
        Vector3 direction = new Vector3(target.position.x - transform.position.x, 0, 0);
        float distance = direction.magnitude;
        float duration = 1.0f;
        transform.Translate(direction * (Time.deltaTime * (distance / duration)));
    }
}
