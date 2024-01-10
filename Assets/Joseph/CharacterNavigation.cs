using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CharacterNavigation : MonoBehaviour
{
    private Camera arCamera;
    private Animator characterAnimator; // Reference to the character's animator
    private bool isWalking = false;
    public float moveSpeed = 1.0f; // Adjust this for the desired constant speed

    void Start()
    {
        Debug.Log("started");
        // Get the AR camera
        arCamera = Camera.main;

        // Get the character's animator component
        characterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterAnimator.SetBool("IsWalking", true);
            Debug.Log("walking");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            characterAnimator.SetBool("IsWalking", false);
            Debug.Log("Not walking");
        }
        // Check for tap on the screen
        if (Input.touchCount > 0)
        {
            Debug.Log("Casted");
            // Perform a raycast from the touch position
            Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (!isWalking) { 
                // Move and rotate the character to the tapped position
                MoveToPosition(hit.point);
           
            }
            }
        }
    }

    void MoveToPosition(Vector3 position)
    {
        // Calculate the direction to the tapped position
        Vector3 direction = position - transform.position;
        direction.y = 0f; // Keep the character upright

        // Rotate the character to face the tapped position
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = toRotation;
        }

        // Play the walking animation (assuming you have a "Walk" animation)
        characterAnimator.SetBool("IsWalking", true);

        // Move the character to the tapped position
        StartCoroutine(MoveCharacter(position));
    }

    IEnumerator MoveCharacter(Vector3 targetPosition)
    {
        isWalking = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Stop the walking animation
        characterAnimator.SetBool("IsWalking", false);
        isWalking = false;
    }
}
