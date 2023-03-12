using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class AimStateManager : MonoBehaviour
{

    public AimBaseState currentState;
    public HipfireState Hip = new HipfireState();
    public AimState Aim = new AimState();
    public UIControls controlSystem;

    //public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] float mouseSense = 1; 
    float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;
    [SerializeField] GameObject impactEffect;

    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float adsFov = 40;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed = 10;

    public Transform aimPos;
    [HideInInspector] public Vector3 actualAimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] public LayerMask aimMask;

    float xfollowPos;
    float yFollowPos, ogYPos;
    [SerializeField] float crouchCamHeight = 0.6f;
    [SerializeField] float shoulderSwapSpeed = 10;
    MovementStateManager moving;


    // Start is called before the first frame update
    void Start()
    {
        controlSystem = FindObjectOfType<UIControls>();
        moving = GetComponent<MovementStateManager>();
        xfollowPos = camFollowPos.localPosition.x;
        ogYPos = camFollowPos.localPosition.y;
        yFollowPos = ogYPos;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponentInChildren<Animator>();
        SwitchState(Hip);
    }

    // Update is called once per frame
    void Update()
    {
        if(controlSystem.isDisabled == true)
        {
            xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
            yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
            yAxis = Mathf.Clamp(yAxis, -80, 80);

            vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
            {
                aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            }
            else
            {
                aimPos.position = Vector3.Lerp(aimPos.position, ray.GetPoint(500), aimSmoothSpeed * Time.deltaTime);
                actualAimPos = hit.point;
            }

            MoveCamera();
            currentState.UpdateState(this);

        } else if(controlSystem.isDisabled == false)
        {
            return;
        }

    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void MoveCamera()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) xfollowPos = -xfollowPos;
        if (moving.currentState == moving.Crouch) yFollowPos = crouchCamHeight;
        else yFollowPos = ogYPos;

        Vector3 newFollowPos = new Vector3(xfollowPos, yFollowPos, camFollowPos.localPosition.x);
        camFollowPos.localPosition = Vector3.Lerp(camFollowPos.localPosition, newFollowPos, shoulderSwapSpeed * Time.deltaTime);
    }
}
