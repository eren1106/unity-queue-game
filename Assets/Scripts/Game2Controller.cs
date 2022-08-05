using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Controller : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform[] _targets;
    [SerializeField] private GameObject[] _carPrefabs;
    [SerializeField] private RectTransform[] _spawnPoints;
    private static Canvas canvas;
    private static RectTransform[] targets;
    private static RectTransform[] spawnPoints;
    private static GameObject[] carPrefabs;
    private static GameObject[] currentCars;

    void Awake()
    {
        canvas = _canvas;
        targets = _targets;
        spawnPoints = _spawnPoints;
        carPrefabs = _carPrefabs;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            createNewCar(i);
        }

        currentCars = new GameObject[targets.Length];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Canvas getCanvas()
    {
        return canvas;
    }

    public static RectTransform[] getTargets()
    { //the four answer places
        return targets;
    }

    public static RectTransform[] getSpawnPoints()
    {
        return spawnPoints;
    }

    public static GameObject[] getCurrentCars()
    {
        return currentCars;
    }

    public static void setCurrentCars(int index, GameObject newCar)
    {
        currentCars[index] = newCar;
    }

    public static void createNewCar(int index)
    {
        Debug.Log(index);
        GameObject car = Instantiate(carPrefabs[index], spawnPoints[index].anchoredPosition, Quaternion.identity) as GameObject;
        car.transform.SetParent(canvas.transform, false);
    }
}
