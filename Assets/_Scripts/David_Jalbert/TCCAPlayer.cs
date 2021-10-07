using UnityEngine;


public class TCCAPlayer : MonoBehaviour
{
    private TCCABody carBody;
    private TCCAWheel[] wheels;

    [Header("Behavior")]
    [Tooltip("How much torque to apply to the wheels. 1 is full speed forward, -1 is full speed backward, 0 is rest.")]
    public float motorDelta = 0;
    [Tooltip("How much steering to apply to the wheels. 1 is right, -1 is left, 0 is straight.")]
    public float steeringDelta = 0;
    [Tooltip("How much boost to apply to the wheels. 1 is full boost, 0 is no boost.")]
    public float boostDelta = 0;
    [Tooltip("Whether to apply the handbrake to the wheels.")]
    public bool applyHandbrake = false;
    [Header("Speed boost")]
    [Tooltip("Speed multiplier to apply when using the boost.")]
    public float boostMaxSpeedMultiplier = 2;
    [Tooltip("Acceleration multiplier to apply when using the boost.")]
    public float boostAccelerationMultiplier = 2;

    void Awake()
    {
        carBody = GetComponentInChildren<TCCABody>();
        carBody.initialize(this);

        wheels = GetComponentsInChildren<TCCAWheel>();
        foreach (TCCAWheel wheel in wheels)
        {
            wheel.initialize(this);
        }
    }

    private void FixedUpdate()
    {
        foreach (TCCAWheel wheel in wheels)
        {
            wheel.setAccelerationMultiplier(Mathf.Lerp(1, boostAccelerationMultiplier, boostDelta));
            wheel.setSpeedMultiplier(Mathf.Lerp(1, boostMaxSpeedMultiplier, boostDelta));
            wheel.setMotor(motorDelta);
            wheel.setSteering(steeringDelta);
            wheel.setHandbrake(applyHandbrake);
        }
    }

    public TCCABody getCarBody()
    {
        return carBody;
    }

    public Rigidbody getRigidbody()
    {
        return getCarBody()?.getRigidbody() ?? null;
    }

    public void setMotor(float d)
    {
        motorDelta = d;
    }

    public void setSteering(float d)
    {
        steeringDelta = d;
    }

    public void setHandbrake(bool e)
    {
        applyHandbrake = e;
    }

    public void setBoost(float d)
    {
        boostDelta = d;
    }

    public float getMotor()
    {
        return motorDelta;
    }

    public float getSteering()
    {
        return steeringDelta;
    }

    public bool getHandbrake()
    {
        return applyHandbrake;
    }

    public float getBoost()
    {
        return boostDelta;
    }

    public float getWheelsMaxSpin(int direction = 0)
    {
        float maxSpin = 0;
        foreach (TCCAWheel w in wheels)
        {
            float spin = w.getForwardSpinVelocity();
            if ((direction == 0 && Mathf.Abs(spin) > Mathf.Abs(maxSpin)) || (direction == 1 && spin > maxSpin) || (direction == -1 && spin < maxSpin))
            {
                maxSpin = spin;
            }
        }
        return maxSpin;
    }

    public float getWheelsMaxSpeed()
    {
        float maxSpeed = 0;
        foreach (TCCAWheel w in wheels)
        {
            if (w.motorMaxSpeed > maxSpeed) maxSpeed = w.motorMaxSpeed;
        }
        return maxSpeed;
    }

    public float getPitchAngle()
    {
        return getCarBody()?.getPitchAngle() ?? 0;
    }

    public float getRollAngle()
    {
        return getCarBody()?.getRollAngle() ?? 0;
    }

    public float getForwardVelocity()
    {
        return getCarBody()?.getForwardVelocity() ?? 0;
    }

    public float getForwardVelocityDelta()
    {
        if (getWheelsMaxSpeed() == 0) return 0;
        return getForwardVelocity() / getWheelsMaxSpeed();
    }

    public float getLateralVelocity()
    {
        return getCarBody()?.getLateralVelocity() ?? 0;
    }

    public bool isPartiallyGrounded()
    {
        return isGrounded() && !isFullyGrounded();
    }

    public bool isGrounded()
    {
        foreach (TCCAWheel w in wheels)
        {
            if (w.isTouchingGround()) return true;
        }
        return false;
    }

    public bool isFullyGrounded()
    {
        foreach (TCCAWheel w in wheels)
        {
            if (!w.isTouchingGround()) return false;
        }
        return true;
    }

    // syncing the value across all players
}