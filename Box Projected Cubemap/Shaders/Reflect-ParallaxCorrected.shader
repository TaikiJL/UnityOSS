Shader "Reflective/Parallax-corrected" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
    _ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
    _MainTex ("Base (RGB) RefStrength (A)", 2D) = "white" {}
    _Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
    _BumpMap ("Normalmap", 2D) = "bump" {}
    _BoxPosition ("Bounding Box Position", Vector) = (0, 0, 0)
    _BoxSize ("Bounding Box Size", Vector) = (10, 10, 10)
}

SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 300
    
CGPROGRAM

#pragma surface surf Lambert

sampler2D _MainTex;
sampler2D _BumpMap;
samplerCUBE _Cube;

fixed4 _Color;
fixed4 _ReflectColor;
fixed4 _BoxSize;
float4 _BoxPosition;

struct Input {
    float2 uv_MainTex;
    float2 uv_BumpMap;
    float3 worldRefl;
    fixed3 worldPos;
    float3 worldNormal;
    INTERNAL_DATA
};

void surf (Input IN, inout SurfaceOutput o) {
	// Base diffuse texture
    fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
    fixed4 c = tex * _Color;
    o.Albedo = c.rgb;
    
    fixed3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
    
    // Parallax-correction
    float3 viewDirection = IN.worldPos - _WorldSpaceCameraPos;
    float3 worldNormalDirection = IN.worldNormal;
    worldNormalDirection.xy -= n;
    float3 reflDirection = reflect(viewDirection, worldNormalDirection);
    
    float3 nReflDirection = normalize(reflDirection);
    float3 boxStart = _BoxPosition -_BoxSize/2;
    //boxStart.y = 0;
    float3 firstPlaneIntersect = (boxStart + _BoxSize - IN.worldPos) / nReflDirection;
    float3 secondPlaneIntersect = (boxStart - IN.worldPos) / nReflDirection;
    float3 furthestPlane = (nReflDirection > 0) ? firstPlaneIntersect : secondPlaneIntersect;
    float intersectDistance = min(min(furthestPlane.x, furthestPlane.y), furthestPlane.z);

    float3 intersectPosition = IN.worldPos + nReflDirection * intersectDistance;

    reflDirection = intersectPosition - (boxStart + _BoxSize/2);
    
    // Reflection display
    fixed4 reflcol = texCUBE (_Cube, reflDirection);
	reflcol *= tex.a;
	o.Emission = reflcol.rgb * _ReflectColor.rgb;
	o.Alpha = reflcol.a * _ReflectColor.a;
}

ENDCG

}
	
FallBack "Reflective/VertexLit"
}