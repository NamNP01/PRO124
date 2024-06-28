using UnityEngine;

public class Melee : MonoBehaviour
{
    public float moveSpeed = 3f; // Tốc độ di chuyển của Melee Tower
    public float attackRange = 2f; // Phạm vi tấn công của Melee Tower
    public int damage = 5; // Sát thương của Melee Tower

    private Transform target; // Mục tiêu tấn công (Goblin)
    private bool isAttacking = false; // Trạng thái tấn công
    private EnemyController currentEnemy; // Đối tượng EnemyController đang được tấn công

    void Update()
    {
        if (!isAttacking)
        {
            FindAndAttackTarget();
        }
        else if (target != null)
        {
            // Kiểm tra khoảng cách để quyết định di chuyển hoặc tấn công
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget > attackRange)
            {
                MoveTowardsTarget();
            }
            else
            {
                AttackEnemy();
            }
        }
    }

    void FindAndAttackTarget()
    {
        // Tìm tất cả các enemy trong phạm vi tấn công
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = collider.transform;
                }
            }
        }

        if (nearestEnemy != null)
        {
            Attack(nearestEnemy);
        }
    }

    void Attack(Transform enemy)
    {
        // Bắt đầu tấn công và đi theo enemy
        target = enemy;
        isAttacking = true;

        // Gọi hàm dừng di chuyển của enemy và bắt đầu tấn công
        currentEnemy = target.GetComponent<EnemyController>();
        if (currentEnemy != null)
        {
            currentEnemy.StopMovingByMelee();
            currentEnemy.StartAttack();
        }
    }

    void MoveTowardsTarget()
    {
        // Di chuyển về phía enemy
        transform.Translate((target.position - transform.position).normalized * moveSpeed * Time.deltaTime);
    }

    void AttackEnemy()
    {
        // Gây sát thương cho enemy
        if (currentEnemy != null)
        {
            //currentEnemy.TakeDamage(damage);
            //currentEnemy.EndAttack();
        }

        // Đặt lại trạng thái sau khi tấn công
        target = null;
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Vẽ hình tròn biểu thị phạm vi tấn công
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
