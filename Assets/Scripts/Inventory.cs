using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance; // ???
    public List<AllKeys> inventoryKeys = new List<AllKeys>(); // ???

    private void Awake()
    {
        Instance = this;
    }

    public void AddKey(AllKeys key)
    {
        inventoryKeys.Add(key);
    }

    public void RemoveKey(AllKeys key)
    {
        inventoryKeys.Remove(key);
    }

    public enum AllKeys // All available keys
    {
        KeyRed,
        KeyBlue
    }
}
