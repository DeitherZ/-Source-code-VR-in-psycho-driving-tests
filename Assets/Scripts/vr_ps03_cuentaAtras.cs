using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class vr_ps03_cuentaAtras : MonoBehaviour
{
    public static vr_ps03_cuentaAtras Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image cuentaAtras;
    [SerializeField] private TMP_Text textCuentaAtras;
    [SerializeField] private TMP_Text textErrores;
    [SerializeField] private AudioSource preparadoListoComience;

    private int countBack;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        int segundos = Mathf.FloorToInt(time % 60);
        countBack = 3 - segundos;
        textCuentaAtras.text = "" + countBack;
        if (countBack == 0)
        {
            cuentaAtras.gameObject.SetActive(false);
            vr_ps03_movimientoObjeto.Instance.enabled = true;
            vr_ps03_timer.Instance.enabled = true;
            vr_ps_menuExit.Instance.enabled = true;
            textErrores.text = "Errores: 0";
            this.enabled = false;
        }
    }

    public void Iniciador()
    {
        this.enabled = true;
        Invoke("Audio", 0.5f);
    }

    private void Audio()
    {
        preparadoListoComience.Play();
    }
}
