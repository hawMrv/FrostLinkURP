using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject[] _Panels;

    private bool _showUI = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleUI();
        }
    }

    public void ToggleUI()
    {
        _showUI = !_showUI;

        for (int i = 0; i < _Panels.Length; i++) _Panels[i].gameObject.SetActive(_showUI);
    }
}
