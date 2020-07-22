using System.Collections;
using UnityEngine;

public class ChildDelayedRotation : MonoBehaviour
{
    public float delay;
    public float rotationSpeed;

    private Quaternion targetRotation;
    private Quaternion pendingTargetRotation;
    private Quaternion lastRotation;

    private void Start()
    {
        StartCoroutine(setTargetRotation(transform.parent.localRotation));
        lastRotation = transform.localRotation;
    }

    private IEnumerator setTargetRotation(Quaternion rotation)
    {
        pendingTargetRotation = rotation;
        yield return new WaitForSeconds(delay);
        targetRotation = pendingTargetRotation;
    }

    private void LateUpdate()
    {
        // overwrite any changes due to external rotation
        transform.rotation = lastRotation;

        // rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 5 * Time.deltaTime);

        // store new current rotation for next frame
        lastRotation = transform.rotation;

        // if the parent has a new rotation, set it as the new target rotation
        if (transform.parent.localRotation != targetRotation && transform.parent.localRotation != pendingTargetRotation)
        {
            StartCoroutine(setTargetRotation(transform.parent.localRotation));
        }
    }
}