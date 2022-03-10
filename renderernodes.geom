#version 450 core

layout(points) in;
layout(line_strip, max_vertices = 10) out;

uniform float uRadius;
uniform mat4 uViewProjectionMatrix;

void main()
{
    // front
    gl_Position = (gl_in[0].gl_Position + vec4(uRadius, uRadius, uRadius, 0.0)) * uViewProjectionMatrix;
    EmitVertex();

    gl_Position = (gl_in[0].gl_Position + vec4(-uRadius, uRadius, uRadius, 0.0)) * uViewProjectionMatrix;
    EmitVertex();
    
    gl_Position = (gl_in[0].gl_Position + vec4(-uRadius, -uRadius, uRadius, 0.0)) * uViewProjectionMatrix;
    EmitVertex();
    
    gl_Position = (gl_in[0].gl_Position + vec4(uRadius, -uRadius, uRadius, 0.0)) * uViewProjectionMatrix;
    EmitVertex();

    gl_Position = (gl_in[0].gl_Position + vec4(uRadius, uRadius, uRadius, 0.0)) * uViewProjectionMatrix;
    EmitVertex();
    EndPrimitive();

    // back
    gl_Position = (gl_in[0].gl_Position + vec4(uRadius, uRadius, -uRadius, 0.0)) * uViewProjectionMatrix;
    EmitVertex();

    gl_Position = (gl_in[0].gl_Position + vec4(-uRadius, uRadius, -uRadius, 0.0)) * uViewProjectionMatrix;
    EmitVertex();
    
    gl_Position = (gl_in[0].gl_Position + vec4(-uRadius, -uRadius, -uRadius, 0.0)) * uViewProjectionMatrix;
    EmitVertex();
    
    gl_Position = (gl_in[0].gl_Position + vec4(uRadius, -uRadius, -uRadius, 0.0)) * uViewProjectionMatrix;
    EmitVertex();

    gl_Position = (gl_in[0].gl_Position + vec4(uRadius, uRadius, -uRadius, 0.0)) * uViewProjectionMatrix;
    EmitVertex();
    EndPrimitive();
}