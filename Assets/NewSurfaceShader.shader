Shader "Custom/HeightDependentTint"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	_HeightMin("Height Min", Float) = -1
		_ColorMin("Tint Color At Min", Color) = (0,0,0,1)

		_Height1("Height 1", Float) = 10
		_Color1("Tint Color At Height 1", Color) = (1,1,1,1)


		_Height2("Height 2", Float) = 50
		_Color2("Tint Color At Height 2", Color) = (1,1,1,1)


		_Height3("Height 3", Float) = 200
		_Color3("Tint Color At Height 3", Color) = (1,1,1,1)
	}

		SubShader
	{
		Tags{ "RenderType" = "Opaque" }

		CGPROGRAM
#pragma surface surf Lambert

		sampler2D _MainTex;
	fixed4 _ColorMin;
	float _HeightMin;

	fixed4 _Color1;
	float _Height1;


	fixed4 _Color2;
	float _Height2;


	fixed4 _Color3;
	float _Height3;

	struct Input
	{
		float2 uv_MainTex;
		float3 worldPos;
	};

	void surf(Input IN, inout SurfaceOutput o)
	{	

		if (IN.worldPos.y > _Height3)
			o.Albedo = _Color3;
		if (IN.worldPos.y <= _Height3)
			o.Albedo = lerp(_Color2, _Color3, (IN.worldPos.y - _Height2) / (_Height3 - _Height2));
		if (IN.worldPos.y <= _Height2)
			o.Albedo = lerp(_Color1, _Color2, (IN.worldPos.y - _Height1) / (_Height2 - _Height1));
		if (IN.worldPos.y <= _Height1)
			o.Albedo = lerp(_ColorMin, _Color1, (IN.worldPos.y - _HeightMin) / (_Height1 - _HeightMin));


	}
	ENDCG
	}
		Fallback "Diffuse"
}