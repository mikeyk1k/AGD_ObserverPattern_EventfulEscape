using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    public delegate void LightSwitchDelegate();
    public static LightSwitchDelegate OnLightSwitchToggled;

    private void OnEnable()
    {
        OnLightSwitchToggled += LightSwitchToggled;
    }
    private void Start() => currentState = SwitchState.Off;

    public void Interact()
    {
        OnLightSwitchToggled?.Invoke();
    }
    private void toggleLights()
    {
        bool lights = false;

        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                lights = false;
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
                lights = true;
                break;
            case SwitchState.Unresponsive:
                break;
        }
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }
    private void LightSwitchToggled()
    {
        toggleLights();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
        GameService.Instance.GetInstructionView().HideInstruction();
    }
}
