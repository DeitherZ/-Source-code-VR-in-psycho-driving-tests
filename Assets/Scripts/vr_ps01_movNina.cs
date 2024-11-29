using UltimateXR.CameraUtils;
using UnityEngine;

public class vr_ps01_movNina : MonoBehaviour
{
    public static vr_ps01_movNina Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private float velocidad = 1f;

    private UxrCameraFade fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindAnyObjectByType<UxrCameraFade>();    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ColliderPeriferic" && !fade.IsFading)
        {
            vr_ps01_destello.Instance.Destello();
            vr_ps01_sema.Instance.CountErrExtern();
            vr_ps01_sema.Instance.CancelInvokeControl();
        }
    }

    public void UpdateVelocidad()
    {
        if(velocidad <= 3.5f)
        {
            velocidad += 0.15f;
            //Debug.Log("Aumenta velocidad a " + velocidad);
        }
        /*else
        {
            Debug.Log("Se alcanzó el limite de la velocidad " + velocidad);
        }*/
    }
}
