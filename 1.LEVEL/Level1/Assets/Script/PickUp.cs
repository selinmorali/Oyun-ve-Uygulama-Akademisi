using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    [Space(15)]
    public Image Crosshair;
    [Space(15)]
    public float _Distance;             //Max distance between character and object to be able to interact
    public GameObject KeyInHand;
    [Space(15)]
    [SerializeField] public float holdRange;
    private GameObject heldObject;
    public Transform holdParent;
    [SerializeField] float moveForce;
    [Space(15)]
    public float positionDistance;
    [Space(15)]
    [SerializeField] GameObject character;
    [SerializeField] bool pushObject = true;
    [SerializeField] bool pullObject = true;
    [Space(15)]                                            //public Transform holdParentOriginalPos;
    [SerializeField] float maxPushDistance;
    [SerializeField] float maxPullDistance;
    [Space(15)]
    [SerializeField] Animator animator;
    [Space(15)]
    [SerializeField] LayerMask layerMask;
    private void Start()
    {
    }

    void Update()
    {
        PickUpYourHand();
        bugCheck();
        HoldItem();
    }
    /*void PickUpYourHandEvents()
    {
        PickUpYourHand();
    }
    */

    void PickUpYourHand()
    {
        Vector3 _forward = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Crosshair.color = Color.white;

        if (Physics.Raycast(transform.position, _forward, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            if (hit.distance <= _Distance && hit.collider.gameObject.tag == "Collectable")
            {
                Crosshair.color = Color.red;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                    KeyInHand.SetActive(true);
                    animator.SetTrigger("PickUp");

                }
            }
        }
    }

    void HoldItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, holdRange))
                {
                    PickUpObject(hit.transform.gameObject);
                }
            }
            else
            {
                dropObject();
            }
        }
        if (heldObject != null)
        {
            MoveObject();
        }
    }
    void PickUpObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 15;

            objRig.transform.parent = holdParent.transform.parent;
            heldObject = pickObj;
        }
    }
    void MoveObject()
    {
        if (Vector3.Distance(heldObject.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && pushObject) // Object push
        {
            holdParent.transform.position = holdParent.transform.position + holdParent.transform.forward;

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && pullObject) // Object pull
        {
            holdParent.transform.position = holdParent.transform.position - holdParent.transform.forward;
        }
    }
    void dropObject()
    {
        Rigidbody heldRig = heldObject.GetComponent<Rigidbody>();
        heldRig.useGravity = true;
        heldRig.drag = 1;

        heldObject.transform.parent = null;
        heldObject = null;
        holdParent.transform.localPosition = new Vector3(0, 0, 7.94f);
    }
    void bugCheck()     //Max & Min push-pull distance to avoid bugs
    {
        positionDistance = Vector3.Distance(holdParent.transform.position, character.transform.position);
        if (positionDistance < maxPullDistance)
        {
            pullObject = false;
        }
        else
        {
            pullObject = true;
        }
        if (positionDistance > maxPushDistance)
        {
            pushObject = false;
        }
        else
        {
            pushObject = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        holdParent.transform.position = new Vector3(holdParent.position.x, 0, holdParent.position.z);
    }
    /*IEnumerator  PickUpandTime()
    {
        yield return new WaitForSecondsRealtime(1);
        RaycastHit hit;
        Physics.Raycast();
        hit.collider.gameObject.transform.parent = handPos.transform;


    }
    */
}
