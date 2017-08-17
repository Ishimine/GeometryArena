using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public bool zoomApuntando = false;
    public bool zoomDinamico = false;
    [SerializeField]float zoomObjetivo;
    float zoomVel;
    public float smooth;
    float vel;
    public float velMin = 2;
    public float velMax = 10;

    public float zoomMin = 5;
    public float zoomMax = 9;


   
    public bool visualizarArea;

    public PlayerSwipeMovement target;
    public Rigidbody2D targetRb;
    public Collider2D targetCol;

    Vector2 offsetVel;
    public Vector2 offset;
    public Vector2 offsetObjetivo;
    public float smoothOffset = .5f;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public Vector2 focusAreaSize;

    FocusArea focusArea;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStopped;


    static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
            //SceneManager.sceneLoaded += NivelCargado;
            NivelCargado();
            //transform.position += Vector3.forward * 10;
            zoomObjetivo = zoomMin;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void NivelCargado(Scene scene, LoadSceneMode mode)
    {
        NivelCargado();
    }

    public void SetTarget(PlayerSwipeMovement t, Collider2D col, Rigidbody2D rb)
    {
        target = t;
        targetCol = col;
        targetRb = rb;
        focusArea = new FocusArea(targetCol.bounds, focusAreaSize);
    }


    void NivelCargado()
    {
        Camera[] a = FindObjectsOfType<Camera>();

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i].gameObject != instance)
            {
                Destroy(a[i]);
            }
        }

        target = FindObjectOfType<PlayerSwipeMovement>();

        if (target != null)
        {
            targetCol = target.GetComponent<Collider2D>();
            targetRb = target.GetComponent<Rigidbody2D>();
            focusArea = new FocusArea(targetCol.bounds, focusAreaSize);
           // transform.position = target.transform.position;
        }
        //transform.position += -Vector3.forward * 10;

    }


    void ActualizarOffset()
    {
        offset = Vector2.SmoothDamp(offset, offsetObjetivo, ref offsetVel, smoothOffset,30,Time.deltaTime);
    }

    void ActualizarZoomObjetivo()
    {
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, zoomObjetivo, ref zoomVel, smooth);
    }

    void ModificarOffset(Vector2 dir, float cant)
    {
        if (cant > 10) cant = 10;

        zoomObjetivo = zoomMin + cant / 5;
        offsetObjetivo = dir.normalized * cant;
    }

    void ResetOffset()
    {
        offsetObjetivo = Vector2.zero;
        zoomObjetivo = zoomMin;
    }

    void Start()
    {
        if(target != null)
            focusArea = new FocusArea(targetCol.bounds, focusAreaSize);

        TouchControl tc = FindObjectOfType<TouchControl>();
        if (tc != null)
        {
            tc.DireccionEstacionaria += ModificarOffset;
            tc.touchOut += ResetOffset;
        }

    }

    void ActualizarZoom()
    {        
        float v = focusArea.velocity.magnitude;
        if (v < velMin) v = velMin;
        else if (v >  velMax) v = velMax;
        float t = v / velMax;
        v = Mathf.Lerp(zoomMin, zoomMax, t);
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize,v,ref vel, smooth);
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        ActualizarOffset();
       if(zoomApuntando) ActualizarZoomObjetivo();
        focusArea.Update(targetCol.bounds);

        Vector2 focusPosition = focusArea.centre + offset;

        if (focusArea.velocity.x != 0)
        {
            lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
            if (Mathf.Sign(targetRb.velocity.normalized.x) == Mathf.Sign(focusArea.velocity.x) && targetRb.velocity.normalized.x != 0)
            {
                lookAheadStopped = false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;
            }
            else
            {
                if (!lookAheadStopped)
                {
                    lookAheadStopped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
                }
            }
        }


        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        focusPosition += Vector2.right * currentLookAheadX;
        transform.position = (Vector3)focusPosition + Vector3.forward * -10;
        if (zoomDinamico) ActualizarZoom();
    }





    void OnDrawGizmos()
    {
        if (!visualizarArea)
            return;
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float top, bottom;


        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }

    

}
