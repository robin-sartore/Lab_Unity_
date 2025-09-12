using UnityEngine;

public class rollCoiin : MonoBehaviour
{
    [SerializeField]
    public Vector3 CoinRoll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Transform>().Translate(new Vector3(0.1f,0,0));
        GetComponent<Transform>().Rotate(CoinRoll);
    }
}
