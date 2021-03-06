﻿using OpenKh.Imaging;
using System;

namespace OpenKh.Engine.Renders
{
    public enum BlendMode
    {
        Default,
        Add,
        Subtract,
    }

    public enum TextureWrapMode
    {
        Default = 0,
        Clamp = 1,
        Repeat = 2,
    }

    public struct ColorF
    {
        public static readonly ColorF Black = new ColorF(0.0f, 0.0f, 0.0f, 1.0f);
        public static readonly ColorF White = new ColorF(1.0f, 1.0f, 1.0f, 1.0f);

        public float R, G, B, A;

        public ColorF(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static ColorF operator +(ColorF colorA, ColorF colorB) => new ColorF
        {
            R = colorA.R + colorB.R,
            G = colorA.G + colorB.G,
            B = colorA.B + colorB.B,
            A = colorA.A + colorB.A
        };

        public static ColorF operator *(ColorF colorA, ColorF colorB) => new ColorF
        {
            R = colorA.R * colorB.R,
            G = colorA.G * colorB.G,
            B = colorA.B * colorB.B,
            A = colorA.A * colorB.A
        };

        public static ColorF FromRgba(int r, int g, int b, int a) => new ColorF
        {
            R = r / 255.0f,
            G = g / 255.0f,
            B = b / 255.0f,
            A = a / 255.0f,
        };

        public static ColorF FromRgba(int rgba) => new ColorF
        {
            R = ((rgba >> 0) & 0xff) / 255.0f,
            G = ((rgba >> 8) & 0xff) / 255.0f,
            B = ((rgba >> 16) & 0xff) / 255.0f,
            A = ((rgba >> 24) & 0xff) / 255.0f,
        };

        public static ColorF FromRgba(uint rgba) => new ColorF
        {
            R = ((rgba >> 0) & 0xff) / 255.0f,
            G = ((rgba >> 8) & 0xff) / 255.0f,
            B = ((rgba >> 16) & 0xff) / 255.0f,
            A = ((rgba >> 24) & 0xff) / 255.0f,
        };

        public override string ToString() => $"({R}, {G}, {B}, {A})";
    }

    public class SpriteDrawingContext
    {
        public float SourceLeft { get; set; }
        public float SourceTop { get; set; }
        public float SourceRight { get; set; }
        public float SourceBottom { get; set; }

        public float DestinationX { get; set; }
        public float DestinationY { get; set; }
        public float DestinationWidth { get; set; }
        public float DestinationHeight { get; set; }

        public ColorF Color0 { get; set; }
        public ColorF Color1 { get; set; }
        public ColorF Color2 { get; set; }
        public ColorF Color3 { get; set; }

        public ISpriteTexture SpriteTexture { get; set; }
        public BlendMode BlendMode { get; set; }

        public TextureWrapMode TextureWrapU { get; set; } = TextureWrapMode.Default;
        public TextureWrapMode TextureWrapV { get; set; } = TextureWrapMode.Default;
        public float TextureHorizontalShift { get; set; }
        public float TextureVerticalShift { get; set; }
        public float TextureRegionLeft { get; set; }
        public float TextureRegionRight { get; set; }
        public float TextureRegionTop { get; set; }
        public float TextureRegionBottom { get; set; }
    }

    public static class SpriteDrawingContextExtensions
    {
        private static readonly ColorF ColorWhite = new ColorF(1.0f, 1.0f, 1.0f, 1.0f);

        public static SpriteDrawingContext SourceLTRB(this SpriteDrawingContext context, float left, float top, float right, float bottom)
        {
            context.SourceLeft = left;
            context.SourceTop = top;
            context.SourceRight = right;
            context.SourceBottom = bottom;
            return context;
        }

        public static SpriteDrawingContext Source(this SpriteDrawingContext context, float x, float y, float width, float height)
        {
            context.SourceLeft = x;
            context.SourceTop = y;
            context.SourceRight = x + width;
            context.SourceBottom = y + height;
            return context;
        }

