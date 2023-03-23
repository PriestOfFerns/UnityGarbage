using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    public float Sensitivity;



    public static float ClampAngle(float current, float min, float max)
    {
        float dtAngle = Mathf.Abs(((min - max) + 180) % 360 - 180);
        float hdtAngle = dtAngle * 0.5f;
        float midAngle = min + hdtAngle;

        float offset = Mathf.Abs(Mathf.DeltaAngle(current, midAngle)) - hdtAngle;
        if (offset > 0)
            current = Mathf.MoveTowardsAngle(current, midAngle, offset);
        return current;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
   
    void Update()
    {
        transform.position = Player.transform.position;
       

        
        Vector3 Current = transform.rotation.eulerAngles;

        transform.eulerAngles = new Vector3(ClampAngle(Current.x- Input.GetAxis("Mouse Y")* Sensitivity,-90f,90f), Player.transform.eulerAngles.y, Current.z);

       
    }
}
