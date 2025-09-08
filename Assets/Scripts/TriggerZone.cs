using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] GameObject doorPrefab;
    [SerializeField] float doorSpeed;

    Vector3 originalDoorPosition;
    Vector3 openedDoorPosition;

    enum DoorStates
    {
        Opening,
        Closing,
        Resting
    }

    private DoorStates doorStates = DoorStates.Resting;

    private void Start()
    {
        originalDoorPosition = doorPrefab.transform.position;
        openedDoorPosition = originalDoorPosition - new Vector3(0, 6, 0);
    }

    private void Update()
    {
        if (doorStates == DoorStates.Opening)
        {
            // Use current position as start
            doorPrefab.transform.position = Vector3.MoveTowards(doorPrefab.transform.position, openedDoorPosition, doorSpeed * Time.deltaTime
            );

            // Stop moving when fully open
            if (doorPrefab.transform.position == openedDoorPosition)
            {
                doorStates = DoorStates.Resting;
            }
        }
        else if (doorStates == DoorStates.Closing)
        {
            doorPrefab.transform.position = Vector3.MoveTowards(doorPrefab.transform.position, originalDoorPosition, doorSpeed * Time.deltaTime
            );

            // Stop moving when fully closed
            if (doorPrefab.transform.position == originalDoorPosition)
            {
                doorStates = DoorStates.Resting;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorStates = DoorStates.Opening;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorStates = DoorStates.Closing;
        }
    }
}
