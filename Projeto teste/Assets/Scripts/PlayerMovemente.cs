using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovemente : MonoBehaviour
{
    [SerializeField] private Vector2 joystickSize = new Vector2(200, 200);

    [SerializeField] private Image joystick;
    [SerializeField] private Image knobJoystick;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float speed;
    [SerializeField] private float punchForce = 30;
    [SerializeField] private GameObject stack;
    [SerializeField] private int currentMoney = 0;

    private int maxCarry = 1;
    private int currentCarrying = 0;

    private Finger movementFinger;
    private Vector2 movementAmount;
    private int costUpCarry = 100;
    private int costChangeColor = 100;
    private Renderer[] materials;
    private void Start()
    {
        materials = GetComponentsInChildren<Renderer>();
        GameEvents.instance.UpdateCarryAmount += SetCurrent;
        GameEvents.instance.UpdateCurrentCarryUpgradeCost(costUpCarry);
        GameEvents.instance.UpdateCurrentColorText(costChangeColor);
    }
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += Touch_onFingerDown;
        ETouch.Touch.onFingerUp += Touch_onFingerUp;
        ETouch.Touch.onFingerMove += Touch_onFingerMove;
    }
    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= Touch_onFingerDown;
        ETouch.Touch.onFingerUp -= Touch_onFingerUp;
        ETouch.Touch.onFingerMove -= Touch_onFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        Vector3 playerMovement = speed * Time.deltaTime * new Vector3(movementAmount.x, 0, movementAmount.y);
        playerRb.transform.LookAt(playerRb.transform.position + playerMovement, Vector3.up);
        playerRb.transform.position += playerMovement;
    }
    #region touchScreen functions
    private void Touch_onFingerMove(Finger fingerMove)
    {
        if (fingerMove == movementFinger)
        {
            Vector2 knobPosition;
            float movement = joystickSize.x / 2f;
            ETouch.Touch currentTouch = fingerMove.currentTouch;
            if (Vector2.Distance(currentTouch.screenPosition, joystick.rectTransform.anchoredPosition) > movement)
            {
                knobPosition = (currentTouch.screenPosition - joystick.rectTransform.anchoredPosition).normalized * movement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - joystick.rectTransform.anchoredPosition;
            }
            knobJoystick.rectTransform.anchoredPosition = knobPosition;
            movementAmount = knobPosition / movement;
        }
    }

    private void Touch_onFingerUp(Finger fingerUp)
    {
        if (fingerUp == movementFinger)
        {
            movementFinger = null;
            joystick.GetComponentInChildren<RectTransform>().anchoredPosition = Vector2.zero;
            joystick.gameObject.SetActive(false);
            movementAmount = Vector2.zero;
        }
    }

    private void Touch_onFingerDown(Finger fingerDown)
    {
        if (movementFinger == null)
        {
            movementFinger = fingerDown;
            movementAmount = Vector2.zero;
            joystick.gameObject.SetActive(true);
            joystick.rectTransform.sizeDelta = joystickSize;
            joystick.rectTransform.anchoredPosition = fingerDown.screenPosition;
        }
    }
    #endregion
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            collision.transform.root.GetComponent<IDamageble>().TakeDamage(new Vector3(-rot.x, 0, rot.z), punchForce);
            collision.transform.root.GetComponent<IPickable>().PickObject(currentCarrying, maxCarry);
        }
    }
    public void GainMoney()
    {
        currentMoney += 100 * currentCarrying;
        GameEvents.instance.UpdateCurrentMoney(currentMoney);
        currentCarrying = 0;
        GameEvents.instance.UpdateCurrentCarryAmount(currentCarrying);
        stack.GetComponent<StackEnemyController>().DeleteChild();
    }
    public void SetCurrent(int current)
    {
        currentCarrying = current;
    }
    public void UPMaxAmount()
    {
        if (currentMoney >= costUpCarry)
        {
            currentMoney -= costUpCarry;
            GameEvents.instance.UpdateCurrentMoney(currentMoney);
            maxCarry++;
            costUpCarry += 50;
            GameEvents.instance.UpdateCurrentCarryUpgradeCost(costUpCarry);
        }
    }
    public void ChangeColor()
    {
        if (currentMoney >= costChangeColor)
        {
            foreach (Renderer item in materials)
            {
                Color newColor = new Color(Random.value, Random.value, Random.value, 1f);
                item.material.color = newColor;
            }
            currentMoney -= costChangeColor;
            GameEvents.instance.UpdateCurrentMoney(currentMoney);
            GameEvents.instance.UpdateCurrentColorText(costChangeColor);

        }
    }
}
