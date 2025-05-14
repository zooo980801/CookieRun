using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class ObjectPool<T> where T : MonoBehaviour
{
    private Stack<T> pool;
    private GameObject prefab;
    public ObjectPool(GameObject prefab, int initialize)
    {
        this.prefab = prefab;
        pool = new Stack<T>();
        Initialize(initialize);
    }

    private void Initialize(int size)
    {
        for (int i = 0; i < size; i++)
        {
            T obj = CreateNewObject();
            pool.Push(obj);
        }
    }

    public T CreateNewObject()
    {
        GameObject obj = GameObject.Instantiate(prefab);
        obj.SetActive(false);
        return obj.GetComponent<T>();
    }

    public T GetObject()
    {
        while (pool.Count > 0)
        {
            T obj = pool.Pop();

            if (obj != null)
            {
                obj.gameObject.SetActive(true);
                Debug.Log($"{obj.name} 활성화 setActive true");
                return obj;
            }
            else
            {
                Debug.LogWarning("풀에서 꺼낸 객체가 null입니다. 새 객체를 생성합니다.");
            }
        }

        return CreateNewObject();
    }
    public void ReturnObject(T obj)
    {
        if (obj != null)
        {
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }

}