Shader "Unlit/PhantomObject"
{
	Properties{
		_Color("Main Color", Color) = (1,1,1,0)
		_MainTex("Texture", 2D) = "white" {}
	}

	SubShader{
		Tags {"Queue" = "Geometry" "RenderType" = "Transparent"}

		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			SetTexture[_MainText] {
			constantColor[_Color]
			Combine texture * constant, texture * constant
			}
		}
	}
}
