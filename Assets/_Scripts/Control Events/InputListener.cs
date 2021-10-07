using UnityEngine;

public class InputListener : MonoBehaviour
{

    // for controller reference
    private ControllerSelf controllerSelf;


    #region Unity Methods

    private void OnEnable()
    {
        ControlInput.upArrowPressed += ForwardPressed;
        ControlInput.upArrowReleased += ForwardReleased;
        ControlInput.onDownButtonPressed += DownPressed;
        ControlInput.onDownButtonReleased += DownReleased;
        ControlInput.onRightButtonPressed += RightPressed;
        ControlInput.onRightButtonReleased += RightReleased;
        ControlInput.onLeftButtonPressed += LeftPressed;
        ControlInput.onLeftButtonReleased += LeftReleased;
    }

    private void OnDisable()
    {
        ControlInput.upArrowPressed -= ForwardPressed;
        ControlInput.upArrowReleased -= ForwardReleased;
        ControlInput.onDownButtonPressed -= DownPressed;
        ControlInput.onDownButtonReleased -= DownReleased;
        ControlInput.onRightButtonPressed -= RightPressed;
        ControlInput.onRightButtonReleased -= RightReleased;
        ControlInput.onLeftButtonPressed -= LeftPressed;
        ControlInput.onLeftButtonReleased -= LeftReleased;
    }

    private void Start()
    {
        controllerSelf = GetComponent<ControllerSelf>();
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

    private void DownReleased()
    {
        controllerSelf.SetInputVertical(0f);
    }

    private void DownPressed()
    {
        controllerSelf.SetInputVertical(-1f);
    }

    private void ForwardReleased()
    {
        controllerSelf.SetInputVertical(0f);
    }

    private void ForwardPressed()
    {
        controllerSelf.SetInputVertical(1f);
    }

    #endregion
}
