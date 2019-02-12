Shader "Custom/Water"
{
	Properties
	{
		_DepthGradientShallow("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
		_DepthGradientDeep("Depth Gradient Deep", Color) = (0.086, 0.407, 1, 0.749)
		_DepthMaxDistance("Depth Maximum Distance", Float) = 1
		_SurfaceNoise("Surface Noise", 2D) = "white" {}
		_SurfaceNoiseCutoff("Surface Noise Cutoff", Range(0, 1)) = 0.777
		_FoamDistance("Foam Distance", Float) = 0.4
		_SurfaceNoiseScroll("Surface Noise Scroll Amount", Vector) = (0.03, 0.03, 0, 0)
		_SurfaceDistortion("Surface Distortion", 2D) = "white" {}
		_SurfaceDistortionAmount("Surface Distortion Amount", Range(0, 1)) = 0.27
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
			}

			Pass
			{
				Blend SrcAlpha OneMinusSrcAlpha
				ZWrite Off

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				// Properties
				float4 _DepthGradientShallow;
				float4 _DepthGradientDeep;
				float _DepthMaxDistance;

				float4 _SurfaceNoise_ST;
				float _SurfaceNoiseCutoff;
				float2 _SurfaceNoiseScroll;

				float _FoamDistance;

				float4 _SurfaceDistortion_ST;
				float _SurfaceDistortionAmount;

				sampler2D _SurfaceDistortion;
				sampler2D _CameraDepthTexture;
				sampler2D _SurfaceNoise;

				struct appdata {
					float4 vertex : POSITION;
					float4 uv : TEXCOORD0;
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					float4 screenPosition : TEXCOORD2;
					float2 noiseUV : TEXCOORD0;
					float2 distortUV : TEXCOORD1;
				};

				v2f vert(appdata i)
				{
					v2f o;

					o.vertex = UnityObjectToClipPos(i.vertex);
					o.vertex.y += sin(_Time * i.vertex.x ) * 0.1;

					o.screenPosition = ComputeScreenPos(o.vertex);
					o.noiseUV = TRANSFORM_TEX(i.uv, _SurfaceNoise);
					o.distortUV = TRANSFORM_TEX(i.uv, _SurfaceDistortion);

					return o;
				}

				float4 frag(v2f i) : COLOR
				{
					float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
					float existingDepthLinear = LinearEyeDepth(existingDepth01);

					float depthDifference = existingDepthLinear - i.screenPosition.w;

					float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
					float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);

					float2 distortSample = (tex2D(_SurfaceDistortion, i.distortUV).xy * 2 - 1) * _SurfaceDistortionAmount;

					float2 noiseUV = float2((i.noiseUV.x + _Time.y * _SurfaceNoiseScroll.x) + distortSample.x, (i.noiseUV.y + _Time.y * _SurfaceNoiseScroll.y) + distortSample.y);
					float surfaceNoiseSample = tex2D(_SurfaceNoise, noiseUV).r;
					float foamDepthDifference01 = saturate(depthDifference / _FoamDistance);
					float surfaceNoiseCutoff = foamDepthDifference01 * _SurfaceNoiseCutoff;

					float surfaceNoise = surfaceNoiseSample > surfaceNoiseCutoff ? 1 : 0;

					return waterColor + surfaceNoise;
				}
				ENDCG
			}
		}
}