using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Path : MonoBehaviour {

	// Properties
	//

	public List<Vector3> PathNodes = new List<Vector3>();

	// Methods
	//

	// Use this for initialization
	void Start () {
	
	}



	#if UNITY_EDITOR
	public void OnDrawGizmos(){

		if(Selection.activeGameObject == this.gameObject){

			// If there is some point in the table
			if(this.PathNodes.Count > 1){

				// For every vector2
				for(int i=0; i< this.PathNodes.Count-1; ++i){
					Gizmos.color = Color.white;
					Gizmos.DrawLine(new Vector3(this.PathNodes[i].x, this.PathNodes[i].y, 0), new Vector3(this.PathNodes[i+1].x, this.PathNodes[i+1].y, 0));

				}

			}
		}

	}
	#endif

}

#if UNITY_EDITOR
// 
[CustomEditor(typeof(Path))]
public class DrawPathPoints : Editor {

	public Path path;



	public void OnSceneGUI(){

		this.path = target as Path;

		// If there is some point in the table
		if(this.path.PathNodes.Count > 1){

			// For every vector2
			for(int i=0; i< this.path.PathNodes.Count; ++i){

				EditorGUI.BeginChangeCheck();
				Vector3 res = Handles.DoPositionHandle(new Vector3(this.path.PathNodes[i].x, this.path.PathNodes[i].y ,0f), Quaternion.identity);
				if (EditorGUI.EndChangeCheck()) {
					Undo.RecordObject(this.path, "Move Point");
					EditorUtility.SetDirty(this.path);
					this.path.PathNodes[i] = this.path.gameObject.transform.InverseTransformPoint(res);
				}

			}

		}

	}

}
#endif
