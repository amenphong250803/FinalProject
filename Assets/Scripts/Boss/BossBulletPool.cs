using UnityEngine;

public class BossBulletPool : MonoBehaviour
{
    public static BossBulletPool intan;

    [Header("Đặt sẵn danh sách bullet vào đây")]
    public GameObject[] bullets;
    private void Awake()
    {
        intan = this;
    }

    public GameObject GetBullet()
    {
        foreach (GameObject b in bullets)
        {
            if (!b.activeInHierarchy)
                return b;
        }

        Debug.LogWarning("⚠ Hết bullet trong pool! Tăng số lượng trong mảng bullets!");
        return null;
    }
}
