shader_type canvas_item;

uniform vec4 hurt_color = vec4(1.0,1.0,1.0,1.0);
uniform bool is_hurt = false;

void fragment() {
	vec4 color = texture(TEXTURE, UV);
	
	if(is_hurt)
	{
		color.rgb = mix(color.rgb, hurt_color.rgb, 0.5);
	}
	
	COLOR = color;
}

