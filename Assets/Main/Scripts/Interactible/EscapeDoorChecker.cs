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
    void Update()
    {
        if(UIManger.instance.objectiveList.Find(obj => obj.id == 6).isActive == true && !_doorTrigger.canOpen)
        {
            _doorTrigger.canOpen = true;
            _escapeAlarmSFX.Play();
        }
    }
}
