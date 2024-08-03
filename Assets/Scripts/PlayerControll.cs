using System.Collections;
using System.Collections.Generic;
using UnityEngine; //코드가 사용할 라이브러리를 불러오는 부분 (유니티엔진 사용)

public class PlayerControll : MonoBehaviour //MonoBehaviour의 클래스를 상속받는다 
{
    Rigidbody2D rb; //플레이어의 물리적인 속성 제어
    [SerializeField] private float moveSpeed; //플레이어 이동속도 조절
    Vector2 movement; //플레이어 움직임 방향 
    public Animator anim; //애니메이션 제어하기위한 변수

    bool facingRight = true; //오른쪽방향 상태

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //rb변수에 현재 게임 오브젝트에 부착된 Rigidbody2D컴포넌트 할당
        anim = GetComponent<Animator>();
    }

    private void Update() //매 프레임마다 호출
    {
        //Input.GetAxisRaw()를 사용하여 수평 및 수직 입력감지하고 그 값을 벡터에 저장
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1")) //공격동작 시전 (마우스왼쪽 or CTRL키)
        {
            anim.SetTrigger("attack");
        }




    }

    private void FixedUpdate() //물리 연산 업데이트 주기마다 호출, 물리연산에 관련된 작업을 수행 (물리적인 이동이나, 충돌처리는 여기서 처리하는게 좋음)
    {

        #region move
        //rb.position에 벡터와 스피트, 시간 더한값으로 플레이어 이동/ 델타타임은 이동에 사용되는 프레임 간
        //시간 간격을 보정하기 위해 사용 
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        if (movement.y != 0 || movement.x != 0)
        {
            anim.SetBool("isRunnig", true);
        }

        else
        {
            anim.SetBool("isRunnig", false);
        }

        if(movement.x < 0 && facingRight) //수평이동값이 0보다 작고 오른쪽방향상태일때
        {
            flip(); //왼쪽으로 방향 바꿈
            facingRight = !facingRight; //현재 방향에서 반대로
        }
        else if (movement.x >0 && !facingRight) //수평이동값이 0보다크고 왼쪽방향상태일때
        {
            flip(); //오른쪽으로 변경
            facingRight = !facingRight; //현재 방향에서 반대로 
        }
        #endregion 

       
    }

    void flip() //플레이어의 방향을 변경 
    {
        transform.Rotate(0, 180f, 0); //플레이어를 회전하여 방향을 변경 (y축값 180도로)
    }
}