        public static SpriteDrawingContext Position(this SpriteDrawingContext context, float x, float y)
        {
            context.DestinationX = x;
            context.DestinationY = y;
            return context;
        }

        public static SpriteDrawingContext Traslate(this SpriteDrawingContext context, float x, float y)
        {
            context.DestinationX += x;
            context.DestinationY += y;
            return context;
        }

        public static SpriteDrawingContext MatchSourceSize(this SpriteDrawingContext context)
        {
            context.DestinationWidth = Math.Abs(context.SourceRight - context.SourceLeft);
            context.DestinationHeight = Math.Abs(context.SourceBottom - context.SourceTop);
            return context;
        }

        public static SpriteDrawingContext DestinationSize(this SpriteDrawingContext context, float width, float height)
        {
            context.DestinationWidth = width;
            context.DestinationHeight = height;
            return context;
        }

        public static SpriteDrawingContext ScaleSize(this SpriteDrawingContext context, float scale)
        {
            context.DestinationWidth *= scale;
            context.DestinationHeight *= scale;
            return context;
        }

        public static SpriteDrawingContext ScaleSize(this SpriteDrawingContext context, float scaleX, float scaleY)
        {
            context.DestinationWidth *= scaleX;
            context.DestinationHeight *= scaleY;
            return context;
        }

        public static SpriteDrawingContext ColorDefault(this SpriteDrawingContext context)
        {
            context.Color0 = ColorWhite;
            context.Color1 = ColorWhite;
            context.Color2 = ColorWhite;
            context.Color3 = ColorWhite;
            return context;
        }

        public static SpriteDrawingContext Color(this SpriteDrawingContext context, ColorF color)
        {
            context.Color0 = color;
            context.Color1 = color;
            context.Color2 = color;
            context.Color3 = color;
            return context;
        }

        public static SpriteDrawingContext ColorAdd(this SpriteDrawingContext context, ColorF color)
        {
            context.Color0 += color;
            context.Color1 += color;
            context.Color2 += color;
            context.Color3 += color;
            return context;
        }

        public static SpriteDrawingContext ColorMultiply(this SpriteDrawingContext context, ColorF color)
        {
            context.Color0 *= color;
            context.Color1 *= color;
            context.Color2 *= color;
            context.Color3 *= color;
            return context;
        }

        public static SpriteDrawingContext SpriteTexture(this SpriteDrawingContext context, ISpriteTexture spriteTexture)
        {
            context.SpriteTexture = spriteTexture;
            return context;
        }

        public static SpriteDrawingContext TextureWrapHorizontal(this SpriteDrawingContext context, TextureWrapMode mode, float left, float right)
        {
            context.TextureWrapU = mode;
            context.TextureRegionLeft = left;
            context.TextureRegionRight = right;
            return context;
        }

        public static SpriteDrawingContext TextureWrapVertical(this SpriteDrawingContext context, TextureWrapMode mode, float top, float bottom)
        {
            context.TextureWrapV = mode;
            context.TextureRegionTop = top;
            context.TextureRegionBottom = bottom;
            return context;
        }
    }

    public interface IMappedResource : IDisposable
    {
        IntPtr Data { get; }

        int Stride { get; }

        int Length { get; }
    }

    public interface ISpriteTexture : IDisposable
    {
        int Width { get; }
        int Height { get; }

        IMappedResource Map();
    }

    public interface ISpriteDrawing : IDisposable
    {
        ISpriteTexture DestinationTexture { get; set; }

        ISpriteTexture CreateSpriteTexture(IImageRead image);

        ISpriteTexture CreateSpriteTexture(int width, int height);

        void SetViewport(float left, float right, float top, float bottom);

        void Clear(ColorF color);

        void AppendSprite(SpriteDrawingContext context);

        void Flush();
    }
}
