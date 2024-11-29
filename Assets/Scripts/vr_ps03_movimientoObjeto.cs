using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;
using UnityEngine;

public class vr_ps03_movimientoObjeto : MonoBehaviour
{
    public static vr_ps03_movimientoObjeto Instance;

    void Awake()
    {
        Instance = this;
    }

    private float velocidad = 0.4f;
    private Vector3 posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Right, UxrInputButtons.JoystickUp))
        {
            //Debug.Log("Moviendo hacia el frente");
            transform.Translate(Vector3.forward * Time.deltaTime * velocidad, Space.World);
        }
        if (/*Input.GetKey(KeyCode.S) ||*/ UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Right, UxrInputButtons.JoystickDown))
        {
            //Debug.Log("Moviendo hacia atrás");
            transform.Translate(Vector3.back * Time.deltaTime * velocidad, Space.World);
        }
        if (/*Input.GetKey(KeyCode.A) ||*/ UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Left, UxrInputButtons.JoystickLeft))
        {
            //Debug.Log("Moviendo hacia la izquierda");
            transform.Translate(Vector3.left * Time.deltaTime * velocidad, Space.World);
        }
        if (/*Input.GetKey(KeyCode.D) ||*/ UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Left, UxrInputButtons.JoystickRight))
        {
            //Debug.Log("Moviendo hacia la derecha");
            transform.Translate(Vector3.right * Time.deltaTime * velocidad, Space.World);
        }
    }

    public void RegresoInicio()
    {
        transform.position = posicionInicial;
    }

    public void ActualizarPosicion()
    {
        posicionInicial = transform.position;
    }
}
