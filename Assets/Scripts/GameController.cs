using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField]
    GameObject lightBeam;
    [SerializeField]
    GameObject peekText;
    [SerializeField]
    bool isPriority;
    [SerializeField]
    Image nextCarSprite;
    [SerializeField]
    TextMeshProUGUI nextCarName;

    private GameObject newCarPrefab;
    private GameObject[] currentCars;
    private int currentIndex = 0;
    private bool isPeek = false;

    // Start is called before the first frame update
    void Start()
    {
        currentCars = new GameObject[points.Length+1];
        generateNextCar();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentIndex > 0){
            peekText.GetComponent<TextMeshProUGUI>().text = "Peek: " + currentCars[0].GetComponent<CarScript>().getCarName();
        }
        else{
            peekText.GetComponent<TextMeshProUGUI>().text = "Peek: Null";
        }
    }

    void generateNextCar(){
        newCarPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
        nextCarSprite.sprite = newCarPrefab.GetComponent<SpriteRenderer>().sprite;
        nextCarName.text = newCarPrefab.GetComponent<CarScript>().getCarName();
    }

    public void offer()
    {
        if(currentIndex >= points.Length) return;

        GameObject newCar = Instantiate(newCarPrefab, spawnPoint.position, Quaternion.identity);

        if(isPriority && currentIndex > 0){
            int tempIndex = currentIndex -1;
            while(tempIndex >= 0 && comparePriority(newCar, currentCars[tempIndex])){
                currentCars[tempIndex+1] = currentCars[tempIndex];
                moveCar(currentCars[tempIndex+1], points[tempIndex+1]);
                tempIndex--;
            }
            currentCars[tempIndex+1] = newCar;
            moveCar(newCar, points[tempIndex+1]);
        }
        else{
            currentCars[currentIndex] = newCar;
            moveCar(newCar, points[currentIndex]);
        }

        generateNextCar();
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

    public void peek(){
        isPeek = !isPeek;
        lightBeam.SetActive(isPeek);
        peekText.SetActive(isPeek);
    }

    bool comparePriority(GameObject car1, GameObject car2){
        //return true if car1 > car2
        return car1.GetComponent<CarScript>().getPriority() > car2.GetComponent<CarScript>().getPriority();
    }
}
