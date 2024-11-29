using UltimateXR.Avatar;
using UltimateXR.CameraUtils;
using UltimateXR.Core;
using UltimateXR.Devices;
using UnityEngine;
using UnityEngine.UI;

public class vr_ps03_index : MonoBehaviour
{
    [SerializeField] private AudioSource indicaciones;
    [SerializeField] private Image panelIndicaciones;
    [SerializeField] private GameObject popUpIndicaciones;

    private UxrCameraFade fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindAnyObjectByType<UxrCameraFade>();
        panelIndicaciones.gameObject.SetActive(true);
        Invoke("Indicaciones", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if ((((UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Left, UxrInputButtons.Trigger)) ||
            (UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Trigger)) ||
            (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Trigger)))
            || Input.GetKeyDown(KeyCode.Return)) && !fade.IsFading)
        {
            indicaciones.Stop();
            LeanTween.scale(popUpIndicaciones, new Vector3(0f, 0f, 0f), 0.5f);
            Invoke("IniciarPrueba", 0.6f);
        }
    }

    private void IniciarPrueba()
    {
        panelIndicaciones.gameObject.SetActive(false);
        vr_ps03_cuentaAtras.Instance.Iniciador();
        this.enabled = false;
    }

    private void Indicaciones()
    {
        indicaciones.Play();
    }
}
