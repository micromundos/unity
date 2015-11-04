//Camara Lucida shader
//https://github.com/chparsons/ofxCamaraLucida/blob/master/src/cml/shaders/render.h

Shader "Custom/CamaraLucidaShader" {
 Properties {
      render_tex ("Render Texture", Rect) = "white" {}
      
 }
 SubShader {
 Pass {
  GLSLPROGRAM // here begins the part in Unity's GLSL

         #ifdef VERTEX // here begins the vertex shader
		 #extension GL_EXT_gpu_shader4 : enable

		 const float epsilon = 1e-30;

         uniform sampler2D	render_tex;
 		 uniform sampler2D	depth_tex;
 		 
//		 uniform float near;
//		 uniform float far;
//		 uniform float far_clamp;
//		 uniform float width;
//		 uniform float height;
//		 uniform float fx;
//		 uniform float fy;
//		 uniform float cx;
//		 uniform float cy;
//		 uniform float xoff;
//		 uniform float tex_width;
//		 uniform float tex_height;
//		 uniform float depth_cam_far_clamp;
		 
		 float unity_plane_z_offset = 1000.0;
		 
		 float width = 640.;
		 float height = 480.;
		 float near = 200.;
		 float far = 6000.;
		 float cx = 300.;
		 float cy = 260.;
		 float fx = 536.;
		 float fy = 542.;
		 float xoff = 8.;
		 float far_clamp = 5000.;
		 float tex_width = 1536.;
		 float tex_height = 1152.;
		 
//		 varying vec3 p3;
		 
		 vec3 unproject( vec2 p2, float z ) 
		 {
		 	return vec3((p2.x + xoff - cx) * z / fx, (p2.y - cy) * z / fy, z);
		 }
		 
 		 float lerp2d( float x, float x1, float x2, float y1, float y2 ) 
		 {
			 return (x-x1) / (x2-x1) * (y2-y1) + y1;
		 }
 		 
         void main() // all vertex shaders define a main() function
         {
         	vec3 xxx = gl_Vertex.xyz; // WTF ????!!!!
         	
        	gl_TexCoord[0] = gl_MultiTexCoord0;

            vec2 tc = gl_TexCoord[0].st;
            vec2 d2 = vec2(tc.x * width, tc.y * height);
			
			float zmm = unity_plane_z_offset;
//			float zmm = (texture2D( depth_tex, d2 ).r * far_clamp);
//			zmm = clamp( ( zmm < epsilon ? far_clamp : zmm ), 0.0, far_clamp );
			vec3 p3 = unproject(d2,zmm);
			
			p3.z -= unity_plane_z_offset;
			
//			p3 = vec3(
//				lerp2d(tc.x, 0.0, 1.0, -0.5, 0.5),
//				lerp2d(tc.y, 0.0, 1.0, -0.5, 0.5),
//				lerp2d(p2.x, 0.0, 640.0, -0.5, 0.5),
//				lerp2d(p2.y, 0.0, 480.0, -0.5, 0.5),
//				zmm);
//			p3.x *= 1000.;
//			p3.y *= 1000.;
			
			vec4 p4 = vec4( p3.x, p3.y, p3.z, 1.0 );
		
			gl_Position = gl_ModelViewProjectionMatrix * p4;
			gl_FrontColor = gl_Color;
         }

         #endif // here ends the definition of the vertex shader


         #ifdef FRAGMENT // here begins the fragment shader

 		 uniform sampler2D	render_tex;
 		 uniform sampler2D  depth_tex;
 		 
//		 varying vec3 p3;
 		 
 		 
 		 float lerp2d( float x, float x1, float x2, float y1, float y2 ) 
		 {
			 return (x-x1) / (x2-x1) * (y2-y1) + y1;
		 }
 		 
         void main()
         {
         	
         	vec4 render_color = texture2D(render_tex,gl_TexCoord[0].st);
//         	vec2 tc = gl_TexCoord[0].st;
//            vec4 render_color = vec4(
////	            lerp2d(tc.x, 0.0, 1.0, 0.0, 1.0),
////	            lerp2d(tc.y, 0.0, 1.0, 0.0, 1.0),
////	            lerp2d(glvertex.x, -500., 500., 0.0, 1.0),
////	            lerp2d(glvertex.y, -500., 500., 0.0, 1.0),
//	            lerp2d(p3.x, -500., 500., 0.0, 1.0),
//	            lerp2d(p3.y, -500., 500., 0.0, 1.0),
////	            lerp2d(p3.x, 0.0, 1.0, 1.0, 0.0),
////	            lerp2d(p3.y, 0.0, 1.0, 1.0, 0.0),
//	            0.0,
//	            1.0);
         	
         	gl_FragColor = render_color;
         }

         #endif // here ends the definition of the fragment shader

         ENDGLSL // here ends the part in GLSL 
 	}
 }
}
