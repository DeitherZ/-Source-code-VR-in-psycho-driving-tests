using TMPro;
using UnityEngine;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;

public class vr_ps02_sema : MonoBehaviour
{
    public static vr_ps02_sema Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private AudioSource sonidoIzq;
    [SerializeField] private AudioSource ambulanciason;
    [SerializeField] private GameObject semaVerde;
    [SerializeField] private GameObject semaRojo;
    [SerializeField] private GameObject ambulanciacar;
    [SerializeField] private AudioSource ambson;
    [SerializeField] private TMP_Text scoreAciertos;
    [SerializeField] private TMP_Text scoreErrores;

    private int sema = 0;
    private int aciertos = 0;
    private int errores = 0;
    private int apariciones = 0;
    private float tiempo = 2f;

    // Start is called before the first frame update
    void Start()
    { 
        ambulanciacar.SetActive(false);
        ambulanciason.gameObject.SetActive(false);
        ambson.gameObject.SetActive(false);
        semaRojo.SetActive(false);
        semaVerde.SetActive(false);
        sonidoIzq.gameObject.SetActive(false);
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //A => Luz Roja
        //D => Luz Verde
        if ((UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Trigger) /*|| Input.GetKeyDown(KeyCode.D)*/))
        {
            if (ambulanciacar.activeInHierarchy || semaRojo.activeInHierarchy || ambulanciason.gameObject.activeInHierarchy)
            {
                CancelInvokeControl();
                ambulanciacar.SetActive(false);
                ambulanciason.gameObject.SetActive(false);
                ambson.gameObject.SetActive(false);
                semaRojo.SetActive(false);
                aciertos++;
                scoreAciertos.text = "Aciertos: " + aciertos;
            }else if (semaVerde.activeInHierarchy || sonidoIzq.gameObject.activeInHierarchy)
            {
                CancelInvokeControl();
                semaVerde.SetActive(false);
                sonidoIzq.gameObject.SetActive(false);
                vr_ps02_destello.Instance.Destello();
                errores++;
                scoreErrores.text = "Errores: " + errores;
            }
        }

