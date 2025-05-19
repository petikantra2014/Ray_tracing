using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;


namespace Ray_Tracing
{
    class View
    {
        private int BasicProgramID;
        private Vector3[] vertdata;
        private int attribute_vpos;
        private int uniform_pos;
        private Vector3 campos;
        private int uniform_aspect;
        private double aspect;
        

        void loadShader(String filename, ShaderType type, uint program, out uint address)
        {
            address = (uint)GL.CreateShader(type);
            using (System.IO.StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource((int)address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog((int)address));
        }
        void InitShaders()
        {
            BasicProgramID = GL.CreateProgram();
            uint BasicVertexShader;
            loadShader("C:\\Users\\T2215\\Desktop\\raytracing.vert", ShaderType.VertexShader, (uint)BasicProgramID, out BasicVertexShader);
            uint BasicFragmentShader;
            loadShader("C:\\Users\\T2215\\Desktop\\raytracing.frag", ShaderType.FragmentShader, (uint)BasicProgramID, out BasicFragmentShader);
            GL.LinkProgram(BasicProgramID);
            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));
        }

        void Buffer()
        {
            int vbo_position;
            vertdata = new Vector3[] {
            new Vector3(-1f, -1f, 0f),
            new Vector3( 1f, -1f, 0f),
            new Vector3( 1f, 1f, 0f),
            new Vector3(-1f, 1f, 0f) };

            GL.GenBuffers(1, out vbo_position);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length *
                     Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.Uniform3(uniform_pos, campos);
            GL.Uniform1(uniform_aspect, aspect);
            GL.UseProgram(BasicProgramID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}

struct SCamera
{
    vec3 Position;
    vec3 View;
    vec3 Up;
    vec3 Side;
    vec2 Scale;
}

struct SRay
{
    vec3 Origin;
    vec3 Direction;
}