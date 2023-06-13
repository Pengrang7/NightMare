using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// http://forum.unity3d.com/threads/really-need-some-help-with-mouselook-s.49243/

[AddComponentMenu("Camera/Simple Smooth Mouse Look ")]
public class PlayerControllerX : MonoBehaviour
{
    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    public Vector2 clampInDegrees = new Vector2(360, 180);
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);
    public Vector2 targetDirection;
    public Vector2 targetCharacterDirection;

    // Assign this if there's a parent object controlling motion, such as a Character Controller.
    // Yaw rotation will affect this object instead of the camera if set.
    public GameObject characterBody;

    private bool _mouselookEnabled = false;
    private bool _shifted = false;
    public float flySpeed = 0.5f;
    public GameObject defaultCamera;


    void Start()
    {
        // Set target direction to the camera's initial orientation.
        targetDirection = transform.localRotation.eulerAngles;

        // Set target direction for the character body to its inital state.
        if (characterBody)
            targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.LeftShift) & _shifted)
            _shifted = false;

        if ((Input.GetKeyDown(KeyCode.LeftShift) & !_shifted) |
            (Input.GetKeyDown(KeyCode.Escape) & _mouselookEnabled))
        {
            _shifted = true;

            if (!_mouselookEnabled)
            {
                _mouselookEnabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    _shifted = false;

                _mouselookEnabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        if (!_mouselookEnabled)
            return;

        //ensure these stay this way
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Allow the script to clamp based on a desired target value.
        var targetOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        // Get raw mouse input for a cleaner reading on more sensitive mice.
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Find the absolute mouse movement value from point zero.
        _mouseAbsolute += _smoothMouse;

        // Clamp and apply the local x value first, so as not to be affected by world transforms.
        if (clampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        // Then clamp and apply the global y value.
        if (clampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        var xRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right);
        transform.localRotation = xRotation * targetOrientation;

        // If there's a character body that acts as a parent to the camera
        if (characterBody)
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, characterBody.transform.up);
            characterBody.transform.localRotation = yRotation;
            characterBody.transform.localRotation *= targetCharacterOrientation;
        }
        else
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
            transform.localRotation *= yRotation;
        }

        //movement
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(defaultCamera.transform.forward * flySpeed * Input.GetAxis("Vertical"), Space.World);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(defaultCamera.transform.right * flySpeed * Input.GetAxis("Horizontal"), Space.World);
        }
        if (Input.GetKey(KeyCode.R))
        {
            transform.Translate(Vector3.up * flySpeed * 0.5f, Space.World);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(-Vector3.up * flySpeed * 0.5f, Space.World);
        }
    }

    //[Header("Info")]
    //private List<float> _rotArrayX = new List<float>();
    //private List<float> _rotArrayY = new List<float>();
    //private float rotAverageX;
    //private float rotAverageY;
    //private float mouseDeltaX;
    //private float mouseDeltaY;

    //[Header("Settings")]
    //public bool _isLocked;
    //public float _sensitivityX = 1.5f;
    //public float _sensitivityY = 1.5f;
    //[Tooltip("The more steps, the smoother it will be.")]
    //public int _averageFromThisManySteps = 3;

    //[Header("References")]
    //[Tooltip("Object to be rotated when mouse moves left/right.")]
    //public Transform _playerRootT;
    //[Tooltip("Object to be rotated when mouse moves up/down.")]
    //public Transform _cameraT;

    ////============================================
    //// FUNCTIONS (UNITY)
    ////============================================

    //void Update()
    //{
    //    MouseLookAveraged();
    //}

    ////============================================
    //// FUNCTIONS (CUSTOM)
    ////============================================

    //void MouseLookAveraged()
    //{
    //    rotAverageX = 0f;
    //    rotAverageY = 0f;
    //    mouseDeltaX = 0f;
    //    mouseDeltaY = 0f;

    //    mouseDeltaX += Input.GetAxis("Mouse X") * _sensitivityX;
    //    mouseDeltaY += Input.GetAxis("Mouse Y") * _sensitivityY;

    //    // Add current rot to list, at end
    //    _rotArrayX.Add(mouseDeltaX);
    //    _rotArrayY.Add(mouseDeltaY);

    //    // Reached max number of steps? Remove oldest from list
    //    if (_rotArrayX.Count >= _averageFromThisManySteps)
    //        _rotArrayX.RemoveAt(0);

    //    if (_rotArrayY.Count >= _averageFromThisManySteps)
    //        _rotArrayY.RemoveAt(0);

    //    // Add all of these rotations together
    //    for (int i_counterX = 0; i_counterX < _rotArrayX.Count; i_counterX++)
    //        rotAverageX += _rotArrayX[i_counterX];

    //    for (int i_counterY = 0; i_counterY < _rotArrayY.Count; i_counterY++)
    //        rotAverageY += _rotArrayY[i_counterY];

    //    // Get average
    //    rotAverageX /= _rotArrayX.Count;
    //    rotAverageY /= _rotArrayY.Count;

    //    // Apply
    //    _playerRootT.Rotate(0f, rotAverageX, 0f, Space.World);
    //    _cameraT.Rotate(-rotAverageY, 0f, 0f, Space.Self);
    //}

}