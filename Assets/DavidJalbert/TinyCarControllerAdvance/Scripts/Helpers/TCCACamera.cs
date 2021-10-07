using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DavidJalbert.TinyCarControllerAdvance
{
    public class TCCACamera : MonoBehaviour
    {
        public enum CAMERA_MODE
        {
            TopDown, ThirdPerson
        }

        [Tooltip("Which player object the camera should track.")]
        public TCCAPlayer carController;
        [Tooltip("Top Down: Only change the camera's position, keep rotation fixed.\nThird Person: Change both the position and rotation relative to the vehicle.")]
        public CAMERA_MODE viewMode = CAMERA_MODE.TopDown;

        [Header("Top Down parameters")]
        [Tooltip("Distance of the camera from the target.")]
        public float topDownDistance = 50;
        [Tooltip("Rotation of the camera.")]
        public Vector3 topDownAngle = new Vector3(60, 0, 0);

        [Header("Third Person parameters")]
        [Tooltip("Position of the target relative to the car.")]
        public Vector3 thirdPersonOffsetStart = new Vector3(0, 0, 0);
        [Tooltip("Position of the camera relative to the car.")]
        public Vector3 thirdPersonOffsetEnd = new Vector3(0, 2, -10);
        [Tooltip("Rotation of the camera relative to the target.")]
        public Vector3 thirdPersonAngle = new Vector3(15, 0, 0);
        [Tooltip("The minimum distance to keep when an obstacle is in the way of the camera.")]
        public float thirdPersonSkinWidth = 0.1f;
        [Tooltip("Smoothing of the camera's rotation. The lower the value, the smoother the rotation. Set to 0 to disable smoothing.")]
        public float thirdPersonInterpolation = 10;
        [Tooltip("Lowers the camera's rotation if the velocity of the rigidbody is below this value. Set to 0 to disable.")]
        public float interpolationUpToSpeed = 50;

        void Update()
        {
            if (carController == null) return;

            Vector3 followPosition = carController.getRigidbody().transform.position;
            Quaternion followRotation = carController.getRigidbody().transform.rotation;
            
            Vector3 targetPosition = transform.position;
            Quaternion targetRotation = transform.rotation;

            float deltaTime = Time.deltaTime;

            switch (viewMode)
            {
                case CAMERA_MODE.ThirdPerson:
                    float interpolationMultiplier = 1;

                    Rigidbody body = carController.getRigidbody();
                    if (body != null)
                    {
                        float forwardVelocity = carController.getCarBody().getForwardVelocity();
                        interpolationMultiplier = interpolationUpToSpeed > 0 ? Mathf.Clamp01(Mathf.Abs(forwardVelocity) / interpolationUpToSpeed) : 1f;
                        Vector3 rotationDirection = Vector3.Lerp(body.transform.forward, body.velocity.normalized, Mathf.Clamp01(forwardVelocity));
                        followRotation = Quaternion.LookRotation(rotationDirection, Vector3.back);
                    }

                    Vector3 rotationEuler = thirdPersonAngle + Vector3.up * followRotation.eulerAngles.y;

                    Quaternion xzRotation = Quaternion.Euler(new Vector3(rotationEuler.x, targetRotation.eulerAngles.y, rotationEuler.z));
                    Quaternion lerpedRotation = Quaternion.Euler(rotationEuler);
                    targetRotation = Quaternion.Lerp(xzRotation, lerpedRotation, Mathf.Clamp01(thirdPersonInterpolation <= 0 ? interpolationMultiplier : thirdPersonInterpolation * deltaTime * interpolationMultiplier));

                    Vector3 forwardDirection = targetRotation * Vector3.forward;
                    Vector3 rightDirection = targetRotation * Vector3.right;
                    Vector3 directionVector = forwardDirection * thirdPersonOffsetEnd.z + Vector3.up * thirdPersonOffsetEnd.y + rightDirection * thirdPersonOffsetEnd.x;

                    Vector3 directionVectorNormal = directionVector.normalized;
                    float directionMagnitude = directionVector.magnitude;

                    Vector3 cameraWorldDirection = directionVectorNormal;
                    Vector3 startCast = followPosition + thirdPersonOffsetStart;
                    RaycastHit[] hits = Physics.RaycastAll(startCast, cameraWorldDirection, directionMagnitude);
                    float hitDistance = -1;
                    foreach (RaycastHit hit in hits)
                    {
                        if (!isChildOf(hit.transform, carController.transform)) hitDistance = hitDistance >= 0 ? Mathf.Min(hitDistance, hit.distance) : hit.distance;
                    }
                    if (hitDistance >= 0)
                    {
                        targetPosition = followPosition + thirdPersonOffsetStart + directionVectorNormal * Mathf.Max(thirdPersonSkinWidth, hitDistance - thirdPersonSkinWidth);
                    }
                    else
                    {
                        targetPosition = directionVector + thirdPersonOffsetStart + followPosition;
                    }
                    break;

                case CAMERA_MODE.TopDown:
                    targetRotation = Quaternion.Euler(topDownAngle);
                    targetPosition = followPosition + targetRotation * Vector3.back * topDownDistance;
                    break;
            }

            transform.position = targetPosition;
            transform.rotation = targetRotation;
        }

        public bool isChildOf(Transform source, Transform target)
        {
            Transform child = source;
            while (child != null)
            {
                if (child == target) return true;
                child = child.parent;
            }
            return false;
        }
    }
}