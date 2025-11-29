using UnityEngine;

public class BossLaserAttack : MonoBehaviour
{
    [Header("Laser Object")]
    public GameObject laserObject;

    [Header("Timing Settings")]
    public float initialDelay = 20f;
    public float chargeTime = 3f;
    public float beamTime = 2f;
    public float cooldown = 12f;

    private Animator anim;

    private float nextActionTime = 0f;
    private enum LaserState { Idle, Charging, Firing, Cooldown }
    private LaserState state = LaserState.Idle;

    void Start()
    {
        anim = laserObject.GetComponent<Animator>();
        laserObject.SetActive(false);

        nextActionTime = Time.time + initialDelay;
    }

    void Update()
    {
        if (Time.time < nextActionTime)
            return;

        switch (state)
        {
            case LaserState.Idle:
                StartCharge();
                break;

            case LaserState.Charging:
                StartBeam();
                break;

            case LaserState.Firing:
                StopLaser();
                break;

            case LaserState.Cooldown:
                ResetLaserCycle();
                break;
        }
    }

    void StartCharge()
    {
        state = LaserState.Charging;
        laserObject.SetActive(true);
        anim.Play("Laser_Charge");

        nextActionTime = Time.time + chargeTime;
    }

    void StartBeam()
    {
        state = LaserState.Firing;
        anim.Play("Laser_Beam");

        nextActionTime = Time.time + beamTime;
    }

    void StopLaser()
    {
        state = LaserState.Cooldown;
        laserObject.SetActive(false);

        nextActionTime = Time.time + cooldown;
    }

    void ResetLaserCycle()
    {
        state = LaserState.Idle;
    }
}
