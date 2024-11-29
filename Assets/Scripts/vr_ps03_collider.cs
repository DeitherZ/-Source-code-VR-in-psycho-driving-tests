using UnityEngine;
using TMPro;

public class vr_ps03_collider : MonoBehaviour
{
    public static vr_ps03_collider Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private TMP_Text textErrores;
    [SerializeField] private AudioSource audioError;
    [SerializeField] private GameObject padre;

    private int errores = 0;
    private int auxError = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Cubo")
        {
            auxError++;
            vr_ps03_movimientoObjeto.Instance.enabled = false;
            vr_ps03_destello.Instance.LuzRoja();
            audioError.Play();
            vr_ps03_movimientoObjeto.Instance.RegresoInicio();
            Invoke("ActivarMovimiento", 0.5f);
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Finaliza la colición");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cubo")
        {
            Debug.Log("Eureca");
        }
    }

    private void ActivarMovimiento()
    {
        vr_ps03_movimientoObjeto.Instance.enabled = true;
        if(auxError > 1)
        {
            //errores++;
            auxError = 0;
            textErrores.text = "Errores: " + errores;
            return;
        }
        errores++;
        auxError = 0;
        textErrores.text = "Errores: " + errores;
    }

    public int GetErrores()
    {
        return errores;
    }
}
