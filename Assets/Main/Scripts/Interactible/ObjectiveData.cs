using UnityEngine;

[CreateAssetMenu(fileName = "newObjective", menuName = "BoyAnomaly/Objective")] //Create Menu Option for making an Objective Scriptable Object.
public class ObjectiveData : ScriptableObject
{
    public int id; //This is used to identify which objective to remove
    public string objectiveText;
    public bool isActive = false; //this is to identify which objectives should be shown
}

//The use of Scriptable Object here is for later when a checkpoint option is added.
