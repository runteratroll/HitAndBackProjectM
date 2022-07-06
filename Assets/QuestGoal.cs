using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoal 
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

}


public enum GoalType
{
    Kill,
    Gathering
}