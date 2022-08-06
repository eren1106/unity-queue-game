using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Game2Controller : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform[] _targets;
    [SerializeField] private GameObject[] _carPrefabs;
    [SerializeField] private RectTransform[] _spawnPoints;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private bool isPriority;
    private static Canvas canvas;
    private static RectTransform[] targets;
    private static RectTransform[] spawnPoints;
    private static GameObject[] carPrefabs;
    private static GameObject[] currentCars;
    private Game2Questions questionMaster;

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

        //generate question
        questionMaster = new Game2Questions(isPriority);
        questionMaster.generateQuestion();
        questionText.text = questionMaster.getQuestionText();
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
        GameObject car = Instantiate(carPrefabs[index], spawnPoints[index].anchoredPosition, Quaternion.identity) as GameObject;
        car.transform.SetParent(canvas.transform, false);
    }

    public void submitAnswer(){
        string[] ans = new string[4];
        Array.Copy(questionMaster.getAnswers(), 0, ans , 0, questionMaster.getAnswers().Length);
        for(int i = 0; i<ans.Length; i++){
            if((currentCars[i] == null && ans[i] != null) || (currentCars[i] != null && currentCars[i].GetComponent<CarScript2>().getCarName() != ans[i])){
                Debug.Log("Wrong!");
                return;
            }
        }
        Debug.Log("Correct!");
    }
}
