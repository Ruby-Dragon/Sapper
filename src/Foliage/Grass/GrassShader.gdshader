shader_type spatial;
render_mode diffuse_burley, cull_disabled;

uniform float Angle;

varying float Wind;

void vertex() {
	
	//get the position in world space of the object
	vec3 WorldPos = (MODEL_MATRIX * vec4(VERTEX, 1.0)).xyz;
	
	//get UV coords in world space
	vec2 WorldUV = WorldPos.xz;
	
	//Direction of wind movement
	vec2 Direction = normalize(vec2(cos(Angle * PI / 180.0), sin(Angle * PI / 180.0)));
	
	//use waves generated by FFT as wind strength
	//float WindMovement = pow(VERTEX.y, 2) * normalize(texture(Waves, WorldUV)).r * 0.1;
	
	//sin based wind generator
	float WindMovement = pow(VERTEX.y, 2) * (sin((WorldUV.x * Direction.x + WorldUV.y * Direction.y + TIME) * 1.0) + 1.0) * 0.1;
	
	//debug
	Wind = WindMovement;

	//add the wind movement to the vertex location in given direction
	VERTEX.xz += ((WindMovement) * -Direction);
}

void fragment() {
	vec3 light = vec3(0.4,0.8,0.4);
	
	vec3 dark = vec3(0.2,0.6,0.2);
	
	ALBEDO = mix(light, dark, UV.y*3.0);
	
	RIM = 0.1;
	
	RIM_TINT = 0.5;
	
	//ALBEDO = vec3(Wind, Wind, Wind);
	
	if (!FRONT_FACING)
	{
		NORMAL = -NORMAL;
	}
	//ALBEDO = vec3(UV, 0);
}