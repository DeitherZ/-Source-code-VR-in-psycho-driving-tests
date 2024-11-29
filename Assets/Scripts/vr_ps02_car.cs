using UnityEngine;

public class vr_ps02_car : MonoBehaviour
{
    public static vr_ps02_car Instance;

    void Awake()
    {
        Instance = this;
    }

    private float Velocidad = 6.5f;
    private bool stopCar = false;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
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
                vr_ps02_timer.Instance.StopTimer();
                vr_ps02_finish.Instance.Activador();
                this.enabled = false;
            }
        }
    }

    public void DetenerCarro(bool detenerCar)
    {
        stopCar = detenerCar;
    }
}
