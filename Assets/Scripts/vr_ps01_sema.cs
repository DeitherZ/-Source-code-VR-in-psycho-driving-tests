using TMPro;
using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;

public class vr_ps01_sema : MonoBehaviour
{
    public static vr_ps01_sema Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject sema;
    [SerializeField] private TMP_Text scoreAciertos;
    [SerializeField] private TMP_Text scoreErrores;

    private int lado = 0;
    private int aciertos = 0;
    private int errores = 0;
    private int apariciones = 0;
    private float tiempo = 2f;
    private Vector3 scaleGirlFBX;

    // Start is called before the first frame update
    void Start()
    {
        //sema.transform.LeanScale(new Vector3(0f, 0f, 0f), 0f);
        //scaleGirlFBX = sema.transform.localScale;
        sema.SetActive(false);
        this.enabled = false;
    }

    // Update is called once per frame
    void Update(){
        if(UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Trigger) /*|| Input.GetKeyDown(KeyCode.A)*/)
        {
            if (!sema.activeInHierarchy)
            {
                vr_ps01_destello.Instance.Destello();
            }
            else if (lado == 1)
            {
                aciertos++;
                CancelInvokeControl();
            }
            else
            {
                errores++;
                CancelInvokeControl();
                vr_ps01_destello.Instance.Destello();
            }
        }

        if(UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Trigger) /*|| Input.GetKeyDown(KeyCode.D)*/)
        {
            if (!sema.activeInHierarchy)            
            {
                vr_ps01_destello.Instance.Destello();
            }
            else if (lado == 2)
            {
                aciertos++;
                CancelInvokeControl();
            }
            else
            {
                errores++;
                CancelInvokeControl();
                vr_ps01_destello.Instance.Destello();
            }
        }
    }

    void CambiarVisibilidad()
    {
        //1 => Izquierda
        //2 => Derecha
        if (!sema.activeInHierarchy)
        {
            apariciones++;
            if (tiempo > 0.5f)
            {
                tiempo -= 0.05f;
            }
            vr_ps01_movNina.Instance.UpdateVelocidad();
        }
        if (apariciones > 40)
        {
            this.enabled = false;
            sema.SetActive(false);
            CancelInvoke("CambiarVisibilidad");
            vr_ps01_car.Instance.DetenerCarro(true);
            vr_ps_menuExit.Instance.enabled = false;
            return;
        }
        else
        {
            lado = Random.Range(1, 3);
            if (lado == 1)
            {
                sema.SetActive(!sema.activeInHierarchy);
                positionRotation(-180f, 3.5f);
            }
            else
            {
                sema.SetActive(!sema.activeInHierarchy);
                positionRotation(0f, -6.5f);
            }
        }
    }

    void positionRotation(float rotationY, float positionZ)
    {
        Vector3 nuevaPosicion = new Vector3(sema.transform.position.x, sema.transform.position.y, positionZ);
        var angles = sema.transform.rotation.eulerAngles;
        angles.y = rotationY;
        sema.transform.rotation = Quaternion.Euler(angles);
        sema.transform.position = nuevaPosicion;
    }

    void InvokeConfigStart()
    {
        RefreshScore();
        Vector3 positionSema = sema.transform.position;
        //sema.transform.LeanS  cale(scaleGirlFBX, 0f);
        //sema.transform.position = new Vector3(positionSema.x - 1.53f, positionSema.y, positionSema.z);
        this.enabled = true;
        Invoke("CambiarVisibilidad", tiempo);
    }

    public void CancelInvokeControl()
    {
        RefreshScore();
        CancelInvoke("CambiarVisibilidad");
        sema.SetActive(false);
        Invoke("CambiarVisibilidad", tiempo);
    }

    public void Iniciador()
    {
        InvokeConfigStart();
    }

    public int getAciertos()
    {
        return aciertos;
    }

    public int getErrores()
    {
        return errores;
    }

    public void CountErrExtern()
    {
        errores += 1;
    }

    private void RefreshScore()
    {
        scoreAciertos.text = "Aciertos: " + aciertos;
        scoreErrores.text = "Errores: " + errores;
    }
}
