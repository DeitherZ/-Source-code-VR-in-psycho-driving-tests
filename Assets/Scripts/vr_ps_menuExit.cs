using UnityEngine;
using UnityEngine.UI;

public class vr_ps_menuExit : MonoBehaviour
{
    public static vr_ps_menuExit Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] public Image menuExit;
    private bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Q)) && !isPause)
        {
            isPause = true;
            menuExit.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Y) && isPause)
        {
            Salir();
        }

        if (Input.GetKeyDown(KeyCode.N) && isPause)
        {
            Continuar();
        }
    }

    public void Continuar()
    {
        isPause = false;
        menuExit.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Salir()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
