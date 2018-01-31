// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Shaders101/my2shader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DisplaceTex("Displacement Texture", 2D) = "white" {}
		_Magnitude("Magnitude", Range(0,1)) = 1
		_Radius1("Radius1", Range(0,2500)) = 1200
		_Radius2("Radius2", Range(0,2500)) = 2000
		_Target ("Target Position", Vector) = (0, 0, 0, 0)
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float2 screenpos : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.screenpos = ComputeScreenPos(o.vertex);
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _DisplaceTex;
			float _Magnitude;
			float _Radius1;
			float _Radius2;
            float4 _Target;
            
			float4 frag (v2f i) : SV_Target
			{
			    
                float2 worldpos = i.screenpos * _ScreenParams.xy;

                float2 dist =  worldpos - _Target;
                float d = dist.x*dist.x +dist.y*dist.y;
                float amount;
                    
                if( d < _Radius1 ){
                    amount = 0;
                }else{
                    amount =  1-clamp( (d-_Radius1)/(_Radius2-_Radius1), 0,1);
                }
                amount *= _Magnitude;
                        
                float2 uv2 = i.uv + float2(_SinTime.x, _SinTime.x);
				float2 disp = tex2D(_DisplaceTex, uv2).xy;
				disp = disp*amount*_Magnitude;            
    
                
				float4 col = tex2D(_MainTex, i.uv+disp);
				
				/*if(  frac(dist.x+dist.y/34) > 1-amount ){
				    return float4(0.74,0.15,0.2,1);
				} */
				//col.xy += disp;
				return col;
			}
			ENDCG
		}
	}
}
