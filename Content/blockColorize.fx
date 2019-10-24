#if OPENGL
	#define SV_POSITION POSITION
	#define PS_SHADERMODEL ps_3_0
#else
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler2D TextureSampler;
float externalColorR;
float externalColorG;
float externalColorB;

float4 MainPS(float2 texCoord : TEXCOORD0, float4 color : COLOR0) : COLOR
{
    float4 externalColor = float4(externalColorR, externalColorG, externalColorB, 0.0);
	float4 baseColor = tex2D(TextureSampler, texCoord);	
    //if (any(baseColor != float4(1.0, 1.0, 1.0, 0.0)))
    //    return baseColor;
    //else
        return externalColor;
}

technique BasicColorDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};