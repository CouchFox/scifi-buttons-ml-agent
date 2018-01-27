using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
[ExecuteInEditMode]
public class ObliqueCamera : MonoBehaviour {

	public Vector2 obliqueness = Vector2.zero;

	private Camera thisCamera;
	private Matrix4x4 targetMatrix;
	private Vector2 baseAspectScale;
	private float dollyZoomDenominatorCache;
	private float invertedNormalizedDollyZoomFocalDistance;
	private Matrix4x4 pointToRayMatrix;
	private Vector3 pointConversionPosition;


	public void Awake () {
		thisCamera = GetComponent (typeof(Camera)) as Camera;

	}

	public void OnEnable () {
		targetMatrix = thisCamera.projectionMatrix;

		baseAspectScale.x = targetMatrix.m00;
		baseAspectScale.y = targetMatrix.m11;
	}

	public void OnDisable () {
		thisCamera.ResetProjectionMatrix ();
	}

	public void OnPreCull () {
		
		thisCamera.ResetProjectionMatrix ();
		targetMatrix = thisCamera.projectionMatrix;

		baseAspectScale.x = targetMatrix.m00;
		baseAspectScale.y = targetMatrix.m11;


		if (thisCamera.orthographic) {
			targetMatrix.m02 = obliqueness.x / thisCamera.orthographicSize / thisCamera.aspect;
			targetMatrix.m12 = obliqueness.y / thisCamera.orthographicSize;
		} else {
			targetMatrix.m02 = obliqueness.x * 2;
			targetMatrix.m12 = obliqueness.y * 2;
		}
		thisCamera.projectionMatrix = targetMatrix;
	}


}