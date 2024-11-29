using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class vr_ps02_cuentaAtras : MonoBehaviour
{
    public static vr_ps02_cuentaAtras Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image cuentaAtras;
    [SerializeField] private TMP_Text textCuentaAtras;
    [SerializeField] private AudioSource preparadoListoComience;

    private float time = 0;
    private int countBack;


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
            vr_ps02_timer.Instance.enabled = true;
            vr_ps02_sema.Instance.Iniciador();
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