        if ((UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Trigger) /*|| Input.GetKeyDown(KeyCode.A)*/))
        {
            if (semaVerde.activeInHierarchy || sonidoIzq.gameObject.activeInHierarchy)
            {
                CancelInvokeControl();
                semaVerde.SetActive(false);
                sonidoIzq.gameObject.SetActive(false);
                aciertos++;
                scoreAciertos.text = "Aciertos: " + aciertos;
            }
            else if (ambulanciacar.activeInHierarchy || semaRojo.activeInHierarchy || ambulanciason.gameObject.activeInHierarchy)
            {
                CancelInvokeControl();
                ambulanciacar.SetActive(false);
                ambulanciason.gameObject.SetActive(false);
                ambson.gameObject.SetActive(false);
                semaRojo.SetActive(false);
                vr_ps02_destello.Instance.Destello();
                errores++;
                scoreErrores.text = "Errores: " + errores;
            }
        }
        /*if ((UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Trigger) && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Trigger)) || (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)))
        {
            if (!obja.activeInHierarchy && !objn.activeInHierarchy && !sonn.isPlaying && !sona.isPlaying)
            {
                classDestello.Destello();
                offtime++;
            }
            else if (sona.isPlaying)
            {
                sona.Stop();
                aciertos++;
                scoreAciertos.text = "Aciertos: " + aciertos;
            }
            else if (objn.activeInHierarchy || sonn.isPlaying)
            {
                CancelInvokeControl(objn);
                sona.Stop();
                sonn.Stop();
                classDestello.Destello();
                errores++;
                scoreErrores.text = "Errores: " + errores;
            }
        }
        /*if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Button1))
        {
            if (!semaRojo.activeInHierarchy && !semaVerde.activeInHierarchy && !sonidoIzq.isPlaying && !sonidoDer.isPlaying)
            {
                classDestello.Destello();
                offtime++;
            }
            else if (sonidoIzq.isPlaying)
            {
                aciertos++;
                scoreAciertos.text = "Aciertos: " + aciertos;
            }
            else if (semaRojo.activeInHierarchy || semaVerde.activeInHierarchy || sonidoDer.isPlaying)
            {
                classDestello.Destello();
                errores++;
                scoreErrores.text = "Errores: " + errores;
            }
        }
        if (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Button1))
        {
            if (!semaRojo.activeInHierarchy && !semaVerde.activeInHierarchy && !sonidoIzq.isPlaying && !sonidoDer.isPlaying)
            {
                classDestello.Destello();
                offtime++;
            }
            else if (sonidoDer.isPlaying)
            {
                aciertos++;
                scoreAciertos.text = "Aciertos: " + aciertos;
            }
            else if (semaRojo.activeInHierarchy || semaVerde.activeInHierarchy || sonidoIzq.isPlaying)
            {
                classDestello.Destello();
                errores++;
                scoreErrores.text = "Errores: " + errores;
            }
        }*/
    }

    void CambiarVisibilidad()
    {
        //0 => Luz Verde
        //1 => Luz Roja
        if (!ambulanciacar.activeInHierarchy && !semaRojo.activeInHierarchy && !semaVerde.activeInHierarchy 
            && !sonidoIzq.gameObject.activeInHierarchy && !ambulanciason.gameObject.activeInHierarchy)
        {
            apariciones++;
            if (tiempo > 0.5f)
            {
                tiempo -= 0.05f;
            }
        }
        if (apariciones > 40)
        {
            this.enabled = false;
            ambulanciacar.SetActive(false);
            ambulanciason.gameObject.SetActive(false);
            ambson.gameObject.SetActive(false);
            semaRojo.SetActive(false);
            semaVerde.SetActive(false);
            sonidoIzq.gameObject.SetActive(false);
            CancelInvoke("CambiarVisibilidad");
            vr_ps02_car.Instance.DetenerCarro(true);
            vr_ps_menuExit.Instance.enabled = false;
            return;
        }
        sema = Random.Range(1, 6);

        if(ambulanciacar.activeInHierarchy || sonidoIzq.gameObject.activeInHierarchy || semaRojo.activeInHierarchy 
            || ambulanciason.gameObject.activeInHierarchy || semaVerde.activeInHierarchy)
        {
            errores++;
            scoreErrores.text = "Errores: " + errores;
            vr_ps02_destello.Instance.Destello();
        }

        switch (sema)
        {
            case 1:
                if (ambulanciacar.activeInHierarchy || sonidoIzq.gameObject.activeInHierarchy || semaRojo.activeInHierarchy 
                    || ambulanciason.gameObject.activeInHierarchy || semaVerde.activeInHierarchy)
                {
                    ambulanciacar.SetActive(false);
                    ambulanciason.gameObject.SetActive(false);
                    ambson.gameObject.SetActive(false);
                    semaRojo.SetActive(false);
                    semaVerde.SetActive(false);
                    sonidoIzq.gameObject.SetActive(false);
                }
                else if (!ambulanciacar.activeInHierarchy && !semaRojo.activeInHierarchy && !semaVerde.activeInHierarchy 
                    && !sonidoIzq.gameObject.activeInHierarchy && !ambulanciason.gameObject.activeInHierarchy)
                {
                    ambulanciacar.SetActive(!ambulanciacar.activeInHierarchy);
                    PositionA(ambulanciacar);
                    ambson.gameObject.SetActive(true);
                    ambson.Play();
                    Debug.Log("Caso 1");
                }
                break;

            case 2:
                if (ambulanciacar.activeInHierarchy || sonidoIzq.gameObject.activeInHierarchy || semaRojo.activeInHierarchy 
                    || ambulanciason.gameObject.activeInHierarchy || semaVerde.activeInHierarchy)
                {
                    ambulanciacar.SetActive(false);
                    ambulanciason.gameObject.SetActive(false);
                    ambson.gameObject.SetActive(false);
                    semaRojo.SetActive(false);
                    semaVerde.SetActive(false);
                    sonidoIzq.gameObject.SetActive(false);
                }
                else if (!ambulanciacar.activeInHierarchy && !semaRojo.activeInHierarchy && !semaVerde.activeInHierarchy 
                    && !sonidoIzq.gameObject.activeInHierarchy && !ambulanciason.gameObject.activeInHierarchy)
                {
                    semaRojo.SetActive(!semaRojo.activeInHierarchy);
                    Position(semaRojo);
                    Debug.Log("Caso 2");
                }
                break;

            case 3:
                if (ambulanciacar.activeInHierarchy || sonidoIzq.gameObject.activeInHierarchy || semaRojo.activeInHierarchy 
                    || ambulanciason.gameObject.activeInHierarchy || semaVerde.activeInHierarchy)
                {
                    ambulanciacar.SetActive(false);
                    ambulanciason.gameObject.SetActive(false);
                    ambson.gameObject.SetActive(false);
                    semaRojo.SetActive(false);
                    semaVerde.SetActive(false);
                    sonidoIzq.gameObject.SetActive(false);
                }
                else if (!ambulanciacar.activeInHierarchy && !semaRojo.activeInHierarchy && !semaVerde.activeInHierarchy 
                    && !sonidoIzq.gameObject.activeInHierarchy && !ambulanciason.gameObject.activeInHierarchy)
                {
                    semaVerde.SetActive(!semaVerde.activeInHierarchy);
                    Position(semaVerde);
                    Debug.Log("Caso 3");
                }
                break;

            case 4:
                if (ambulanciacar.activeInHierarchy || sonidoIzq.gameObject.activeInHierarchy || semaRojo.activeInHierarchy 
                    || ambulanciason.gameObject.activeInHierarchy || semaVerde.activeInHierarchy)
                {
                    ambulanciacar.SetActive(false);
                    ambulanciason.gameObject.SetActive(false);
                    ambson.gameObject.SetActive(false);
                    semaRojo.SetActive(false);
                    semaVerde.SetActive(false);
                    sonidoIzq.gameObject.SetActive(false);
                }
                else if (!ambulanciacar.activeInHierarchy && !semaRojo.activeInHierarchy && !semaVerde.activeInHierarchy 
                    && !sonidoIzq.gameObject.activeInHierarchy && !ambulanciason.gameObject.activeInHierarchy)
                {
                    sonidoIzq.gameObject.SetActive(!sonidoIzq.gameObject.activeInHierarchy);
                    sonidoIzq.Play();
                    Debug.Log("Caso 4");
                }
                break;
            case 5:
                if (ambulanciacar.activeInHierarchy || sonidoIzq.gameObject.activeInHierarchy || semaRojo.activeInHierarchy 
                    || ambulanciason.gameObject.activeInHierarchy || semaVerde.activeInHierarchy)
                {
                    ambulanciacar.SetActive(false);
                    ambulanciason.gameObject.SetActive(false);
                    ambson.gameObject.SetActive(false);
                    semaRojo.SetActive(false);
                    semaVerde.SetActive(false);
                    sonidoIzq.gameObject.SetActive(false);
                }
                else if (!ambulanciacar.activeInHierarchy && !semaRojo.activeInHierarchy && !semaVerde.activeInHierarchy && 
                    !sonidoIzq.gameObject.activeInHierarchy && !ambulanciason.gameObject.activeInHierarchy)
                {
                    ambulanciason.gameObject.SetActive(!ambulanciason.gameObject.activeInHierarchy);
                    ambulanciason.Play();
                    Debug.Log("Caso 5");
                }
                break;
        }
    }

    public void Position(GameObject semaforo)
    {
        Vector3 nuevaPosicion = new Vector3(semaforo.transform.position.x, Random.Range(0.1f, 1f), Random.Range(0f, -4f));
        semaforo.transform.position = nuevaPosicion;
    }

    public void PositionA(GameObject semaforo)
    {
        Vector3 nuevaPosicion = new Vector3(semaforo.transform.position.x, semaforo.transform.position.y, Random.Range(0f, -4f));
        semaforo.transform.position = nuevaPosicion;
    }

    void InvokeConfigStart()
    {
        vr_ps02_index.Instance.DesactivarPanel();
        ambulanciacar.SetActive(false);
        ambulanciason.gameObject.SetActive(false);
        ambson.gameObject.SetActive(false);
        semaRojo.SetActive(false);
        semaVerde.SetActive(false);
        sonidoIzq.gameObject.SetActive(false);
        InvokeRepeating("CambiarVisibilidad", tiempo, tiempo + 1);
    }

    void CancelInvokeControl()
    {
        CancelInvoke("CambiarVisibilidad");
        this.enabled = true;
        InvokeRepeating("CambiarVisibilidad", tiempo, tiempo + 1f);
    }

    public void Iniciador()
    {
        scoreAciertos.text = "Aciertos: " + aciertos;
        scoreErrores.text = "Errores: " + errores;
        this.enabled = true;
        vr_ps_menuExit.Instance.enabled = true;
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
}