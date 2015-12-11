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
 		 
		 uniform float near;
		 uniform float far;
		 uniform float far_clamp;
		 uniform float width;
		 uniform float height;
		 uniform float fx;
		 uniform float fy;
		 uniform float cx;
		 uniform float cy;
		 uniform float xoff;
		 uniform float tex_width;
		 uniform float tex_height;
		 
		 float unity_plane_z_offset = 1000.0;
		 
		 mat4 transform = mat4(	0.00232101, 	9.71977e-05, 	0.0, 	0.0,  // 1. column
                  				-2.38543e-05, 	0.00317804, 	0.0, 	0.0,  // 2. column
                  				3.312e-05, 		3.25951e-05, 	0.0, 	0.0,  // 3. column
                  				-0.244142, 		-0.368346, 		0.0, 	1.0); // 4. column
                  				
//         mat4 transform = mat4(	0.00232101, -2.38543e-05, 3.312e-05, -0.244142,  	// 1. column
//                  				9.71977e-05, 0.00317804, 3.25951e-05, -0.368346, 	// 2. column
//                  				0.0,        0.0,        0.0,        0.0,  			// 3. column
//                  				0.0,        0.0,        0.0,        1.0); 			// 4. column
		 
//		0.00232101, -2.38543e-05, 3.312e-05, -0.244142
//		9.71977e-05, 0.00317804, 3.25951e-05, -0.368346
//      0,        0,        0,        0
//      0,        0,        0,        1
		 
//		 varying vec3 p3;
		 
		 vec3 unproject( vec2 p2, float z ) 
		 {
//		 	camara lucida:
//		 	return vec3((p2.x + xoff - cx) * z / fx, (p2.y - cy) * z / fy, z);

//		 	ofxReprojection:			
		 	vec4 p3 = vec4( p2.x * 4./3., p2.y, z, 1.0 );
//		 	vec4 p3 = vec4( p2.x, p2.y, z, 1.0 );
		 	p3 = p3 * transform;
			p3.z = z;
			return vec3(p3);
		 }
		 
 		 float lerp2d( float x, float x1, float x2, float y1, float y2 ) 
		 {
			 return (x-x1) / (x2-x1) * (y2-y1) + y1;
		 }
 		 
         void main() // all vertex shaders define a main() function
         {
         	vec4 xxx = gl_Vertex; // WTF ????!!!!
         	
        	gl_TexCoord[0] = gl_MultiTexCoord0;

            vec2 tc = gl_TexCoord[0].st;
            vec2 d2 = vec2( (tc.x * width), (tc.y * height) );
			
//			float zmm = unity_plane_z_offset;
//			float zmm = texture2D( depth_tex, d2 ).r * far_clamp;
			float zmm = texture2D(depth_tex, tc).r * far_clamp;
//			zmm = clamp( ( zmm < epsilon ? far_clamp : zmm ), 0.0, far_clamp );
			vec3 p3 = unproject(d2,zmm);
//			vec3 p3 = vec3( tc.x * 1000.0-500.0, tc.y * 1000.0-500.0, zmm );
//			vec3 p3 = vec3(xxx);
			
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
         	
         	vec2 tc = gl_TexCoord[0].st;
         	
//         	float zmm = texture2D(depth_tex, tc).r;
//         	vec4 render_color = vec4( zmm, zmm, zmm, 1.0 );
         	
//         	vec4 render_color = texture2D(depth_tex, tc);
         	vec4 render_color = texture2D(render_tex, tc);

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
