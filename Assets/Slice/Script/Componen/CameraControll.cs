using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;	
	Vector3 originalPos;
	public bool follow;
	public Transform Target1;
	public Transform Target2;
	Vector3 FollowPos;
    Vector3 velocity = Vector3.zero;
    float smoothTime = 0.3f;
	public bool centerTarget = false;
	public GameObject Background;
	public GameObject Cover;
	public float focusZoom;
	float normalZoom;
	Camera cam;
	void Awake()
	{
		cam = GetComponent<Camera>();
		normalZoom = cam.orthographicSize;
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}
	public void FocusMode(float time){
		StartCoroutine(ZoomCam(cam.orthographicSize,focusZoom,time));
	}
	public void UnFocus(){
		// print(normalZoom);
		StopAllCoroutines();
		cam.orthographicSize = normalZoom;
		GetComponent<FollowScript>().minHeight = 0;
		Cover.transform.localScale = new Vector3(scaleTemp,scaleTemp,scaleTemp);
		Background.transform.localScale = new Vector3(scaleTemp,scaleTemp,scaleTemp);
	}
	float scaleTemp = 1;
    IEnumerator ZoomCam(float startZoom, float endZoom, float time){
        float t = 0;
        
        float dist = Mathf.Abs(endZoom-startZoom);
		float scale = Cover.transform.localScale.x;
		scaleTemp = scale;
		float TargetScale = endZoom/startZoom*scale;
		float distScale = Mathf.Abs(TargetScale-scale);

        while(t < 1)
        {
            yield return null;
            t += Time.deltaTime / time;
            cam.orthographicSize = startZoom+(dist*t);
			GetComponent<FollowScript>().minHeight = (dist*t);
			
			Cover.transform.localScale = new Vector3(scale+distScale*t,scale+distScale*t,scale+distScale*t);
			Background.transform.localScale = new Vector3(scale+distScale*t,scale+distScale*t,scale+distScale*t);
        }
		cam.orthographicSize = endZoom;
		GetComponent<FollowScript>().minHeight = dist;

		Cover.transform.localScale = new Vector3(TargetScale,TargetScale,TargetScale);
		Background.transform.localScale = new Vector3(TargetScale,TargetScale,TargetScale);

        yield return null;
    }
	void FixedUpdate()
	{
		// Shake
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			if(follow){
				if(centerTarget && Target2 && Target1){
					Vector3 center = Vector3.Lerp(Target1.position, Target2.position,.5f);
					FollowPos = new Vector3(center.x,center.y,transform.position.z);
				}else if(Target1){
					FollowPos = Target1.position;
				}else if(Target2){
					FollowPos = Target2.position;
				}
				originalPos = transform.position;

				// Define a target position above and behind the target transform
				Vector3 targetPosition = new Vector3(FollowPos.x, FollowPos.y, transform.position.z);

				// Smoothly move the camera towards that target position
				transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
			}
		}
	}
}
