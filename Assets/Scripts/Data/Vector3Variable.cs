using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector3 Variable.asset", menuName = "Create Vector3 Variable")]
public class Vector3Variable : ScriptableObject,ISerializationCallbackReceiver
{
    public Vector3 Value;

    [NonSerialized]
    public Vector3 RuntimeValue;

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        RuntimeValue = Value;
    }
}