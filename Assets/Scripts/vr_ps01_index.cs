using UltimateXR.Avatar;
using UltimateXR.CameraUtils;
using UltimateXR.Core;
using UltimateXR.Devices;
using UnityEngine;
using UnityEngine.UI;

public class vr_ps01_index : MonoBehaviour
{
    [SerializeField] private Image index;
    [SerializeField] private GameObject popUpIndicaciones;
    [SerializeField] private AudioSource sonidoIndicacion;

    private UxrCameraFade fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindAnyObjectByType<UxrCameraFade>();
        index.gameObject.SetActive(true);
        Invoke("Indicaciones", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if ((((UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Left, UxrInputButtons.Trigger)) ||
            (UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Trigger)) ||
            (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Trigger)))
            || Input.GetKeyDown(KeyCode.Return)) && !fade.IsFading)
        {
            sonidoIndicacion.Stop();
            LeanTween.scale(popUpIndicaciones, new Vector3(0f, 0f, 0f), 0.2f);
            Invoke("IniciarPrueba", 0.2f);
        }
    }


    void IniciarPrueba()
    {
        index.gameObject.SetActive(false);
        vr_ps01_car.Instance.enabled = true;
        vr_ps01_cuentaAtras.Instance.Iniciador();
        this.enabled = false;
    }

    void Indicaciones()
    {
        sonidoIndicacion.Play();
    }
}
