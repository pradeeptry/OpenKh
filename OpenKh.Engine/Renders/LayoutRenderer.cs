﻿using OpenKh.Engine.Renders;
using OpenKh.Kh2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenKh.Engine.Renderers
{
    public class LayoutRenderer
    {
        private readonly Layout layout;
        private readonly ISpriteDrawing drawing;
        private readonly ISpriteTexture[] surfaces;
        private int selectedSequenceGroupIndex;
        private IDebugLayoutRenderer _debugLayoutRenderer;

        public int SelectedSequenceGroupIndex
        {
            get => selectedSequenceGroupIndex;
            set
            {
                if (value < 0 || value >= layout.SequenceGroups.Count)
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "Cannot be negative or greater than the amount of sequence groups.");

                selectedSequenceGroupIndex = value;
            }
        }

        public int FrameIndex { get; set; }

        public LayoutRenderer(Layout layout, ISpriteDrawing drawing, IEnumerable<ISpriteTexture> surfaces)
        {
            this.layout = layout;
            this.drawing = drawing;
            this.surfaces = surfaces.ToArray();
            _debugLayoutRenderer = new DefaultDebugLayoutRenderer();
        }

        public void SetDebugLayoutRenderer(IDebugLayoutRenderer debugLayoutRenderer) =>
            _debugLayoutRenderer = debugLayoutRenderer;

        public void Draw()
        {
            if (!_debugLayoutRenderer.IsSequenceGroupVisible(selectedSequenceGroupIndex))
                return;
            DrawLayoutGroup(layout.SequenceGroups[selectedSequenceGroupIndex]);
        }

        private void DrawLayoutGroup(Layout.SequenceGroup l2)
        {
            var index = l2.L1Index;
            var count = l2.L1Count;
            for (var i = 0; i < count; i++)
            {
                if (!_debugLayoutRenderer.IsSequencePropertyVisible(index + i))
                    continue;
                DrawLayout(layout.SequenceProperties[index + i]);
            }
        }

        private void DrawLayout(Layout.SequenceProperty l1)
        {
            var currentFrameIndex = FrameIndex - l1.ShowAtFrame;
            if (currentFrameIndex < 0)
                return;

            var sequence = layout.SequenceItems[l1.SequenceIndex];
            var surface = surfaces[l1.TextureIndex];
            var sequenceRenderer = new SequenceRenderer(sequence, drawing, surface);
            sequenceRenderer.Draw(l1.AnimationGroup, currentFrameIndex, l1.PositionX, l1.PositionY);
        }
    }
}
