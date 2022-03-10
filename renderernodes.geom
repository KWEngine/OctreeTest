#version 450 core

layout(points) in;
layout(line_strip, max_vertices = 20) out;

uniform float uRadius;
uniform mat4 uViewProjectionMatrix;

void main()
{
    // front
    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(uRadius, uRadius, uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(-uRadius, uRadius, uRadius, 0.0));
    EmitVertex();
    
    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(-uRadius, -uRadius, uRadius, 0.0));
    EmitVertex();
    
    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(uRadius, -uRadius, uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(uRadius, uRadius, uRadius, 0.0));
    EmitVertex();
    EndPrimitive();

    // back
    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(uRadius, uRadius, -uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(uRadius, -uRadius, -uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(-uRadius, -uRadius, -uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(-uRadius, uRadius, -uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(uRadius, uRadius, -uRadius, 0.0));
    EmitVertex();
    EndPrimitive();

    // left
    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(-uRadius, uRadius, -uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(-uRadius, -uRadius, -uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(-uRadius, -uRadius, uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(-uRadius, uRadius, uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(-uRadius, uRadius, -uRadius, 0.0));
    EmitVertex();
    EndPrimitive();

    // left
    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(uRadius, uRadius, -uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(uRadius, -uRadius, -uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(uRadius, -uRadius, uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(uRadius, uRadius, uRadius, 0.0));
    EmitVertex();

    gl_Position = uViewProjectionMatrix * (gl_in[0].gl_Position + vec4(uRadius, uRadius, -uRadius, 0.0));
    EmitVertex();
    EndPrimitive();
}