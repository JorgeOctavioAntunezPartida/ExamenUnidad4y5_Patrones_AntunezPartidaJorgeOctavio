using UnityEngine;

public class TurretEffects : MonoBehaviour
{
    private GameManager _gm;
    private AudioSource audioShoot;

    private void Awake()
    {
        audioShoot = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _gm = GameManager.Instance;
    }

    void Update()
    {
        bool isWalking = _gm.moveInput != Vector2.zero ? true : false;
        _gm.anim.SetBool("isWalking", isWalking);
    }

    #region Eventos Susbcritos
    private void OnEnable()
    {
        BulletPool.OnSuccessfulShot += Effects;
    }

    private void OnDisable()
    {
        BulletPool.OnSuccessfulShot -= Effects;
    }

    private void Effects(bool result)
    {
        if (result)
        {
            audioShoot.Play();
        }
    }
    #endregion
}