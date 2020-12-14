sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float coloralpha;
float hue2rgb(float p, float q, float t){
            if(t < 0) t += 1;
            if(t > 1) t -= 1;
            if(t < 0.166f) return p + (q - p) * 6.0f * t;
            if(t < 0.5f) return q;
            if(t < 0.66f) return p + (q - p) * (0.66f - t) * 6.0f;
            return p;
        }
float3 hslToRgb(float h, float s, float l){
    float r, g, b;
        float q = l < 0.5 ? l * (1 + s) : (l + s) - (l * s);
        float p = (2 * l) - q;
        r = hue2rgb(p, q, h + 0.33f); 
        g = hue2rgb(p, q, h);
        b = hue2rgb(p, q, h - 0.33f); 
	return float3(r,g,b);
}
float4 White(float2 coords : TEXCOORD0) : COLOR0
{
    float3 prismColor = hslToRgb(((coloralpha / 9)  + (coords.y / 1.5f)) % 1, 0.7f, 0.7f);
    float4 colour = tex2D(uImage0, coords);
        colour.rgb *= prismColor;
    colour.a *= 0.75f;

    return colour;
}

technique BasicColorDrawing
{
    pass WhiteSprite
    {
        PixelShader = compile ps_2_0 White();
    }
};