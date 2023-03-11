using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public enum Device
{
    Keyboard,
    Gamepad
}


public class GUIInputHellper : MonoBehaviour
{

    [SerializeField] public Vector3 PositionHellper;

    [SerializeField] public string TriggerTag;

    [SerializeField] public bool IsNotTrigger;

    [SerializeField] public List<DeviceKey> DeviceKeys;

    [SerializeField] public GUIStyle SkinInputHellper;

    
    private string _hellpText = "";

    private bool _isPaintHellp = false;

    private string HellpText()
    {
        if (Gamepad.current != null && Gamepad.current.IsActuated(0))
            return DeviceKeys.Where(x => x.Device.Equals(Device.Gamepad)).Last().Key;
        else if (Keyboard.current.IsActuated(0))
            return DeviceKeys.Where(x => x.Device.Equals(Device.Keyboard)).Last().Key;
        else
            return _hellpText;
    }



    private void OnGUI()
    {
        if (_isPaintHellp || IsNotTrigger)
        {
            _hellpText = HellpText();
            var rect = new Rect(Camera.main.WorldToScreenPoint(transform.position + PositionHellper), new Vector2(100, 10));
            GUI.Label(rect, _hellpText, SkinInputHellper);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isPaintHellp = collision.CompareTag(TriggerTag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isPaintHellp = !collision.CompareTag(TriggerTag);
    }

    [Serializable]
    public class DeviceKey
    {
        [SerializeField] public Device Device;
        [SerializeField] public string Key;   
    }
}
