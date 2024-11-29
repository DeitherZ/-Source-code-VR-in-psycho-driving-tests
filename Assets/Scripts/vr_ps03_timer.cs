using UnityEngine;
using TMPro;

public class vr_ps03_timer : MonoBehaviour
{
    public static vr_ps03_timer Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private TMP_Text timer;

    private float tiempo = 0f;
    private string tiempoText;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        tiempoText = string.Format("{00:00}:{1:00}", minutos, segundos);
        timer.text = tiempoText;
    }

    public void StopTime()
    {
        vr_ps_singleton.Instance.SetTimePrecision(GetTiempo());
        this.enabled = false;
    }

    public string GetTiempo()
    {
        return tiempoText;
    }
}
