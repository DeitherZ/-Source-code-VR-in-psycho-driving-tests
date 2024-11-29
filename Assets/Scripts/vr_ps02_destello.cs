using UnityEngine;
using UnityEngine.UI;

public class vr_ps02_destello : MonoBehaviour
{
    public static vr_ps02_destello Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image destello;

    private float duracion = 0.2f;
    private float intensidad = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destello()
    {
        destello.color = new Color(1f, 0f, 0f, intensidad);
        destello.gameObject.SetActive(true);
        Invoke("OcultarDestello", duracion);
    }

    void OcultarDestello()
    {
        destello.gameObject.SetActive(false);
    }
}
