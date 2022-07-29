using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Transform[] points;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    Transform endPoint;
    [SerializeField]
    GameObject[] carPrefabs;
    private GameObject[] currentCars;
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentCars = new GameObject[points.Length+1];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void offer()
    {
        if(currentIndex >= points.Length) return;
        GameObject newCarPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
        GameObject newCar = Instantiate(newCarPrefab, spawnPoint.position, Quaternion.identity);
        currentCars[currentIndex] = newCar;
        newCar.GetComponent<CarScript>().setTarget(points[currentIndex]);
        currentIndex++;
    }

    public void poll()
    {
        if(currentIndex <= 0) return;
        moveCar(currentCars[0], endPoint);
        for(int i = 1; i<points.Length; i++){
            if(currentCars[i] != null){
                moveCar(currentCars[i], points[i-1]);
                currentCars[i-1] = currentCars[i];
                currentCars[i] = currentCars[i+1];
            }
        }
        currentIndex--;
    }

    void moveCar(GameObject car, Transform target){
        car.GetComponent<CarScript>().setTarget(target);
    }
}
