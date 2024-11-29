using TMPro;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class vr_ps02_finish : MonoBehaviour
{
    public static vr_ps02_finish Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image finish;
    [SerializeField] private GameObject popUpFinish;
    [SerializeField] private TMP_Text aciertos;
    [SerializeField] private TMP_Text errores;
    [SerializeField] private AudioSource audioFinish;

    // Start is called before the first frame update
    void Start()
    {
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
            SceneManager.LoadScene("vrps03-Precision");
        }

    }

    public void Activador()
    {
        finish.gameObject.SetActive(true);
        aciertos.text = "Aciertos\n" + vr_ps02_sema.Instance.getAciertos();
        errores.text = "Errores\n" + vr_ps02_sema.Instance.getErrores();
        LeanTween.scale(popUpFinish, new Vector3(1f, 1f, 1f), 0.3f);
        audioFinish.Play();
        vr_ps_singleton.Instance.SetDataReaccion(vr_ps02_sema.Instance.getAciertos(), vr_ps02_sema.Instance.getErrores());
        this.enabled = true;
    }
}
