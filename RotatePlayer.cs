using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField]
    public float walkSpeed;

    [SerializeField]
    public float lookSensitivity;

    [SerializeField]
    public float cameraRotationLimit;
    public float currentCameraRotationX;
    public float FlashRotationLimit;
    public float currentFlashRotationX;

    [SerializeField]
    public Camera theCamera;
    public GameObject theFlash;
    public Rigidbody myRigid;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();  // private
    }

    void Update()  // 컴퓨터마다 다르지만 대략 1초에 60번 실행
    {
        Move();                 // 1️⃣ 키보드 입력에 따라 이동
        CameraRotation();       // 2️⃣ 마우스를 위아래(Y) 움직임에 따라 카메라 X 축 회전 
        CharacterRotation();    // 3️⃣ 마우스 좌우(X) 움직임에 따라 캐릭터 Y 축 회전 
        FlashRotation();
    }

    private void Move()
    {
        //float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        //Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity =  _moveVertical.normalized * walkSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;

        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
    
    private void FlashRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _FlashaRotationX = _xRotation * lookSensitivity;

        currentFlashRotationX -= _FlashaRotationX;
        currentFlashRotationX = Mathf.Clamp(currentFlashRotationX, -FlashRotationLimit, FlashRotationLimit);

        theFlash.transform.localEulerAngles = new Vector3(currentFlashRotationX, 1.5f, 0.3f);
    }

    private void CharacterRotation()  // 좌우 캐릭터 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); // 쿼터니언 * 쿼터니언
        // Debug.Log(myRigid.rotation);  // 쿼터니언
        // Debug.Log(myRigid.rotation.eulerAngles); // 벡터
    }


    //private float speed = 200;
    //public GameObject player;

    //public float turnSpeed = 4.0f; // 마우스 회전 속도    
    //private float xRotate = 0.0f; // 내부 사용할 X축 회전량은 별도 정의 ( 카메라 위 아래 방향 )

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    float horizontalInput = Input.GetAxis("Horizontal");
    //    transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);

    //    // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
    //    float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
    //    // 현재 y축 회전값에 더한 새로운 회전각도 계산
    //    float yRotate = transform.eulerAngles.y + yRotateSize;

    //    // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
    //    float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
    //    // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
    //    // Clamp 는 값의 범위를 제한하는 함수
    //    xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

    //    // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
    //    transform.eulerAngles = new Vector3(0, yRotate, 0);

    //}
}
