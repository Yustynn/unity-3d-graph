Shader "UCLA Game Lab/Wireframe/Double-Sided"
{
	Properties
	{
		_ColorBase ("Base Color", Color) = (1,1,1,1)
		_Color1 ("Level 1 Color", Color) = (1,1,1,1)
		_Color2 ("Level 2 Color", Color) = (1,1,1,1)
		_Color3 ("Level 3 Color", Color) = (1,1,1,1)

		_HeightBase("Base Height", Float) = 0
		_Height1("Height Level 1", Float) = 10
		_Height2("Height Level 2", Float) = 20
		_Height3("Height Level 3", Float) = 30

		_MainTex ("Main Texture", 2D) = "white" {}
		_Thickness ("Thickness", Float) = 1
	}

	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }




		// First pass that renders the back faces of the model (cull front faces)
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Cull Front
			LOD 200

			CGPROGRAM
				#pragma target 5.0
				#include "UnityCG.cginc"
				#include "UCLA GameLab Wireframe Functions.cginc"
				#pragma vertex vert
				#pragma fragment frag
				#pragma geometry geom




				struct Input
				{
					float2 uv_MainTex;
					float3 worldPos;
				};

				// Vertex Shader
				UCLAGL_v2g vert(appdata_base v)
				{
					return UCLAGL_vert(v);
				}

				// Geometry Shader
				[maxvertexcount(3)]
				void geom(triangle UCLAGL_v2g p[3], inout TriangleStream<UCLAGL_g2f> triStream)
				{
					UCLAGL_geom( p, triStream);
				}

				// Fragment Shader
				float4 frag(UCLAGL_g2f input) : COLOR
				{
					return UCLAGL_frag(input);
				}

			ENDCG
		}
		// Second pass to render the fronts of polygons.
		// Guarantees render order of back then front to avoid render artifacts
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Cull Back
			LOD 200

			CGPROGRAM
				#pragma target 5.0
				#include "UnityCG.cginc"
				#include "UCLA GameLab Wireframe Functions.cginc"
				#pragma vertex vert
				#pragma fragment frag
				#pragma geometry geom

				// Vertex Shader
				UCLAGL_v2g vert(appdata_base v)
				{
					return UCLAGL_vert(v);
				}

				// Geometry Shader
				[maxvertexcount(3)]
				void geom(triangle UCLAGL_v2g p[3], inout TriangleStream<UCLAGL_g2f> triStream)
				{
					UCLAGL_geom( p, triStream);
				}

				// Fragment Shader
				float4 frag(UCLAGL_g2f input) : COLOR
				{
					return UCLAGL_frag(input);
				}

			ENDCG
		}
	}
}
