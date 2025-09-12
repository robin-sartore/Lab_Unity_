using UnityEngine;

public class newposition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GetComponent<Transform>().position = new Vector3(5,5,5);
        //GetComponent<Transform>().rotation = Quaternion.Euler(90f,90f,90f);


    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Transform>().Translate(new Vector3(0.1f,0,0));
        GetComponent<Transform>().Rotate(new Vector3(0, 1, 0));

    }
}
