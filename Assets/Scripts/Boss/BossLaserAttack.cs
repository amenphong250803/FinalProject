using UnityEngine;

public class BossLaserAttack : MonoBehaviour
{
    [Header("Laser Object")]
    public GameObject laserObject;

    [Header("Timing Settings")]
    public float initialDelay = 20f;    // Chờ trước lần đầu
    public float chargeTime = 3f;    // Laser_Charge
    public float beamTime = 2f;        // Laser_Beam
    public float cooldown = 12f;        // Nghỉ giữa các lần bắn

    private Animator anim;

    private float nextActionTime = 0f;
    private enum LaserState { Idle, Charging, Firing, Cooldown }
    private LaserState state = LaserState.Idle;

    void Start()
    {
        anim = laserObject.GetComponent<Animator>();
        laserObject.SetActive(false);

        // Sau initialDelay, laser mới bắt đầu charge
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
        Debug.Log("⚡ Laser charging...");

        // Charge xong thì sang BEAM
        nextActionTime = Time.time + chargeTime;
    }

    void StartBeam()
    {
        state = LaserState.Firing;
        anim.Play("Laser_Beam");
        Debug.Log("🔥 Laser beam active!");

        // Beam xong thì STOP
        nextActionTime = Time.time + beamTime;
    }

    void StopLaser()
    {
        state = LaserState.Cooldown;
        laserObject.SetActive(false);
        Debug.Log("❄ Laser off");

        // Đợi cooldown trước khi beam lại
        nextActionTime = Time.time + cooldown;
    }

    void ResetLaserCycle()
    {
        state = LaserState.Idle;
        Debug.Log("🔁 Laser cooldown done → ready again");
    }
}
