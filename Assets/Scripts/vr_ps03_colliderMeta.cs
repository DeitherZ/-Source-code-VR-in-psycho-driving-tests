using UnityEngine;

public class vr_ps03_colliderMeta : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ColliderValidator")
        {
            vr_ps03_movimientoObjeto.Instance.enabled = false;
            vr_ps03_timer.Instance.StopTime();
            vr_ps_singleton.Instance.SetDataPrecision(vr_ps03_collider.Instance.GetErrores());
            vr_ps03_finish.Instance.Activador();
            vr_ps_menuExit.Instance.enabled = false;
        }
    }
}
