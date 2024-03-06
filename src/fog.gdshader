shader_type spatial;
render_mode unshaded, cull_disabled;

uniform sampler2D depth_texture : hint_depth_texture, filter_linear_mipmap;
uniform sampler2D screen_texture : hint_screen_texture, filter_linear_mipmap;

uniform float density : hint_range(0.001, 1.0, 0.01) = 0.1;
uniform vec4 fog_color;

varying mat4 CAMERA;

void vertex() {
	// Called for every vertex the material is visible on.
	POSITION = vec4(VERTEX, 1.0);
	CAMERA = INV_VIEW_MATRIX;
}

void fragment() {
	// Called for every pixel the material is visible on.
	vec3 screen_color = texture(screen_texture, SCREEN_UV).rgb;
	float depth = texture(depth_texture, SCREEN_UV).x;
  	vec3 ndc = vec3(SCREEN_UV * 2.0 - 1.0, depth);
	
	vec4 world = CAMERA * INV_PROJECTION_MATRIX * vec4(ndc, 1.0);
  	vec3 world_position = world.xyz / world.w;
	
	vec4 view = INV_PROJECTION_MATRIX * vec4(ndc, 1.0);
	view.xyz /= view.w;
	float linear_depth = -view.z; 
	
	//float fog_intensity = linear_depth * amount; //linear fog
	float fog_intensity = 1.0 / pow(2.0f,pow(linear_depth * density, 2.0f));
	
	//fog_intensity = clamp(fog_intensity *  1.0 / pow((world_position.y), 2.0), 0.0f, 1.0f);
	
	//float fog_factor = clamp(linear_depth * fog_intensity, 0.0, 1.0);
	vec4 fogged_color = mix(fog_color, vec4(screen_color, 1.0), fog_intensity);
	
	ALBEDO = fogged_color.rgb; // we then apply the fog color and alpha to the albedo and alpha channels
	//ALPHA = fogged_color.a;
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}
