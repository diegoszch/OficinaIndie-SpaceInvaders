using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private Dictionary<int, List<EnemyAttack>> enemies;

    [SerializeField] private int line;
    [SerializeField] private int column;

    private float paddingX = 1.5f;
    private float paddingY = 1f;

    [SerializeField] private float timeMoveDown;
    [SerializeField] private float paddingMoveDown;
    private float nextMoveDown;
    private bool canMoveDown;

    [SerializeField] private float timeMoveSide;
    [SerializeField] private float paddingMoveSide;
    private float nextMoveSide;
    private bool canMoveSide;

    private bool isLeft;

    private bool canAttack;
    private float nextAttack;

    void Start()
    {
        enemies = new Dictionary<int, List<EnemyAttack>>();

        StartCoroutine(CreateEnemies());
    }

    IEnumerator CreateEnemies()
    {
        yield return null;

        for (int columnIdx = 0; columnIdx < column; columnIdx++)
        {
            for (int rowIdx = 0; rowIdx < line; rowIdx++)
            {
                Vector2 pos = new Vector2(
                    transform.position.x + (columnIdx * paddingX),
                    transform.position.y - (rowIdx * paddingY)
                );

                if (!enemies.ContainsKey(columnIdx))
                    enemies[columnIdx] = new List<EnemyAttack>();

                var enemy = Instantiate(enemyPrefab, pos, Quaternion.identity, transform)
                    .GetComponent<EnemyAttack>();

                enemy.name = $"enemy-{Guid.NewGuid()}";

                enemies[columnIdx].Add(enemy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Attack();
    }

    void Attack()
    {
        if (!canMoveDown && !canMoveDown && canAttack && enemies.Count > 0)
        {
            var selected = UnityEngine.Random.Range(0, enemies.Count - 1);

            var enemyAttack = enemies[selected].Where(x => x != null).LastOrDefault();
            if (enemyAttack != null)
                enemyAttack.Attack();

            canAttack = false;
        }

        if (nextAttack > 3f)
        {
            canAttack = true;
            nextAttack = 0f;
        }

        nextAttack += Time.deltaTime;
    }

    void Move()
    {
        MoveSide();

        MoveDown();
    }

    void MoveDown()
    {
        if (canMoveDown)
        {
            transform.position += Vector3.down * paddingMoveDown;
            canMoveDown = false;
        }

        if (nextMoveDown > timeMoveDown && timeMoveDown != 0)
        {
            canMoveDown = true;
            nextMoveDown = 0;
        }

        nextMoveDown += Time.deltaTime;
    }

    void MoveSide()
    {
        if (canMoveSide)
        {
            transform.position += (isLeft ? Vector3.left : Vector3.right) * paddingMoveSide;

            isLeft = !isLeft;
            canMoveSide = false;
        }

        if (nextMoveSide > timeMoveSide && timeMoveSide != 0)
        {
            canMoveSide = true;
            nextMoveSide = 0;
        }

        nextMoveSide += Time.deltaTime;
    }
}
