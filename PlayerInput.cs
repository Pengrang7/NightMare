using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveHorizontalAxisName = "Horizontal"; // �¿� ȸ���� ���� �Է��� �̸� (??����Ƽ �Է� ������ �⺻���� �߰� ���ִ� �̸�)
    public string moveVerticalAxisName = "Vertical"; // �յ� �������� ���� �Է��� �̸� (??����Ƽ �Է� ������ �⺻���� �߰� ���ִ� �̸�)

    public string fireButtonName = "Fire1"; // �߻縦 ���� �Է� ��ư �̸� (??����Ƽ �Է� ������ �⺻���� �߰� ���ִ� �̸�)
    public string jumpButtonName = "Jump";  // ������ ���� �Է� ��ư �̸� (??����Ƽ �Է� ������ �⺻���� �߰� ���ִ� �̸�)
    public string reloadButtonName = "Reload"; // �������� ���� �Է� ��ư �̸�


    // �� �Ҵ��� ���ο����� ����
    public Vector2 moveInput { get; private set; }  // Horizontal, Vertical
    public bool fire { get; private set; } // ������ �߻� �Է°�
    public bool reload { get; private set; } // ������ ������ �Է°�
    public bool jump { get; private set; } // ������ ���� �Է°�


    // �� ������ ����� �Է��� ����
    private void Update()
    {
        // ���ӿ��� ���¿����� ����� �Է��� �������� �ʴ´�
        if (GameManager.Instance != null
            && GameManager.Instance.isGameover)
        {
            moveInput = Vector2.zero;
            fire = false;
            reload = false;
            jump = false;

            return;
        }

        moveInput = new Vector2(Input.GetAxis(moveHorizontalAxisName), Input.GetAxis(moveVerticalAxisName));

        if (moveInput.sqrMagnitude > 1) moveInput = moveInput.normalized;

        jump = Input.GetButtonDown(jumpButtonName);
        fire = Input.GetButton(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);
    }
}