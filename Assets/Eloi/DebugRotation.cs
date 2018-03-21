
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
public class DebugRotation : MonoBehaviour
{

    public Transform _toDebug;
    public float _length = 2f;
    // Use this for initialization
    void Start()
    {


    }

    public void Update()
    {
        Debug.DrawLine(_toDebug.position, _toDebug.position + _toDebug.forward * _length, Color.blue);
        Debug.DrawLine(_toDebug.position, _toDebug.position + _toDebug.right * _length, Color.red);
        Debug.DrawLine(_toDebug.position, _toDebug.position + _toDebug.up * _length, Color.green);
    }
    public void Reset()
    {
        if (_toDebug == null)
            _toDebug = transform;
    }
}
    