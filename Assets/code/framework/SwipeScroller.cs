using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(InputDetector))]
public class SwipeScroller : MonoBehaviour
{
    public enum Direction
    {
        Horizontal,
        Vertical
    }

    private enum State
    {
        Idle,
        Touched,
        Dragging,
        Sliding
    }

    public Direction direction = Direction.Vertical;
    public float power = 0.01f;
    public float smooth = 0.1f;
    public float minDragDistance = 0.0015f;
    public float returnSmooth = 2f;

    public Camera useSpecificCamera;

    private SwipeScrollerContent content;

    private State state;
    private Vector3 offset;
    private Vector3 touchStartPosition;
    private Vector3 previousPosition;
    private float speed;

    // Use this for initialization
    void Start()
    {
        state = State.Idle;
        content = GetComponentInChildren<SwipeScrollerContent>();
        GetComponent<InputDetector>().OnTouchAnywhere += OnTouchAnywhere;
        GetComponent<InputDetector>().OnTouchReleaseAnywhere += OnTouchReleaseAnywhere;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Touched:
                Vector3 mouseWorld = GetInputWorldPosition();
                mouseWorld.z = content.transform.position.z;

                Vector3 delta = mouseWorld - touchStartPosition;
                if (delta.sqrMagnitude > minDragDistance)
                {
                    state = State.Dragging;

                    offset = content.transform.position - mouseWorld;
                }
                break;
            case State.Dragging:
                DragginUpdate();
                break;
            case State.Sliding:
                SlidingUpdate();
                break;
        }
    }

    void OnTouchAnywhere(InputDetectorTouch touch)
    {
        if (RayHitsCollider(touch.screenPosition))
        {
            touchStartPosition = Camera.main.ScreenToWorldPoint(touch.screenPosition);
            touchStartPosition.z = content.transform.position.z;

            previousPosition = content.transform.position;

            state = State.Touched;
            speed = 0f;
        }
    }

    void OnTouchReleaseAnywhere(InputDetectorTouch touch)
    {
        state = State.Sliding;
    }

    bool RayHitsCollider(Vector3 touchPosition)
    {
        bool rayHitsCollider = false;

        Camera cameraToUse = Camera.main;

        if (this.useSpecificCamera != null)
        {
            cameraToUse = this.useSpecificCamera;
        }

        Ray ray = cameraToUse.ScreenPointToRay(touchPosition);

        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                rayHitsCollider = true;
                break;
            }
        }

        return rayHitsCollider;
    }

    void DragginUpdate()
    {
        Vector3 mouseWorld = GetInputWorldPosition();
        mouseWorld.z = content.transform.position.z;

        Vector3 newpos = mouseWorld + offset;

        if (direction == Direction.Horizontal)
        {
            newpos.y = content.transform.position.y;
        }
        else
        {
            newpos.x = content.transform.position.x;
        }

        MeasureSpeed();

        content.transform.position = newpos;
    }

    void MeasureSpeed()
    {
        Vector3 distance = content.transform.position - previousPosition;

        if (direction == Direction.Horizontal)
        {
            this.speed = distance.x / Time.deltaTime;
        }
        else if (direction == Direction.Vertical)
        {
            this.speed = distance.y / Time.deltaTime;
        }

        previousPosition = content.transform.position;
    }

    void SlidingUpdate()
    {
        float translation = Time.deltaTime * this.speed;

        if (direction == Direction.Vertical)
        {
            content.transform.Translate(0, translation, 0);
        }
        else
        {
            content.transform.Translate(translation, 0, 0);
        }

        this.speed = Mathf.MoveTowards(this.speed, 0f, smooth * Time.deltaTime);

        CheckForReturn();
    }

    private void CheckForReturn()
    {
        float distanceToTop = collider.bounds.max.y - content.collider.bounds.max.y;
        float distanceToBottom = collider.bounds.min.y - content.collider.bounds.min.y;

        if (distanceToTop > 0f)
        {
            Vector3 newPos = content.transform.localPosition;
            newPos.y += distanceToTop;
            content.transform.localPosition = Vector3.Lerp(content.transform.localPosition, newPos, returnSmooth * Time.deltaTime);
            this.speed = 0f;
        }
        else if (distanceToBottom < 0f)
        {
            Vector3 newPos = content.transform.localPosition;
            newPos.y += distanceToBottom;
            content.transform.localPosition = Vector3.Lerp(content.transform.localPosition, newPos, returnSmooth * Time.deltaTime);
            this.speed = 0f;
        }
    }

    public void AlignContentToBounds()
    {
        if (content == null)
        {
            content = GetComponentInChildren<SwipeScrollerContent>();
        }

        float distanceToTop = collider.bounds.max.y - content.collider.bounds.max.y;
        float distanceToBottom = collider.bounds.min.y - content.collider.bounds.min.y;

        if (distanceToTop > 0f)
        {
            Vector3 newPos = content.transform.localPosition;
            newPos.y += distanceToTop;
            content.transform.localPosition = newPos;
            speed = 0f;
        }
        else if (distanceToBottom < 0f)
        {
            Vector3 newPos = content.transform.localPosition;
            newPos.y += distanceToBottom;
            content.transform.localPosition = newPos;
            speed = 0f;
        }
    }

    Vector3 GetInputWorldPosition()
    {
        Vector3 retval = Vector3.zero;

        if (Application.isEditor)
        {
            retval = Input.mousePosition;
        }
        else if (Input.touchCount > 0)
        {
            retval = Input.touches[0].position;
        }

        return Camera.main.ScreenToWorldPoint(retval);
    }

    void KeepContentInBounds()
    {
        if (direction == Direction.Vertical)
        {
            if (content.collider.bounds.min.y > this.collider.bounds.min.y)
            {
                Vector3 delta = content.collider.bounds.min - this.collider.bounds.min;
                delta.z = 0f;
                delta.x = 0f;

                content.transform.position -= delta;

                this.speed = 0f;
            }
            else if (content.collider.bounds.max.y < this.collider.bounds.max.y)
            {
                Vector3 delta = content.collider.bounds.max - this.collider.bounds.max;
                delta.z = 0f;
                delta.x = 0f;

                content.transform.position -= delta;

                this.speed = 0f;
            }
        }
        else
        {
            if (content.collider.bounds.min.x > this.collider.bounds.min.x)
            {
                Vector3 delta = content.collider.bounds.min - this.collider.bounds.min;
                delta.z = 0f;
                delta.y = 0f;

                content.transform.position -= delta;

                this.speed = 0f;
            }
            else if (content.collider.bounds.max.x < this.collider.bounds.max.x)
            {
                Vector3 delta = content.collider.bounds.max - this.collider.bounds.max;
                delta.z = 0f;
                delta.y = 0f;

                content.transform.position -= delta;

                this.speed = 0f;
            }
        }
    }
}
