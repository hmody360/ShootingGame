using UnityEngine;

public class ObjectiveModfier : MonoBehaviour
{

    public int[] CloseObjectives;
    public int[] OpenObjectives;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            foreach (int index in CloseObjectives)
            {
                ObjectiveData objToRemove = UIManger.instance.objectiveList.Find(obj => obj.id == index);

                if (objToRemove != null)
                {
                    objToRemove.isActive = false;
                }

            }

            foreach (int index in OpenObjectives)
            {
                ObjectiveData objToActivate = UIManger.instance.objectiveList.Find(obj => obj.id == index);

                if (objToActivate != null)
                {
                    objToActivate.isActive = true;
                }

            }

            UIManger.instance.updateObjectiveList();

            Destroy(gameObject);

        }
    }


}
