using TMPro;
using UnityEngine;

public class vr_ps01_timer : MonoBehaviour
{
    public static vr_ps01_timer Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] public TMP_Text timer;

    private float tiempo = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        timer.text = string.Format("{00:00}:{1:00}", minutos, segundos);
    }

    public void StopTimer()
    {
        Debug.Log("Tiempo de la prueba: " + timer.text);
        vr_ps_singleton.Instance.SetTimePeriferica(GetTime());
        this.enabled = false;
    }

    public string GetTime()
    {
        return timer.text;
    }
}
