using System.Collections;
using System.Collections.Generic;
using UnityEngine; //�ڵ尡 ����� ���̺귯���� �ҷ����� �κ� (����Ƽ���� ���)

public class PlayerControll : MonoBehaviour //MonoBehaviour�� Ŭ������ ��ӹ޴´� 
{
    Rigidbody2D rb; //�÷��̾��� �������� �Ӽ� ����
    [SerializeField] private float moveSpeed; //�÷��̾� �̵��ӵ� ����
    Vector2 movement; //�÷��̾� ������ ���� 
    public Animator anim; //�ִϸ��̼� �����ϱ����� ����

    bool facingRight = true; //�����ʹ��� ����

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //rb������ ���� ���� ������Ʈ�� ������ Rigidbody2D������Ʈ �Ҵ�
        anim = GetComponent<Animator>();
    }

    private void Update() //�� �����Ӹ��� ȣ��
    {
        //Input.GetAxisRaw()�� ����Ͽ� ���� �� ���� �Է°����ϰ� �� ���� ���Ϳ� ����
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1")) //���ݵ��� ���� (���콺���� or CTRLŰ)
        {
            anim.SetTrigger("attack");
        }




    }

    private void FixedUpdate() //���� ���� ������Ʈ �ֱ⸶�� ȣ��, �������꿡 ���õ� �۾��� ���� (�������� �̵��̳�, �浹ó���� ���⼭ ó���ϴ°� ����)
    {

        #region move
        //rb.position�� ���Ϳ� ����Ʈ, �ð� ���Ѱ����� �÷��̾� �̵�/ ��ŸŸ���� �̵��� ���Ǵ� ������ ��
        //�ð� ������ �����ϱ� ���� ��� 
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        if (movement.y != 0 || movement.x != 0)
        {
            anim.SetBool("isRunnig", true);
        }

        else
        {
            anim.SetBool("isRunnig", false);
        }

        if(movement.x < 0 && facingRight) //�����̵����� 0���� �۰� �����ʹ�������϶�
        {
            flip(); //�������� ���� �ٲ�
            facingRight = !facingRight; //���� ���⿡�� �ݴ��
        }
        else if (movement.x >0 && !facingRight) //�����̵����� 0����ũ�� ���ʹ�������϶�
        {
            flip(); //���������� ����
            facingRight = !facingRight; //���� ���⿡�� �ݴ�� 
        }
        #endregion 

       
    }

    void flip() //�÷��̾��� ������ ���� 
    {
        transform.Rotate(0, 180f, 0); //�÷��̾ ȸ���Ͽ� ������ ���� (y�ప 180����)
    }
}
