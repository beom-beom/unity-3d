using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movements : MonoBehaviour
{
    public float moveSpeed = 5;        // �̵� �ӵ�
    public Vector3 moveDirection;      // �̵� ����

    private CharacterController characterController;

    public float MoveSpeed
    {
        // �̵��ӵ��� 2 ~ 5 ������ ���� ���� ����
        set => moveSpeed = Mathf.Clamp(value, 2.0f, 5.0f);
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
   
         characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
    }

   
}
