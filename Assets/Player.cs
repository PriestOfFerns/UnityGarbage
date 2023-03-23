using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    public Rigidbody rb;
    public float Sensitivity;
    public GameObject Cam;
    public float speed;
    
    public float grid;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Cam.GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    float Gridify(float num)
    {


        return num - num % grid;
    }

    // Update is called once per frame

    GameObject BuildingBlock;
    void Update()
    {
        float dt = Time.deltaTime;

        Vector3 forward = transform.forward;
        forward.y = 0f;
        Vector3 right = transform.right;
        right.y = 0f;

        float AirTime = 1;
        

        Vector3 Vel = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal"));
        Vel.Normalize();

        if (rb.velocity.y != 0)
        {
            AirTime = 0.5f;
        }
        Vel *= speed * AirTime;
        Vel.y = rb.velocity.y;
        rb.velocity = Vel;


        Vector3 Current = transform.rotation.eulerAngles;
        
        transform.eulerAngles= new Vector3(Current.x,Current.y+Input.GetAxis("Mouse X")*Sensitivity,Current.z);

        
        RaycastHit HitInfo;
        
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(Cam.transform.position, Cam.transform.forward, out HitInfo, 100.0f) )
        {
            GameObject Block = GameObject.Instantiate(BuildingBlock);
            Block.transform.parent = GameObject.Find("Cubes").transform;

            Block.name = "NewCube";

            Vector3 OgPoint;

            

            if (HitInfo.transform.gameObject.name == "NewCube")
            {
                Vector3 Norm = HitInfo.normal * grid;


                OgPoint = HitInfo.transform.position + Norm;


            } else
            {
                OgPoint = new Vector3(Gridify(HitInfo.point.x), Gridify(HitInfo.point.y + grid), Gridify(HitInfo.point.z));
            }

            Block.transform.position = OgPoint;
        }

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Cam.transform.position, Cam.transform.forward, out HitInfo, 100.0f) && HitInfo.transform.gameObject.name == "NewCube")
        {
            Destroy(HitInfo.transform.gameObject);
        }

            if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y < 0.1 && rb.velocity.y > -0.1)
        {
            rb.AddForce(transform.up * 300);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) ) {
            BuildingBlock = GameObject.Find("Cube");
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BuildingBlock = GameObject.Find("AirshipCube");
        }
    }
}
