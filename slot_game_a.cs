using UnityEngine;


public class slot_game_a : MonoBehaviour
{
    private Animator anim;
    // mClick이 더 이상 필요 없다면 제거해도 되지만, 유지한 상태로 수정했습니다.

    private int Try;
    private int a, b, c;
    private const int PityThreshold = 15;
    private const float WinProbability = 0.01f;
    private bool isReset = true;
    //private bool Reset = false;

    void Awake()
    {
        Try = 0;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // R 키: 무조건 리셋
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var state = anim.GetCurrentAnimatorStateInfo(0);

            // 이 로그를 찍어보세요! 콘솔창에 지금 상태가 무엇인지 나옵니다.
            Debug.Log("현재 애니메이터 상태: " + state.fullPathHash);
            // 혹은 state.IsName("이름")이 정확한지 확인을 위해 직접 출력:
            Debug.Log("Stay 상태인가?: " + state.IsName("stay"));

            if (state.IsName("stay"))
            {
                StartSpin();
            }
            else if (state.IsName("Spinning"))
            {
                StopSpin();
            }
        }
    }

    void StartSpin()
    {


        anim.SetTrigger("StartSpin"); // 'StartSpin' 애니메이션 실행
        Debug.Log("스핀 시작!");
    }

    void StopSpin()
    {

        CheckSlot(); // 결과 계산 및 애니메이션 설정
    }

    void ResetGame()
    {
        // 1. C# 변수 초기화
        a = 0; b = 0; c = 0;

        // 2. 애니메이터 파라미터 초기화 (핵심!)
        // SlotResult 값을 0으로 만들어서 "결과 없음" 상태로 만듭니다.
        anim.SetInteger("SlotResult", 0);

        // 3. 트리거 실행
        anim.SetTrigger("ResetToStay");

        Debug.Log("게임 리셋: 변수와 애니메이터 파라미터 모두 0으로 초기화되었습니다.");
    }

    void CheckSlot()
    {
        bool isWin = false;
        Try++; // 시도 횟수 증가 로직 추가 (기존 코드에 없어서 추가했습니다)

        if (Try >= PityThreshold || Random.value <= WinProbability)
        {
            isWin = true;
            Try = 0;
        }

        if (isWin)
        {
            a = 6;
            if (a == 6 && b == 6 && c == 6)
            {
                Debug.Log($"당첨! 결과: {a}, {b}, {c} (시도: {Try})");
            }
            anim.SetInteger("SlotResult", 6);
        }

        else
        {
            a = Random.Range(1, 7);
            b = Random.Range(1, 7);
            c = Random.Range(1, 7);
            Debug.Log($"꽝! 결과: {a}, {b}, {c} (시도: {Try})");
            anim.SetInteger("SlotResult", a);
        }
    }
}