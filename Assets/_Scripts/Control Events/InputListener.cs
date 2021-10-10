using UnityEngine;

public class InputListener : MonoBehaviour
{

    // for controller reference
    private ControllerSelf controllerSelf;
    private bool isBraking;


    #region Unity Methods

    private void OnEnable()
    {
        //ControlInput.upArrowPressed += ForwardPressed;
        //ControlInput.upArrowReleased += ForwardReleased;
        //ControlInput.onDownButtonPressed += DownPressed;
        //ControlInput.onDownButtonReleased += DownReleased;
        ControlInput.onRightButtonPressed += RightPressed;
        ControlInput.onRightButtonReleased += RightReleased;
        ControlInput.onLeftButtonPressed += LeftPressed;
        ControlInput.onLeftButtonReleased += LeftReleased;
        ControlInput.onBrakesPressed += BrakesPressed;
        ControlInput.onBrakeReleased += BrakesReleased;
        ControlInput.onNitroPressed += NitroPressed;
        ControlInput.onNitroReleased += NitroReleased;
    }

    private void OnDisable()
    {
        //ControlInput.upArrowPressed -= ForwardPressed;
        //ControlInput.upArrowReleased -= ForwardReleased;
        //ControlInput.onDownButtonPressed -= DownPressed;
        //ControlInput.onDownButtonReleased -= DownReleased;
        ControlInput.onRightButtonPressed -= RightPressed;
        ControlInput.onRightButtonReleased -= RightReleased;
        ControlInput.onLeftButtonPressed -= LeftPressed;
        ControlInput.onLeftButtonReleased -= LeftReleased;
        ControlInput.onBrakesPressed -= BrakesPressed;
        ControlInput.onBrakeReleased -= BrakesReleased;
        ControlInput.onNitroPressed -= NitroPressed;
        ControlInput.onNitroReleased -= NitroReleased;
    }

    private void Start()
    {
        controllerSelf = GetComponent<ControllerSelf>();
    }

    private void Update()
    {
        // when not braking
        if (!isBraking)
        {
            controllerSelf.SetInputVertical(1);
        }
        // when brakes applied
        else
        {
            controllerSelf.SetInputVertical(0);
        }
    }

    #endregion


    #region Event Listeners

    private void LeftReleased()
    {
        controllerSelf.SetInputHorizontal(0);
    }

    private void LeftPressed()
    {
        controllerSelf.SetInputHorizontal(-1f);
    }

    private void RightReleased()
    {
        controllerSelf.SetInputHorizontal(0);
    }

    private void RightPressed()
    {
        controllerSelf.SetInputHorizontal(1f);
    }

    //private void DownReleased()
    //{
    //    controllerSelf.SetInputVertical(0f);
    //}

    //private void DownPressed()
    //{
    //    controllerSelf.SetInputVertical(-1f);
    //}

    //private void ForwardReleased()
    //{
    //    controllerSelf.SetInputVertical(0f);
    //}

    //private void ForwardPressed()
    //{
    //    controllerSelf.SetInputVertical(1f);
    //}

    private void BrakesPressed()
    {
        isBraking = true;
        controllerSelf.SethandBrakeValue(true);
    }

    private void BrakesReleased()
    {
        isBraking = false;
        controllerSelf.SethandBrakeValue(false);
    }

    private void NitroPressed()
    {
        controllerSelf.SetBoostValue(1);
    }

    private void NitroReleased()
    {
        controllerSelf.SetBoostValue(0);
    }

    #endregion
}
