using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2Questions
{
    private string questionText = "";
    private string[] answers = new string[4];
    private Queue<Car> inQueue = new Queue<Car>();
    private Queue<Car> inPoll = new Queue<Car>();
    private PriorityQueue<Car> pInQueue = new PriorityQueue<Car>();
    private bool isInQueue;
    private bool isPriority;

    public Game2Questions(bool isPriority)
    {
        this.isPriority = isPriority;
    }

    public void generateQuestion()
    {
        newGame();
        int questionLine = 7;
        int currentLine = 0;

        if (isPriority)
        {
            while (currentLine < questionLine)
            {
                int rand = Random.Range(0, 2); // 0 -> dequeue, 1 -> enqueue
                if ((rand == 0 && pInQueue.Count == 0) || (rand == 1 && pInQueue.Count == 4)) continue;
                generateTask(rand);
                currentLine++;
            }
        }
        else
        {
            while (currentLine < questionLine)
            {
                int rand = Random.Range(0, 2); // 0 -> dequeue, 1 -> enqueue
                if ((rand == 0 && inQueue.Count == 0) || (rand == 1 && inQueue.Count == 4)) continue;
                generateTask(rand);
                currentLine++;
            }
        }

        calculateAnswer();

        if (isInQueue)
        {
            questionText += "\r\nArrange the car(s) inside queue";
        }
        else
        {
            questionText += "\r\nArrange the car(s) outside queue (poll)";
        }

        getAnswers();
    }

    void generateTask(int num) //generate task
    {
        if (num == 1)
        {
            Car car = new Car(Random.Range(0, 6));

            if (isPriority)
            {
                pInQueue.Enqueue(car);
                questionText += "priorityQueue.offer(" + car.getCarName() + ");\r\n";
            }
            else
            {
                inQueue.Enqueue(car);
                questionText += "queue.offer(" + car.getCarName() + ");\r\n";
            }
            return;
        }

        if (isPriority)
        {
            inPoll.Enqueue(pInQueue.Dequeue());
            questionText += "priorityQueue.poll();\r\n";
        }
        else
        {
            inPoll.Enqueue(inQueue.Dequeue());
            questionText += "queue.poll();\r\n";
        }
    }

    public string getQuestionText()
    {
        return questionText;
    }

    void calculateAnswer()
    {
        int i = 0;

        if (isPriority)
        {
            if (isInQueue)
            {
                while (pInQueue.Count > 0)
                {
                    answers[i] = pInQueue.Dequeue().getCarName();
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
        else
        {
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
    }

    public string[] getAnswers()
    {
        string answerText = "";
        foreach (string ans in answers)
        {
            answerText += ans + ", ";
        }
        Debug.Log(answerText);
        return answers;
    }

    void newGame()
    {
        isInQueue = Random.Range(0, 2) == 1 ? true : false; //1 -> inqueue, 0 -> inpoll
        questionText = "";
        answers = new string[4];
        inQueue = new Queue<Car>();
        inPoll = new Queue<Car>();
        pInQueue = new PriorityQueue<Car>();
    }
}
