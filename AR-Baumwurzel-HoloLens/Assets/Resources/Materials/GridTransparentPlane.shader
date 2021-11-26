Shader "Unlit/GridTransparentPlane"
{
	Properties{
		_Color("Main Color", Color) = (1,1,1,0)
		_MainTex("Texture", 2D) = "black" {}
	}

		SubShader{
			Tags {"Queue" = "Geometry" "RenderType" = "Transparent"}

			ZWrite On
			Blend SrcAlpha OneMinusSrcAlpha

			Stencil{
				Ref 0
				Comp NotEqual
				Pass keep
			}

			Pass {
				SetTexture[_MainText] {
						constantColor[_Color]
						Combine texture * constant, texture * constant
				}
			}
		}
}
