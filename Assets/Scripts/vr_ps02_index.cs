using UltimateXR.Avatar;
using UltimateXR.CameraUtils;
using UltimateXR.Core;
using UltimateXR.Devices;
using UnityEngine;
using UnityEngine.UI;

public class vr_ps02_index : MonoBehaviour
{
    public static vr_ps02_index Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image index;
    [SerializeField] private GameObject popUpIndicaciones;
    [SerializeField] private AudioSource audioIndicacion;

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
        if ((((UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Left, UxrInputButtons.Trigger)) ||
            (UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Trigger)) ||
            (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Trigger)))
            || Input.GetKeyDown(KeyCode.Return)) && !fade.IsFading)
        {
            audioIndicacion.Stop();
            LeanTween.scale(popUpIndicaciones, new Vector3(0f, 0f, 0f), 0.3f);
            Invoke("Iniciador", 0.3f);
        }
    }

    public void DesactivarPanel()
    {
        index.gameObject.SetActive(false);
    }

    void Iniciador()
    {
        index.color = new Color(0f, 0f, 0f, 0f);
        vr_ps02_car.Instance.enabled = true;
        vr_ps02_cuentaAtras.Instance.Iniciador();
        this.enabled = false;
    }

    private void Indicaciones()
    {
        audioIndicacion.Play();
    }
}
