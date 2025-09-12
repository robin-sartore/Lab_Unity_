using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    


    public GameObject catPrefab;
    [SerializeField]
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int x =Random.Range(-440, -395);
        int z = Random.Range(370, 420);
        Instantiate(catPrefab, new Vector3(x, 66.061f, z), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
