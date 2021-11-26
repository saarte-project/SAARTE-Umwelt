Shader "Unlit/UndergroundPlane"
{
    Properties
    {
		[HDR]_Color("Main Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

		Stencil{
			Ref 0
			Comp NotEqual
			Pass keep
		}

        Pass
        {
			SetTexture[_MainText] {
				constantColor[_Color]
				Combine texture * constant, texture * constant
			}
        }
    }
}
