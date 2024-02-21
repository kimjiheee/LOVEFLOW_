using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public string _joyStickNum;
    public Player1 _PPlayer;

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject targetGround;
    [SerializeField]
    GameObject[] targetWallList;
    [SerializeField]
    GameObject fountain;
    [SerializeField]
    GameObject fountain_transparent;
    [SerializeField]
    float radius = 8.0f;
    [SerializeField]
    float camHeight = 8.0f;

    [SerializeField]
    GameObject _startPipe1;
    [SerializeField]
    GameObject _startPipe2;
    [SerializeField]
    GameObject _startPipe3;
    [SerializeField]
    GameObject _startPipe4;

    public Transform _CameraTrans;
    public Transform _PrevCameraTrans;

    Camera currentCamera;

    private float angle = 0.0f;

    private bool zoomINOUT = false;
    private bool installMode = false;

    private bool isFKeyPushed = false;
    private bool isRKeyPushed = false;


    // Start is called before the first frame update
    void Start()
    {
        _PPlayer = player.GetComponent<Player1>();
        currentCamera = GetComponent<Camera>();
        _joyStickNum = "Player1";
        Debug.Log(currentCamera.transform.position);
        Debug.Log(player.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        if (_PPlayer._mode == Player1.GameMode.INSTALLMODE) installMode = true;
        else installMode = false;
        // Change culling mask layer of camera component when player is on second floor.
        // When player is on first floor
        if (player.transform.position.y < -1.5f)
        {
            // Remove "SecondFloor" Layer
            currentCamera.cullingMask = currentCamera.cullingMask & ~(1 << LayerMask.NameToLayer("SecondFloor"));
            // Add "SecondFloorTransparent" Layer
            currentCamera.cullingMask |= 1 << LayerMask.NameToLayer("SecondFloorTransparent");
        }
        else // When player is on second floor
        {
            // Add "SecondFloor" Layer
            currentCamera.cullingMask |= 1 << LayerMask.NameToLayer("SecondFloor");
            // Remove "SecondFloorTransparent" Layer
            currentCamera.cullingMask = currentCamera.cullingMask & ~(1 << LayerMask.NameToLayer("SecondFloorTransparent"));
        }

        // ZoomOut mode
        if (Input.GetAxis(_joyStickNum + "_LeftTrigger") == 1 && installMode == false || Input.GetKey(KeyCode.R))
        {
            isRKeyPushed = true;
        }
        else
        {
            isRKeyPushed = false;
        }
        if (isRKeyPushed)
        {
            for (int i = 0; i < targetWallList.Length; ++i)
            {
                if (targetWallList[i].activeSelf == false)
                    targetWallList[i].SetActive(true);
            }
            RaycastHit hit;
            if (Physics.Raycast(currentCamera.transform.position, targetGround.transform.position - currentCamera.transform.position, out hit, 100))
            {
                if (hit.collider.gameObject.name == "FrontWall_P1" ||
                    hit.collider.gameObject.name == "FrontWall_P2" ||
                    hit.collider.gameObject.name == "TransparentWall_Front")
                {
                    targetWallList[0].SetActive(false);
                    transform.position = new Vector3(0, 24, 22.2f);
                    transform.rotation = Quaternion.Euler(135, 0, 180);
                }
                else if (hit.collider.gameObject.name == "LeftWall_P1" ||
                    hit.collider.gameObject.name == "LeftWall_P2" ||
                    hit.collider.gameObject.name == "TransparentWall_Left")
                {
                    targetWallList[1].SetActive(false);
                    transform.position = new Vector3(-22.2f, 24, 0);
                    transform.rotation = Quaternion.Euler(45, 90, 0);
                }
                else if (hit.collider.gameObject.name == "BackWall_P1" ||
                    hit.collider.gameObject.name == "BackWall_P2" ||
                    hit.collider.gameObject.name == "TransparentWall_Back")
                {
                    targetWallList[2].SetActive(false);
                    transform.position = new Vector3(0, 24, -22.2f);
                    transform.rotation = Quaternion.Euler(45, 0, 0);
                }
                else if (hit.collider.gameObject.name == "RightWall_P1" ||
                    hit.collider.gameObject.name == "RightWall_P2" ||
                    hit.collider.gameObject.name == "TransparentWall_Right")
                {
                    targetWallList[3].SetActive(false);
                    transform.position = new Vector3(22.2f, 24, 0);
                    transform.rotation = Quaternion.Euler(135, 90, 180);

                }
            }
            Debug.DrawRay(currentCamera.transform.position, (targetGround.transform.position - currentCamera.transform.position) * 100, Color.red);
            //Debug.Log(hit.collider.gameObject.name);
            fountain.SetActive(false);
            fountain_transparent.SetActive(true);
            return;
        }
        else
        {
            fountain.SetActive(true);
            fountain_transparent.SetActive(false);
        }

        // Camera rotation
        if (Input.GetAxis(_joyStickNum + "_RightHorizontal") < -0.05 || Input.GetKey(KeyCode.Q))
        {
            angle += 40.0f * Time.deltaTime;
        }
        if (Input.GetAxis(_joyStickNum + "_RightHorizontal") > 0.05 || Input.GetKey(KeyCode.E))
        {
            angle -= 40.0f * Time.deltaTime;
        }

        // Control Camera Height
        //if (Input.GetKey(KeyCode.X))
        //{
        //    camHeight -= 10.0f * Time.deltaTime;
        //    if(camHeight < 0)
        //    {
        //        camHeight = 0.0f;
        //    }
        //}
        //if (Input.GetKey(KeyCode.C))
        //{
        //    camHeight += 10.0f * Time.deltaTime;
        //    if (camHeight > 30.0f)
        //    {
        //        camHeight = 30.0f;
        //    }
        //}

        Vector3 targetPos = Vector3.zero;
        if (installMode == false)
        {
            fountain.SetActive(true);

            _startPipe1.SetActive(true);
            _startPipe2.SetActive(true);
            _startPipe3.SetActive(true);
            _startPipe4.SetActive(true);

            //currentCamera.transform.position = _PrevCameraTrans.transform.position;
            //currentCamera.transform.rotation = _PrevCameraTrans.transform.rotation;


            float radian = Mathf.Deg2Rad * angle;
            transform.position = new Vector3(radius * Mathf.Cos(radian) + player.transform.position.x,
                camHeight + player.transform.position.y, radius * Mathf.Sin(radian) + player.transform.position.z);
            transform.LookAt(player.transform.position);

            //float radian = Mathf.Deg2Rad * angle;
            //transform.position = new Vector3(radius + player.transform.position.x,
            //    camHeight + player.transform.position.y, radius  + player.transform.position.z);
            //transform.LookAt(player.transform.position);
        }
        else
        {
            //_startPipe1.SetActive(false);
            //_startPipe2.SetActive(false);
            //_startPipe3.SetActive(false);
            //_startPipe4.SetActive(false);
            fountain.SetActive(false);


            Transform cameraTransform = currentCamera.transform;
            Transform playerTransform = player.transform;
            Vector3 Offset = cameraTransform.position - playerTransform.position;

            cameraTransform.position = playerTransform.position;
            Vector3 tempTrans = playerTransform.forward * 10;
            tempTrans += new Vector3(0, 6, 0);

            cameraTransform.position += tempTrans;
            cameraTransform.LookAt(playerTransform.position);

        }

        if (_PPlayer._mode == Player1.GameMode.CLEARMODE)
        {
            Transform cameraTransform = currentCamera.transform;
            Transform playerTransform = player.transform;
            Vector3 Offset = cameraTransform.position - playerTransform.position;

            cameraTransform.position = playerTransform.position;
            Vector3 tempTrans = playerTransform.forward * 10;
            tempTrans += new Vector3(0, 6, 0);

            cameraTransform.position += tempTrans;
            cameraTransform.LookAt(playerTransform.position);
        }

        // hide the wall object covering the screen
        for (int i = 0; i < targetWallList.Length; ++i)
        {
            if (targetWallList[i].activeSelf == false)
                targetWallList[i].SetActive(true);
            // Dot Product between camera and wall
            CamWallDot(targetWallList[i]);
        }
    }

    private void CamWallDot(GameObject wall)
    {
        Vector3 camForward = transform.forward;
        Vector3 wallForward = wall.transform.up;
        if (Vector3.Dot(camForward, wallForward) >= 0.3f)
        {
            if (wall.activeSelf == true)
            {
                wall.SetActive(false);
            }
        }
    }
}

