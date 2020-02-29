using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace UWB_GraphicsLibrary
{
    /// <summary>
    /// Becareful, C# Matrix is a Struct (NOT a class!!)
    /// </summary>
    public class MatrixStack
    {
        // List pretend to be a stack
        // End of list is the top of the stack
        List<Matrix> mTheStack;

        public MatrixStack()
        {
            InitStack();
        }

        /// <summary>
        /// empty out the stack and loads a new identity
        /// </summary>
        public void InitStack()
        {
            mTheStack = new List<Matrix>();
            push(Matrix.Identity);
        }

        /// <summary>
        /// Duplicate the top
        /// </summary>
        /// <returns></returns>
        public void Push()
        {
            Matrix m = GetTopOfStack();
            mTheStack.Add(m);
        }

        /// <summary>
        /// Push matrix to the top of the stack
        /// </summary>
        /// <param name="m"></param>
        public void push(Matrix m)
        {
            mTheStack.Add(m);
        }

        /// <summary>
        /// get a copy of the top without touching the stack
        /// </summary>
        /// <returns></returns>
        public Matrix GetTopOfStack()
        {
            Debug.Assert(mTheStack.Count >= 1);
            return mTheStack[mTheStack.Count - 1];
        }

        /// <summary>
        /// Overwrite the top of the stack
        /// </summary>
        /// <param name="m"></param>
        public void OverwriteTop(Matrix m)
        {
            Debug.Assert(mTheStack.Count >= 1);
            mTheStack[mTheStack.Count - 1] = m;
        }

        /// <summary>
        /// remove top of stack and returns
        /// </summary>
        /// <returns></returns>
        public Matrix Pop()
        {
            Debug.Assert(mTheStack.Count >= 1);
            Matrix m = GetTopOfStack();
            mTheStack.RemoveAt(mTheStack.Count - 1);
            return m;
        }

        /// <summary>
        /// Top <- m * Top
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public void ConcateToTop(Matrix m)
        {
            Debug.Assert(mTheStack.Count >= 1);
            mTheStack[mTheStack.Count - 1] = m * GetTopOfStack();
        }

        /// Transformation support
        public void Translation(Vector3 t)
        {
            Matrix m = Matrix.CreateTranslation(t);
            ConcateToTop(m);
        }
        public void Scale(Vector3 s)
        {
            Matrix m = Matrix.CreateScale(s);
            ConcateToTop(m);
        }
        public void Rotate(Quaternion q)
        {
            Matrix m = Matrix.CreateFromQuaternion(q);
            ConcateToTop(m);
        }
    }
}