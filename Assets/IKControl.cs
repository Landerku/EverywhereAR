using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKControl : MonoBehaviour
{
    private Animator animator;
    public bool ikActive = true;
    public Transform headTarget;
    public float lookAtWeight = 1.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if (ikActive && animator)
        {
            // 控制头部完全看向目标点
            animator.SetLookAtWeight(lookAtWeight);
            animator.SetLookAtPosition(headTarget.position);
        }
    }

    void Update()
    {
        // 限制角色身体的旋转，只允许绕Y轴旋转
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, eulerAngles.y, 0);
    }
}
