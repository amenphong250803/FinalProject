using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject bulletPrefab;
    public int poolSize = 30;

    private GameObject[] pool;

    private void Awake()
    {
        Instance = this;
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(bulletPrefab);
            pool[i].SetActive(false);
        }
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
                return pool[i];
        }

        // nếu hết bullet → tạo thêm
        GameObject extra = Instantiate(bulletPrefab);
        extra.SetActive(false);
        return extra;
    }
}
