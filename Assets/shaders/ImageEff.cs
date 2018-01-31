using UnityEngine;

[ExecuteInEditMode]
public class ImageEff : MonoBehaviour
{
	public Material EffectMaterial;
	private Transform _target;
	private float _scale;
	private Vector3 _offset;
	void Start()
	{
		_target = GameObject.FindGameObjectWithTag("Ball").transform;
		_scale = Screen.height/(2*Camera.main.orthographicSize);
		_offset = new Vector4(Screen.width / 2f, Screen.height / 2f);
	}
	
	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		if (EffectMaterial != null && _target != null)
		{
			EffectMaterial.SetVector("_Target", _target.position*_scale+_offset);
			Graphics.Blit(src, dst, EffectMaterial);
		}
	}
}