﻿#version 450 core

layout(location = 0) in vec3 aPosition;

uniform mat4 uModelMatrix;
uniform mat4 uViewProjectionMatrix;

void main()
{
	gl_Position = (uViewProjectionMatrix * uModelMatrix) * vec4(aPosition, 1.0);
}