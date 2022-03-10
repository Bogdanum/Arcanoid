using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupsContainer : MonoBehaviour
{
    public Dictionary<Type, BasePopup> PopupsStorage { get; private set; }

    public void Init(Dictionary<Type, BasePopup> popupsStorage)
    {
        PopupsStorage = popupsStorage;
    }

}
