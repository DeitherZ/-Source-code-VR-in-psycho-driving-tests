using UnityEngine;

public class vr_ps03_destello : MonoBehaviour
{
    public static vr_ps03_destello Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Light luzRoja;
    [SerializeField] private Light luzSala;

    private float duracion = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        luzRoja.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LuzRoja()
    {
        luzSala.gameObject.SetActive(false);
        luzRoja.gameObject.SetActive(true);
        Invoke("CancelarLuzRoja", duracion);
    }

    void CancelarLuzRoja()
    {
        luzSala.gameObject.SetActive(true);
        luzRoja.gameObject.SetActive(false);
    }
}
