using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static Dictionary<string, KeyCode> keyMapping;
    static string[] keyMaps = new string[9]
    {
        "Jump",
        "Left",
        "Right",
        "Change Weapon",
        "Attack 1",
        "Attack 2",
        "Dodge",
        "Block",
        "Escape"
    };
    static KeyCode[] defaults = new KeyCode[9]
    {
        KeyCode.W,
        KeyCode.A,
        KeyCode.D,
        KeyCode.Q,
        KeyCode.H,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.Escape
    };

    static KeyCode[] wesDefault = new KeyCode[9]
    {
        KeyCode.W,
        KeyCode.A,
        KeyCode.D,
        KeyCode.Q,
        KeyCode.Mouse0,
        KeyCode.Mouse1,
        KeyCode.LeftShift,
        KeyCode.F,
        KeyCode.Escape
    };

    static bool defaultOn = true;

    static InputManager()
    {
        keyMapping = new Dictionary<string, KeyCode>();
        for (int i = 0; i < keyMaps.Length; ++i)
        {
            keyMapping.Add(keyMaps[i], defaults[i]);
        }
    }

    public static void UpdateKeyMap(KeyCode[] changedKeyCodes)
    {
        keyMapping.Clear();

        for (int i = 0; i < keyMaps.Length; ++i)
        {
            keyMapping.Add(keyMaps[i], changedKeyCodes[i]);
        }
    }

    public static bool SwapBinding()
    {
        if (defaultOn)
        {
            UpdateKeyMap(wesDefault);
            defaultOn = false;
            return true;
        }
        else
        {
            UpdateKeyMap(defaults);
            defaultOn = true;
        }

        return false;
    }

    public static KeyCode[] GetKeyCodes()
    {
        return defaults;
    }

    public static bool CheckKey(KeyCode key)
    {
        return keyMapping.ContainsValue(key);
    }

    public static bool GetKeyDown(string keyMap)
    {
        return Input.GetKeyDown(keyMapping[keyMap]);
    }
    public static bool GetKeyUp(string keyMap)
    {
        return Input.GetKeyUp(keyMapping[keyMap]);
    }
    public static bool GetKey(string keyMap)
    {
        return Input.GetKey(keyMapping[keyMap]);
    }
}
