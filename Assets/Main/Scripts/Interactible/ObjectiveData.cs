using UnityEngine;

[CreateAssetMenu(fileName = "newObjective", menuName = "BoyAnomaly/Objective")]
public class ObjectiveData : ScriptableObject
{
    public int id;
    public string objectiveText;
    public bool isActive = false;
}
