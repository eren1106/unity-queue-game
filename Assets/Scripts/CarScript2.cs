using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarScript2 : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private int carId;
    [SerializeField] private string carName;
    private RectTransform rectTransform;
    private Vector2 prevTransform;
    private Vector3 oriLocalScale;
    private Vector3 newLocalScale;
    private int currentIndex;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        prevTransform = rectTransform.anchoredPosition;
        oriLocalScale = rectTransform.localScale;
        newLocalScale = new Vector3(0.7f, 0.7f, 1);
        currentIndex = -1; //initial index is -1
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform.localScale = newLocalScale;
        FindObjectOfType<AudioManager>().Play("Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / Game2Controller.getCanvas().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int nearestIndex = findNearest(rectTransform, Game2Controller.getTargets());

        if(nearestIndex == -1){ //back to previous position if the distance is too far from the target
            backToPrev();
            FindObjectOfType<AudioManager>().Play("Drag");
            return;
        }
        if(currentIndex != -1 && nearestIndex == Game2Controller.getTargets().Length-1){ //destroy this gameobject when it move to delete icon
            Destroy(this.gameObject);
            FindObjectOfType<AudioManager>().Play("Drop");
            return;
        }
        if(nearestIndex == currentIndex || Game2Controller.getCurrentCars()[nearestIndex] != null || nearestIndex == Game2Controller.getTargets().Length-1){ //back to prev if the index is same or already got car at thr
            backToPrev();
            FindObjectOfType<AudioManager>().Play("Drag");
            return;
        }
        
        moveToTargets(nearestIndex);
        FindObjectOfType<AudioManager>().Play("Drop");
    }

    int findNearest(RectTransform source, RectTransform[] targets){
        float nearestDistance = float.MaxValue;
        int index = 0;
        for(int i = 0; i<targets.Length; i++){
            float distance = Vector3.Distance(source.anchoredPosition, targets[i].anchoredPosition);
            if(distance < nearestDistance){
                nearestDistance = distance;
                index = i;
            } 
        }
        if(nearestDistance > 200) return -1; //return -1 if the distance is too far
        return index;
    }

    void backToPrev(){
        rectTransform.anchoredPosition = prevTransform;
        if(prevTransform == Game2Controller.getSpawnPoints()[carId].anchoredPosition){
            rectTransform.localScale = oriLocalScale;
        }
    }

    void moveToTargets(int targetIndex){
        prevTransform = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(Game2Controller.getTargets()[targetIndex].anchoredPosition.x, Game2Controller.getTargets()[targetIndex].anchoredPosition.y + 50f);
        Game2Controller.setCurrentCars(targetIndex, this.gameObject);
        if(currentIndex != -1){ //currentIndex == -1 means that the car is drag from spawner
            Game2Controller.setCurrentCars(currentIndex, null);
        }
        currentIndex = targetIndex;
        Game2Controller.createNewCar(carId);
    }

    public string getCarName(){
        return carName;
    }
}
