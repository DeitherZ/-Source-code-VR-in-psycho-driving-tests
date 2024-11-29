using UnityEngine;

public class vr_ps01_car : MonoBehaviour
{
    public static vr_ps01_car Instance;

    void Awake()
    {
        Instance = this;
    }

    private float Velocidad = 8f;
    private bool stopCar = false;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Mover la furgoneta hacia delante
        //transform.Translate(0,0,1);
        if (!stopCar)
        {
            transform.Translate(Vector3.right * Time.deltaTime * Velocidad);
        }
        else
        {
            if (Velocidad > 0f)
            {
                Velocidad -= 0.05f;
                transform.Translate(Vector3.right * Time.deltaTime * Velocidad);
            }
            else
            {
                vr_ps01_timer.Instance.StopTimer();
                vr_ps01_finish.Instance.Activador();
                this.enabled = false;
            }
        }
    }

    public void DetenerCarro(bool detenerCar)
    {
        stopCar = detenerCar;
    }
}
