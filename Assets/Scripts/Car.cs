using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car
{
    private string carName;
    private int priority;

    public Car(int num)
    {
        switch (num)
        {
            case 0:
                setCar("Blue Car", 1);
                break;
            case 1:
                setCar("Yellow Car", 1);
                break;
            case 2:
                setCar("Orange Car", 1);
                break;
            case 3:
                setCar("Ambulance Car", 2);
                break;
            case 4:
                setCar("Police Car", 3);
                break;
            case 5:
                setCar("Motorcycle", 4);
                break;
        }
    }

    private void setCar(string carName, int priority)
    {
        this.carName = carName;
        this.priority = priority;
    }

    public string getCarName()
    {
        return carName;
    }
}
