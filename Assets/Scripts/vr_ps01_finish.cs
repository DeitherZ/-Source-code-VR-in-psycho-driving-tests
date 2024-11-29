using TMPro;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class vr_ps01_finish : MonoBehaviour
{
    public static vr_ps01_finish Instance;

    void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image finish;
    [SerializeField] private GameObject popUpFinish;
    [SerializeField] private TMP_Text aciertos;
    [SerializeField] private TMP_Text errores;
    [SerializeField] private AudioSource sonidoFinish;

    private Vector3 scalePopUpFinish;


    // Start is called before the first frame update
    void Start()
    {
        scalePopUpFinish = popUpFinish.transform.localScale;
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
            sonidoFinish.Stop();
            SceneManager.LoadScene("vrps02-Reaccion");
        }
        Debug.Log("La ecena tuvo que haber cargado");

    }

    public void Activador()
    {
        finish.gameObject.SetActive(true);
        LeanTween.scale(popUpFinish, scalePopUpFinish, 0.5f);
        sonidoFinish.Play();
        aciertos.text = "<b>Aciertos</b>\n" + vr_ps01_sema.Instance.getAciertos();
        errores.text = "<b>Errores</b>\n" + vr_ps01_sema.Instance.getErrores();
        vr_ps_singleton.Instance.SetDataPeriferica(vr_ps01_sema.Instance.getAciertos(), vr_ps01_sema.Instance.getErrores());
        this.enabled = true;
    }
}
