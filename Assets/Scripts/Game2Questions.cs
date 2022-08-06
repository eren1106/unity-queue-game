using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Questions
{
    private string questionText = "";
    private string[] answers = new string[4];
    private Queue<Car> inQueue = new Queue<Car>();
    private Queue<Car> inPoll = new Queue<Car>();
    private bool isInQueue;

    public void generateQuestion()
    {
        isInQueue = Random.Range(0, 2) == 1 ? true : false; //1 -> inqueue, 0 -> inpoll
        int questionLine = 7;
        int currentLine = 0;
        while (currentLine < questionLine)
        {
            int rand = Random.Range(0, 2); // 0 -> dequeue, 1 -> enqueue
            if ((rand == 0 && inQueue.Count == 0) || (rand == 1 && inQueue.Count == 4)) continue;
            generateTask(rand);
            currentLine++;
        }
        calculateAnswer();

        Debug.Log(getQuestionText());
        Debug.Log(isInQueue);
        getAnswers();
    }

    void generateTask(int num) //generate task
    {
        if (num == 1)
        {
            Car car = new Car(Random.Range(0, 6));
            inQueue.Enqueue(car);
            questionText += "queue.offer(" + car.getCarName() + ");\r\n";
            return;
        }
        inPoll.Enqueue(inQueue.Dequeue());
        questionText += "queue.poll();\r\n";
    }

    public string getQuestionText()
    {
        return questionText;
    }

    void calculateAnswer()
    {
        int i = 0;
        if (isInQueue)
        {
            while (inQueue.Count > 0)
            {
                answers[i] = inQueue.Dequeue().getCarName();
                i++;
            }
            return;
        }
        while (inPoll.Count > 0)
        {
            answers[i] = inPoll.Dequeue().getCarName();
            i++;
        }
    }

    public string[] getAnswers()
    {
        string answerText = "";
        foreach(string ans in answers){
            answerText += ans + ", ";
        }
        Debug.Log(answerText);
        return answers;
    }
}
