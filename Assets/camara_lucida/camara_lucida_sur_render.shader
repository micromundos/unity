Shader "Custom/SurfacesViz" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	 
	SubShader {
	 	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
	 
 Pass {
  GLSLPROGRAM // here begins the part in Unity's GLSL

         #ifdef VERTEX // here begins the vertex shader
		 #extension GL_EXT_gpu_shader4 : enable

		
		
         uniform sampler2D	_MainTex;
 
         void main() // all vertex shaders define a main() function
         {
         
         	
        	gl_TexCoord[0] = gl_MultiTexCoord0;

      
			gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
			gl_FrontColor = gl_Color;
         }

         #endif // here ends the definition of the vertex shader


         #ifdef FRAGMENT // here begins the fragment shader

 		 uniform sampler2D	_MainTex;

 		
         void main()
         {
         	
         	vec2 tc = gl_TexCoord[0].st;
         	vec4 render_color = texture2D(_MainTex, tc);
         	//render_color.g = 0.0;
			render_color.a = /*render_color.r < 0.1 ? 0.0 : 1.0*/ render_color.r;
         	gl_FragColor = render_color;
         }

         #endif // here ends the definition of the fragment shader

         ENDGLSL // here ends the part in GLSL 
 	}
 }
}
