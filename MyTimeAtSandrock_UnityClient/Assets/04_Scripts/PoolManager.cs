using UnityEngine;

public class PoolManager
{
    public GameObject GetPoolable(string path)
    {
        var res = Resources.Load<GameObject>(path);
        var obj = MonoBehaviour.Instantiate(res);
        return obj;
    }
}