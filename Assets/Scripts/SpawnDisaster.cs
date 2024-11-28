using UnityEngine;

public class SpawnDisaster : MonoBehaviour
{
    public Transform target;
    public GameObject prefab;
    public GameObject prefab2;

    private int index;
    private GameObject current;

    public void PressDisaster1()
    {
        if (index == 1)
        {
            index = 0;

            if (current != null)
            {
                Destroy(current);
            }
        }
        else
        {
            index = 1;

            if (current != null)
            {
                Destroy(current);
            }

            current = Instantiate(prefab);
            current.transform.parent = target;
            current.transform.position = target.position;
        }
    }

    public void PressDisaster2()
    {
        if (index == 2)
        {
            index = 0;

            if (current != null)
            {
                Destroy(current);
            }
        }
        else
        {
            index = 2;

            if (current != null)
            {
                Destroy(current);
            }

            current = Instantiate(prefab2);
            current.transform.parent = target;
            current.transform.position = target.position;
        }
    }
}
