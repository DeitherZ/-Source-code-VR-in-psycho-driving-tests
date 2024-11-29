using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;

public class vr_ps03_finish : MonoBehaviour
{
    public static vr_ps03_finish Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image panelFinish;
    [SerializeField] private TMP_Text resultErrores;
    [SerializeField] private TMP_Text resultTimer;
    [SerializeField] private AudioSource audioFinish;
    [SerializeField] private GameObject popUpFinish;
    
    private Vector3 scaleInicial;

    // Start is called before the first frame update
    void Start()
    {
        scaleInicial = popUpFinish.transform.localScale;
        LeanTween.scale(popUpFinish, new Vector3(0f, 0f, 0f), 0f);
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (((UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Left, UxrInputButtons.Trigger)) ||
            (UxrAvatar.LocalAvatarInput.GetButtonsPress(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Trigger)) ||
            (UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Trigger)
            && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Trigger)))
            || Input.GetKeyDown(KeyCode.Return))
        {
            vr_ps_GenerarPDF.Instance.GenerarPDF();
        }
    }

    public void Activador()
    {
        this.enabled = true;
        panelFinish.gameObject.SetActive(true);
        LeanTween.scale(popUpFinish, scaleInicial, 0.5f);
        resultErrores.text = "Errores\n" + vr_ps03_collider.Instance.GetErrores();
        resultTimer.text = "Tiempo\n" + vr_ps03_timer.Instance.GetTiempo();
        audioFinish.Play();
        vr_ps_singleton.Instance.SetDataPrecision(vr_ps03_collider.Instance.GetErrores());
    }
}
