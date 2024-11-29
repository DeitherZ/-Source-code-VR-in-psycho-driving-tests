using UnityEngine;
using UnityEngine.UI;
using sydiag = System.Diagnostics;
using System.Linq;

public class vr_ps00_menuExit : MonoBehaviour
{
    public static vr_ps00_menuExit Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image menuExit;
    [SerializeField] private Canvas PanelInicial;
    private bool isPause = false;
    private bool returnActivateInitial = false;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Q)) && !isPause) 
        {
            if (PanelInicial.gameObject.activeInHierarchy)
            {
                PanelInicial.gameObject.SetActive(false);
                returnActivateInitial = true;
            }
            isPause = true;
            menuExit.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        if(Input.GetKeyDown(KeyCode.Y) && isPause)
        {
            Salir();
        }

        if(Input.GetKeyDown(KeyCode.N) && isPause)
        {
            Continuar();
        }
    }

    public void Continuar()
    {
        isPause = false;
        if (returnActivateInitial)
        {
            PanelInicial.gameObject.SetActive(true);
        }
        menuExit.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Salir()
    {
        sydiag.Process ventanaDataUser = sydiag.Process.GetProcessesByName("DataUser").FirstOrDefault();
        sydiag.Process ventanaConfig = sydiag.Process.GetProcessesByName("Config").FirstOrDefault();
        if ((ventanaDataUser != null))
        {
            ventanaDataUser.CloseMainWindow();
        }
        if ((ventanaConfig != null))
        {
            ventanaConfig.CloseMainWindow();
        }

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
