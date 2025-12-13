using UnityEngine;

public class EscapeDoorChecker : MonoBehaviour
{
    private DoorTrigger _doorTrigger;
    [SerializeField] private AudioSource _escapeAlarmSFX;

    private void Awake()
    {
        _doorTrigger = GetComponent<DoorTrigger>();
    }

    // Update is called once per frame
    void Update() //Check if the final objective has been activated and make the door openable.
    {
        if(UIManger.instance.objectiveList.Find(obj => obj.id == 6).isActive == true && !_doorTrigger.canOpen)
        {
            _doorTrigger.canOpen = true;
            _escapeAlarmSFX.Play();
        }
    }
}
