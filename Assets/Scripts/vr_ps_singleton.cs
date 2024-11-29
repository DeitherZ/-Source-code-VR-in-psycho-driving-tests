using UnityEngine;

public class vr_ps_singleton : MonoBehaviour
{
    public static vr_ps_singleton Instance;

    [SerializeField] private int aciertosVrPs01;
    [SerializeField] private int erroresVrPs01;
    [SerializeField] private string timeVrPs01;
    [SerializeField] private int aciertosVrPs02;
    [SerializeField] private int erroresVrPs02;
    [SerializeField] private string timeVrPs02;
    [SerializeField] private int erroresVrPs03;
    [SerializeField] private string timeVrPs03;

    void Awake()
    {
        if (vr_ps_singleton.Instance == null)
        {
            vr_ps_singleton.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDataPeriferica(int acierto, int errores)
    {
        aciertosVrPs01 = acierto;
        erroresVrPs01 = errores;
    }

    public void SetTimePeriferica(string time)
    {
        timeVrPs01 = time;
    }

    public void SetDataReaccion(int acierto, int errores)
    {
        aciertosVrPs02 = acierto;
        erroresVrPs02 = errores;
    }

    public void SetTimeReaccion(string time)
    {
        timeVrPs02 = time;
    }

    public void SetDataPrecision(int errores)
    {
        erroresVrPs03 = errores;
    }

    public void SetTimePrecision(string time)
    {
        timeVrPs03 = time;
    }

    public int GetPerifericaAcierto()
    {
        return aciertosVrPs01;
    }

    public int GetPerifericaErrores()
    {
        return erroresVrPs01;
    }

    public string GetPerifericaTime()
    {
        return timeVrPs01;
    }

    public int GetReaccionAcierto()
    {
        return aciertosVrPs02;
    }

    public int GetReaccionErrores()
    {
        return erroresVrPs02;
    }

    public string GetReaccionTime()
    {
        return timeVrPs02;
    }

    public int GetPrecisionErrores()
    {
        return erroresVrPs03;
    }

    public string GetPrecisionTime()
    {
        return timeVrPs03;
    }

    public void ResetData()
    {
        aciertosVrPs01 = 0;
        erroresVrPs01 = 0;
        timeVrPs01 = "";
        aciertosVrPs02 = 0;
        erroresVrPs02 = 0;
        timeVrPs02 = "";
        erroresVrPs03 = 0;
        timeVrPs03 = "" ;
    }

}
