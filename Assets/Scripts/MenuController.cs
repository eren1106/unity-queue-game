using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject queueButton;
    [SerializeField] private GameObject priorityQueueButton;
    [SerializeField] private GameObject q1Button;
    [SerializeField] private GameObject q2Button;
    [SerializeField] private GameObject p1Button;
    [SerializeField] private GameObject p2Button;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject muteButton;
    [SerializeField] private GameObject unmuteButton;

    void Start(){
        if(FindObjectOfType<AudioManager>().getMute()){
            muteButton.SetActive(false);
            unmuteButton.SetActive(true);
        }
        else{
            muteButton.SetActive(true);
            unmuteButton.SetActive(false);
        }
    }

    public void toQueue(){
        FindObjectOfType<AudioManager>().Play("Peek");
        backButton.SetActive(true);
        queueButton.SetActive(false);
        priorityQueueButton.SetActive(false);
        q1Button.SetActive(true);
        q2Button.SetActive(true);
    }

    public void toPriorityQueue(){
        FindObjectOfType<AudioManager>().Play("Peek");
        backButton.SetActive(true);
        queueButton.SetActive(false);
        priorityQueueButton.SetActive(false);
        p1Button.SetActive(true);
        p2Button.SetActive(true);
    }

    public void toQueueGame1(){
        FindObjectOfType<AudioManager>().Play("Peek");
        SceneManager.LoadScene("Game1");
    }

    public void toQueueGame2(){
        FindObjectOfType<AudioManager>().Play("Peek");
        SceneManager.LoadScene("Game2");
    }

    public void toPriorityQueueGame1(){
        FindObjectOfType<AudioManager>().Play("Peek");
        SceneManager.LoadScene("PGame1");
    }

    public void toPriorityQueueGame2(){
        FindObjectOfType<AudioManager>().Play("Peek");
        SceneManager.LoadScene("PGame2");
    }

    public void back(){
        FindObjectOfType<AudioManager>().Play("Peek");
        backButton.SetActive(false);
        queueButton.SetActive(true);
        priorityQueueButton.SetActive(true);
        q1Button.SetActive(false);
        q2Button.SetActive(false);
        p1Button.SetActive(false);
        p2Button.SetActive(false);
    }

    public void mute(){
        FindObjectOfType<AudioManager>().Play("Peek");
        FindObjectOfType<AudioManager>().Mute();
        muteButton.SetActive(false);
        unmuteButton.SetActive(true);
    }

    public void unmute(){
        FindObjectOfType<AudioManager>().Play("Peek");
        FindObjectOfType<AudioManager>().Unmute();
        muteButton.SetActive(true);
        unmuteButton.SetActive(false);
    }
}
