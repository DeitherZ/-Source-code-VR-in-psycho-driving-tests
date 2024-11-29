using UnityEngine;
using TMPro;
using sysio = System.IO;
using sydiag = System.Diagnostics;
using UnityEngine.SceneManagement;
using UltimateXR.CameraUtils;
using UnityEngine.UI;
using System.Linq;
using System.Runtime.InteropServices;
using System;

public class vr_ps00_iniciar : MonoBehaviour
{
    public static vr_ps00_iniciar Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Canvas inicio;
    [SerializeField] private Image msg;
    [SerializeField] private TMP_Text textMsg;

    private UxrCameraFade fade;
    private bool iniciar = false;
    private bool mensajeDataUser = false;
    private bool mensajeConfig = false;

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    // Start is called before the first frame update
    void Start()
    {
        fade = FindAnyObjectByType<UxrCameraFade>();
        sydiag.Process.Start(sysio.Path.Combine(sysio.Directory.GetCurrentDirectory(), "dataUserUI", "DataUser.exe"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !fade.IsFading)
        {
            iniciar = true;
        }

        if(iniciar) 
        {
            IniciarPrueba();
        }
    }

    public void IniciarPrueba()
    {
        //msg.gameObject.SetActive(true);
        inicio.gameObject.SetActive(false);
        iniciar = true;
        //SceneManager.LoadScene("vrps01-VisionPeriferica");
        sydiag.Process ventanaDataUser = sydiag.Process.GetProcessesByName("DataUser").FirstOrDefault();
        sydiag.Process ventanaConfig = sydiag.Process.GetProcessesByName("Config").FirstOrDefault();
        if ((ventanaDataUser != null))
        {
            msg.gameObject.SetActive(true);
            textMsg.text = "Esperando a que el Evaluador llene sus datos";
            if (!mensajeDataUser)
            {
                IntPtr mainWindowHandle = ventanaDataUser.MainWindowHandle;
                if (mainWindowHandle != IntPtr.Zero)
                {
                    SetForegroundWindow(mainWindowHandle);
                }
                mensajeDataUser = true;
            }
            return;
        }
        if ((ventanaConfig != null))
        {
            msg.gameObject.SetActive(true);
            textMsg.text = "Esperando a que el Evaluador aplique la configuración";
            if (!mensajeConfig)
            {
                IntPtr mainWindowHandle = ventanaConfig.MainWindowHandle;
                if (mainWindowHandle != IntPtr.Zero)
                {
                    SetForegroundWindow(mainWindowHandle);
                }
                mensajeConfig = true;
            }
            return;
        }
        SceneManager.LoadScene("vrps01-VisionPeriferica");
    }
}
