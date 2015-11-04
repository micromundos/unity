using UnityEngine;
using System.Collections;
using System.IO;

public class SCalibrationLoader : MonoBehaviour {

	// Use this for initialization
	///Users/micromundos/dev/of/apps/micromundos/data/calib
	const string	data_path = "/Users/micromundos/dev/of/apps/micromundos/data/calib/";
	public Matrix4x4	tags_matrix;
	public Matrix4x4	cl_projection_matrix;
	public Matrix4x4	cl_model_view_matrix;
	public	bool	ready = false;

	void Start () {
		tags_matrix = new Matrix4x4 ();
		cl_projection_matrix = new Matrix4x4 ();
		StreamReader tags_calib_stream = File.OpenText (data_path + "unity_tags.yml");
		string tags_calib_string = tags_calib_stream.ReadToEnd ();
		//	tags
		string[] lines = tags_calib_string.Split ('\n');
		tags_matrix [0, 0] = float_from_line (lines [1]);
		tags_matrix [1, 0] = float_from_line (lines [2]);
		tags_matrix [2, 0] = float_from_line (lines [3]);
		tags_matrix [3, 0] = 0.0f;
		tags_matrix [0, 1] = float_from_line (lines [4]);
		tags_matrix [1, 1] = float_from_line (lines [5]);
		tags_matrix [2, 1] = float_from_line (lines [6]);
		tags_matrix [3, 1] = 0.0f;
		tags_matrix [0, 2] = float_from_line (lines [7]);
		tags_matrix [1, 2] = float_from_line (lines [8]);
		tags_matrix [2, 2] = float_from_line (lines [9]);
		tags_matrix [3, 2] = 0.0f;
		tags_matrix [0, 3] = 0.0f;
		tags_matrix [1, 3] = 0.0f;
		tags_matrix [2, 3] = 0.0f;
		tags_matrix [3, 3] = 1.0f;
		//	camara lucida
		StreamReader cl_calib_stream = File.OpenText (data_path + "unity_cml.yml");
		string cl_calib_string = cl_calib_stream.ReadToEnd ();
		lines = cl_calib_string.Split('\n');
		cl_projection_matrix = PerspectiveOffCenter(get_val_by_id(lines,"proj_frustum_left"), get_val_by_id(lines,"proj_frustum_right"), get_val_by_id(lines,"proj_frustum_bottom"),get_val_by_id(lines,"proj_frustum_top"), get_val_by_id(lines,"proj_near"),get_val_by_id(lines,"proj_far"));

		//Debug.Log ("*** MODEL VIEW ***");
		cl_model_view_matrix = new Matrix4x4 ();
		for (int i=0; i<16; i++) {
			cl_model_view_matrix[i] = get_val_by_id(lines,"proj_modelview_matrix_" + i.ToString());
			//Debug.Log(cl_model_view_matrix[i]);
		}


		ready = true;
	}

	float	float_from_line(string _line)
	{
		return float.Parse(_line.Split(':')[1]);
	}

	string	id_from_line(string	_line)
	{
		return _line.Split(':')[0];
	}

	float	get_val_by_id(string[]	_lines,string	_id)
	{
		int i = 0;
		int found = -1;
		float ret = 0.0f;
		while (i < _lines.Length && found == -1) {
			if(id_from_line(_lines[i]).Equals(_id))
			{
				found = i;
			}
			i++;
		}
		if (found != -1) {
			ret = float_from_line(_lines[found]);
		}
		//Debug.Log (_id + " = " + ret.ToString ());
		return ret;

	}

	static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far) {
		float x = 2.0F * near / (right - left);
		float y = 2.0F * near / (top - bottom);
		float a = (right + left) / (right - left);
		float b = (top + bottom) / (top - bottom);
		float c = -(far + near) / (far - near);
		float d = -(2.0F * far * near) / (far - near);
		float e = -1.0F;
		Matrix4x4 m = new Matrix4x4();
		m[0, 0] = x;
		m[0, 1] = 0;
		m[0, 2] = a;
		m[0, 3] = 0;
		m[1, 0] = 0;
		m[1, 1] = y;
		m[1, 2] = b;
		m[1, 3] = 0;
		m[2, 0] = 0;
		m[2, 1] = 0;
		m[2, 2] = c;
		m[2, 3] = d;
		m[3, 0] = 0;
		m[3, 1] = 0;
		m[3, 2] = e;
		m[3, 3] = 0;
		return m;
	}




	// Update is called once per frame
	void Update () {
		
	}
}
