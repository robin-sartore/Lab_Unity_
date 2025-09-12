using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject atomPrefab;
    [SerializeField]
    Vector3 initPosAtom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(atomPrefab, initPosAtom, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
