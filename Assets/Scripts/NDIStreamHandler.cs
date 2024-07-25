using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Klak;
using Klak.Ndi;
using UnityEngine.UI;

public class NDIStreamHandler : MonoBehaviour
{
    public NdiSender _MainSender;

    public GameObject[] _ExtraSender;

    public Toggle _TglEnableDualNDI;

    private bool _enableExtraNDI = false;

    private void Start()
    {
        EnableExtraNDI(_TglEnableDualNDI.isOn);
    }

    public void EnableExtraNDI(bool enable)
    {
        _enableExtraNDI = enable;

        _MainSender.enabled = !_enableExtraNDI;

        for (int i = 0; i < _ExtraSender.Length; i++)
        {
            _ExtraSender[i].SetActive(enable);
        }
    }
}
