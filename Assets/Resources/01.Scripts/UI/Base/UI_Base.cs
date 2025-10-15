using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Base : MonoBehaviour
{
    public abstract void Init();
    public abstract void Open();
    public abstract void Close();
    public abstract void Release();
}
