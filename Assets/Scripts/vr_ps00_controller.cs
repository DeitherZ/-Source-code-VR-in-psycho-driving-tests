using System.Collections;
using System.Collections.Generic;
using UltimateXR.CameraUtils;
using UltimateXR.Core;
using UnityEngine;
using UnityEngine.UI;
using sysio = System.IO;
using sydiag = System.Diagnostics;
using System.Diagnostics;
using System.Linq;
using System;
using System.Runtime.InteropServices;

public class vr_ps00_controller : MonoBehaviour
{
    [SerializeField] private Canvas formulario;

    //private static [DllImport("user32.dll")]

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            vr_ps00_iniciar.Instance.IniciarPrueba();
        }
    }
}
