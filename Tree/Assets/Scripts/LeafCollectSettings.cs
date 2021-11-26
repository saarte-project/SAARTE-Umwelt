using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// Handles and stores settings regarding the collecting leaves challenge.
/// </summary>
public class LeafCollectSettings : MonoBehaviour
{
    static int leafGoal = 10;

    static LeafType leafTypeSelection = LeafType.Eiche;

    [SerializeField]
    private TextMeshPro leafGoalDisplay = null;


/// <summary>
/// Increases/decreases amount of leaves which need to be collected during the challenge.
/// </summary>
    public void AddToGoal(int summand)
    {
        if(leafGoal + summand > 1)
        {
            leafGoal += summand;

            leafGoalDisplay.text = "Gather amount = <b>" + leafGoal + "</b>";
        }
    }

/// <summary>
/// Returns amount of leaves which need to be collected during the challenge.
/// </summary>
    public static int GetLeafGoal()
    {
        return leafGoal;
    }

/// <summary>
/// Sets desired leaf type which has to be collected during the challenge.
/// </summary>
    public void SetLeafTypeSelection(string leafType_in)
    {
        switch (leafType_in)
        {
            case "Ahorn":
                leafTypeSelection = LeafType.Ahorn;
                break;

            case "Birke":
                leafTypeSelection = LeafType.Birke;
                break;

            case "Eiche":
                leafTypeSelection = LeafType.Eiche;
                break;

            default:
                Debug.Log("LeafType parameter can not be handled");
                break;
        }
            
        //leafTypeSelection = leafType_in;
    }

/// <summary>
/// Returns desired leaf type which has to be collected during the challenge.
/// </summary>
    public static LeafType GetLeafTypeSelection()
    {
        return leafTypeSelection;
    }
}
