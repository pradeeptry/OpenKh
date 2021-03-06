﻿using OpenKh.Engine.Maths;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenKh.Engine.Parsers.Kddf2
{
    public class IndexAssignment
    {
        public Vector2 uv;
        public int indexToVertexAssignment;
        public int vertexFlag;

        public IndexAssignment(Vector2 uv, int localVertexIndex, int vertexFlag)
        {
            this.uv = uv;
            this.indexToVertexAssignment = localVertexIndex;
            this.vertexFlag = vertexFlag;
        }
    }
}
