using UltimateXR.CameraUtils;
using UnityEngine;
using sysio = System.IO;
using sydiag = System.Diagnostics;
using System.Linq;
using System;
using System.Runtime.InteropServices;

public class vr_ps_config : MonoBehaviour
{
    private UxrCameraFade fade;

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<UxrCameraFade>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.C)) && !fade.IsFading)
        {
            sydiag.Process proceso = sydiag.Process.GetProcessesByName("Config").FirstOrDefault();
            if (proceso == null)
            {
                sydiag.Process.Start(sysio.Path.Combine(sysio.Directory.GetCurrentDirectory(), "configUI", "Config.exe"));
            }
            else
            {
                IntPtr mainWindowHandle = proceso.MainWindowHandle;
                if(mainWindowHandle != IntPtr.Zero) 
                {
                    SetForegroundWindow(mainWindowHandle);
                }
            }
        }
    }
}
