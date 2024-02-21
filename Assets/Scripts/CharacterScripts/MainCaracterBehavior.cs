using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCaracterBehavior : MonoBehaviour
{
    public float yProjection;


    private float h_speed = 0.2f; // горизонтальная скорость
    public float jumpForce = 4f; // сила прыжка

    private float maxHeight = 0;

    private bool isOnPlatform = false; // в воздухе чи не

    private Vector2 velocity;
    private Vector2 yAxis = new Vector2(0, 1);


    public Rigidbody2D rb;

    public Camera mainCamera; // камера

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        // Получение проекции вектора движения на ось y
        velocity = rb.velocity;
        float yProjection = Vector2.Dot(velocity, yAxis);
        



        // Управление ВПРАВО-ЛЕВО
        float h_input = Input.GetAxis("Horizontal");

        if (h_input != 0)
        {
            transform.Translate(new Vector3(h_input, 0, 0) * h_speed);
        }

        // АВТОПРЫЖОК
        if (isOnPlatform)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Замер высоты
        maxHeight = Mathf.Max(maxHeight, transform.position.y);

        // ПОВЕДЕНИЕ КАМЕРЫ
        // Поднимаем камеру до maxHeight с использованием Lerp
        Vector3 targetPosition = new Vector3(mainCamera.transform.position.x, maxHeight - 3, mainCamera.transform.position.z);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, 0.2f);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))// дотронулся до платформы или нет
        {
            isOnPlatform = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))// дотронулся до платформы или нет
        {
            isOnPlatform = false;
        }
    }
}
